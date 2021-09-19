using System.Collections.Generic;
using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using UnityEngine;
using System;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEnableSpacePitOnEnterComponent : DungeonPlaceableBehaviour, IPlaceConfigurable {
        
        private bool m_Triggered;

        private RoomHandler m_ParentRoom;
        private EndTimesNebulaController m_NebulaController;
        private GameObject m_NebulaObject;
        
        private void Start() {
            if (FindObjectOfType<EndTimesNebulaController>()) {
                m_NebulaController = FindObjectOfType<EndTimesNebulaController>();
            } else {
                m_NebulaObject = Instantiate(ExpandObjectDatabase.EndTimes, Vector2.zero, Quaternion.identity);
                m_NebulaController = m_NebulaObject.GetComponent<EndTimesNebulaController>();
            }
            if (!m_NebulaController) { return; }
        }

        private void Update() {
            if (!m_NebulaController) {
                m_Triggered = true;
                return;
            }
            if (!m_Triggered && !Dungeon.IsGenerating && !GameManager.Instance.IsLoadingLevel) {
                bool RoomEntered = false;
                if (m_ParentRoom.connectedRoomsByExit != null && m_ParentRoom.connectedRoomsByExit.Count > 0) {
                    foreach (KeyValuePair<PrototypeRoomExit, RoomHandler> keyValue in m_ParentRoom.connectedRoomsByExit) {
                        if (GameManager.Instance.PrimaryPlayer.CurrentRoom == keyValue.Value) {
                            RoomEntered = true;
                            break;
                        }
                    }
                }

                if (!RoomEntered) { if (GameManager.Instance.PrimaryPlayer.CurrentRoom == m_ParentRoom) { RoomEntered = true; } }
                
                if (RoomEntered) {
                    m_Triggered = true;
                    m_NebulaController.BecomeActive();
                }
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) { m_ParentRoom = room; }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

