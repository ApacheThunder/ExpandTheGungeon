using Dungeonator;
using ExpandTheGungeon.ItemAPI;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandHatVFX : BraveBehaviour {

        public static ExpandHatVFX PlaceHatOnObject(GameObject parentObject, Vector3 offset, TargetType parentType, bool attached = true, bool alreadyMiddleCenter = false, bool useHitbox = false) {
            GameObject vfxObject = SpawnManager.SpawnVFX(MrCap.MrCapVFX, false);
            ExpandHatVFX hatVFX = vfxObject.GetComponent<ExpandHatVFX>();
            tk2dBaseSprite hatSprite = vfxObject.GetComponent<tk2dBaseSprite>();
            SpeculativeRigidbody parentRigidBody = parentObject.GetComponent<SpeculativeRigidbody>();
            tk2dBaseSprite parentSprite = parentObject.GetComponent<tk2dBaseSprite>();
            Vector3 a = (!useHitbox || !parentRigidBody || parentRigidBody.HitboxPixelCollider == null) ? parentSprite.WorldCenter.ToVector3ZUp(0f) : parentRigidBody.HitboxPixelCollider.UnitCenter.ToVector3ZUp(0f);
            if (!alreadyMiddleCenter) {
                hatSprite.PlaceAtPositionByAnchor(a + offset, tk2dBaseSprite.Anchor.MiddleCenter);
            } else {
                hatSprite.transform.position = a + offset;
            }
            if (attached) {
                vfxObject.transform.parent = parentObject.transform;
                hatSprite.HeightOffGround = 0.2f;
                parentSprite.AttachRenderer(hatSprite);
                if (parentObject.GetComponent<PlayerController>()) {
                    SmartOverheadVFXController component2 = vfxObject.GetComponent<SmartOverheadVFXController>();
                    if (component2) component2.Initialize(parentObject.GetComponent<PlayerController>(), offset);
                }
            }
            if (!alreadyMiddleCenter) vfxObject.transform.localPosition = vfxObject.transform.localPosition.QuantizeFloor(0.0625f);
            hatVFX.targetType = parentType;
            return hatVFX;
        }

        public ExpandHatVFX() {
            targetType = TargetType.NotConfigured;
            currentDirection = DungeonData.Direction.SOUTH;
            hatDirectionality = HatDirectionality.SIX_WAY;

            CachedSpriteDirections = new Dictionary<string, DungeonData.Direction>();

            AddOutline = true;

            SouthSprite  = "hatty_001";
            WestSprite = "hatty_003";
            NorthSprite = "hatty_004";
            EastSprite = "hatty_005";

            SouthWestSprite = "hatty_002";
            SouthEastSprite = "hatty_006";
            NorthWestSprite = "hatty_004";
            NorthEastSprite = "hatty_004";

            vanishOverride = false;
        }

        public bool AddOutline;

        public string SouthSprite;
        public string WestSprite;
        public string NorthSprite;
        public string EastSprite;

        public string SouthWestSprite;
        public string SouthEastSprite;
        public string NorthWestSprite;
        public string NorthEastSprite;
        
        

        public enum TargetType { NotConfigured, Player, AIActor, Generic }

        public TargetType targetType;
        
        public bool vanishOverride;

        // public AIActor TargetEnemy;
        public PlayerController hatOwner;


        public HatDirectionality hatDirectionality;

        public enum HatDirectionality {
            NONE,
            TWO_WAY_HORIZONTAL,
            TWO_WAY_VERTICAL,
            FOUR_WAY,
            SIX_WAY,
        }

        private bool forwardMeansSouth;
        private bool m_ConfigFinished;

        private tk2dSpriteDefinition m_currentPlayerSpriteDef;

        private DungeonData.Direction currentDirection;

        public Dictionary<string, DungeonData.Direction> CachedSpriteDirections;

        private DungeonData.Direction hatDir;

        public void Start() {
            if (AddOutline && sprite) SpriteOutlineManager.AddOutlineToSprite(sprite, Color.black, 1);
        }


        private bool ShouldBeVanished() {
            if (vanishOverride) return true;
            if (!hatOwner | !hatOwner.IsVisible) return true;
            if (!hatOwner.sprite | !hatOwner.sprite.renderer.enabled)return true;
            if (hatOwner.IsFalling | hatOwner.IsGhost)return true;
            if (hatOwner.IsDodgeRolling | hatOwner.IsSlidingOverSurface) return true;
            if (!hatOwner.spriteAnimator | hatOwner.spriteAnimator.CurrentClip.name == "doorway" | hatOwner.spriteAnimator.CurrentClip.name == "spinfall") return true;
            return false;
        }

        private void Update() {
            if (targetType == TargetType.NotConfigured | Dungeon.IsGenerating | GameManager.Instance.IsLoadingLevel | !hatOwner)return;

            switch (targetType) {
                case TargetType.NotConfigured:
                    return;
                case TargetType.Player:
                    HandleVanish();
                    UpdateSprite();
                    return;
                case TargetType.AIActor:
                    UpdateSprite();
                    return;
                case TargetType.Generic: // Facing Direction will remain static
                    return;
                default: return;
            }
        }

        private void LateUpdate() {
            if (targetType == TargetType.NotConfigured | Dungeon.IsGenerating | GameManager.Instance.IsLoadingLevel | !hatOwner) return;
            
            if (!m_ConfigFinished) {
                DetermineIfForwardMeansSouthOrEast(hatOwner.spriteAnimator);
                m_ConfigFinished = true;
            }
        }

        private void HandleVanish() {
            if (!sprite) return;
            bool Visible = sprite.renderer.enabled;
            bool shouldBeVanished = ShouldBeVanished();

            if (shouldBeVanished) {
                sprite.renderer.enabled = false;
            } else {
                sprite.renderer.enabled = true;
            }
            if (!AddOutline) return;
            if (!Visible && !shouldBeVanished) {
                SpriteOutlineManager.AddOutlineToSprite(sprite, Color.black, 1);
            } else if (Visible && shouldBeVanished) {
                SpriteOutlineManager.RemoveOutlineFromSprite(sprite);
            }
        }

        private void UpdateSprite() {
            if (!sprite) return;
            if (targetType == TargetType.AIActor | targetType == TargetType.Player) {
                m_currentPlayerSpriteDef = hatOwner.sprite.GetCurrentSpriteDef();
                DungeonData.Direction targetDir = FetchOwnerFacingDirection();
                if (targetDir == currentDirection)return; // nothing to update
                currentDirection = targetDir; // cache the actual targetDir rather than adjustedDir so we don't call this every frame unnecessarily
                
                // adjust the direction based on what our hat actually supports
                DungeonData.Direction adjustedDir = targetDir;
                if (hatDirectionality == HatDirectionality.NONE) { 
                    adjustedDir = DungeonData.Direction.SOUTH;
                } else if (hatDirectionality == HatDirectionality.TWO_WAY_HORIZONTAL) { 
                    adjustedDir = (hatOwner && hatOwner.sprite.FlipX) ? DungeonData.Direction.WEST : DungeonData.Direction.EAST;
                } else if (hatDirectionality == HatDirectionality.TWO_WAY_VERTICAL) {
                    if (targetDir == DungeonData.Direction.NORTHWEST || targetDir == DungeonData.Direction.NORTHEAST || targetDir == DungeonData.Direction.NORTH) {
                        adjustedDir = DungeonData.Direction.NORTH;
                    } else { 
                        adjustedDir = DungeonData.Direction.SOUTH;
                    }
                } else if (hatDirectionality == HatDirectionality.FOUR_WAY) {
                    if (targetDir == DungeonData.Direction.NORTHWEST) { 
                        adjustedDir = DungeonData.Direction.WEST;
                    } else if (targetDir == DungeonData.Direction.NORTHEAST) { 
                        adjustedDir = DungeonData.Direction.EAST;
                    }
                }

                /*if (!spriteAnimator) // can be null on very first frame of existence (potential problem with dynamically swapped hats)
                {   
                    Debug.LogWarning("Failed to get hat animator in UpdateHatFacingDirection(), this really shouldn't happen...");
                    return;
                }*/
                // pick the appropriate animation
                // My hat will not have animations so this was changed to sprite.SetSprite instead.
                switch (adjustedDir) {
                    case DungeonData.Direction.SOUTH: sprite.SetSprite(SouthSprite); break;
                    case DungeonData.Direction.NORTH: sprite.SetSprite(NorthSprite); break;
                    case DungeonData.Direction.WEST: sprite.SetSprite(WestSprite); break;
                    case DungeonData.Direction.EAST: sprite.SetSprite(EastSprite); break;
                    case DungeonData.Direction.NORTHWEST: sprite.SetSprite(NorthWestSprite); break;
                    case DungeonData.Direction.NORTHEAST: sprite.SetSprite(NorthEastSprite); break;
                    default:
                        ETGModConsole.Log("ERROR: TRIED TO ROTATE HAT TO A NULL DIRECTION! (wtf?)");
                        break;
                }

                return;
            }
        }

        private DungeonData.Direction GetBaseDirectionForSprite(string spriteName) {
            if (spriteName.Contains("front_right_")) return DungeonData.Direction.EAST;
            if (spriteName.Contains("right_front_")) return DungeonData.Direction.EAST;
            //HACK: charAPI mixed up sprite names and we can't change it now without breaking tons of CCs, so now we get to do this ._.
            if (spriteName.Contains("forward_")) return forwardMeansSouth ? DungeonData.Direction.SOUTH : DungeonData.Direction.EAST;
            if (spriteName.Contains("back_right_")) return DungeonData.Direction.NORTHEAST;
            if (spriteName.Contains("bright_")) return DungeonData.Direction.NORTHEAST;
            if (spriteName.Contains("backwards_")) return DungeonData.Direction.NORTHEAST;
            //HACK: charAPI compatibility AGAIN
            if (spriteName.Contains("backward_")) return forwardMeansSouth ? DungeonData.Direction.NORTH : DungeonData.Direction.NORTHEAST;
            if (spriteName.Contains("bw_")) return DungeonData.Direction.NORTHEAST;
            if (spriteName.Contains("north_")) return DungeonData.Direction.NORTH;
            if (spriteName.Contains("up_")) return DungeonData.Direction.NORTH; // only used by CharAPI characters
            if (spriteName.Contains("back_")) return DungeonData.Direction.NORTH;
            if (spriteName.Contains("south_")) return DungeonData.Direction.SOUTH;
            if (spriteName.Contains("front_")) return DungeonData.Direction.SOUTH;
            return DungeonData.Direction.NORTH; // return a sane default
        }

        private void DetermineIfForwardMeansSouthOrEast(tk2dSpriteAnimator SpriteAnimator) {
            tk2dSpriteAnimationClip southClip = SpriteAnimator.GetClipByName("idle_forward");
            if (southClip == null) {
                Debug.Log($"  {SpriteAnimator.gameObject.name} doesn't have an idle_forward animation for hat purposes");
                return;
            }
            tk2dSpriteAnimationFrame southFrame = southClip.frames[0];
            forwardMeansSouth = southFrame.spriteCollection.spriteDefinitions[southFrame.spriteId].name.Contains("forward");
        }

        private DungeonData.Direction FetchOwnerFacingDirection() {
            if (m_currentPlayerSpriteDef == null)return DungeonData.Direction.SOUTH; // return a sane default if we're ownerless

            // figure out an approximate direction from the player's animation name
            if (!CachedSpriteDirections.TryGetValue(m_currentPlayerSpriteDef.name, out hatDir)) // Contains() is slow so cache the results as necessary
                hatDir = CachedSpriteDirections[m_currentPlayerSpriteDef.name] = GetBaseDirectionForSprite(m_currentPlayerSpriteDef.name);

            if (!hatOwner || !hatOwner.sprite.FlipX)return hatDir;
            if (hatDir == DungeonData.Direction.EAST)return DungeonData.Direction.WEST;
            if (hatDir == DungeonData.Direction.NORTHEAST)return DungeonData.Direction.NORTHWEST;

            return hatDir;
        }

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

