using Dungeonator;
using System;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandDungeonDoorManager : MonoBehaviour {

        // Allow AIActors to open doors. AIActors with IgnoreForRoomClear set will not be able to open doors in COOP. (to prevent companions from opening doors in COOP mode)
        private void CheckForPlayerCollisionHook(Action<DungeonDoorController, SpeculativeRigidbody, Vector2>orig, DungeonDoorController self, SpeculativeRigidbody otherRigidbody, Vector2 normal) {
            orig(self, otherRigidbody, normal);
            bool isSealed = ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "isSealed", self);
            bool m_open = ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "m_open", self);
            if (isSealed || self.isLocked) { return; }
            AIActor component = otherRigidbody.GetComponent<AIActor>();
            if (component != null && !m_open) {
                bool flipped = false;
                if (normal.y < 0f && self.northSouth) { flipped = true; }
                if (normal.x < 0f && !self.northSouth) { flipped = true; }                
                if (GameManager.Instance.CurrentGameType == GameManager.GameType.SINGLE_PLAYER) {
                    self.Open(flipped);
                } else if (!component.IgnoreForRoomClear) {
                    self.Open(flipped);
                }
            }
        }

        public void Expand_Open(Action<DungeonDoorController, bool>orig, DungeonDoorController self, bool flipped = false) {
            DungeonDoorController.DungeonDoorMode doorMode = ReflectionHelpers.ReflectGetField<DungeonDoorController.DungeonDoorMode>(typeof(DungeonDoorController), "doorMode", self);
            bool m_isDestroyed = ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "m_isDestroyed", self);
            bool m_open = ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "m_open", self);
            bool hasEverBeenOpen = ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "hasEverBeenOpen", self);
            bool doorClosesAfterEveryOpen = ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "doorClosesAfterEveryOpen", self);
            
            if (doorMode == DungeonDoorController.DungeonDoorMode.BOSS_DOOR_ONLY_UNSEALS) { return; }
            if (doorMode == DungeonDoorController.DungeonDoorMode.FINAL_BOSS_DOOR) { return; }
            if (doorMode == DungeonDoorController.DungeonDoorMode.ONE_WAY_DOOR_ONLY_UNSEALS) { return; }
            if (self.IsSealed || self.isLocked || m_isDestroyed) { return; }
            if (!m_open) {
                if (!hasEverBeenOpen) {
                    RoomHandler roomHandler = null;
                    if (self.exitDefinition != null) {
                        if (self.exitDefinition.upstreamRoom != null && self.exitDefinition.upstreamRoom.WillSealOnEntry()) {
                            roomHandler = self.exitDefinition.upstreamRoom;
                        } else if (self.exitDefinition.downstreamRoom != null && self.exitDefinition.downstreamRoom.WillSealOnEntry()) {
                            roomHandler = self.exitDefinition.downstreamRoom;
                        }
                    }
                    if (roomHandler != null && (self.subsidiaryDoor || self.parentDoor)) {
                        DungeonDoorController dungeonDoorController = (!self.subsidiaryDoor) ? self.parentDoor : self.subsidiaryDoor;
                        Vector2 center = roomHandler.area.Center;
                        float num = Vector2.Distance(center, self.gameObject.transform.position);
                        float num2 = Vector2.Distance(center, dungeonDoorController.transform.position);
                        if (num2 < num) { roomHandler = null; }
                    }
                    if (roomHandler != null) {
                        BraveMemory.HandleRoomEntered(roomHandler.GetActiveEnemiesCount(RoomHandler.ActiveEnemyType.All));
                    }
                }
                if (GameManager.Instance.Dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.BELLYGEON) { AkSoundEngine.PostEvent("play_OBJ_door_open_01", self.gameObject); }
                ReflectionHelpers.InvokeMethod(typeof(DungeonDoorController), "SetState", self, new object[] { true, flipped });
                // SetState(self, true, flipped);
                if (doorClosesAfterEveryOpen) { GameManager.Instance.StartCoroutine(ExpandDelayedReclose(self)); }
            }
        }
        // West Doors Reclose modified to account for AIActors since I now allow them to open doors.
        // Modified to reclose when player/enemy is certain distance away as reclosing would immediately retrigger open sequence when player is in close proximity to door.
        private IEnumerator ExpandDelayedReclose(DungeonDoorController self) {
            yield return new WaitForSeconds(0.2f);
            bool WillClose = false;
            float AdditionalDelay = 0.01f;
            Vector2 DoorPosition = self.transform.position.XY();
            while (self.IsOpen && !WillClose) {
                bool containsObstruction = false;
                foreach (DungeonDoorController.DoorModule door in self.doorModules) {
                    foreach (PixelCollider collider in door.rigidbody.PixelColliders) {
                        List<SpeculativeRigidbody> overlappingRigidbodies = PhysicsEngine.Instance.GetOverlappingRigidbodies(collider, null, false);
                        for (int k = 0; k < overlappingRigidbodies.Count; k++) {
                            if (overlappingRigidbodies[k].GetComponent<AIActor>() != null) {
                                containsObstruction = true;
                                AdditionalDelay = 0.2f;
                                break;
                            }
                        }
                        if (containsObstruction) { break; }
                    }
                    if (containsObstruction) { break; }
                }
                if (!containsObstruction) { WillClose = true; }
                yield return null;
            }
            yield return new WaitForSeconds(AdditionalDelay);
            PlayerController PrimaryPlayer = GameManager.Instance.PrimaryPlayer;
            PlayerController SecondaryPlayer = null;
            float DistanceFromDoor = 2.9f;
            if (!string.IsNullOrEmpty(self.gameObject.name) && self.gameObject.name.ToLower().Contains("vertical")) { DistanceFromDoor = 2.6f; }
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) { SecondaryPlayer = GameManager.Instance.SecondaryPlayer; }
            while (Vector2.Distance(DoorPosition, PrimaryPlayer.CenterPosition) < DistanceFromDoor) { yield return null; }
            if (SecondaryPlayer) { while (Vector2.Distance(DoorPosition, PrimaryPlayer.CenterPosition) < DistanceFromDoor) { yield return null; } }
            if (GameManager.Instance.Dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.BELLYGEON) { AkSoundEngine.PostEvent("play_OBJ_door_open_01", self.gameObject); }
            self.Close();
            yield break;
        }
    }
}

