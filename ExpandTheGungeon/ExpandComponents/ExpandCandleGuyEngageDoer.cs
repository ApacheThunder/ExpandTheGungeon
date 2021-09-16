namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCandleGuyEngageDoer : CustomEngageDoer {
    
        public ExpandCandleGuyEngageDoer() { }
        
        private bool m_isFinished;
    
        public void Awake() { aiActor.HasDonePlayerEnterCheck = true; }
    
        public override void StartIntro() {
            if (m_isFinished) { return; }
            m_isFinished = true;
            
            if (gameObject.transform.position.GetAbsoluteRoom() != null && gameObject.transform.position.GetAbsoluteRoom().area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) {
                 return;
            }

            if (gameObject.GetComponent<GoopDoer>()) {
                gameObject.GetComponent<GoopDoer>().goopDefinition.eternal = false;
                gameObject.GetComponent<GoopDoer>().goopDefinition.usesLifespan = true;
            }
        }
        
        public override bool IsFinished { get { return m_isFinished; } }
    }
}

