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
                AkSoundEngine.PostEvent("play_OBJ_door_open_01", self.gameObject);
                SetState(self, true, flipped);
                if (doorClosesAfterEveryOpen) {
                    GameManager.Instance.StartCoroutine(ExpandDelayedReclose(self));
                }
            }
        }
        
        private void SetState(DungeonDoorController self, bool openState, bool flipped = false) {
            FieldInfo hasEverBeenOpen_Field = typeof(DungeonDoorController).GetField("hasEverBeenOpen", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo m_open_Field = typeof(DungeonDoorController).GetField("m_open", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo m_openIsFlipped_Field = typeof(DungeonDoorController).GetField("m_openIsFlipped", BindingFlags.Instance | BindingFlags.NonPublic);

            if (openState) { hasEverBeenOpen_Field.SetValue(self, true); }
            self.TriggerPersistentVFXClear();
            m_open_Field.SetValue(self, openState);
            if (!self.northSouth) {
                for (int i = 0; i < self.doorModules.Length; i++) {
                    if (self.doorModules[i].horizontalFlips) {
                        self.doorModules[i].sprite.FlipX = ((!openState) ? ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "m_openIsFlipped", self) : flipped);
                    }
                }
            }
            if (openState) {
                for (int j = 0; j < self.doorModules.Length; j++) {
                    m_openIsFlipped_Field.SetValue(self, flipped);
                    DungeonDoorController.DoorModule doorModule = self.doorModules[j];
                    string text = doorModule.openAnimationName;
                    tk2dSpriteAnimationClip tk2dSpriteAnimationClip = null;
                    if (!string.IsNullOrEmpty(text)) {
                        if (flipped && self.northSouth) { text = text.Replace("_north", "_south"); }
                        tk2dSpriteAnimationClip = doorModule.animator.GetClipByName(text);
                    }
                    if (tk2dSpriteAnimationClip != null) {
                        doorModule.animator.Play(tk2dSpriteAnimationClip);
                    }
                    for (int k = 0; k < doorModule.AOAnimatorsToDisable.Count; k++) {
                        doorModule.AOAnimatorsToDisable[k].PlayAndDisableObject(string.Empty, null);
                    }
                    doorModule.rigidbody.enabled = false;
                    AnimationDepthLerp(self, doorModule.sprite, doorModule.openDepth, tk2dSpriteAnimationClip, doorModule, !self.northSouth && j == 0);
                }
            } else {
                bool m_openIsFlipped = ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "m_openIsFlipped", self);
                for (int l = 0; l < self.doorModules.Length; l++) {
                    DungeonDoorController.DoorModule doorModule2 = self.doorModules[l];
                    string text2 = doorModule2.closeAnimationName;
                    tk2dSpriteAnimationClip tk2dSpriteAnimationClip2 = null;
                    if (!string.IsNullOrEmpty(text2)) {
                        if (m_openIsFlipped && self.northSouth) { text2 = text2.Replace("_north", "_south"); }
                        tk2dSpriteAnimationClip2 = doorModule2.animator.GetClipByName(text2);
                    }
                    if (tk2dSpriteAnimationClip2 != null) {
                        doorModule2.animator.Play(tk2dSpriteAnimationClip2);
                        tk2dSpriteAnimator animator = doorModule2.animator;
                        animator.AnimationCompleted = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>)Delegate.Combine(animator.AnimationCompleted, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip>(self.OnCloseAnimationCompleted));
                    } else {
                        doorModule2.animator.StopAndResetFrame();
                    }
                    for (int m = 0; m < doorModule2.AOAnimatorsToDisable.Count; m++) {
                        doorModule2.AOAnimatorsToDisable[m].gameObject.SetActive(true);
                        doorModule2.AOAnimatorsToDisable[m].StopAndResetFrame();
                    }
                    doorModule2.rigidbody.enabled = true;
                    AnimationDepthLerp(self, doorModule2.sprite, doorModule2.closedDepth, tk2dSpriteAnimationClip2, doorModule2, false);
                }
            }
            IntVector2 startingPosition = self.gameObject.transform.position.IntXY(VectorConversions.Floor);
            if (self.upstreamRoom != null && self.upstreamRoom.visibility != RoomHandler.VisibilityStatus.OBSCURED) {
                Pixelator.Instance.ProcessRoomAdditionalExits(startingPosition, self.upstreamRoom, false);
            }
            if (self.downstreamRoom != null && self.downstreamRoom.visibility != RoomHandler.VisibilityStatus.OBSCURED) {
                Pixelator.Instance.ProcessRoomAdditionalExits(startingPosition, self.downstreamRoom, false);
            }
        }

        protected void AnimationDepthLerp(DungeonDoorController self, tk2dSprite targetSprite, float targetDepth, tk2dSpriteAnimationClip clip, DungeonDoorController.DoorModule m = null, bool isSpecialHorizontalTopCase = false) {
            float duration = 1f;
            if (clip != null) { duration = clip.frames.Length / clip.fps; }
            GameManager.Instance.StartCoroutine(DepthLerp(self, targetSprite, targetDepth, duration, m, isSpecialHorizontalTopCase));
        }

        private IEnumerator DepthLerp(DungeonDoorController self, tk2dSprite targetSprite, float targetDepth, float duration, DungeonDoorController.DoorModule m = null, bool isSpecialHorizontalTopCase = false) {
            bool m_open = ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "m_open", self);
            if (m != null) {
                if (!m_open) { targetSprite.IsPerpendicular = true; }
                m.isLerping = true;
            }
            float elapsed = 0f;
            float startingDepth = targetSprite.HeightOffGround;
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                float t = elapsed / duration;
                targetSprite.HeightOffGround = Mathf.Lerp(startingDepth, targetDepth, t);
                if (ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "m_open", self) && isSpecialHorizontalTopCase) {
                    targetSprite.depthUsesTrimmedBounds = false;
                    targetSprite.HeightOffGround = -5.25f;
                }
                targetSprite.UpdateZDepth();
                yield return null;
            }
            targetSprite.HeightOffGround = (targetSprite.depthUsesTrimmedBounds ? targetDepth : -5.25f);
            targetSprite.UpdateZDepth();
            if (m != null) {
                if (ReflectionHelpers.ReflectGetField<bool>(typeof(DungeonDoorController), "m_open", self)) {
                    targetSprite.IsPerpendicular = m.openPerpendicular;
                }
                m.isLerping = false;
            }
            yield break;
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
            AkSoundEngine.PostEvent("play_OBJ_door_open_01", self.gameObject);
            self.Close();
            yield break;
        }
    }
}

