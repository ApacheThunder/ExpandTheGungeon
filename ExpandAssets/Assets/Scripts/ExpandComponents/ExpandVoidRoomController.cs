using Dungeonator;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandVoidRoomController : BraveBehaviour, IPlaceConfigurable  {

        public ExpandVoidRoomController() {
            StartSoundEvent = "Play_EX_Void_RoomAmbience";
            StopSoundEvent = "Stop_EX_Void_RoomAmbience";
            
            m_IsReady = false;
            m_Triggered = false;
        }
                
        [SerializeField]
        public string StartSoundEvent;

        [SerializeField]
        public string StopSoundEvent;
        
        [NonSerialized]
        private PlayerController m_PrimaryPlayer;

        [NonSerialized]
        private PlayerController m_SecondaryPlayer;
        
        [NonSerialized]
        private bool m_IsReady;

        [NonSerialized]
        private bool m_Triggered;
        
        [NonSerialized]
        private RoomHandler m_ParentRoom;
                
        public void Start() {
            if (!GameManager.HasInstance | !GameManager.Instance.Dungeon) { Destroy(gameObject); return; }
            m_PrimaryPlayer = GameManager.Instance.PrimaryPlayer;
            m_SecondaryPlayer = GameManager.Instance.SecondaryPlayer;
            
            if (!m_PrimaryPlayer && !m_SecondaryPlayer) { Destroy(gameObject); return; }

           m_IsReady = true;
           return;
        }
                
        public void Update() {
            if (!m_IsReady | m_Triggered) return;

            if (!GameManager.HasInstance | !GameManager.Instance.Dungeon |
                GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) {
                return;
            }
                        
            if (m_ParentRoom != null && m_PrimaryPlayer && m_PrimaryPlayer.CurrentRoom != null) {
                if (m_PrimaryPlayer.CurrentRoom == m_ParentRoom) m_Triggered = true;
                if (m_PrimaryPlayer.CurrentRoom.connectedRooms != null && m_PrimaryPlayer.CurrentRoom.connectedRooms.Count > 0) {
                    if (m_PrimaryPlayer.CurrentRoom.connectedRooms.Contains(m_ParentRoom)) m_Triggered = true;
                }
                if (m_Triggered){
                    AkSoundEngine.PostEvent(StartSoundEvent, gameObject);
                    return;
                }
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;
            room.OverrideTilemap = GameManager.Instance.Dungeon.MainTilemap; // prevents rooms from being revealed on minimap
            (typeof(RoomHandler).GetField("m_currentlyVisible", BindingFlags.Instance | BindingFlags.NonPublic)).SetValue(room, true); // Setting this to true prevents OnBecomeVisible from being called keeping the room in darkness.
        }

        protected override void OnDestroy() {
            if (m_Triggered)AkSoundEngine.PostEvent(StopSoundEvent, gameObject);
            base.OnDestroy();
        }
    }
}

