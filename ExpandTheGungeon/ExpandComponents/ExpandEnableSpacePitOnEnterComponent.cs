using System.Collections.Generic;
using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEnableSpacePitOnEnterComponent : DungeonPlaceableBehaviour, IPlaceConfigurable {

        private bool m_Triggered;
        private RoomHandler m_ParentRoom;

        private void Start() { }

        private void Update() {
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
                    if (FindObjectOfType<EndTimesNebulaController>()) {
                        FindObjectOfType<EndTimesNebulaController>().BecomeActive();
                    }
                }
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) { m_ParentRoom = room; }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

