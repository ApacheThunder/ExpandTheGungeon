using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;


namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandInteractableLock : BraveBehaviour, IPlayerInteractable {

        public ExpandInteractableLock() {
            KeyItemID = -1;
            Suppress = false;
            SuppressOutline = false;
            IdleAnimName = "rat_lock_open_idle";
            UnlockAnimName = "rat_lock_open";
            NoKeyAnimName = string.Empty;
            SpitAnimName = string.Empty;
            BustedAnimName = string.Empty;
            lockMode = InteractableLockMode.RAT_REWARD;

            IsLocked = true;
            HasBeenPicked = false;
            IsBusted = false;
        }

        [PickupIdentifier]
        public int KeyItemID;
        public bool Suppress;
        public bool SuppressOutline;
        public InteractableLockMode lockMode;
        public enum InteractableLockMode { NORMAL, RESOURCEFUL_RAT, RAT_REWARD, CUSTOMKEY }

        [CheckAnimation(null)]
        public string IdleAnimName;
        [CheckAnimation(null)]
        public string UnlockAnimName;
        [CheckAnimation(null)]
        public string NoKeyAnimName;
        [CheckAnimation(null)]
        public string SpitAnimName;
        [CheckAnimation(null)]
        public string BustedAnimName;

        [NonSerialized]
        public ExpandInteractableDoor ParentDoor;
        [NonSerialized]
        public Action OnUnlocked;
        [NonSerialized]
        public bool IsLocked;
        [NonSerialized]
        public bool HasBeenPicked;
        [NonSerialized]
        public bool IsBusted;
                
        private bool m_lockHasApproached;
        private bool m_lockHasLaughed;
        private bool m_lockHasSpit;
                
        public void Interact(PlayerController player) {
            if (IsBusted | !IsLocked) { return; }            
            bool HasRequiredKey = false;
            switch (lockMode) {
                default:
                    HasRequiredKey = (player.carriedConsumables.InfiniteKeys || player.carriedConsumables.KeyBullets >= 1);
                    if (!player.carriedConsumables.InfiniteKeys && player.carriedConsumables.KeyBullets > 0) {
                        player.carriedConsumables.KeyBullets = (player.carriedConsumables.KeyBullets - 1);
                    }
                    break;
                case InteractableLockMode.RESOURCEFUL_RAT:
                    for (int i = 0; i < player.passiveItems.Count; i++) {
                        if (player.passiveItems[i] is SpecialKeyItem && (player.passiveItems[i] as SpecialKeyItem).keyType == SpecialKeyItem.SpecialKeyType.RESOURCEFUL_RAT_LAIR) {
                            HasRequiredKey = true;
                            int pickupObjectId = player.passiveItems[i].PickupObjectId;
                            player.RemovePassiveItem(pickupObjectId);
                            GameUIRoot.Instance.UpdatePlayerConsumables(player.carriedConsumables);
                        }
                    }
                    break;
                case InteractableLockMode.RAT_REWARD:
                    if (player.carriedConsumables.ResourcefulRatKeys > 0) {
                        player.carriedConsumables.ResourcefulRatKeys--;
                        HasRequiredKey = true;
                        GameUIRoot.Instance.UpdatePlayerConsumables(player.carriedConsumables);
                    }
                    break;
                case InteractableLockMode.CUSTOMKEY:
                    for (int j = 0; j < player.additionalItems.Count; j++) {
                        if (player.additionalItems[j].PickupObjectId == KeyItemID) {
                            HasRequiredKey = true;
                            Destroy(player.additionalItems[j].gameObject);
                            player.additionalItems.RemoveAt(j);
                        }
                    }
                    break;
            }
            
            if (HasRequiredKey) {
                OnExitRange(player);
                IsLocked = false;
                OnUnlocked?.Invoke();
                if (!string.IsNullOrEmpty(UnlockAnimName)) {
                    spriteAnimator.PlayAndDisableObject(UnlockAnimName, null);
                }
            } else if (!string.IsNullOrEmpty(NoKeyAnimName)) {
                if (!string.IsNullOrEmpty(IdleAnimName) && spriteAnimator.GetClipByName(IdleAnimName) != null) {
                    if (!string.IsNullOrEmpty(SpitAnimName)) {
                        spriteAnimator.Play(NoKeyAnimName);
                    } else {
                        spriteAnimator.PlayForDuration(NoKeyAnimName, 1f, IdleAnimName, false);
                    }
                    m_lockHasSpit = false;
                    m_lockHasLaughed = true;
                } else {
                    spriteAnimator.Play(NoKeyAnimName);
                }
            }
        }

        public void BreakLock() {
            if (IsLocked && !IsBusted && lockMode == InteractableLockMode.NORMAL) {
                IsBusted = true;
                if (!string.IsNullOrEmpty(BustedAnimName) && !spriteAnimator.IsPlaying(BustedAnimName)) {
                    spriteAnimator.Play(BustedAnimName);
                }
            }
        }

        public void ForceUnlock() {
            if (!IsLocked) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            sprite.UpdateZDepth();
            IsLocked = false;
            OnUnlocked?.Invoke();
            if (!string.IsNullOrEmpty(UnlockAnimName)) {
                spriteAnimator.PlayAndDisableObject(UnlockAnimName, null);
            }
        }

        private void ChangeToSpit(tk2dSpriteAnimator arg1, tk2dSpriteAnimationClip arg2) {
            if (spriteAnimator) { spriteAnimator.PlayForDuration(SpitAnimName, -1f, IdleAnimName, false); }
        }

        private void Update() {
            if (!IsBusted) {
                if (IsLocked && !string.IsNullOrEmpty(SpitAnimName)) {
                    float num = Vector2.Distance(sprite.WorldCenter, GameManager.Instance.PrimaryPlayer.specRigidbody.UnitCenter);
                    if (!m_lockHasApproached && num < 2.5f) {
                        spriteAnimator.Play(IdleAnimName);
                        m_lockHasApproached = true;
                    } else if (num > 2.5f) {
                        if (m_lockHasLaughed) { spriteAnimator.Play(SpitAnimName); }
                        m_lockHasLaughed = false;
                        m_lockHasApproached = false;
                    }
                    if (!m_lockHasSpit && spriteAnimator && spriteAnimator.IsPlaying(SpitAnimName) && spriteAnimator.CurrentFrame == 3) {
                        m_lockHasSpit = true;
                        GameObject gameObject = SpawnManager.SpawnVFX(BraveResources.Load("Global VFX/VFX_Lock_Spit", ".prefab") as GameObject, false);
                        tk2dSprite componentInChildren = gameObject.GetComponentInChildren<tk2dSprite>();
                        componentInChildren.UpdateZDepth();
                        componentInChildren.PlaceAtPositionByAnchor(spriteAnimator.sprite.WorldCenter, tk2dBaseSprite.Anchor.UpperCenter);
                    }
                }
            }
        }

        public void OnEnteredRange(PlayerController interactor) {
            if (!this | SuppressOutline) { return; }
            if (!Suppress) {
                tk2dBaseSprite Sprite = sprite;
                if (ParentDoor && ParentDoor.OutlineSpriteIsOnDoorOnly) { Sprite = ParentDoor.sprite; }
                SpriteOutlineManager.AddOutlineToSprite(Sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
                Sprite.UpdateZDepth();
            }
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this | SuppressOutline) { return; }
            if (!Suppress) {
                tk2dBaseSprite Sprite = sprite;
                if (ParentDoor && ParentDoor.OutlineSpriteIsOnDoorOnly) { Sprite = ParentDoor.sprite; }
                SpriteOutlineManager.RemoveOutlineFromSprite(Sprite, false);
                Sprite.UpdateZDepth();
            }
        }

        public float GetDistanceToPoint(Vector2 point) {
            if (IsBusted || !IsLocked || Suppress) { return 10000f; }
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }

        public float GetOverrideMaxDistance() { return -1f; }
        
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

