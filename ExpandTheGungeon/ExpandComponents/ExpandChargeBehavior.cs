using Dungeonator;
using FullInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponent {

    public class ExpandChargeBehavior : BasicAttackBehavior {

        public ExpandChargeBehavior() {
		    primeTime = -1f;
		    stopDuringPrime = true;
		    chargeAcceleration = -1f;
		    maxChargeDistance = -1f;
		    chargeKnockback = 50f;
		    chargeDamage = 0.5f;
		    wallRecoilForce = 10f;
            stoppedByProjectiles = true;
		    collidesWithDodgeRollingPlayers = true;
            avoidExits = false;
            avoidWalls = false;
        }

        [InspectorCategory("Conditions")]
        public float minRange;
        [InspectorHeader("Prime")]
        public float primeTime;
        public bool stopDuringPrime;
        [InspectorHeader("Charge")]
        public float leadAmount;
        public float chargeSpeed;
        public float chargeAcceleration;
        public float maxChargeDistance;
        public float chargeKnockback;
        public float chargeDamage;
        public float wallRecoilForce;
        public bool stoppedByProjectiles;
        public bool endWhenChargeAnimFinishes;
        public bool switchCollidersOnCharge;
        public bool collidesWithDodgeRollingPlayers;
        public bool avoidExits;
        public bool avoidWalls;
        [InspectorCategory("Attack")]
        public GameObject ShootPoint;
        [InspectorCategory("Attack")]
        public BulletScriptSelector bulletScript;
        [InspectorCategory("Visuals")]
        public string primeAnim;
        [InspectorCategory("Visuals")]
        public string chargeAnim;
        [InspectorCategory("Visuals")]
        public string hitAnim;
        [InspectorCategory("Visuals")]
        public bool HideGun;
        [InspectorCategory("Visuals")]
        public GameObject launchVfx;
        [InspectorCategory("Visuals")]
        public GameObject trailVfx;
        [InspectorCategory("Visuals")]
        public Transform trailVfxParent;
        [InspectorCategory("Visuals")]
        public GameObject hitVfx;
        [InspectorCategory("Visuals")]
        public GameObject nonActorHitVfx;
        [InspectorCategory("Visuals")]
        public bool chargeDustUps;
        [InspectorShowIf("chargeDustUps")]
        [InspectorCategory("Visuals")]
        [InspectorIndent]
        public float chargeDustUpInterval;
        private BulletScriptSource m_bulletSource;
        private bool m_initialized;
        private float m_timer;
        private float m_chargeTime;
        private float m_cachedKnockback;
        private float m_cachedDamage;
        private VFXPool m_cachedVfx;
        private VFXPool m_cachedNonActorWallVfx;
        private float m_currentSpeed;
        private float m_chargeDirection;
        private CellTypes m_cachedPathableTiles;
        private bool m_cachedDoDustUps;
        private float m_cachedDustUpInterval;
        private PixelCollider m_enemyCollider;
        private PixelCollider m_enemyHitbox;
        private PixelCollider m_projectileCollider;
        private GameObject m_trailVfx;
        private Vector2 m_collisionNormal;
        private FireState m_state;
        private enum FireState { Idle, Priming, Charging, Bouncing }


        public override void Start() {
	    	base.Start();
	    	m_cachedKnockback = m_aiActor.CollisionKnockbackStrength;
	    	m_cachedDamage = m_aiActor.CollisionDamage;
	    	m_cachedVfx = m_aiActor.CollisionVFX;
	    	m_cachedNonActorWallVfx = m_aiActor.NonActorCollisionVFX;
	    	m_cachedPathableTiles = m_aiActor.PathableTiles;
	    	m_cachedDoDustUps = m_aiActor.DoDustUps;
	    	m_cachedDustUpInterval = m_aiActor.DustUpInterval;
	    	if (switchCollidersOnCharge) {
	    		for (int i = 0; i < m_aiActor.specRigidbody.PixelColliders.Count; i++) {
	    			PixelCollider pixelCollider = m_aiActor.specRigidbody.PixelColliders[i];
	    			if (pixelCollider.CollisionLayer == CollisionLayer.EnemyCollider) { m_enemyCollider = pixelCollider; }
	    			if (pixelCollider.CollisionLayer == CollisionLayer.EnemyHitBox) { m_enemyHitbox = pixelCollider; }
	    			if (!pixelCollider.Enabled && pixelCollider.CollisionLayer == CollisionLayer.Projectile) {
	    				m_projectileCollider = pixelCollider;
	    				m_projectileCollider.CollisionLayerCollidableOverride |= CollisionMask.LayerToMask(CollisionLayer.Projectile);
	    			}
	    		}
	    	}
	    	if (!collidesWithDodgeRollingPlayers) {
	    		SpeculativeRigidbody specRigidbody = m_aiActor.specRigidbody;
	    		specRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(OnPreRigidbodyCollision));
	    	}
	    }

	    public override void Upkeep() {
	    	base.Upkeep();
            DecrementTimer(ref m_timer, false);
	    }

	    public override BehaviorResult Update() {
	    	base.Update();
	    	if (!m_initialized) {
	    		SpeculativeRigidbody specRigidbody = m_aiActor.specRigidbody;
	    		specRigidbody.OnCollision = (Action<CollisionData>)Delegate.Combine(specRigidbody.OnCollision, new Action<CollisionData>(OnCollision));
	    		m_initialized = true;
	    	}
	    	BehaviorResult behaviorResult = base.Update();
	    	if (behaviorResult != BehaviorResult.Continue) { return behaviorResult; }
            
            if (!IsReady()) { return BehaviorResult.Continue; }

            if (!m_aiActor.TargetRigidbody) { return BehaviorResult.Continue; }
	    	Vector2 vector = m_aiActor.TargetRigidbody.specRigidbody.GetUnitCenter(ColliderType.HitBox);
	    	if (leadAmount > 0f) {
	    		Vector2 b = vector + m_aiActor.TargetRigidbody.specRigidbody.Velocity * 0.75f;
	    		b = BraveMathCollege.GetPredictedPosition(vector, m_aiActor.TargetVelocity, m_aiActor.specRigidbody.UnitCenter, chargeSpeed);
	    		vector = Vector2.Lerp(vector, b, leadAmount);
	    	}
	    	float num = Vector2.Distance(m_aiActor.specRigidbody.UnitCenter, vector);
	    	if (num > minRange) {
	    		if (!string.IsNullOrEmpty(primeAnim) || primeTime > 0f) { State = FireState.Priming; } else { State = FireState.Charging; }
	    		m_updateEveryFrame = true;
	    		return BehaviorResult.RunContinuous;
	    	}
	    	return BehaviorResult.Continue;
	    }

	    public override ContinuousBehaviorResult ContinuousUpdate() {
            switch (State) {
                case FireState.Priming:
                    if (!m_aiActor.TargetRigidbody) { return ContinuousBehaviorResult.Finished; }
                    if (m_timer > 0f) {
                        float facingDirection = m_aiAnimator.FacingDirection;
                        float num = (m_aiActor.TargetRigidbody.specRigidbody.GetUnitCenter(ColliderType.HitBox) - m_aiActor.specRigidbody.UnitCenter).ToAngle();
                        float b = BraveMathCollege.ClampAngle180(num - facingDirection);
                        float facingDirection2 = facingDirection + Mathf.Lerp(0f, b, m_deltaTime / (m_timer + m_deltaTime));
                        m_aiAnimator.FacingDirection = facingDirection2;
                    }
                    if (!stopDuringPrime) {
                        float magnitude = m_aiActor.BehaviorVelocity.magnitude;
                        float magnitude2 = Mathf.Lerp(magnitude, 0f, m_deltaTime / (m_timer + m_deltaTime));
                        m_aiActor.BehaviorVelocity = BraveMathCollege.DegreesToVector(m_aiAnimator.FacingDirection, magnitude2);
                    }
                    if ((primeTime <= 0f) ? (!m_aiAnimator.IsPlaying(primeAnim)) : (m_timer <= 0f)) { State = FireState.Charging; }
                    break;
                case FireState.Charging:
                    if (chargeAcceleration > 0f) {
                        m_currentSpeed = Mathf.Min(chargeSpeed, m_currentSpeed + chargeAcceleration * m_deltaTime);
                        m_aiActor.BehaviorVelocity = BraveMathCollege.DegreesToVector(m_chargeDirection, m_currentSpeed);
                    }
                    if (endWhenChargeAnimFinishes && !m_aiAnimator.IsPlaying(chargeAnim)) { return ContinuousBehaviorResult.Finished; }
                    if (maxChargeDistance > 0f) {
                        m_chargeTime += m_deltaTime;
                        if (m_chargeTime * chargeSpeed > maxChargeDistance) { return ContinuousBehaviorResult.Finished; }
                    }
                    break;
                case FireState.Bouncing:
                    if (!m_aiAnimator.IsPlaying(hitAnim)) { return ContinuousBehaviorResult.Finished; }
                    break;
                case FireState.Idle:
                    return ContinuousBehaviorResult.Finished;
            }
            return ContinuousBehaviorResult.Continue;
        }

	    public override void EndContinuousUpdate() {
	    	base.EndContinuousUpdate();
	    	m_updateEveryFrame = false;
	    	State = FireState.Idle;
	    	UpdateCooldowns();
	    }

	    public override void Destroy() {
	    	if (m_aiActor) {
	    		SpeculativeRigidbody specRigidbody = m_aiActor.specRigidbody;
	    		specRigidbody.OnPostRigidbodyMovement = (Action<SpeculativeRigidbody, Vector2, IntVector2>)Delegate.Remove(specRigidbody.OnPostRigidbodyMovement, new Action<SpeculativeRigidbody, Vector2, IntVector2>(OnPostRigidbodyMovement));
	    	}
	    	base.Destroy();
	    }

	    private void Fire() {
	    	if (!m_bulletSource) { m_bulletSource = ShootPoint.GetOrAddComponent<BulletScriptSource>(); }
	    	m_bulletSource.BulletManager = m_aiActor.bulletBank;
	    	m_bulletSource.BulletScript = bulletScript;
	    	m_bulletSource.Initialize();
	    }

	    private void OnPreRigidbodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
	    	if (m_state == FireState.Charging) {
	    		PlayerController playerController = otherRigidbody.gameActor as PlayerController;
	    		if (playerController && playerController.spriteAnimator.QueryInvulnerabilityFrame()) { PhysicsEngine.SkipCollision = true; }
	    	}
	    }

	    private void OnCollision(CollisionData collisionData) {
	    	if (State == FireState.Charging && !m_aiActor.healthHaver.IsDead) {
	    		if (collisionData.OtherRigidbody) {
	    			Projectile projectile = collisionData.OtherRigidbody.projectile;
	    			if (projectile) {
	    				if (!(projectile.Owner is PlayerController)) { return; }
	    				if (!stoppedByProjectiles) { return; }
	    			}
	    		}
	    		if (!string.IsNullOrEmpty(hitAnim)) { State = FireState.Bouncing; } else { State = FireState.Idle; }
	    		if (switchCollidersOnCharge) {
	    			PhysicsEngine.CollisionHaltsVelocity = new bool?(true);
	    			PhysicsEngine.HaltRemainingMovement = true;
	    			PhysicsEngine.PostSliceVelocity = new Vector2?(Vector2.zero);
	    			m_collisionNormal = collisionData.Normal;
	    			SpeculativeRigidbody specRigidbody = m_aiActor.specRigidbody;
	    			specRigidbody.OnPostRigidbodyMovement = (Action<SpeculativeRigidbody, Vector2, IntVector2>)Delegate.Combine(specRigidbody.OnPostRigidbodyMovement, new Action<SpeculativeRigidbody, Vector2, IntVector2>(OnPostRigidbodyMovement));
	    		}
	    		if (!collisionData.OtherRigidbody || !collisionData.OtherRigidbody.knockbackDoer) {
	    			m_aiActor.knockbackDoer.ApplyKnockback(collisionData.Normal, wallRecoilForce, false);
	    		}
	    	}
	    }

	    private void OnPostRigidbodyMovement(SpeculativeRigidbody specRigidbody, Vector2 unitDelta, IntVector2 pixelDelta) {
	    	if (!m_behaviorSpeculator) { return; }
	    	List<CollisionData> list = new List<CollisionData>();
	    	bool flag = false;
	    	if (PhysicsEngine.Instance.OverlapCast(m_aiActor.specRigidbody, list, true, true, null, null, false, null, null, new SpeculativeRigidbody[0])) {
	    		for (int i = 0; i < list.Count; i++) {
	    			SpeculativeRigidbody otherRigidbody = list[i].OtherRigidbody;
	    			if (otherRigidbody && otherRigidbody.transform.parent) {
	    				if (otherRigidbody.transform.parent.GetComponent<DungeonDoorSubsidiaryBlocker>() || otherRigidbody.transform.parent.GetComponent<DungeonDoorController>()) {
	    					flag = true;
	    					break;
	    				}
	    			}
	    		}
	    	}
	    	if (flag) {
	    		if (m_collisionNormal.y >= 0.5f) { m_aiActor.transform.position += new Vector3(0f, 0.5f); }
	    		if (m_collisionNormal.x <= -0.5f) { m_aiActor.transform.position += new Vector3(-0.3125f, 0f); }
	    		if (m_collisionNormal.x >= 0.5f) { m_aiActor.transform.position += new Vector3(0.3125f, 0f); }
	    		m_aiActor.specRigidbody.Reinitialize();
	    	} else {
	    		PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(m_aiActor.specRigidbody, null, false);
	    	}
	    	SpeculativeRigidbody specRigidbody2 = m_aiActor.specRigidbody;
	    	specRigidbody2.OnPostRigidbodyMovement = (Action<SpeculativeRigidbody, Vector2, IntVector2>)Delegate.Remove(specRigidbody2.OnPostRigidbodyMovement, new Action<SpeculativeRigidbody, Vector2, IntVector2>(OnPostRigidbodyMovement));
	    }

	    private FireState State {
	    	get { return m_state; }
	    	set {
	    		if (m_state != value) {
	    			EndState(m_state);
	    			m_state = value;
	    			BeginState(m_state);
	    		}
	    	}
	    }

	    private void BeginState(FireState state) {
            switch (state) {
                case FireState.Idle:
                    if (HideGun) { m_aiShooter.ToggleGunAndHandRenderers(true, "ChargeBehavior"); }
                    m_aiActor.BehaviorOverridesVelocity = false;
                    m_aiAnimator.LockFacingDirection = false;
                    break;
                case FireState.Priming:
                    if (HideGun) { m_aiShooter.ToggleGunAndHandRenderers(false, "ChargeBehavior"); }
                    m_aiAnimator.PlayUntilFinished(primeAnim, true, null, -1f, false);
                    if (primeTime > 0f) { m_timer = primeTime; } else { m_timer = m_aiAnimator.CurrentClipLength; }
                    if (stopDuringPrime) {
                    	m_aiActor.ClearPath();
                    	m_aiActor.BehaviorOverridesVelocity = true;
                    	m_aiActor.BehaviorVelocity = Vector2.zero;
                    } else {
                    	m_aiActor.BehaviorOverridesVelocity = true;
                    	m_aiActor.BehaviorVelocity = m_aiActor.specRigidbody.Velocity;
                    }
                    break;
                case FireState.Charging:
                    if (HideGun) { m_aiShooter.ToggleGunAndHandRenderers(false, "ChargeBehavior"); }
	    		    m_chargeTime = 0f;
	    		    Vector2 vector = m_aiActor.TargetRigidbody.specRigidbody.GetUnitCenter(ColliderType.HitBox);
	    		    if (leadAmount > 0f) {
	    		    	Vector2 b = vector + m_aiActor.TargetRigidbody.specRigidbody.Velocity * 0.75f;
	    		    	b = BraveMathCollege.GetPredictedPosition(vector, m_aiActor.TargetVelocity, m_aiActor.specRigidbody.UnitCenter, chargeSpeed);
	    		    	vector = Vector2.Lerp(vector, b, leadAmount);
	    		    }
	    		    m_aiActor.ClearPath();
	    		    m_aiActor.BehaviorOverridesVelocity = true;
	    		    m_currentSpeed = ((chargeAcceleration <= 0f) ? chargeSpeed : 0f);
	    		    m_chargeDirection = (vector - m_aiActor.specRigidbody.UnitCenter).ToAngle();
	    		    m_aiActor.BehaviorVelocity = BraveMathCollege.DegreesToVector(m_chargeDirection, m_currentSpeed);
	    		    m_aiAnimator.LockFacingDirection = true;
	    		    m_aiAnimator.FacingDirection = m_chargeDirection;
	    		    m_aiActor.CollisionKnockbackStrength = chargeKnockback;
	    		    m_aiActor.CollisionDamage = chargeDamage;
	    		    if (hitVfx) {
	    		    	VFXObject vfxobject = new VFXObject();
	    		    	vfxobject.effect = hitVfx;
	    		    	VFXComplex vfxcomplex = new VFXComplex();
	    		    	vfxcomplex.effects = new VFXObject[] { vfxobject };
	    		    	VFXPool vfxpool = new VFXPool();
	    		    	vfxpool.type = VFXPoolType.Single;
	    		    	vfxpool.effects = new VFXComplex[] { vfxcomplex };
	    		    	m_aiActor.CollisionVFX = vfxpool;
	    		    }
	    		    if (nonActorHitVfx) {
	    		    	VFXObject vfxobject2 = new VFXObject();
	    		    	vfxobject2.effect = nonActorHitVfx;
	    		    	VFXComplex vfxcomplex2 = new VFXComplex();
	    		    	vfxcomplex2.effects = new VFXObject[] { vfxobject2 };
	    		    	VFXPool vfxpool2 = new VFXPool();
	    		    	vfxpool2.type = VFXPoolType.Single;
	    		    	vfxpool2.effects = new VFXComplex[] { vfxcomplex2 };
	    		    	m_aiActor.NonActorCollisionVFX = vfxpool2;
	    		    }
	    		    m_aiActor.PathableTiles = (CellTypes.FLOOR | CellTypes.PIT);
	    		    if (switchCollidersOnCharge) {
	    		    	m_enemyCollider.CollisionLayer = CollisionLayer.TileBlocker;
	    		    	m_enemyHitbox.Enabled = false;
	    		    	m_projectileCollider.Enabled = true;
	    		    }
	    		    m_aiActor.DoDustUps = chargeDustUps;
	    		    m_aiActor.DustUpInterval = chargeDustUpInterval;
	    		    m_aiAnimator.PlayUntilFinished(chargeAnim, true, null, -1f, false);
	    		    if (launchVfx) { SpawnManager.SpawnVFX(launchVfx, m_aiActor.specRigidbody.UnitCenter, Quaternion.identity); }
	    		    if (trailVfx) {
	    		    	m_trailVfx = SpawnManager.SpawnParticleSystem(trailVfx, m_aiActor.sprite.WorldCenter, Quaternion.Euler(0f, 0f, m_chargeDirection));
	    		    	if (trailVfxParent) { m_trailVfx.transform.parent = trailVfxParent; } else { m_trailVfx.transform.parent = m_aiActor.transform; }
	    		    	ParticleKiller component = m_trailVfx.GetComponent<ParticleKiller>();
	    		    	if (component != null) { component.Awake(); }
	    		    }
	    		    if (bulletScript != null && !bulletScript.IsNull) { Fire(); }
	    		    m_aiActor.specRigidbody.ForceRegenerate(null, null);
                    break;
                case FireState.Bouncing:
                    m_aiAnimator.PlayUntilFinished(hitAnim, true, null, -1f, false);
                    break;
            }
	    }

	    private void EndState(FireState state) {
            switch (state) {
                case FireState.Charging:
                    m_aiActor.BehaviorVelocity = Vector2.zero;
	    		    m_aiActor.CollisionKnockbackStrength = m_cachedKnockback;
	    		    m_aiActor.CollisionDamage = m_cachedDamage;
	    		    m_aiActor.CollisionVFX = m_cachedVfx;
	    		    m_aiActor.NonActorCollisionVFX = m_cachedNonActorWallVfx;
	    		    if (m_trailVfx) {
	    		    	ParticleKiller component = m_trailVfx.GetComponent<ParticleKiller>();
	    		    	if (component) { component.StopEmitting(); } else { SpawnManager.Despawn(m_trailVfx); }
	    		    	m_trailVfx = null;
	    		    }
	    		    m_aiActor.DoDustUps = m_cachedDoDustUps;
	    		    m_aiActor.DustUpInterval = m_cachedDustUpInterval;
	    		    m_aiActor.PathableTiles = m_cachedPathableTiles;
	    		    if (switchCollidersOnCharge) {
	    		    	m_enemyCollider.CollisionLayer = CollisionLayer.EnemyCollider;
	    		    	m_enemyHitbox.Enabled = true;
	    		    	m_projectileCollider.Enabled = false;
	    		    }
	    		    if (m_bulletSource != null) { m_bulletSource.ForceStop(); }
	    		    m_aiAnimator.EndAnimationIf(chargeAnim);
                    break;
                default:
                    return;
            }
	    }

        public override bool IsReady() {
            if (avoidExits) {
                if (!GameManager.Instance.Dungeon | GameManager.Instance.Dungeon.data == null | !m_aiActor.specRigidbody) { return false; }
                DungeonData data = GameManager.Instance.Dungeon.data;
                CellData cellSafe = data.GetCellSafe(PhysicsEngine.PixelToUnit(m_aiActor.specRigidbody.PrimaryPixelCollider.LowerLeft).ToIntVector2(VectorConversions.Floor));
                CellData cellSafe2 = data.GetCellSafe(PhysicsEngine.PixelToUnit(m_aiActor.specRigidbody.PrimaryPixelCollider.LowerRight).ToIntVector2(VectorConversions.Floor));
                CellData cellSafe3 = data.GetCellSafe(PhysicsEngine.PixelToUnit(m_aiActor.specRigidbody.PrimaryPixelCollider.UpperLeft).ToIntVector2(VectorConversions.Floor));
                CellData cellSafe4 = data.GetCellSafe(PhysicsEngine.PixelToUnit(m_aiActor.specRigidbody.PrimaryPixelCollider.UpperRight).ToIntVector2(VectorConversions.Floor));
                if (cellSafe != null && cellSafe2 != null && cellSafe3 != null && cellSafe4 != null) {
                    if (cellSafe.isExitCell | cellSafe2.isExitCell | cellSafe3.isExitCell | cellSafe4.isExitCell) { return false; }
                } else {
                    return false;
                }
            }
            if (avoidWalls) {
                if (!GameManager.Instance.Dungeon | GameManager.Instance.Dungeon.data == null | !m_aiActor.specRigidbody) { return false; }
                DungeonData data = GameManager.Instance.Dungeon.data;
                CellData cellSafe = data.GetCellSafe(PhysicsEngine.PixelToUnit(m_aiActor.specRigidbody.PrimaryPixelCollider.LowerLeft).ToIntVector2(VectorConversions.Floor));
                CellData cellSafe2 = data.GetCellSafe(PhysicsEngine.PixelToUnit(m_aiActor.specRigidbody.PrimaryPixelCollider.LowerRight).ToIntVector2(VectorConversions.Floor));
                CellData cellSafe3 = data.GetCellSafe(PhysicsEngine.PixelToUnit(m_aiActor.specRigidbody.PrimaryPixelCollider.UpperLeft).ToIntVector2(VectorConversions.Floor));
                CellData cellSafe4 = data.GetCellSafe(PhysicsEngine.PixelToUnit(m_aiActor.specRigidbody.PrimaryPixelCollider.UpperRight).ToIntVector2(VectorConversions.Floor));
                if (cellSafe != null && cellSafe2 != null && cellSafe3 != null && cellSafe4 != null) {
                    if (cellSafe.isNextToWall | cellSafe2.isNextToWall | cellSafe3.isNextToWall | cellSafe4.isNextToWall) { return false; }
                } else {
                    return false;
                }
            }
            return base.IsReady();
        }
    }
}

