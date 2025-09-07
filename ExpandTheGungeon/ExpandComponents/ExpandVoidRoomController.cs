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
            m_AudioActive = false;
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
        private bool m_AudioActive;

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
            if (!m_IsReady | !GameManager.HasInstance | !GameManager.Instance.Dungeon |
                GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) {
                return;
            }
            if (m_ParentRoom != null && m_PrimaryPlayer && m_PrimaryPlayer.CurrentRoom != null) {
                if (m_PrimaryPlayer.CurrentRoom == m_ParentRoom) m_Triggered = true;
                if (m_PrimaryPlayer.CurrentRoom.connectedRooms != null && m_PrimaryPlayer.CurrentRoom.connectedRooms.Count > 0) {
                    if ((!m_Triggered | !m_AudioActive) && (m_PrimaryPlayer.CurrentRoom == m_ParentRoom | m_PrimaryPlayer.CurrentRoom.connectedRooms.Contains(m_ParentRoom))) {
                        if (!m_Triggered)m_Triggered = true;
                        if (!m_AudioActive)ToggleAudio(true);
                    } else if (m_Triggered && m_AudioActive && m_PrimaryPlayer.CurrentRoom != m_ParentRoom && !m_PrimaryPlayer.CurrentRoom.connectedRooms.Contains(m_ParentRoom)) {
                        ToggleAudio(false);
                    }
                }
            }
        }

        public void LateUpdate() {
            if (!m_IsReady | !m_Triggered | !GameManager.HasInstance | !GameManager.Instance.Dungeon |
                GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) {
                return;
            }
            if (m_ParentRoom != null && m_ParentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) != null &&
                m_ParentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear).Count > 0) {
                for (int i = 0; i < m_ParentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear).Count; i++) {
                    if (m_ParentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)[i] && m_ParentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)[i].visibilityManager) {
                        m_ParentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)[i].visibilityManager.ChangeToVisibility(RoomHandler.VisibilityStatus.VISITED, true);
                        Destroy(m_ParentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)[i].visibilityManager);
                    }
                }
            }
        }

        public void ToggleAudio(bool active) {
            if (active) {
                AkSoundEngine.PostEvent(StartSoundEvent, gameObject);
            } else {
                AkSoundEngine.PostEvent(StopSoundEvent, gameObject);
            }
            m_AudioActive = active;
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;
            room.OverrideTilemap = GameManager.Instance.Dungeon.MainTilemap; // prevents rooms from being revealed on minimap
            (typeof(RoomHandler).GetField("m_currentlyVisible", BindingFlags.Instance | BindingFlags.NonPublic)).SetValue(room, true); // Setting this to true prevents OnBecomeVisible from being called keeping the room in darkness.
        }

        protected override void OnDestroy() {
            if (m_AudioActive) ToggleAudio(false);
            base.OnDestroy();
        }
    }
}

