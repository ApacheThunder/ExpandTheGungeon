using Dungeonator;
using ExpandTheGungeon.ExpandMain;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {
    
    public class ExpandCorruptedObjectDummyComponent : BraveBehaviour {

        public ExpandCorruptedObjectDummyComponent() { }

        public void Init() { ExpandStaticReferenceManager.AllGlitchTiles.Add(this); }

        private void Start() { }
        private void Update() { }
       
        protected override void OnDestroy() {
            ExpandStaticReferenceManager.AllGlitchTiles.Remove(this);
            base.OnDestroy();
        }    
    }

    public class ExpandCorruptedObjectAmbienceComponent : BraveBehaviour {

        public ExpandCorruptedObjectAmbienceComponent() {
            StartCorruptionSoundEvent = "Play_EX_CorruptionAmbience_02";
            StopCorruptionSoundEvent = "Stop_EX_CorruptionAmbience_02";
            startImmediately = false;
            m_HasBeenActivated = false;
        }

        public string StartCorruptionSoundEvent;
        public string StopCorruptionSoundEvent;
        public bool startImmediately;

        public Vector3? UnitOffset;
        
        private bool m_HasBeenActivated;

        private RoomHandler m_Room;
        private GameObject m_SoundObjectChild;

        private void Start() { }
        private void Update() { }

        public void Init(RoomHandler room) {
            m_Room = room;
            if (startImmediately) {
                HandleBecomeVisible();
            } else {
                m_Room.BecameVisible += delegate { HandleBecomeVisible(); };
            }
        }

        private void HandleBecomeVisible() {
            if (!m_HasBeenActivated && m_Room != null) {
                if (UnitOffset.HasValue) {
                    m_SoundObjectChild = new GameObject("RoomCorruptionAmbience_Child_SFX") { layer = 0 };
                    m_SoundObjectChild.transform.position = (UnitOffset.Value + gameObject.transform.position);
                    m_SoundObjectChild.transform.parent = gameObject.transform;
                    AkSoundEngine.PostEvent(StartCorruptionSoundEvent, m_SoundObjectChild);
                } else {
                    AkSoundEngine.PostEvent(StartCorruptionSoundEvent, gameObject);
                }
                m_HasBeenActivated = true;
            }
        }

        protected override void OnDestroy() {
            if (UnitOffset.HasValue && m_SoundObjectChild) {
                AkSoundEngine.PostEvent(StopCorruptionSoundEvent, m_SoundObjectChild);
            } else {
                AkSoundEngine.PostEvent(StopCorruptionSoundEvent, gameObject);
            }
            base.OnDestroy();
        }
    }
}

