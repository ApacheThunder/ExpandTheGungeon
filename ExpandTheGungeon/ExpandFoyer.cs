using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon {

    public class ExpandFoyer : BraveBehaviour {

        public ExpandFoyer() { m_FoyerCheckTriggered = false; }

        private bool m_FoyerCheckTriggered;

        public void Awake() { }
        public void Start() { }

        public void Update() {
            if (m_FoyerCheckTriggered) {
                Destroy(gameObject);
                return;
            }

            if (Foyer.DoIntroSequence && Foyer.DoMainMenu) { return; }

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
            m_FoyerCheckTriggered = true;
        }

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}
