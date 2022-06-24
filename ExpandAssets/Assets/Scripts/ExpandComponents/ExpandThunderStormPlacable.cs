using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using System;

namespace ExpandTheGungeon.ExpandComponents
{
    public class ExpandThunderStormPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable
    {

        public bool useCustomIntensity;
        public float RainIntensity;
        public bool enableLightning;
        public bool isSecretFloor;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ConfigureOnPlacement(RoomHandler room) {
            
        }
    }

}

