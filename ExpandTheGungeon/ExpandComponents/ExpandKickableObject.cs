using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;

// Custom Copy of KickableObject.
// Mainly to allow using this component on things that aren't drums since missing drum rolling animations causes exceptions in the original code.
namespace ExpandTheGungeon.ExpandComponents {

        public class ExpandKickableObject : DungeonPlaceableBehaviour, IPlayerInteractable, IPlaceConfigurable {        
        
        // Use Explosion Data from Grenade Kin
        public ExplosionData TableExplosionData;

        public GameObject SpawnedObject;
        public bool spawnObjectOnSelfDestruct;
        public bool willDefinitelyExplode;
        public bool explodesOnKick;
        public bool useDefaultExplosion;
        public bool hasRollingAnimations;
        public float rollSpeed;
        [CheckAnimation(null)]
        public string[] rollAnimations;
        [CheckAnimation(null)]
        public string[] impactAnimations;
        public bool leavesGoopTrail;
        [ShowInInspectorIf("leavesGoopTrail", false)]
        public GoopDefinition goopType;
        [ShowInInspectorIf("leavesGoopTrail", false)]
        public float goopFrequency;
        [ShowInInspectorIf("leavesGoopTrail", false)]
        public float goopRadius;
        public bool triggersBreakTimer;
        [ShowInInspectorIf("triggersBreakTimer", false)]
        public float breakTimerLength;
        [ShowInInspectorIf("triggersBreakTimer", false)]
        public GameObject timerVFX;
        public bool RollingDestroysSafely;
        public string RollingBreakAnim;
        private float m_goopElapsed;
        private DeadlyDeadlyGoopManager m_goopManager;
        private RoomHandler m_room;
        private bool m_isBouncingBack;
        private bool m_timerIsActive;
        private bool m_objectSpawned;
        [NonSerialized]
        public bool AllowTopWallTraversal;
        public IntVector2? m_lastDirectionKicked;
        private bool m_shouldDisplayOutline;
        private PlayerController m_lastInteractingPlayer;
        private DungeonData.Direction m_lastOutlineDirection;
        private int m_lastSpriteId;

        private bool m_WasKicked;
    
        public ExpandKickableObject() {
            rollSpeed = 6f;
            rollAnimations = null;
            goopFrequency = 0.05f;
            goopRadius = 1f;
            breakTimerLength = 3f;
            RollingDestroysSafely = false;
            triggersBreakTimer = false;
            AllowTopWallTraversal = true;
            explodesOnKick = true;
            willDefinitelyExplode = false;
            spawnObjectOnSelfDestruct = false;
            useDefaultExplosion = false;
            hasRollingAnimations = false;
            RollingBreakAnim = "red_barrel_break";
            m_lastOutlineDirection = (DungeonData.Direction)(-1);
            m_objectSpawned = false;
            TableExplosionData = ExpandUtility.GenerateExplosionData();

            m_WasKicked = false;
        }
    
    	private void Start() {
            if (m_room == null) { m_room = GetAbsoluteParentRoom(); }
            SpeculativeRigidbody specRigidbody = base.specRigidbody;
            try {
                if (specRigidbody != null) {
                    specRigidbody.OnRigidbodyCollision = (SpeculativeRigidbody.OnRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnRigidbodyCollision, new SpeculativeRigidbody.OnRigidbodyCollisionDelegate(OnPlayerCollision));
                }                
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { 
                    ETGModConsole.Log("Exception Caught at [GetDistanceToPoint] in ExpandKickableObject class.", false);
                    ETGModConsole.Log(ex.Message + ex.Source, false);
                    ETGModConsole.Log(ex.StackTrace, false);
                }
                return;
            }
        }
    
    	public void Update() {
            if (m_WasKicked) {
                if (GetAbsoluteParentRoom() == null) {
                    m_WasKicked = false;
                    willDefinitelyExplode = true;
                    SelfDestructOnKick();
                }
                FlippableCover m_Table = GetComponent<FlippableCover>();
                if (m_Table) {
                    if (m_Table.IsBroken) {
                        m_WasKicked = false;
                        willDefinitelyExplode = true;
                        SelfDestructOnKick();
                    }
                }
            }
    		if (m_shouldDisplayOutline) {
    			int num;
    			DungeonData.Direction inverseDirection = DungeonData.GetInverseDirection(DungeonData.GetDirectionFromIntVector2(GetFlipDirection(m_lastInteractingPlayer.specRigidbody, out num)));
    			if (inverseDirection != m_lastOutlineDirection || sprite.spriteId != m_lastSpriteId) {
    				SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
    				SpriteOutlineManager.AddSingleOutlineToSprite<tk2dSprite>(sprite, DungeonData.GetIntVector2FromDirection(inverseDirection), Color.white, 0.25f, 0f);
    			}
                m_lastOutlineDirection = inverseDirection;
                m_lastSpriteId = sprite.spriteId;
    		}
    		if (leavesGoopTrail && specRigidbody.Velocity.magnitude > 0.1f) {
                m_goopElapsed += BraveTime.DeltaTime;
    			if (m_goopElapsed > goopFrequency) {
                    m_goopElapsed -= BraveTime.DeltaTime;
    				if (m_goopManager == null) {
                        m_goopManager = DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(goopType); }
                    m_goopManager.AddGoopCircle(sprite.WorldCenter, goopRadius + 0.1f, -1, false, -1);
    			}
    			if (AllowTopWallTraversal && GameManager.Instance.Dungeon.data.CheckInBoundsAndValid(sprite.WorldCenter.ToIntVector2(VectorConversions.Floor)) && GameManager.Instance.Dungeon.data[sprite.WorldCenter.ToIntVector2(VectorConversions.Floor)].IsFireplaceCell) {
    				MinorBreakable component = GetComponent<MinorBreakable>();
    				if (component && !component.IsBroken) {
    					component.Break(Vector2.zero);
    					GameStatsManager.Instance.SetFlag(GungeonFlags.FLAG_ROLLED_BARREL_INTO_FIREPLACE, true);
    				}
    			}
    		}
    	}
    
    	public void ForceDeregister() {
    		if (m_room != null) { m_room.DeregisterInteractable(this); }
    		RoomHandler.unassignedInteractableObjects.Remove(this);
    	}
    
    	protected override void OnDestroy() { base.OnDestroy(); }
    
    	public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
    		shouldBeFlipped = false;
    		Vector2 inVec = interactor.CenterPosition - specRigidbody.UnitCenter;
    		switch (BraveMathCollege.VectorToQuadrant(inVec)) {
    		case 0:
    			return "tablekick_down";
    		case 1:
    			shouldBeFlipped = true;
    			return "tablekick_right";
    		case 2:
    			return "tablekick_up";
    		case 3:
    			return "tablekick_right";
    		default:
    			Debug.Log("[ExpandKickableObject] Failed to find table animation state. Using fall back 'tablekick_up' instead.");
    			return "tablekick_up";
    		}
    	}
    
    	public void OnEnteredRange(PlayerController interactor) {
    		if (!this) { return; }
            m_lastInteractingPlayer = interactor;
            m_shouldDisplayOutline = true;
    	}
    
    	public void OnExitRange(PlayerController interactor) {
    		if (!this) { return; }
            ClearOutlines();
            m_shouldDisplayOutline = false;
    	}
    
    	public float GetDistanceToPoint(Vector2 point) {
            // Table Tech Rockets used on tables converted to kickableObjects can cause a softlock of player input not working
            // When this occurs, return a default value and destroy the object.
            try {
                Bounds bounds = sprite.GetBounds();
                bounds.SetMinMax(bounds.min + sprite.transform.position, bounds.max + sprite.transform.position);
                float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
                float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
                return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Exception Caught at [GetDistanceToPoint] in ExpandKickableObject class." + ex.Message + ex.Source + ex.InnerException + ex.StackTrace + ex.TargetSite, false); }
                ForceDeregister();
                float defaultFloat = 0f;                
                Destroy(this);
                return defaultFloat;
            }
    	}
    
    	public float GetOverrideMaxDistance() { return -1f; }
    
    	public void Interact(PlayerController player) {
    		GameManager.Instance.Dungeon.GetRoomFromPosition(specRigidbody.UnitCenter.ToIntVector2(VectorConversions.Round)).DeregisterInteractable(this);
    		RoomHandler.unassignedInteractableObjects.Remove(this);
            Kick(player.specRigidbody);
    		AkSoundEngine.PostEvent("Play_OBJ_table_flip_01", player.gameObject);
            ClearOutlines();
            m_shouldDisplayOutline = false;
    		if (GameManager.Instance.InTutorial) { GameManager.BroadcastRoomTalkDoerFsmEvent("playerRolledBarrel"); }
    	}
    
    	private void NoPits(SpeculativeRigidbody specRigidbody, IntVector2 prevPixelOffset, IntVector2 pixelOffset, ref bool validLocation) {
            try { 
                if (!validLocation) { return; }
    		    Func<IntVector2, bool> func = delegate(IntVector2 pixel) {
    		    	Vector2 v = PhysicsEngine.PixelToUnitMidpoint(pixel);
    		    	if (!GameManager.Instance.Dungeon.CellSupportsFalling(v)) { return false; }
    		    	List<SpeculativeRigidbody> platformsAt = GameManager.Instance.Dungeon.GetPlatformsAt(v);
    		    	if (platformsAt != null) {
    		    		for (int i = 0; i < platformsAt.Count; i++) {
    		    			if (platformsAt[i].PrimaryPixelCollider.ContainsPixel(pixel)) { return false; }
    		    		}
    		    	}
    		    	return true;
    		    };
    		    PixelCollider primaryPixelCollider = specRigidbody.PrimaryPixelCollider;
    		    if (primaryPixelCollider != null) {
    		    	IntVector2 a = pixelOffset - prevPixelOffset;
    		    	if (a == IntVector2.Down && func(primaryPixelCollider.LowerLeft + pixelOffset) && func(primaryPixelCollider.LowerRight + pixelOffset) && (!func(primaryPixelCollider.UpperRight + prevPixelOffset) || !func(primaryPixelCollider.UpperLeft + prevPixelOffset))) {
    		    		validLocation = false;
    		    	}
    		    	else if (a == IntVector2.Right && func(primaryPixelCollider.LowerRight + pixelOffset) && func(primaryPixelCollider.UpperRight + pixelOffset) && (!func(primaryPixelCollider.UpperLeft + prevPixelOffset) || !func(primaryPixelCollider.LowerLeft + prevPixelOffset))) {
    		    		validLocation = false;
    		    	}
    		    	else if (a == IntVector2.Up && func(primaryPixelCollider.UpperRight + pixelOffset) && func(primaryPixelCollider.UpperLeft + pixelOffset) && (!func(primaryPixelCollider.LowerLeft + prevPixelOffset) || !func(primaryPixelCollider.LowerRight + prevPixelOffset))) {
    		    		validLocation = false;
    		    	}
    		    	else if (a == IntVector2.Left && func(primaryPixelCollider.UpperLeft + pixelOffset) && func(primaryPixelCollider.LowerLeft + pixelOffset) && (!func(primaryPixelCollider.LowerRight + prevPixelOffset) || !func(primaryPixelCollider.UpperRight + prevPixelOffset))) {
    		    		validLocation = false;
    		    	}
    		    }
    		    if (!validLocation) { StopRolling(true); }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Exception Caught at [NoPits] in ExpandKickableObject class." + ex.Message + ex.Source + ex.InnerException + ex.StackTrace + ex.TargetSite, false); }
                return;
            }
        }
    
    	public void ConfigureOnPlacement(RoomHandler room) { m_room = room; }
    
    	private void OnPlayerCollision(CollisionData rigidbodyCollision) {
    		PlayerController component = rigidbodyCollision.OtherRigidbody.GetComponent<PlayerController>();
    		if (RollingDestroysSafely && component != null && component.IsDodgeRolling) {
    			MinorBreakable component2 = GetComponent<MinorBreakable>();
    			component2.destroyOnBreak = true;
    			component2.makeParallelOnBreak = false;
                if (hasRollingAnimations) {
                    component2.breakAnimName = RollingBreakAnim;
                }                
    			component2.explodesOnBreak = false;
    			component2.Break(-rigidbodyCollision.Normal);
    		}
    	}
    
    	private void OnPreCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
    		MinorBreakable component = otherRigidbody.GetComponent<MinorBreakable>();
    		if (component && !component.onlyVulnerableToGunfire && !component.IsBig) {
    			component.Break(specRigidbody.Velocity);
    			PhysicsEngine.SkipCollision = true;
    		}
    		if (otherRigidbody && otherRigidbody.aiActor && !otherRigidbody.aiActor.IsNormalEnemy) { PhysicsEngine.SkipCollision = true; }
    	}
    
    	private void OnCollision(CollisionData collision) {
    		if (collision.collisionType == CollisionData.CollisionType.Rigidbody && collision.OtherRigidbody.projectile != null) { return; }
    		if (((BraveMathCollege.ActualSign(specRigidbody.Velocity.x) != 0f && Mathf.Sign(collision.Normal.x) != Mathf.Sign(specRigidbody.Velocity.x)) || (BraveMathCollege.ActualSign(specRigidbody.Velocity.y) != 0f && Mathf.Sign(collision.Normal.y) != Mathf.Sign(specRigidbody.Velocity.y))) && ((BraveMathCollege.ActualSign(specRigidbody.Velocity.x) != 0f && Mathf.Sign(collision.Contact.x - specRigidbody.UnitCenter.x) == Mathf.Sign(specRigidbody.Velocity.x)) || (BraveMathCollege.ActualSign(specRigidbody.Velocity.y) != 0f && Mathf.Sign(collision.Contact.y - specRigidbody.UnitCenter.y) == Mathf.Sign(specRigidbody.Velocity.y)))) {
                StopRolling(collision.collisionType == CollisionData.CollisionType.TileMap);
    		}
    	}
        
    	private bool IsRollAnimation() {
            try {
                if (hasRollingAnimations) {
                    for (int i = 0; i < rollAnimations.Length; i++) {
                        if (spriteAnimator.CurrentClip.name == rollAnimations[i]) { return true; }
                    }
                } else {
                    return false;
                }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("Exception Caught at [IsRollAnimation] in ExpandKickableObject class.", false);
                    ETGModConsole.Log(ex.Message, false);
                    ETGModConsole.Log(ex.StackTrace + ex.Source, false);
                }
                return false;
            }
    		return false;
    	}
    
    	private void StopRolling(bool bounceBack) {
    		if (bounceBack && !m_isBouncingBack) {
                StartCoroutine(HandleBounceback());
    		} else {
    			spriteAnimator.Stop();
                if (hasRollingAnimations) {
                    if (IsRollAnimation()) {
                        tk2dSpriteAnimationClip currentClip = spriteAnimator.CurrentClip;
                        spriteAnimator.Stop();
                        spriteAnimator.Sprite.SetSprite(currentClip.frames[currentClip.frames.Length - 1].spriteId);
                    }
                }
                base.specRigidbody.Velocity = Vector2.zero;
    			MinorBreakable component = GetComponent<MinorBreakable>();
    			if (component != null) { component.onlyVulnerableToGunfire = false; }
    			SpeculativeRigidbody specRigidbody = base.specRigidbody;
    			specRigidbody.OnCollision = (Action<CollisionData>)Delegate.Remove(specRigidbody.OnCollision, new Action<CollisionData>(OnCollision));
    			SpeculativeRigidbody specRigidbody2 = base.specRigidbody;
    			specRigidbody2.MovementRestrictor = (SpeculativeRigidbody.MovementRestrictorDelegate)Delegate.Remove(specRigidbody2.MovementRestrictor, new SpeculativeRigidbody.MovementRestrictorDelegate(NoPits));
    			RoomHandler.unassignedInteractableObjects.Add(this);
                m_isBouncingBack = false;
    		}
    	}
    
    	private IEnumerator HandleBounceback() {
    		if (m_lastDirectionKicked != null) {
                m_isBouncingBack = true;
    			Vector2 dirToMove = m_lastDirectionKicked.Value.ToVector2().normalized * -1f;
    			int quadrant = BraveMathCollege.VectorToQuadrant(dirToMove);
                specRigidbody.Velocity = rollSpeed * dirToMove;
    			IntVector2? lastDirectionKicked = m_lastDirectionKicked;
                m_lastDirectionKicked = ((lastDirectionKicked == null) ? null : new IntVector2?(lastDirectionKicked.GetValueOrDefault() * -1));
                if (hasRollingAnimations) {
                    tk2dSpriteAnimationClip rollClip = spriteAnimator.GetClipByName(rollAnimations[quadrant]);
                    if (rollClip.wrapMode == tk2dSpriteAnimationClip.WrapMode.LoopSection) {
                        spriteAnimator.PlayFromFrame(rollClip, rollClip.loopStart);
                    } else {
                        spriteAnimator.Play(rollClip);
                    }
                }
    			float ela = 0f;
    			float dura = 1.5f / specRigidbody.Velocity.magnitude;
    			while (ela < dura && m_isBouncingBack) {
    				ela += BraveTime.DeltaTime;
                    specRigidbody.Velocity = rollSpeed * dirToMove;
    				yield return null;
    			}
    			if (m_isBouncingBack) { StopRolling(false); }
    		}
    		yield break;
    	}
    
    	private void ClearOutlines() {
            m_lastOutlineDirection = (DungeonData.Direction)(-1);
            m_lastSpriteId = -1;
    		SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
    	}
    
    	private IEnumerator HandleBreakTimer() {
            m_timerIsActive = true;
    		if (timerVFX != null) { timerVFX.SetActive(true); }
    		yield return new WaitForSeconds(breakTimerLength);
            minorBreakable.Break();
    		yield break;
    	}
    
    	private void RemoveFromRoomHierarchy() {
    		Transform hierarchyParent = base.transform.position.GetAbsoluteRoom().hierarchyParent;
    		Transform transform = base.transform;
    		while (transform.parent != null) {
    			if (transform.parent == hierarchyParent) {
    				transform.parent = null;
    				break;
    			}
    			transform = transform.parent;
    		}
    	}
    
    	private void Kick(SpeculativeRigidbody kickerRigidbody) {

            m_WasKicked = true;

            if (explodesOnKick) {
                try {
                    if (willDefinitelyExplode) {
                        Invoke("SelfDestructOnKick", UnityEngine.Random.Range(0f, 0.15f));
                    } else {
                        Invoke("SelfDestructOnKick", UnityEngine.Random.Range(0.25f, 3f));
                    }
                } catch (Exception ex) {
                    if (ExpandSettings.debugMode) {
                        ETGModConsole.Log("Exception Caught at [SelfDestructOnKick] in ExpandKickableObject class.", false);
                        ETGModConsole.Log(ex.Message + ex.Source + ex.StackTrace, false);
                    }
                }
            }

            try {
                if (base.specRigidbody && !base.specRigidbody.enabled) { return; }
                RemoveFromRoomHierarchy();
                List<SpeculativeRigidbody> overlappingRigidbodies = PhysicsEngine.Instance.GetOverlappingRigidbodies(base.specRigidbody.PrimaryPixelCollider, null, false);
                for (int i = 0; i < overlappingRigidbodies.Count; i++) {
                    if (overlappingRigidbodies[i] && overlappingRigidbodies[i].minorBreakable && !overlappingRigidbodies[i].minorBreakable.IsBroken && !overlappingRigidbodies[i].minorBreakable.onlyVulnerableToGunfire && !overlappingRigidbodies[i].minorBreakable.OnlyBrokenByCode) {
                        overlappingRigidbodies[i].minorBreakable.Break();
                    }
                }
                int value = ~CollisionMask.LayerToMask(CollisionLayer.PlayerCollider, CollisionLayer.PlayerHitBox);
    
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(base.specRigidbody, new int?(value), false);
    
                SpeculativeRigidbody specRigidbody = base.specRigidbody;
                if (specRigidbody != null && specRigidbody.MovementRestrictor != null) {
                    specRigidbody.MovementRestrictor = (SpeculativeRigidbody.MovementRestrictorDelegate)Delegate.Combine(specRigidbody.MovementRestrictor, new SpeculativeRigidbody.MovementRestrictorDelegate(NoPits));
                    SpeculativeRigidbody specRigidbody2 = base.specRigidbody;
                    specRigidbody2.OnCollision = (Action<CollisionData>)Delegate.Combine(specRigidbody2.OnCollision, new Action<CollisionData>(OnCollision));
                    SpeculativeRigidbody specRigidbody3 = base.specRigidbody;
                    specRigidbody3.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody3.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(OnPreCollision));
                } else if (specRigidbody != null) {
                    specRigidbody.MovementRestrictor = new SpeculativeRigidbody.MovementRestrictorDelegate(NoPits);
                    SpeculativeRigidbody specRigidbody2 = base.specRigidbody;
                    specRigidbody2.OnCollision = (Action<CollisionData>)Delegate.Combine(specRigidbody2.OnCollision, new Action<CollisionData>(OnCollision));
                    SpeculativeRigidbody specRigidbody3 = base.specRigidbody;
                    specRigidbody3.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody3.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(OnPreCollision));
                }

                int num;
                IntVector2 flipDirection = GetFlipDirection(kickerRigidbody, out num);
                if (AllowTopWallTraversal) { base.specRigidbody.AddCollisionLayerOverride(CollisionMask.LayerToMask(CollisionLayer.EnemyBlocker)); }
                base.specRigidbody.Velocity = rollSpeed * flipDirection.ToVector2();
                tk2dSpriteAnimationClip clipByName = null;
                if (hasRollingAnimations) { clipByName = spriteAnimator.GetClipByName(rollAnimations[num]); }
                bool flag = false;
                if (m_lastDirectionKicked != null) {
                    if (m_lastDirectionKicked.Value.y == 0 && flipDirection.y == 0) { flag = true; }
                    if (m_lastDirectionKicked.Value.x == 0 && flipDirection.x == 0) { flag = true; }
                }
                if (hasRollingAnimations) {
                    if (clipByName != null && clipByName.wrapMode == tk2dSpriteAnimationClip.WrapMode.LoopSection && flag) {
                        spriteAnimator.PlayFromFrame(clipByName, clipByName.loopStart);
                    } else {
                        spriteAnimator.Play(clipByName);
                    }
                }    
                if (triggersBreakTimer && !m_timerIsActive) { StartCoroutine(HandleBreakTimer()); }
                MinorBreakable component = GetComponent<MinorBreakable>();
                if (component != null) {
                    if (impactAnimations[num] != null) { component.breakAnimName = impactAnimations[num]; }
                    component.onlyVulnerableToGunfire = true;
                }
                IntVector2 key = transform.PositionVector2().ToIntVector2(VectorConversions.Round);
                GameManager.Instance.Dungeon.data[key].isOccupied = false;
                m_lastDirectionKicked = new IntVector2?(flipDirection);
    
            } catch (Exception) {
                if (ExpandSettings.debugMode) { ETGModConsole.Log("Exception Caught at [Kick] in ExpandKickableObject class.", false); }
                return;
            }
    	}
    
    	public IntVector2 GetFlipDirection(SpeculativeRigidbody kickerRigidbody, out int quadrant) {
            Vector2 inVec = new Vector2(0, 1);
            if (specRigidbody != null) {
                inVec = specRigidbody.UnitCenter - kickerRigidbody.UnitCenter;
            } else if (sprite != null) {
                inVec = sprite.WorldCenter - kickerRigidbody.UnitCenter;
            }

    		quadrant = BraveMathCollege.VectorToQuadrant(inVec);
    		return IntVector2.Cardinals[quadrant];
    	}
    
        private void SelfDestructOnKick() {
            int currentCurse = PlayerStats.GetTotalCurse();
            int currentCoolness = PlayerStats.GetTotalCoolness();
            float ExplodeOnKickChances = 0.25f;
            float ExplodeOnKickDamageToEnemies = 150f;            
            Vector2 ExplosionCenterPosition = sprite.WorldCenter;
            

            bool ExplodeOnKickDamagesPlayer = BraveUtility.RandomBool();

            if (willDefinitelyExplode) {
                ExplodeOnKickDamagesPlayer = false;
                ExplodeOnKickDamageToEnemies = 200f;
            } else {
                if (currentCoolness >= 3) {
                    ExplodeOnKickDamagesPlayer = false;
                    ExplodeOnKickDamageToEnemies = 175f;
                }
                if (currentCurse >= 3) {
                    ExplodeOnKickChances = 0.35f;
                    ExplodeOnKickDamageToEnemies = 200f;
                }
            }

            if (!ExplodeOnKickDamagesPlayer) { TableExplosionData.damageToPlayer = 0; }

            if (spawnObjectOnSelfDestruct && SpawnedObject != null && !m_objectSpawned) {
                m_objectSpawned = true;
                GameObject PlacedGlitchObject = ExpandUtility.GenerateDungeonPlacable(SpawnedObject, false, true).InstantiateObject(transform.position.GetAbsoluteRoom(), (specRigidbody.GetUnitCenter(ColliderType.HitBox) - transform.position.GetAbsoluteRoom().area.basePosition.ToVector2()).ToIntVector2(VectorConversions.Floor));
                PlacedGlitchObject.transform.parent = transform.position.GetAbsoluteRoom().hierarchyParent;

                if (PlacedGlitchObject.GetComponent<PickupObject>() != null) {
                    PickupObject PlacedGltichObjectComponent = PlacedGlitchObject.GetComponent<PickupObject>();
                    PlacedGltichObjectComponent.RespawnsIfPitfall = true;
                }
            }

            if (UnityEngine.Random.value <= ExplodeOnKickChances | willDefinitelyExplode) {
                if (useDefaultExplosion) { 
                    Exploder.DoDefaultExplosion(ExplosionCenterPosition, Vector2.zero, null, true, CoreDamageTypes.None);
                    Exploder.DoRadialDamage(ExplodeOnKickDamageToEnemies, ExplosionCenterPosition, 4f, ExplodeOnKickDamagesPlayer, true, true);
                } else {
                    Exploder.Explode(ExplosionCenterPosition, TableExplosionData, Vector2.zero, null, false, CoreDamageTypes.None, false);
                }
                Destroy(gameObject);
                return;
            }
            return;
        }
    }
}

