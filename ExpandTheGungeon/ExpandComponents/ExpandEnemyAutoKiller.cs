using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEnemyAutoKiller : BraveBehaviour {

        public ExpandEnemyAutoKiller() {
            KillOnRoomClear = true;
            AllowRewards = true;
            KillifSummonerDies = false;

            m_Active = false;
        }

        public bool KillOnRoomClear;
        public bool KillifSummonerDies;
        public bool AllowRewards;

        public AIActor SummonerParent;
        public RoomHandler m_ParentRoom;


        private bool m_Active;
        
        private bool EnemyShouldDieNow() {
            if (KillOnRoomClear && m_ParentRoom.GetActiveEnemiesCount(RoomHandler.ActiveEnemyType.RoomClear) <= 0) return true;
            if (KillifSummonerDies) {
                if (SummonerParent && SummonerParent.healthHaver.IsDead) return true;
                if (!SummonerParent) return true;
            }
            return false;
        }


        public void KillMeNow() {
            m_Active = false;
            if (AllowRewards) {
                aiActor.EraseFromExistenceWithRewards();
            } else {
                aiActor.EraseFromExistence();
            }
            Destroy(this);
        }

        public void Start() {
            if (aiActor) m_ParentRoom = aiActor.ParentRoom;
            if (m_ParentRoom == null) m_ParentRoom = gameObject.transform.position.GetAbsoluteRoom();
            if (m_ParentRoom == null) KillOnRoomClear = false;
            m_Active = true;
        }

        public void Update() {
            if (!m_Active) return;
            if (EnemyShouldDieNow()) KillMeNow();
        }

        protected override void OnDestroy() {
            m_Active = false;
            base.OnDestroy();
        }
    }

}

