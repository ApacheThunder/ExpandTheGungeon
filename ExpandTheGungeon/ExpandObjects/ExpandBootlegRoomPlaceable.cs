using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandCorruptedRoomAmbiencePlacable : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public ExpandCorruptedRoomAmbiencePlacable() {
            CorruptionFXPlayEvent = "Play_EX_CorruptionAmbience_01";
            CorruptionFXStopEvent = "Stop_EX_CorruptionAmbience_01";
            StartImmedietely = false;
            CameFromCorruptionBomb = false;
            GoesAwayEventually = false;
            m_HasBeenActivated = false;
            FadeTimer = 90;
        }
        
        public string CorruptionFXPlayEvent;
        public string CorruptionFXStopEvent;

        public bool StartImmedietely;
        public bool CameFromCorruptionBomb;
        public bool GoesAwayEventually;
        public float FadeTimer;

        public RoomHandler m_Room;
        
        private GameObject m_RoomAmbienceSFX;
        private bool m_HasBeenActivated;

        public void Start() { }
        public void Update() {
            if (GoesAwayEventually) {
                FadeTimer -= BraveTime.DeltaTime;
                if (FadeTimer <= 0) {
                    if(m_RoomAmbienceSFX != null && !CameFromCorruptionBomb) {
                        AkSoundEngine.PostEvent(CorruptionFXStopEvent, m_RoomAmbienceSFX);
                        Destroy(m_RoomAmbienceSFX);
                    } else {
                        AkSoundEngine.PostEvent(CorruptionFXStopEvent, gameObject);
                    }
                    GoesAwayEventually = false;
                }
            }
        }
        
        private void HandleBecomeVisible () {
            if (!m_HasBeenActivated && m_Room != null) {
                if (m_RoomAmbienceSFX == null) {
                    m_RoomAmbienceSFX = new GameObject("RoomCorruptionAmbience_SFX") { layer = 0 };
                    m_RoomAmbienceSFX.transform.position = m_Room.area.Center;
                    m_RoomAmbienceSFX.transform.parent = m_Room.hierarchyParent;
                }
                AkSoundEngine.PostEvent(CorruptionFXPlayEvent, m_RoomAmbienceSFX);
                m_HasBeenActivated = true;
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            m_Room = room;
            if (StartImmedietely && !CameFromCorruptionBomb) {
                HandleBecomeVisible();
            } else if (!CameFromCorruptionBomb) {
                m_Room.BecameVisible += delegate { HandleBecomeVisible(); };
                // m_Room.BecameInvisible += delegate { HandleBecomeInvisible(); };
            } else if (CameFromCorruptionBomb) {
                AkSoundEngine.PostEvent(CorruptionFXPlayEvent, gameObject);
            }

        }

        protected override void OnDestroy() {
            if (m_RoomAmbienceSFX != null && !CameFromCorruptionBomb) {
                AkSoundEngine.PostEvent(CorruptionFXStopEvent, m_RoomAmbienceSFX);
                Destroy(m_RoomAmbienceSFX);
            } else {
                AkSoundEngine.PostEvent(CorruptionFXStopEvent, gameObject);
            }
            base.OnDestroy();
        }
    }
}

