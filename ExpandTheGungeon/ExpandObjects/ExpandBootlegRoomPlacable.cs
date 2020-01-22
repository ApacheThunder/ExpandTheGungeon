using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandUtilities;
using System;
using System.Collections;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandBootlegRoomPlaceable : DungeonPlaceableBehaviour, IPlaceConfigurable {
                
        public static void BuildPrefab() {
            ExpandPrefabs.EXBootlegRoomObject = new GameObject("BootlegRoomPrefab") { layer = 20 };
            ExpandPrefabs.EXBootlegRoomDoorTriggers = new GameObject("BootlegRoomDoorTriggers");
            GameObject m_BorderObject = new GameObject("BootlegRoomBorder") { layer = 20 };
            GameObject m_DoorFrameObject = new GameObject("BootlegRoomDoorFrames") { layer = 20 };
            GameObject m_DoorObject = new GameObject("BootlegRoomDoorFrames") { layer = 20 };
            m_BorderObject.transform.position -= Vector3.one;
            m_BorderObject.transform.parent = ExpandPrefabs.EXBootlegRoomObject.transform;
            m_DoorFrameObject.transform.parent = ExpandPrefabs.EXBootlegRoomObject.transform;
            m_DoorObject.transform.parent = ExpandPrefabs.EXBootlegRoomObject.transform;

            ExpandPrefabs.EXBootlegRoomObject.SetActive(false);
            ExpandPrefabs.EXBootlegRoomDoorTriggers.SetActive(false);

            ItemBuilder.AddSpriteToObject(ExpandPrefabs.EXBootlegRoomObject, "ExpandTheGungeon/Textures/BootlegRoom/BootlegRoom_BottomLayer", false, false);
            ItemBuilder.AddSpriteToObject(m_BorderObject, "ExpandTheGungeon/Textures/BootlegRoom/BootlegRoom_Border", false, false);
            ItemBuilder.AddSpriteToObject(m_DoorFrameObject, "ExpandTheGungeon/Textures/BootlegRoom/BootlegRoom_TopLayer", false, false);
            ItemBuilder.AddSpriteToObject(m_DoorObject, "ExpandTheGungeon/Textures/BootlegRoom/BootlegRoom_Doors", false, false);

            tk2dSprite m_BootlegRoomSprite = ExpandPrefabs.EXBootlegRoomObject.GetComponent<tk2dSprite>();
            m_BootlegRoomSprite.HeightOffGround = -3f;
            m_BootlegRoomSprite.UpdateZDepth();
                        
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: new IntVector2(1, 6));            
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: new IntVector2(1, 6), offset: new IntVector2(0, 8));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: new IntVector2(1, 6), offset: new IntVector2(19, 0));            
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: new IntVector2(1, 6), offset: new IntVector2(19, 8));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: new IntVector2(8, 1), offset: new IntVector2(1, 0));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: new IntVector2(8, 1), offset: new IntVector2(11, 0));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: new IntVector2(8, 1), offset: new IntVector2(1, 13));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, dimensions: new IntVector2(8, 1), offset: new IntVector2(11, 13));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 80), offset: new IntVector2(16, 16));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 80), offset: new IntVector2(16, 128));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 80), offset: new IntVector2(296, 16));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 80), offset: new IntVector2(296, 128));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(120, 8), offset: new IntVector2(24, 16));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(120, 8), offset: new IntVector2(176, 16));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(120, 8), offset: new IntVector2(24, 200));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(120, 8), offset: new IntVector2(176, 200));

            /*ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomDoorTriggers, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(1, 4), offset: new IntVector2(2, 5));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomDoorTriggers, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(1, 4), offset: new IntVector2(17, 5));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomDoorTriggers, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(4, 1), offset: new IntVector2(8, 2));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomDoorTriggers, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(4, 1), offset: new IntVector2(8, 11));*/
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomDoorTriggers, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(16, 10), offset: new IntVector2(2, 2));

            ExpandBootlegRoomPlaceable m_BootlegRoomPlacable = ExpandPrefabs.EXBootlegRoomObject.AddComponent<ExpandBootlegRoomPlaceable>();
            m_BootlegRoomPlacable.Border = m_BorderObject;
            m_BootlegRoomPlacable.DoorFrames = m_DoorFrameObject;
            m_BootlegRoomPlacable.Doors = m_DoorObject;

            ExpandBootlegRoomDoorBlockerPlacables m_BooleftDoorBlockersPlacable = ExpandPrefabs.EXBootlegRoomDoorTriggers.AddComponent<ExpandBootlegRoomDoorBlockerPlacables>();

            FakePrefab.MarkAsFakePrefab(ExpandPrefabs.EXBootlegRoomObject);
            FakePrefab.MarkAsFakePrefab(ExpandPrefabs.EXBootlegRoomDoorTriggers);
            DontDestroyOnLoad(ExpandPrefabs.EXBootlegRoomObject);
            DontDestroyOnLoad(ExpandPrefabs.EXBootlegRoomDoorTriggers);
        }


        public ExpandBootlegRoomPlaceable() {
            m_Enabled = false;
            m_DoorsVisible = false;
        }

        public GameObject Border;
        public GameObject DoorFrames;
        public GameObject Doors;

        private bool m_Enabled;
        private bool m_DoorsVisible;
        private RoomHandler m_ParentRoom;
        
        private void Start() {
            m_Enabled = true;
        }

        private void LateUpdate() {

            if (!m_Enabled) { return; }

            if (!Dungeon.IsGenerating && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && m_ParentRoom != null) {
                if (!m_DoorsVisible && m_ParentRoom.IsSealed && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == m_ParentRoom) {                    
                    Doors.SetActive(true);
                    m_DoorsVisible = true;
                } else if (m_DoorsVisible && !m_ParentRoom.IsSealed && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == m_ParentRoom) {
                    Doors.SetActive(false);
                    m_DoorsVisible = false;
                }
            }
        }
        
        public void ConfigureOnPlacement(RoomHandler room) {
            gameObject.SetActive(true);
            enabled = true;
            m_ParentRoom = room;

            Border.transform.parent = m_ParentRoom.hierarchyParent;
            Border.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            Border.GetComponent<tk2dSprite>().HeightOffGround = 4f;
            Border.GetComponent<tk2dSprite>().UpdateZDepth();

            DoorFrames.transform.parent = m_ParentRoom.hierarchyParent;
            DoorFrames.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            DoorFrames.GetComponent<tk2dSprite>().HeightOffGround = 4f;
            DoorFrames.GetComponent<tk2dSprite>().UpdateZDepth();

            Doors.transform.parent = m_ParentRoom.hierarchyParent;
            DoorFrames.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            Doors.GetComponent<tk2dSprite>().HeightOffGround = -2f;
            Doors.GetComponent<tk2dSprite>().UpdateZDepth();            
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }

    public class ExpandBootlegRoomDoorBlockerPlacables : DungeonPlaceableBehaviour, IPlaceConfigurable {

        private RoomHandler m_ParentRoom;
        private SpeculativeRigidbody m_DoorBlockersRigidBody;
        
        private bool m_DoorsClosed;
        private bool m_triggered;

        public ExpandBootlegRoomDoorBlockerPlacables() {
            m_DoorsClosed = false;
            m_triggered = false;
        }

        private void Start() {

            if (specRigidbody) {
                specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
            }
        }

        private void HandleTriggerCollision(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_triggered | m_DoorsClosed | m_ParentRoom == null) { return; }
            if (!m_ParentRoom.IsSealed) { return; }
            PlayerController player = specRigidbody.GetComponent<PlayerController>();
            if (player) { m_triggered = true; }
        }

        private void Update() {

            if (Dungeon.IsGenerating | m_ParentRoom == null | GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == null | GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != m_ParentRoom) { return; }

            if (m_ParentRoom != null) {
                if (m_ParentRoom.IsSealed && !m_DoorsClosed && m_triggered) {
                    EnableDoorBlockers();
                    m_DoorsClosed = true;
                } else if (!m_ParentRoom.IsSealed && m_DoorsClosed) {
                    m_DoorsClosed = false;
                    m_triggered = false;
                    DisableDoorBlockers();
                }
            }
        }
        
        private void EnableDoorBlockers() {
            m_DoorBlockersRigidBody.CollideWithOthers = true;
            m_DoorBlockersRigidBody.Reinitialize();
            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(specRigidbody);
        }

        private void DisableDoorBlockers() {
            m_DoorBlockersRigidBody.CollideWithOthers = false;
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            gameObject.SetActive(true);
            enabled = true;

            m_ParentRoom = room;

            GameObject m_DoorBlockersObject = new GameObject("BootlegRoomPhantomDoorBlockerCollision");
            m_DoorBlockersObject.transform.position = gameObject.transform.position;
            m_DoorBlockersObject.transform.parent = m_ParentRoom.hierarchyParent;
            ExpandUtility.GenerateOrAddToRigidBody(m_DoorBlockersObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(24, 32), offset: new IntVector2(0, 96));
            ExpandUtility.GenerateOrAddToRigidBody(m_DoorBlockersObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(24, 32), offset: new IntVector2(296, 96));
            ExpandUtility.GenerateOrAddToRigidBody(m_DoorBlockersObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 24), offset: new IntVector2(144, 0));
            ExpandUtility.GenerateOrAddToRigidBody(m_DoorBlockersObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 24), offset: new IntVector2(144, 200));

            m_DoorBlockersRigidBody = m_DoorBlockersObject.GetComponent<SpeculativeRigidbody>();
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

