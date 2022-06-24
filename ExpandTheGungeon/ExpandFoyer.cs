using Dungeonator;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using MonoMod.RuntimeDetour;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon {

    public class ExpandFoyer : BraveBehaviour {

        public ExpandFoyer() { m_State = State.PreFoyerCheck; }

        private enum State { PreFoyerCheck, CheckSettings, SpawnObjects, Exit };
        private State m_State;

        public void Awake() { }
        public void Start() { }

        public void Update() {
            switch (m_State) {
                case State.PreFoyerCheck:
                    if (Foyer.DoIntroSequence && Foyer.DoMainMenu) { return; }
                    if (ExpandTheGungeon.GameManagerHook == null) {
                        if (ExpandSettings.debugMode) { Debug.Log("[ExpandTheGungeon] Installing GameManager.Awake Hook...."); }
                        ExpandTheGungeon.GameManagerHook = new Hook(
                            typeof(GameManager).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                            typeof(ExpandTheGungeon).GetMethod("GameManager_Awake", BindingFlags.NonPublic | BindingFlags.Instance),
                            typeof(GameManager)
                        );
                    }
                    m_State = State.CheckSettings;
                    return;
                case State.CheckSettings:
                    if (!ExpandTheGungeon.ListsCleared) {
                        // This should fix issus with Pasts trying to spawn inactive versions of custom enemies
                        // (and any other mod that has created a custom AIActor or object that has a HealthHaver component)
                        // Moved to ExpandFoyer so this can clean up fakeprefabs from other mods regardless of mods.txt load order
                        StaticReferenceManager.AllHealthHavers.Clear();
                        // Remove any custom instances that use BroController
                        StaticReferenceManager.AllBros.Clear();
                        // Clear any fakeprefab AIActors from lists.
                        StaticReferenceManager.AllEnemies.Clear();
                        ExpandTheGungeon.ListsCleared = true;
                    }
                    if (ExpandSettings.EnableLanguageFix) {
                        GameManager.Options.CurrentLanguage = ExpandUtility.IntToLanguage(ExpandSettings.GameLanguage);
                        StringTableManager.CurrentLanguage = ExpandUtility.IntToLanguage(ExpandSettings.GameLanguage);
                    }
                    if (ExpandSettings.EnableTestDungeonFlow) {
                        GameManager.Instance.InjectedFlowPath = ExpandSettings.TestFlow;
                        GameManager.Instance.InjectedLevelName = ExpandSettings.TestFloor;
                    }
                    m_State = State.SpawnObjects;
                    return;
                case State.SpawnObjects:
                    if (!GameStatsManager.Instance.GetFlag(GungeonFlags.BLACKSMITH_BULLET_COMPLETE)) {
                        m_State = State.Exit;
                        return;
                    }
                    GameObject FoyerButton = Instantiate(ExpandPrefabs.EXFoyerTrigger, new Vector3(50.2f, 60.7f, 61.8f), Quaternion.identity);
                    RoomHandler FoyerRoom = FoyerButton.transform.position.GetAbsoluteRoom();
                    ExpandCasinoWarpTrigger CasinoWarpTrigger = FoyerButton.GetComponent<ExpandCasinoWarpTrigger>();
                    CasinoWarpTrigger.ConfigureOnPlacement(gameObject.transform.position.GetAbsoluteRoom());
                    FoyerRoom.RegisterInteractable(CasinoWarpTrigger);
                    m_State = State.Exit;
                    return;
                case State.Exit:
                    if (gameObject) { Destroy(gameObject); }
                    return;
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}
