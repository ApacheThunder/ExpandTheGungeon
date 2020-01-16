using System;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandObjects;


namespace ExpandTheGungeon.ExpandComponents {

    class ExpandSecretFloorController : DungeonPlaceableBehaviour, IPlayerInteractable {
        
        public ExpandSecretFloorController() {
            TargetTileset = GlobalDungeonData.ValidTilesets.PHOBOSGEON;
            IsSecretGlitchFloorPit = true;
            targetLevelName = "ss_resourcefulrat";
            WorldLocks = GetComponent<SecretFloorInteractableController>().WorldLocks;
            FlightCollider = GetComponent<SecretFloorInteractableController>().FlightCollider;
            OverridePitIndex = GetComponent<SecretFloorInteractableController>().OverridePitIndex;
        }


        public bool IsSecretGlitchFloorPit;
        public string targetLevelName;

        public List<InteractableLock> WorldLocks;

        public SpeculativeRigidbody FlightCollider;

        public GlobalDungeonData.ValidTilesets TargetTileset;

        public TileIndexGrid OverridePitIndex;
        
        private bool m_hasOpened;
        private bool m_isLoading;

        // private FsmBool m_cryoBool;
        // private FsmBool m_normalBool;

        private float m_timeHovering;



        public override GameObject InstantiateObject(RoomHandler targetRoom, IntVector2 loc, bool deferConfiguration = false) {
            if (IsSecretGlitchFloorPit) {
                IntVector2 intVector = loc + targetRoom.area.basePosition;
                int num = intVector.x;
                int num2 = intVector.x + placeableWidth;
                int num3 = intVector.y;
                int num4 = intVector.y + placeableHeight;
                for (int i = num; i < num2; i++) {
                    for (int j = num3; j < num4; j++) {
                        CellData cellData = GameManager.Instance.Dungeon.data.cellData[i][j];
                        cellData.type = CellType.PIT;
                        cellData.fallingPrevented = true;
                    }
                }
            }
            return base.InstantiateObject(targetRoom, loc, deferConfiguration);
        }
        
        private void Start() {

            if (gameObject.GetComponent<SecretFloorInteractableController>() != null) {
                Destroy(gameObject.GetComponent<SecretFloorInteractableController>());
            }

            RoomHandler absoluteParentRoom = GetAbsoluteParentRoom();
            foreach (InteractableLock Lock in WorldLocks) {
                absoluteParentRoom.RegisterInteractable(Lock);
            }
            
            if (IsSecretGlitchFloorPit) {
                SpeculativeRigidbody specRigidbody = base.specRigidbody;
                specRigidbody.OnEnterTrigger = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnEnterTrigger, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerEntered));
                SpeculativeRigidbody specRigidbody2 = base.specRigidbody;
                specRigidbody2.OnExitTrigger = (SpeculativeRigidbody.OnTriggerExitDelegate)Delegate.Combine(specRigidbody2.OnExitTrigger, new SpeculativeRigidbody.OnTriggerExitDelegate(HandleTriggerExited));
                // SpeculativeRigidbody specRigidbody3 = base.specRigidbody;
                // specRigidbody3.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody3.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerRemain));
            }
            if (FlightCollider) {
                SpeculativeRigidbody flightCollider = FlightCollider;
                flightCollider.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(flightCollider.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleFlightCollider));
            }
        }
        
        private void HandleFlightCollider(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (!GameManager.Instance.IsLoadingLevel && IsValidForUse()) {
                PlayerController component = specRigidbody.GetComponent<PlayerController>();
                if (component && component.IsFlying && !m_isLoading && !GameManager.Instance.IsLoadingLevel && !string.IsNullOrEmpty(targetLevelName)) {
                    m_timeHovering += BraveTime.DeltaTime;
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(component);
                        if (component.IsFlying && !otherPlayer.IsFlying && !otherPlayer.IsGhost) { m_timeHovering = 0f; }
                    }
                    if (m_timeHovering > 0.5f) {
                        m_isLoading = true;
                        if (IsSecretGlitchFloorPit) {
                            GameManager.Instance.InjectedFlowPath = "secretglitchfloor_Flow";                            
                            ExpandUtility.RatDungeon = DungeonDatabase.GetOrLoadByName("Base_ResourcefulRat");
                            ExpandUtility.RatDungeon.LevelOverrideType = GameManager.LevelOverrideState.NONE;
                            ExpandPrefabs.InitCanyonTileSet(ExpandUtility.RatDungeon, GlobalDungeonData.ValidTilesets.PHOBOSGEON);
                        }
                        component.LevelToLoadOnPitfall = targetLevelName;
                        component.ForceFall();
                        // GameManager.Instance.LoadCustomLevel(targetLevelName);
                    }
                }
            }
        }

        private void HandleTriggerEntered(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            PlayerController component = specRigidbody.GetComponent<PlayerController>();
            if (component) {
                if (IsSecretGlitchFloorPit) {
                    GameManager.Instance.InjectedFlowPath = "secretglitchfloor_flow";
                    ExpandUtility.RatDungeon = DungeonDatabase.GetOrLoadByName("Base_ResourcefulRat");
                    ExpandUtility.RatDungeon.LevelOverrideType = GameManager.LevelOverrideState.NONE;
                    ExpandPrefabs.InitCanyonTileSet(ExpandUtility.RatDungeon, GlobalDungeonData.ValidTilesets.PHOBOSGEON);
                }
                component.LevelToLoadOnPitfall = targetLevelName;
            }
        }

        
        private void HandleTriggerExited(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody) {
            PlayerController component = specRigidbody.GetComponent<PlayerController>();
            if (component) {
                if (IsSecretGlitchFloorPit) {
                    GameManager.Instance.InjectedFlowPath = string.Empty;
                    ExpandUtility.RatDungeon = null;
                }
                component.LevelToLoadOnPitfall = string.Empty;
            }
        }

        
        private void Update() {
            if (!m_hasOpened && IsSecretGlitchFloorPit && IsValidForUse()) {
                m_hasOpened = true;
                spriteAnimator.Play();
                IntVector2 intVector = transform.position.IntXY(VectorConversions.Floor);
                int num = intVector.x;
                int num2 = intVector.x + placeableWidth;
                int num3 = intVector.y;
                int num4 = intVector.y + placeableHeight;                
                for (int i = num; i < num2; i++) {
                    int j = num3;
                    while (j < num4) {
                        if (i != intVector.x + 1 || j != intVector.y + 1) {
                            if (i != intVector.x + 1 || j != intVector.y + placeableHeight - 2) {
                                if (i != intVector.x + placeableWidth - 2 || j != intVector.y + 1) {
                                    if (i != intVector.x + placeableWidth - 2 || j != intVector.y + placeableHeight - 2) { goto IL_15C; }
                                }
                            }
                        }
                        IL_180:
                        j++;
                        continue;
                        IL_15C:
                        CellData cellData = GameManager.Instance.Dungeon.data.cellData[i][j];
                        cellData.fallingPrevented = false;
                        goto IL_180;
                    }
                }
            }
        }
        
        private bool IsValidForUse() {
            bool result = true;
            for (int i = 0; i < WorldLocks.Count; i++) {
                if (WorldLocks[i].IsLocked || WorldLocks[i].spriteAnimator.IsPlaying(WorldLocks[i].spriteAnimator.CurrentClip)) { result = false; }
            }
            return result;
        }
        
        public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }
        
        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            sprite.UpdateZDepth();
        }
        
        public float GetDistanceToPoint(Vector2 point) {
            if (IsSecretGlitchFloorPit) { return 1000f; }
            if (!IsValidForUse()) { return 1000f; }
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }
        
        public float GetOverrideMaxDistance() { return -1f; }
        
        public void Interact(PlayerController player) {
            if (IsValidForUse()) {
                if (IsSecretGlitchFloorPit) {
                    ExpandUtility.RatDungeon = DungeonDatabase.GetOrLoadByName("Base_ResourcefulRat");
                    ExpandUtility.RatDungeon.LevelOverrideType = GameManager.LevelOverrideState.NONE;
                    ExpandPrefabs.InitCanyonTileSet(ExpandUtility.RatDungeon, GlobalDungeonData.ValidTilesets.PHOBOSGEON);
                    GameManager.Instance.StartCoroutine(ExpandUtility.DelayedGlitchLevelLoad(1f, "SecretGlitchFloor_Flow", true));
                } else {
                    GameManager.Instance.LoadCustomLevel(targetLevelName);
                }
            }
        }
        
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    
    }
    
}

