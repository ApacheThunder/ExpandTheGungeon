using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using System;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCasinoRoomController : BraveBehaviour {

        public ExpandCasinoRoomController() { }
        
        public ExpandCasinoGameController CasinoGame_Punchout;


        [NonSerialized]
        private RoomHandler m_ParentRoom;

        private void Start() {
            m_ParentRoom = gameObject.transform.position.GetAbsoluteRoom();
            Vector3 RoomPosition = m_ParentRoom.area.basePosition.ToVector3();

            if (CasinoGame_Punchout) {
                CasinoGame_Punchout.ConfigureOnPlacement(m_ParentRoom);
                m_ParentRoom.RegisterInteractable(CasinoGame_Punchout);

                Dungeon NakatomiPrefab = ExpandCustomDungeonPrefabs.LoadOfficialDungeonPrefab("Base_Nakatomi");

                GameObject m_Table = gameObject.transform.Find("casino_poker_table").gameObject;

                Vector3 ChairOffset1 = (m_Table.transform.position + new Vector3(2.5f, 2.5f));
                Vector3 ChairOffset2 = (m_Table.transform.position - new Vector3(1, 1));
                Vector3 ChairOffset3 = (m_Table.transform.position + new Vector3(2.5f, -1));
                Vector3 ChairOffset4 = (m_Table.transform.position + new Vector3(-1, 2.5f));
                
                Instantiate(ExpandObjectDatabase.KitchenChair_Front, ChairOffset1, Quaternion.identity);
                Instantiate(ExpandObjectDatabase.KitchenChair_Right, ChairOffset2, Quaternion.identity);
                Instantiate(ExpandObjectDatabase.KitchenChair_Left, ChairOffset3, Quaternion.identity);
                Instantiate(ExpandObjectDatabase.KitchenChair_Front, ChairOffset4, Quaternion.identity);

                Vector3 WaterCoolerOffset1 = (RoomPosition + new Vector3(0.25f, 12));
                Vector3 WaterCoolerOffset2 = (RoomPosition + new Vector3(15.5f, 3));
                Vector3 WaterCoolerOffset3 = (RoomPosition + new Vector3(3, 15.5f));

                Instantiate(NakatomiPrefab.stampData.objectStamps[4].objectReference, WaterCoolerOffset1, Quaternion.identity);
                Instantiate(NakatomiPrefab.stampData.objectStamps[4].objectReference, WaterCoolerOffset2, Quaternion.identity);
                Instantiate(NakatomiPrefab.stampData.objectStamps[3].objectReference, WaterCoolerOffset3, Quaternion.identity);

                // Office Chairs (left)
                Instantiate(NakatomiPrefab.stampData.objectStamps[2].objectReference, RoomPosition + new Vector3(15.25f, 2), Quaternion.identity);                

                // Boxes
                Instantiate(NakatomiPrefab.stampData.objectStamps[7].objectReference, RoomPosition + new Vector3(-0.25f, 1f), Quaternion.identity);
                Instantiate(NakatomiPrefab.stampData.objectStamps[7].objectReference, RoomPosition + new Vector3(2f, 1f), Quaternion.identity);
                Instantiate(NakatomiPrefab.stampData.objectStamps[8].objectReference, RoomPosition + new Vector3(1f, 2f), Quaternion.identity);

                NakatomiPrefab = null;
            }
        }

        private void Update() { }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

