using Dungeonator;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandVoidController : BraveBehaviour  {

        public ExpandVoidController() {
            IsBackroomsVoidController = true;
            MaxVoidTime = 5;

            StartSoundEvent = "Play_EX_Void_DoorAmbience";
            StopSoundEvent = "Stop_EX_Void_DoorAmbience";
            
            m_IsReady = false;
            m_VoidTimer = 0;
            m_Triggered = false;
            m_AudioTriggered = false;
            m_VoidRoomList = new List<RoomHandler>();
        }
        [SerializeField]
        public float MaxVoidTime;

        [SerializeField]
        public bool IsBackroomsVoidController;

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
        private bool m_AudioTriggered;

        [NonSerialized]
        private float m_VoidTimer;
               
        [NonSerialized]
        private List<RoomHandler> m_VoidRoomList;

        [NonSerialized]
        private GameObject m_OutofBoundsVoidSFX;


        public void Start() {
            if (!GameManager.HasInstance | !GameManager.Instance.Dungeon) { Destroy(gameObject); return; }
            m_PrimaryPlayer = GameManager.Instance.PrimaryPlayer;
            m_SecondaryPlayer = GameManager.Instance.SecondaryPlayer;
            
            if (!m_PrimaryPlayer && !m_SecondaryPlayer) { Destroy(gameObject); return; }

            gameObject.transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform);
            
            foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) {
                if (!string.IsNullOrEmpty(room.GetRoomName())) {
                    if (room.GetRoomName().ToLower().StartsWith("backrooms_warpwingroom")) {
                        bool isConnectedToEntrance = false;
                        if (room.connectedRooms != null && room.connectedRooms.Count > 0) {
                            foreach (RoomHandler connectedRoom in room.connectedRooms) {
                                if (connectedRoom.area != null && connectedRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.ENTRANCE) {
                                    isConnectedToEntrance = true;
                                }
                            }
                        }
                        if (!isConnectedToEntrance)room.OverrideTilemap = GameManager.Instance.Dungeon.MainTilemap; // prevents rooms from being revealed on minimap
                    }
                    if (room.GetRoomName().ToLower().StartsWith("backrooms_voidroom_")) {
                        m_VoidRoomList.Add(room);
                    }
                }
            }

            m_VoidTimer = MaxVoidTime;
            m_IsReady = true;
        }

        
        private void DoForcedTeleport() {
            if (m_PrimaryPlayer) {
                m_PrimaryPlayer.RespawnInPreviousRoom(false, PlayerController.EscapeSealedRoomStyle.TELEPORTER, false);
                AkSoundEngine.PostEvent("Play_VO_lichA_cackle_01", m_PrimaryPlayer.gameObject);
            }
            m_Triggered = false;
        }


        public bool IsValidPlayerPosition(PlayerController player, IntVector2? checkRadius = null) {
            IntVector2 checkedPosition = player.transform.position.XY().ToIntVector2(VectorConversions.Floor);
            IntVector2 PositionRadius = new IntVector2(2, 2);
            if (checkRadius.HasValue) PositionRadius = checkRadius.Value;
            for (int i = 0; i < PositionRadius.x; i++) {
                for (int j = 0; j < PositionRadius.y; j++) {
                    if (!GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(checkedPosition + new IntVector2(i, j))) {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public void Update() {
            if (!m_IsReady | m_Triggered) return;

            if (!GameManager.HasInstance | !GameManager.Instance.Dungeon |
                GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) {
                return;
            }
                        
            if (m_VoidRoomList != null && m_VoidRoomList.Count > 0) {
                foreach (RoomHandler room in m_VoidRoomList)room.SetRoomActive(true);
            }

            // Disable's rat thefts for this floor. (also prevents The Lead Key from being used here)
            if (IsBackroomsVoidController && GameManager.Instance.Dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.RATGEON) {
                GameManager.Instance.Dungeon.tileIndices.tilesetId = GlobalDungeonData.ValidTilesets.RATGEON;
            }
            if ((m_PrimaryPlayer && !IsValidPlayerPosition(m_PrimaryPlayer)) | (m_SecondaryPlayer && !IsValidPlayerPosition(m_SecondaryPlayer))) {
                m_VoidTimer -= BraveTime.DeltaTime;
                if (!m_OutofBoundsVoidSFX)m_OutofBoundsVoidSFX = new GameObject("Expand OutofBounds SFX Tracker");
                m_OutofBoundsVoidSFX.transform.position = m_PrimaryPlayer.transform.position;
                if (!m_AudioTriggered) {
                    AkSoundEngine.PostEvent(StartSoundEvent, m_OutofBoundsVoidSFX);
                    m_AudioTriggered = true;
                }
            }
            if (m_VoidTimer <= 0) {
                m_VoidTimer = MaxVoidTime;
                m_Triggered = true;
                DoForcedTeleport();
                return;
            }
        }
        
        protected override void OnDestroy() {
            if (m_OutofBoundsVoidSFX) {
                if (m_AudioTriggered) AkSoundEngine.PostEvent(StopSoundEvent, m_OutofBoundsVoidSFX);
                Destroy(m_OutofBoundsVoidSFX);
            }
            base.OnDestroy();
        }
    }
}

