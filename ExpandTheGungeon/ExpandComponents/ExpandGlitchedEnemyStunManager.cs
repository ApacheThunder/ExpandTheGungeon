using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandGlitchedEnemyStunManager : BraveBehaviour {

        public ExpandGlitchedEnemyStunManager() { stunDuration = 0.65f; }
        
        public float stunDuration;

        private RoomHandler m_StartRoom;

        private void Start() { m_StartRoom = aiActor.GetAbsoluteParentRoom(); }

        private void Update() { CheckPlayerRoom(); }

        private void CheckPlayerRoom() {
            
            if (!GameManager.Instance.PrimaryPlayer | Dungeon.IsGenerating | GameManager.Instance.IsLoadingLevel) { return; }
            
            if (!aiActor | !aiActor.behaviorSpeculator | m_StartRoom == null) {
                Destroy(this);
                return;
            }
            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != m_StartRoom) {
                if (aiActor.behaviorSpeculator.enabled) { aiActor.behaviorSpeculator.enabled = false; }
                return;
            } else {
                aiActor.behaviorSpeculator.enabled = true;
                aiActor.HasDonePlayerEnterCheck = true;
                aiActor.HasBeenEngaged = true;
                aiActor.behaviorSpeculator.Stun(stunDuration, false);
                Destroy(this);
                return;
            }            
        }
                
        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

