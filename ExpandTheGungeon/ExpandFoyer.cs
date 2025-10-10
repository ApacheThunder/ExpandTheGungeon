using System;
// using System.Reflection;
// using MonoMod.RuntimeDetour;
using UnityEngine;
using Dungeonator;

using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandLoadingScreens;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ItemAPI;

namespace ExpandTheGungeon {

    public class ExpandFoyer : BraveBehaviour {

        [NonSerialized]
        public static GameObject EXFoyerChecker;
        [NonSerialized]
        public static ExpandFoyer Instance;
        
        public static void CreateFoyerController() {
            if (!Instance) {
                GameObject m_FoyerInstance = Instantiate(EXFoyerChecker, Vector3.zero, Quaternion.identity);
                Instance = m_FoyerInstance.GetComponent<ExpandFoyer>();
                DontDestroyOnLoad(m_FoyerInstance);
            } else {
                return;
            }
        }

        public ExpandFoyer() {
            m_State = State.PreFoyerCheck;
        }
        
        private enum State { PreFoyerCheck, CheckSettings, SpawnObjects, Exit, Inactive };
        private State m_State;
        private GameObject m_FoyerButton;
        

        public void Update() {
            if (GameManager.IsShuttingDown | !GameManager.HasInstance | GameStatsManager.Instance == null) return;
            switch (m_State) {
                case State.PreFoyerCheck:
                    if (ExpandTheGungeon.loadStatus != ExpandTheGungeon.LoadStatus.LoadFinished) return;
                    if (!BlackAndGoldenRevolver.RevolverExceptionListsBuilt)BlackAndGoldenRevolver.BuildExceptionsList();
                    if (Foyer.DoIntroSequence) return;
                    if (ExpandLoadingScreen.LoadingScreenObject.activeSelf) ExpandLoadingScreen.LoadingScreenObject.SetActive(false);
                    if (ExpandLoadingScreen.LoadingBarObject.activeSelf) ExpandLoadingScreen.LoadingBarObject.SetActive(false);
                    m_State = State.CheckSettings;
                    return;
                case State.CheckSettings:
                    if (!ExpandTheGungeon.ListsCleared) {
                        // This should fix issus with Pasts trying to spawn inactive versions of custom enemies
                        // (and any other mod that has created a custom AIActor or object that has a HealthHaver component)
                        // Moved to ExpandFoyer so this can clean up fakeprefabs from other mods regardless of mods.txt load order
                        StaticReferenceManager.AllHealthHavers.Clear();
                        if (GameManager.Instance) {
                            if (GameManager.Instance.PrimaryPlayer)StaticReferenceManager.AllHealthHavers.Add(GameManager.Instance.PrimaryPlayer.healthHaver);
                            if (GameManager.Instance.SecondaryPlayer)StaticReferenceManager.AllHealthHavers.Add(GameManager.Instance.SecondaryPlayer.healthHaver);
                        }

                        // Remove any custom instances that use BroController
                        StaticReferenceManager.AllBros.Clear();
                        // Clear any fakeprefab AIActors from lists.
                        StaticReferenceManager.AllEnemies.Clear();
                        ExpandTheGungeon.ListsCleared = true;
                    }
                    if (ExpandSettings.EnableTestDungeonFlow) {
                        GameManager.Instance.InjectedFlowPath = ExpandSettings.TestFlow;
                        GameManager.Instance.InjectedLevelName = ExpandSettings.TestFloor;
                        ExpandSettings.EnableTestDungeonFlow = false;
                    }
                    if (GameManager.Instance.EnemyReplacementTiers != null)ExpandEnemyReplacements.Init(GameManager.Instance.EnemyReplacementTiers);
                    ExpandDungeonMusicAPI.EnteredNewCustomFloor = false;
                    m_State = State.SpawnObjects;
                    return;
                case State.SpawnObjects:
                    if (!GameStatsManager.Instance.GetFlag(GungeonFlags.BLACKSMITH_BULLET_COMPLETE)) {
                        m_State = State.Exit;
                        return;
                    }
                    CreateCasinoWarp();
                    m_State = State.Exit;
                    return;
                case State.Exit:
                    m_State = State.Inactive;
                    if (gameObject)Destroy(gameObject);
                    return;
                case State.Inactive:
                    return;
            }
        }

        public void CreateCasinoWarp() {
            if (m_FoyerButton) return;
            m_FoyerButton = Instantiate(ExpandPrefabs.EXFoyerTrigger, new Vector3(50.2f, 60.7f, 61.8f), Quaternion.identity);
            RoomHandler FoyerRoom = m_FoyerButton.transform.position.GetAbsoluteRoom();
            ExpandCasinoWarpTrigger CasinoWarpTrigger = m_FoyerButton.GetComponent<ExpandCasinoWarpTrigger>();
            CasinoWarpTrigger.ConfigureOnPlacement(m_FoyerButton.transform.position.GetAbsoluteRoom());
            FoyerRoom.RegisterInteractable(CasinoWarpTrigger);
        }

        protected override void OnDestroy() {
            Instance = null;
            base.OnDestroy();
        }
    }
}
