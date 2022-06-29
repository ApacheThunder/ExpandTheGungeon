using System;
using System.Collections;
using UnityEngine;
using Dungeonator;
using System.Reflection;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandPunchoutArcadeController : BraveBehaviour {

        public ExpandPunchoutArcadeController() {
            m_Configured = false;
            m_PunchOutEnded = false;
        }

        [NonSerialized]
        public static List<int> RewardIDs = new List<int>();
        [NonSerialized]
        public static int RatKeyCount = -1;
        [NonSerialized]
        public static bool WonRatGame = false;

        [NonSerialized]
        public GameObject ScanlineFX;

        [NonSerialized]
        public PlayerController Player;
        [NonSerialized]
        public ExpandCasinoGameController ParentGameController;
        
        [NonSerialized]
        private PunchoutController m_punchoutController;
        [NonSerialized]
        private dfTextureSprite m_PunchoutOverlay;
        [NonSerialized]
        private bool m_Configured;
        [NonSerialized]
        private bool m_PunchOutEnded;

        private void LateUpdate() { }

        private void Update() { }
    }
}

