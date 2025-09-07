using System;
using System.Collections;
using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEntityManager : BraveBehaviour {
        
        public ExpandEntityManager() {
            Configured = false;
            IsOnBackRoomsFloor = false;
            MaxPlayerAwayTime = 20;
            EntitySize = new IntVector2(2, 2);
            AIAnimatorSpawnClip = "spawn";
            AIAnimatorDeSpawnClip = "despawn";
            EntityPlayScreemEvent = "Play_EX_EntityScreams_01";
            EntityStopScreemEvent = "Stop_EX_EntityScreams_01";

            m_SettingsApplied = false;
            m_IsTeleporting = false;
            m_ScreemStarted = false;
            m_PlayerEaten = false;
            m_PlayerAwayTime = 0;
        }

        [NonSerialized]
        public bool Configured;
        [NonSerialized]
        public bool IsOnBackRoomsFloor;
        [SerializeField]
        public float MaxPlayerAwayTime;
        [SerializeField]
        public IntVector2 EntitySize;
        [SerializeField]
        public string AIAnimatorSpawnClip;
        [SerializeField]
        public string AIAnimatorDeSpawnClip;
        [SerializeField]
        public string EntityPlayScreemEvent;
        [SerializeField]
        public string EntityStopScreemEvent;

        [NonSerialized]
        private bool m_SettingsApplied;
        [NonSerialized]
        private bool m_IsTeleporting;
        [NonSerialized]
        private bool m_ScreemStarted;
        [NonSerialized]
        private bool m_PlayerEaten;
        [NonSerialized]
        private float m_PlayerAwayTime;

        [NonSerialized]
        private PlayerController m_Player;
        [NonSerialized]
        private RoomHandler m_CurrentRoom;
        [NonSerialized]
        private RoomHandler m_TargetTeleportRoom;
    }
}

