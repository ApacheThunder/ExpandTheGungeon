﻿using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandUtilities;
using System;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandBootlegRoomPlaceable : DungeonPlaceableBehaviour, IPlaceConfigurable {
                
        public static void BuildPrefab(AssetBundle expandSharedAssets1) {
            ExpandPrefabs.EXBootlegRoomObject = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomPrefab");
            ExpandPrefabs.EXBootlegRoomDoorTriggers = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomDoorTriggers");
            GameObject m_BorderObject_West = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomBorder_West");
            GameObject m_BorderObject_East = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomBorder_East");
            GameObject m_BorderObject_North = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomBorder_North");
            GameObject m_BorderObject_South = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomBorder_South");
            GameObject m_DoorFrameObject = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomDoorFramesTop");
            GameObject m_DoorObject = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomDoorFrames");
            GameObject m_DoorBlockObject_West = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomDoorBlocker_West");
            GameObject m_DoorBlockObject_East = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomDoorBlocker_East");
            GameObject m_DoorBlockObject_South = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomDoorBlocker_South");
            GameObject m_DoorBlockObject_North = expandSharedAssets1.LoadAsset<GameObject>("BootlegRoomDoorBlocker_North");
            m_BorderObject_West.transform.SetParent(ExpandPrefabs.EXBootlegRoomObject.transform);
            m_BorderObject_East.transform.SetParent(ExpandPrefabs.EXBootlegRoomObject.transform);
            m_BorderObject_North.transform.SetParent(ExpandPrefabs.EXBootlegRoomObject.transform);
            m_BorderObject_South.transform.SetParent(ExpandPrefabs.EXBootlegRoomObject.transform);
            m_DoorFrameObject.transform.SetParent(ExpandPrefabs.EXBootlegRoomObject.transform);
            m_BorderObject_West.transform.localPosition -= new Vector3(3, 3);
            m_BorderObject_East.transform.localPosition -= new Vector3(3, 3);
            m_BorderObject_North.transform.localPosition -= new Vector3(3, 3);
            m_BorderObject_South.transform.localPosition -= new Vector3(3, 3);

            m_DoorObject.transform.SetParent(ExpandPrefabs.EXBootlegRoomDoorTriggers.transform);
            m_DoorBlockObject_West.transform.SetParent(ExpandPrefabs.EXBootlegRoomDoorTriggers.transform);
            m_DoorBlockObject_East.transform.SetParent(ExpandPrefabs.EXBootlegRoomDoorTriggers.transform);
            m_DoorBlockObject_South.transform.SetParent(ExpandPrefabs.EXBootlegRoomDoorTriggers.transform);
            m_DoorBlockObject_North.transform.SetParent(ExpandPrefabs.EXBootlegRoomDoorTriggers.transform);

            // ExpandPrefab.EXBootlegRoomObject.SetActive(false);
            // ExpandPrefab.EXBootlegRoomDoorTriggers.SetActive(false);

            ItemBuilder.AddSpriteToObject(ExpandPrefabs.EXBootlegRoomObject, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_BottomLayer"), false, false);
            ItemBuilder.AddSpriteToObject(m_BorderObject_West, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_ExitTiles_West.png"), false, false);
            ItemBuilder.AddSpriteToObject(m_BorderObject_East, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_ExitTiles_East.png"), false, false);
            ItemBuilder.AddSpriteToObject(m_BorderObject_North, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_ExitTiles_North.png"), false, false);
            ItemBuilder.AddSpriteToObject(m_BorderObject_South, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_ExitTiles_South.png"), false, false);
            ItemBuilder.AddSpriteToObject(m_DoorFrameObject, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_TopLayer"), false, false);
            ItemBuilder.AddSpriteToObject(m_DoorObject, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_Doors"), false, false);
            ItemBuilder.AddSpriteToObject(m_DoorBlockObject_West, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_DoorBlock_West"), false, false);
            ItemBuilder.AddSpriteToObject(m_DoorBlockObject_East, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_DoorBlock_East"), false, false);
            ItemBuilder.AddSpriteToObject(m_DoorBlockObject_South, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_DoorBlock_South"), false, false);
            ItemBuilder.AddSpriteToObject(m_DoorBlockObject_North, expandSharedAssets1.LoadAsset<Texture2D>("BootlegRoom_DoorBlock_North"), false, false);


            
            tk2dSprite m_BootlegRoomSprite = ExpandPrefabs.EXBootlegRoomObject.GetComponent<tk2dSprite>();
            m_BootlegRoomSprite.HeightOffGround = -3f;
            m_BootlegRoomSprite.UpdateZDepth();
                        
            tk2dSprite m_BorderSprite_West = m_BorderObject_West.GetComponent<tk2dSprite>();
            m_BorderSprite_West.HeightOffGround = -3f;
            m_BorderSprite_West.UpdateZDepth();

            tk2dSprite m_BorderSprite_East = m_BorderObject_East.GetComponent<tk2dSprite>();
            m_BorderSprite_East.HeightOffGround = -3f;
            m_BorderSprite_East.UpdateZDepth();

            tk2dSprite m_BorderSprite_North = m_BorderObject_North.GetComponent<tk2dSprite>();
            m_BorderSprite_North.HeightOffGround = -3f;
            m_BorderSprite_North.UpdateZDepth();

            tk2dSprite m_BorderSprite_South = m_BorderObject_South.GetComponent<tk2dSprite>();
            m_BorderSprite_South.HeightOffGround = -3f;
            m_BorderSprite_South.UpdateZDepth();

            tk2dSprite m_DoorSprites = m_DoorObject.GetComponent<tk2dSprite>();
            m_DoorSprites.HeightOffGround = -2.5f;
            m_DoorSprites.UpdateZDepth();

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
            /*ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefab.EXBootlegRoomObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 80), offset: new IntVector2(16, 16));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefab.EXBootlegRoomObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 80), offset: new IntVector2(16, 128));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefab.EXBootlegRoomObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 80), offset: new IntVector2(296, 16));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefab.EXBootlegRoomObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(8, 80), offset: new IntVector2(296, 128));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefab.EXBootlegRoomObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(120, 8), offset: new IntVector2(24, 16));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefab.EXBootlegRoomObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(120, 8), offset: new IntVector2(176, 16));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefab.EXBootlegRoomObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(120, 8), offset: new IntVector2(24, 200));
            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefab.EXBootlegRoomObject, CollisionLayer.EnemyBlocker, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(120, 8), offset: new IntVector2(176, 200));*/

            ExpandUtility.GenerateOrAddToRigidBody(ExpandPrefabs.EXBootlegRoomDoorTriggers, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(16, 10), offset: new IntVector2(2, 2));

            ExpandBootlegRoomPlaceable m_BootlegRoomPlacable = ExpandPrefabs.EXBootlegRoomObject.AddComponent<ExpandBootlegRoomPlaceable>();
            m_BootlegRoomPlacable.ExitTiles_West = m_BorderObject_West;
            m_BootlegRoomPlacable.ExitTiles_East = m_BorderObject_East;
            m_BootlegRoomPlacable.ExitTiles_North = m_BorderObject_North;
            m_BootlegRoomPlacable.ExitTiles_South = m_BorderObject_South;
            m_BootlegRoomPlacable.DoorFrames = m_DoorFrameObject;

            ExpandBootlegRoomDoorsPlacables m_BootlegDoorBlockersPlacable = ExpandPrefabs.EXBootlegRoomDoorTriggers.AddComponent<ExpandBootlegRoomDoorsPlacables>();
            m_BootlegDoorBlockersPlacable.Doors = m_DoorObject;
            m_BootlegDoorBlockersPlacable.DoorBlocker_West = m_DoorBlockObject_West;
            m_BootlegDoorBlockersPlacable.DoorBlocker_East = m_DoorBlockObject_East;
            m_BootlegDoorBlockersPlacable.DoorBlocker_South = m_DoorBlockObject_South;
            m_BootlegDoorBlockersPlacable.DoorBlocker_North = m_DoorBlockObject_North;



            /*FakePrefab.MarkAsFakePrefab(ExpandPrefab.EXBootlegRoomObject);
            FakePrefab.MarkAsFakePrefab(ExpandPrefab.EXBootlegRoomDoorTriggers);
            DontDestroyOnLoad(ExpandPrefab.EXBootlegRoomObject);
            DontDestroyOnLoad(ExpandPrefab.EXBootlegRoomDoorTriggers);*/
        }


        public ExpandBootlegRoomPlaceable() { }

        public GameObject ExitTiles_West;
        public GameObject ExitTiles_East;
        public GameObject ExitTiles_North;
        public GameObject ExitTiles_South;
        public GameObject DoorFrames;
        
        private void Start() { }

        private void LateUpdate() { }
        
        public void ConfigureOnPlacement(RoomHandler room) {
            //specRigidbody.enabled = true;
            specRigidbody.CollideWithOthers = true;
            specRigidbody.Reinitialize();

            ExitTiles_West.SetActive(false);
            ExitTiles_East.SetActive(false);
            ExitTiles_North.SetActive(false);
            ExitTiles_South.SetActive(false);
            
            DoorFrames.transform.SetParent(room.hierarchyParent);
            DoorFrames.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            DoorFrames.GetComponent<tk2dSprite>().HeightOffGround = 4f;
            DoorFrames.GetComponent<tk2dSprite>().UpdateZDepth();

            if (room.area.instanceUsedExits != null && room.area.instanceUsedExits.Count > 0) {
                foreach (PrototypeRoomExit exit in room.area.instanceUsedExits) {
                    if (exit.exitDirection == DungeonData.Direction.WEST) {
                        ExitTiles_West.SetActive(true);
                    } else if (exit.exitDirection == DungeonData.Direction.EAST) {
                        ExitTiles_East.SetActive(true);
                    } else if (exit.exitDirection == DungeonData.Direction.NORTH) {
                        ExitTiles_North.SetActive(true);
                    } else if (exit.exitDirection == DungeonData.Direction.SOUTH) {
                        ExitTiles_South.SetActive(true);
                    }
                }
            }

            //specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.EnemyCollider));
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }

    public class ExpandBootlegRoomDoorsPlacables : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public ExpandBootlegRoomDoorsPlacables() {
            m_DoorsClosed = false;
            m_triggered = false;
        }

        public GameObject Doors;
        public GameObject DoorBlocker_West;
        public GameObject DoorBlocker_East;
        public GameObject DoorBlocker_South;
        public GameObject DoorBlocker_North;
        

        private RoomHandler m_ParentRoom;
        private SpeculativeRigidbody m_DoorBlockersRigidBody;
        
        private bool m_DoorsClosed;
        private bool m_triggered;

        private void Start() {
            if (specRigidbody) {
                specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                m_DoorBlockersRigidBody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.EnemyCollider));
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
            Doors.SetActive(true);
            Doors.GetComponent<tk2dSprite>().renderer.enabled = true;
        }

        private void DisableDoorBlockers() {
            Doors.GetComponent<tk2dSprite>().renderer.enabled = false;
            Doors.SetActive(false);
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;

            Doors.transform.parent = room.hierarchyParent;
            Doors.GetComponent<tk2dSprite>().HeightOffGround = -2f;
            Doors.GetComponent<tk2dSprite>().UpdateZDepth();

            DoorBlocker_West.transform.parent = room.hierarchyParent;
            DoorBlocker_East.transform.parent = room.hierarchyParent;
            DoorBlocker_South.transform.parent = room.hierarchyParent;
            DoorBlocker_North.transform.parent = room.hierarchyParent;
            DoorBlocker_West.GetComponent<tk2dSprite>().HeightOffGround = -1f;
            DoorBlocker_West.GetComponent<tk2dSprite>().UpdateZDepth();
            DoorBlocker_East.GetComponent<tk2dSprite>().HeightOffGround = -1f;
            DoorBlocker_East.GetComponent<tk2dSprite>().UpdateZDepth();
            DoorBlocker_South.GetComponent<tk2dSprite>().HeightOffGround = -1f;
            DoorBlocker_South.GetComponent<tk2dSprite>().UpdateZDepth();
            DoorBlocker_North.GetComponent<tk2dSprite>().HeightOffGround = -1f;
            DoorBlocker_North.GetComponent<tk2dSprite>().UpdateZDepth();
            

            GameObject m_DoorBlockersObject = new GameObject("BootlegRoomPhantomDoorBlockerCollision");
            m_DoorBlockersObject.transform.position = gameObject.transform.position;
            m_DoorBlockersObject.transform.parent = m_ParentRoom.hierarchyParent;
            ExpandUtility.GenerateOrAddToRigidBody(m_DoorBlockersObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(24, 32), offset: new IntVector2(0, 96));
            ExpandUtility.GenerateOrAddToRigidBody(m_DoorBlockersObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(24, 32), offset: new IntVector2(296, 96));
            ExpandUtility.GenerateOrAddToRigidBody(m_DoorBlockersObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 24), offset: new IntVector2(144, 0));
            ExpandUtility.GenerateOrAddToRigidBody(m_DoorBlockersObject, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CollideWithOthers: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 24), offset: new IntVector2(144, 200));

            m_DoorBlockersRigidBody = m_DoorBlockersObject.GetComponent<SpeculativeRigidbody>();

            if (room.area.instanceUsedExits != null && room.area.instanceUsedExits.Count > 0) {
                foreach (PrototypeRoomExit exit in room.area.instanceUsedExits) {
                    if (exit.exitDirection == DungeonData.Direction.WEST) {
                        m_DoorBlockersRigidBody.PixelColliders[0].Enabled = false;
                        DoorBlocker_West.SetActive(false);
                    } else if (exit.exitDirection == DungeonData.Direction.EAST) {
                        m_DoorBlockersRigidBody.PixelColliders[1].Enabled = false;
                        DoorBlocker_East.SetActive(false);
                    } else if (exit.exitDirection == DungeonData.Direction.SOUTH) {
                        m_DoorBlockersRigidBody.PixelColliders[2].Enabled = false;
                        DoorBlocker_South.SetActive(false);
                    } else if (exit.exitDirection == DungeonData.Direction.NORTH) {
                        m_DoorBlockersRigidBody.PixelColliders[3].Enabled = false;
                        DoorBlocker_North.SetActive(false);
                    }
                }
                m_DoorBlockersRigidBody.Reinitialize();
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

