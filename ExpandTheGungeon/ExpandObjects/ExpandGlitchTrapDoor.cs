/*using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandDungeonFlows;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandGlitchTrapDoor : DungeonPlaceableBehaviour, IPlaceConfigurable {
        
        public ExpandGlitchTrapDoor() {
            placeableWidth = 4;
            placeableHeight = 4;
            difficulty = PlaceableDifficulty.BASE;
            isPassable = true;

            spawnUnlocked = false;
            m_Opened = false;
        }

        public bool spawnUnlocked;
        public string targetPitFallRoomName;
        public TileIndexGrid OverridePitGrid;        
        public InteractableLock Lock;
                
        public SpeculativeRigidbody FlightCollider;

        [NonSerialized]
        public GameObject MinimapIcon;
        public GameObject PitBorderObject;
        public GameObject PitObject;        
        public RoomHandler PitFallTargetRoom;

        private RoomHandler parentRoom;
        private float m_timeHovering;
        private bool m_Opened;

        private IEnumerator Start() {
            
            if (string.IsNullOrEmpty(targetPitFallRoomName)) {
                targetPitFallRoomName = ExpandDungeonFlow.SecretFloorEntranceInjector.exactRoom.name;
            }
            
            if (PitFallTargetRoom == null) {
                FindTargetPitFallRoom();
                yield return null;
            }
            
            if (PitFallTargetRoom == null) {
                if (ExpandStats.debugMode) {
                    ETGModConsole.Log("[ExpandTheGungeon] ERROR: Target pitfall room for ExpandGlitchTrapDoor could not be found!");                    
                }
                Debug.Log("[ExpandTheGungeon] ERROR: Target pitfall room for ExpandGlitchTrapDoor could not be found!");
                Destroy(gameObject);
                yield break;
            }
            
            if (spawnUnlocked) { Lock.ForceUnlock(); }
            
            if (FlightCollider) {
                SpeculativeRigidbody flightCollider = FlightCollider;
                flightCollider.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(flightCollider.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleFlightCollider));
            }

            List<IntVector2> CachedPositions = new List<IntVector2>();
            IntVector2 RandomGlitchEnemyPosition1 = ExpandObjectDatabase.GetRandomAvailableCellForPlacable(GameManager.Instance.Dungeon, parentRoom, CachedPositions, false, true);
            IntVector2 RandomGlitchEnemyPosition2 = ExpandObjectDatabase.GetRandomAvailableCellForPlacable(GameManager.Instance.Dungeon, parentRoom, CachedPositions, false, true);
            IntVector2 RandomGlitchEnemyPosition3 = ExpandObjectDatabase.GetRandomAvailableCellForPlacable(GameManager.Instance.Dungeon, parentRoom, CachedPositions, false, true);
            ExpandGlitchedEnemies m_GlitchEnemyDatabase = new ExpandGlitchedEnemies();
            m_GlitchEnemyDatabase.SpawnGlitchedRat(parentRoom, RandomGlitchEnemyPosition1);
            m_GlitchEnemyDatabase.SpawnGlitchedRat(parentRoom, RandomGlitchEnemyPosition2);
            m_GlitchEnemyDatabase.SpawnGlitchedRat(parentRoom, RandomGlitchEnemyPosition3);
            yield return null;
            m_GlitchEnemyDatabase = null;
            CachedPositions.Clear();

            parentRoom.RegisterInteractable(Lock);

            if (!MinimapIcon) {
                Minimap.Instance.RegisterRoomIcon(parentRoom, ExpandPrefabs.RRMinesHiddenTrapDoorController.MinimapIcon, false);
            } else {
                Minimap.Instance.RegisterRoomIcon(parentRoom, MinimapIcon, false);
            }
            yield break;
        }


        private void FindTargetPitFallRoom() {
            if (GameManager.Instance.Dungeon.data.rooms != null && GameManager.Instance.Dungeon.data.rooms.Count > 0) {
                foreach (RoomHandler room in GameManager.Instance.Dungeon.data.rooms) {
                    if (!string.IsNullOrEmpty(room.GetRoomName())) {
                        if (room.GetRoomName().ToLower() == targetPitFallRoomName.ToLower()) {
                            PitFallTargetRoom = room;

                            GameObject ArrivalObject = GameObject.Find("Arrival(Clone)");
                            if (ArrivalObject != null) { ArrivalObject.name = "Arrival"; }

                            ElevatorDepartureController targetElevator = null;
                            if (FindObjectsOfType<ElevatorDepartureController>() != null) {
                                foreach (ElevatorDepartureController elevator in FindObjectsOfType<ElevatorDepartureController>()) {
                                    if (elevator.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith(room.GetRoomName())) {
                                        targetElevator = elevator;
                                    }
                                }
                            }
                            if (targetElevator != null) {
                                targetElevator.gameObject.AddComponent<ExpandElevatorDepartureManager>();
                                ExpandElevatorDepartureManager expandElevatorComponent = targetElevator.gameObject.GetComponent<ExpandElevatorDepartureManager>();
                                expandElevatorComponent.UsesOverrideTargetFloor = true;
                                expandElevatorComponent.OverrideTargetFloor = GlobalDungeonData.ValidTilesets.PHOBOSGEON;
                                if (expandElevatorComponent.gameObject.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                                    foreach (tk2dBaseSprite baseSprite in expandElevatorComponent.gameObject.GetComponentsInChildren<tk2dBaseSprite>()) {
                                        ExpandShaders.Instance.ApplyGlitchShader(baseSprite);
                                    }
                                }
                            }
                            
                            GameObject m_RatLadder = ExpandPrefabs.ResourcefulRat_LongMinecartRoom_01.placedObjects[2].nonenemyBehaviour.InstantiateObject(room, new IntVector2(0, 6));
                            m_RatLadder.transform.position += new Vector3(0.5f, 0);
                            room.DeregisterInteractable(m_RatLadder.GetComponent<UsableBasicWarp>());
                            m_RatLadder.AddComponent<ExpandUsableBasicWarp>();

                            return;
                        }
                    }
                }
            }
        }

        private void LateUpdate() {

            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != parentRoom) { return; }

            if (!m_Opened && !Lock.IsLocked) {
                m_Opened = true;
                Open();
            }
        }

        public void Open() {            
            StartCoroutine(DelayedOpen());
            return;
        }

        private IEnumerator DelayedOpen() {
            if (parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                while (parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { yield return null; }
            }
            AssignPitfallRoom(PitFallTargetRoom);
            while (Lock.IsLocked) { yield return null; }
            AkSoundEngine.PostEvent("Play_OBJ_hugedoor_open_01", gameObject);
            spriteAnimator.Play("trapdoor_open");
            while (spriteAnimator.IsPlaying("trapdoor_open")) { yield return null; }
            StartCoroutine(HandleFlaggingCells());
            yield break;
        }

        private void HandleFlightCollider(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (!GameManager.Instance.IsLoadingLevel && !Lock.IsLocked) {                
                PlayerController component = specRigidbody.GetComponent<PlayerController>();
                if (component && component.IsFlying) {
                    m_timeHovering += BraveTime.DeltaTime;
                    if (m_timeHovering > 0.5f) {
                        component.ForceFall();
                        m_timeHovering = 0f;
                    }
                }
            }
        }

        
        private IEnumerator HandleFlaggingCells() {
            IntVector2 basePosition = transform.position.IntXY(VectorConversions.Floor);
            for (int i = 1; i < 4 - 1; i++) {
                for (int j = 1; j < 4 - 1; j++) {
                    IntVector2 cellPos = new IntVector2(i, j) + basePosition;
                    DeadlyDeadlyGoopManager.ForceClearGoopsInCell(cellPos);
                }
            }
            yield return new WaitForSeconds(0.4f);
            for (int k = 1; k < 4 - 1; k++) {
                for (int l = 1; l < 4 - 1; l++) {
                    IntVector2 key = new IntVector2(k, l) + basePosition;
                    CellData cellData = GameManager.Instance.Dungeon.data[key];
                    cellData.fallingPrevented = false;
                }
            }
            yield break;
        }

        private void AssignPitfallRoom(RoomHandler target) {
            IntVector2 b = transform.position.IntXY(VectorConversions.Floor);
            for (int i = 0; i < 4; i++) {
                for (int j = -2; j < 4; j++) {
                    IntVector2 key = new IntVector2(i, j) + b;
                    CellData cellData = GameManager.Instance.Dungeon.data[key];
                    cellData.targetPitfallRoom = target;
                    cellData.forceAllowGoop = false;
                }
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            parentRoom = room;
            IntVector2 b = transform.position.IntXY(VectorConversions.Floor);
            for (int X = 1; X < 4 - 1; X++) {
                for (int Y = 1; Y < 4 - 1; Y++) {
                    IntVector2 key = new IntVector2(X, Y) + b;
                    CellData cellData = GameManager.Instance.Dungeon.data[key];
                    cellData.forceAllowGoop = true;
                    cellData.type = CellType.PIT;
                    cellData.fallingPrevented = true;
                }
            }

            // FakePrefab doesn't hook DungeonPlaceableBehaviour.InstantiateObject so we must enable these things ourselves.
            PitBorderObject.SetActive(true);
            PitObject.SetActive(true);            
            Lock.gameObject.SetActive(true);
            Lock.enabled = true;                        
            enabled = true;            
        }


        protected override void OnDestroy() { base.OnDestroy(); }
    }
}*/

