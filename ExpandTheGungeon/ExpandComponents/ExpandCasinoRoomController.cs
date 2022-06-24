using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using System;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCasinoRoomController : BraveBehaviour {

        public ExpandCasinoRoomController() { }
        
        public ExpandCasinoGameController CasinoGame_Punchout;
        public ExpandCasinoGameController CasinoGame_GunBall;
        public GameObject Table;
        public GameObject Table2;

        [NonSerialized]
        private RoomHandler m_ParentRoom;

        private void Start() {
            m_ParentRoom = gameObject.transform.position.GetAbsoluteRoom();
            Vector3 RoomPosition = m_ParentRoom.area.basePosition.ToVector3();

            if (CasinoGame_Punchout) {
                CasinoGame_Punchout.ConfigureOnPlacement(m_ParentRoom);
                m_ParentRoom.RegisterInteractable(CasinoGame_Punchout);
            }
            if (CasinoGame_GunBall) {
                CasinoGame_GunBall.ConfigureOnPlacement(m_ParentRoom);
                m_ParentRoom.RegisterInteractable(CasinoGame_GunBall);
            }
            
            if (Table && Table2) {
                Vector3 ChairOffset1 = (Table.transform.position + new Vector3(2.5f, 2.5f));
                Vector3 ChairOffset2 = (Table.transform.position - new Vector3(1, 1));
                Vector3 ChairOffset3 = (Table.transform.position + new Vector3(2.5f, -1));
                Vector3 ChairOffset4 = (Table2.transform.position + new Vector3(2.8f, 1));
                Vector3 ChairOffset5 = (Table2.transform.position + new Vector3(-1, 1));


                GameObject m_Chair_01 = Instantiate(ExpandObjectDatabase.KitchenChair_Front, ChairOffset1, Quaternion.identity);
                GameObject m_Chair_02 = Instantiate(ExpandObjectDatabase.KitchenChair_Right, ChairOffset2, Quaternion.identity);
                GameObject m_Chair_03 = Instantiate(ExpandObjectDatabase.KitchenChair_Left, ChairOffset3, Quaternion.identity);
                GameObject m_Chair_04 = Instantiate(ExpandObjectDatabase.KitchenChair_Left, ChairOffset4, Quaternion.identity);
                GameObject m_Chair_05 = Instantiate(ExpandObjectDatabase.KitchenChair_Right, ChairOffset5, Quaternion.identity);
                m_Chair_01.transform.parent = m_ParentRoom.hierarchyParent;
                m_Chair_02.transform.parent = m_ParentRoom.hierarchyParent;
                m_Chair_03.transform.parent = m_ParentRoom.hierarchyParent;
                m_Chair_04.transform.parent = m_ParentRoom.hierarchyParent;
                m_Chair_05.transform.parent = m_ParentRoom.hierarchyParent;
            }
            
            // Casino Props
            GameObject m_HatRack = Instantiate(ExpandPrefabs.EXCasino_HatRack, RoomPosition + new Vector3(0.35f, 10), Quaternion.identity);
            m_HatRack.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_Litter_01 = Instantiate(ExpandPrefabs.EXCasino_Litter_Paper, RoomPosition + new Vector3(9f, 14.75f), Quaternion.identity);
            m_Litter_01.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_Litter_02 = Instantiate(ExpandPrefabs.EXCasino_Litter_Paper, RoomPosition + new Vector3(4f, 2f), Quaternion.identity);
            m_Litter_02.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_Cans_01 = Instantiate(ExpandPrefabs.EXCasino_Litter_Cans, RoomPosition + new Vector3(3f, 8.5f), Quaternion.identity);
            m_Cans_01.transform.parent = m_ParentRoom.hierarchyParent;

            Vector3 DecorativeArcadePosition1 = (RoomPosition + new Vector3(10.5f, 8));
            Vector3 DecorativeArcadePosition2 = (RoomPosition + new Vector3(13.5f, 8));
            Vector3 DecorativeArcadePosition3 = (RoomPosition + new Vector3(13.5f, 11));

            GameObject m_Arcade01 = Instantiate(ExpandPrefabs.EXArcadeGame_Prop_Depressed, DecorativeArcadePosition1, Quaternion.identity);
            m_Arcade01.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_Arcade02 = Instantiate(ExpandPrefabs.EXArcadeGame_Prop, DecorativeArcadePosition2, Quaternion.identity);
            m_Arcade02.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_Arcade03 = Instantiate(ExpandPrefabs.EXArcadeGame_Prop, DecorativeArcadePosition3, Quaternion.identity);
            m_Arcade03.transform.parent = m_ParentRoom.hierarchyParent;

            
            // Misc Props
            Dungeon NakatomiPrefab = ExpandCustomDungeonPrefabs.LoadOfficialDungeonPrefab("Base_Nakatomi");

            Vector3 WaterCoolerOffset1 = (RoomPosition + new Vector3(0.25f, 12));
            Vector3 WaterCoolerOffset2 = (RoomPosition + new Vector3(15.5f, 3));
            Vector3 WaterCoolerOffset3 = (RoomPosition + new Vector3(15.25f, 15.5f));

            GameObject m_WaterCooler_01 = Instantiate(NakatomiPrefab.stampData.objectStamps[4].objectReference, WaterCoolerOffset1, Quaternion.identity);
            GameObject m_WaterCooler_02 = Instantiate(NakatomiPrefab.stampData.objectStamps[4].objectReference, WaterCoolerOffset2, Quaternion.identity);
            GameObject m_WaterCooler_03 = Instantiate(NakatomiPrefab.stampData.objectStamps[3].objectReference, WaterCoolerOffset3, Quaternion.identity);
            m_WaterCooler_01.transform.parent = m_ParentRoom.hierarchyParent;
            m_WaterCooler_02.transform.parent = m_ParentRoom.hierarchyParent;
            m_WaterCooler_03.transform.parent = m_ParentRoom.hierarchyParent;

            // Office Chairs (left)
            GameObject m_Office_Chair = Instantiate(NakatomiPrefab.stampData.objectStamps[2].objectReference, RoomPosition + new Vector3(15.25f, 2), Quaternion.identity);
            m_Office_Chair.transform.parent = m_ParentRoom.hierarchyParent;

            // Boxes
            GameObject m_Box_01 = Instantiate(NakatomiPrefab.stampData.objectStamps[7].objectReference, RoomPosition + new Vector3(-0.25f, 1f), Quaternion.identity);
            GameObject m_Box_02 = Instantiate(NakatomiPrefab.stampData.objectStamps[7].objectReference, RoomPosition + new Vector3(2f, 1f), Quaternion.identity);
            GameObject m_Box_03 = Instantiate(NakatomiPrefab.stampData.objectStamps[8].objectReference, RoomPosition + new Vector3(1f, 2f), Quaternion.identity);
            m_Box_01.transform.parent = m_ParentRoom.hierarchyParent;
            m_Box_02.transform.parent = m_ParentRoom.hierarchyParent;
            m_Box_03.transform.parent = m_ParentRoom.hierarchyParent;


            // Temp Objects
            GameObject m_Litter = new GameObject("EX Litter 01") { layer = LayerMask.NameToLayer("BG_Critical") };
            tk2dSprite m_LitterSprite = SpriteSerializer.AddSpriteToObject(m_Litter, ExpandPrefabs.EXFoyerCollection, "casino_litter_paper_001", tk2dBaseSprite.PerpendicularState.FLAT);
            m_LitterSprite.HeightOffGround = -1.7f;
            m_LitterSprite.usesOverrideMaterial = true;
            m_LitterSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;
            m_Litter.transform.position = (RoomPosition + new Vector3(11, 3));
            m_Litter.transform.parent = m_ParentRoom.hierarchyParent;

            GameObject m_CoveredArcade = Instantiate(ExpandPrefabs.EXArcadeGame_Prop, (RoomPosition + new Vector3(10.5f, 5)), Quaternion.identity);
            m_CoveredArcade.GetComponent<tk2dSprite>().SetSprite("cabinet_covered_001");
            m_CoveredArcade.transform.parent = m_ParentRoom.hierarchyParent;
            
            GameObject m_Box_04 = Instantiate(NakatomiPrefab.stampData.objectStamps[7].objectReference, RoomPosition + new Vector3(13, 2f), Quaternion.identity);
            GameObject m_Box_05 = Instantiate(NakatomiPrefab.stampData.objectStamps[8].objectReference, RoomPosition + new Vector3(13, 5f), Quaternion.identity);
            GameObject m_SlipperySign = Instantiate(NakatomiPrefab.stampData.objectStamps[12].objectReference, RoomPosition + new Vector3(11.5f, 1.5f), Quaternion.identity);

            // Lights
            GameObject m_Light_01 = Instantiate(ExpandPrefabs.WestLight, RoomPosition + new Vector3(0.25f, 6f), Quaternion.identity);
            m_Light_01.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_LightFixture_01 = Instantiate(ExpandObjectDatabase.DefaultTorchSide, RoomPosition + new Vector3(0.25f, 6f), Quaternion.identity);
            m_LightFixture_01.transform.parent = m_ParentRoom.hierarchyParent;
            
            GameObject m_Light_02 = Instantiate(ExpandPrefabs.WestLight, RoomPosition + new Vector3(0.25f, 14f), Quaternion.identity);
            m_Light_02.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_LightFixture_02 = Instantiate(ExpandObjectDatabase.DefaultTorchSide, RoomPosition + new Vector3(0.25f, 14f), Quaternion.identity);
            m_LightFixture_02.transform.parent = m_ParentRoom.hierarchyParent;
            
            GameObject m_Light_03 = Instantiate(ExpandPrefabs.WestLight, RoomPosition + new Vector3(16.4f, 6), Quaternion.identity);
            m_Light_03.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_LightFixture_03 = Instantiate(ExpandObjectDatabase.DefaultTorchSide, RoomPosition + new Vector3(16.4f, 6), Quaternion.identity);
            m_LightFixture_03.transform.parent = m_ParentRoom.hierarchyParent;
            m_LightFixture_03.GetComponent<tk2dSprite>().FlipX = true;

            GameObject m_Light_04 = Instantiate(ExpandPrefabs.WestLight, RoomPosition + new Vector3(16.4f, 14f), Quaternion.identity);
            m_Light_04.transform.parent = m_ParentRoom.hierarchyParent;
            GameObject m_LightFixture_04 = Instantiate(ExpandObjectDatabase.DefaultTorchSide, RoomPosition + new Vector3(16.4f, 14f), Quaternion.identity);
            m_LightFixture_04.transform.parent = m_ParentRoom.hierarchyParent;
            m_LightFixture_04.GetComponent<tk2dSprite>().FlipX = true;
            
            NakatomiPrefab = null;
        }

        private void Update() { }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

