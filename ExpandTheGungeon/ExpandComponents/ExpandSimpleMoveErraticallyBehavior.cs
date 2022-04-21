using Dungeonator;
using Pathfinding;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    // Original version tended to result in AIActors getting caught in a wierd loop where the path never completes with AIActor getting stuck and "vibrating" left/right movement.
    // This version of that behavior will instead add a new timer to force new pathing if AIActor Takes too long to complete path.
    // If PointReachedPauseTime is set 0, random values between 4 to 6 will be used instead.
    public class ExpandSimpleMoveErraticallyBehavior : MovementBehaviorBase {

        public ExpandSimpleMoveErraticallyBehavior() {
            AllowFearState = false;
            PathInterval = 0.5f;
            InitialDelay = 1;
            PointReachedPauseTime = 0;
            PathingTime = 6;
        }

        public bool AllowFearState;

        public float PathInterval;
        public float InitialDelay;
        public float PointReachedPauseTime;
        public float PathingTime;

        private float m_repathTimer;
        private float m_pauseTimer;

        private bool m_IsPathing;

        private IntVector2? m_targetPos;
        
        public override void Start() {
            base.Start();
            m_IsPathing = false;
            m_repathTimer = PathInterval;
            m_pauseTimer = InitialDelay;
        }

        public void ResetPauseTimer() { m_pauseTimer = 0f; }

        public override bool AllowFearRunState { get { return AllowFearState; } }

        public override void Upkeep() {
            base.Upkeep();
            DecrementTimer(ref m_repathTimer, false);
            DecrementTimer(ref m_pauseTimer, false);
        }

        public override BehaviorResult Update() {
            BehaviorResult behaviorResult = base.Update();
            if (behaviorResult != BehaviorResult.Continue) { return behaviorResult; }
            if (!m_aiActor) { return BehaviorResult.Continue; }
            if (!m_IsPathing && m_repathTimer > 0) { return BehaviorResult.Continue; }
            if (m_pauseTimer > 0) { return BehaviorResult.SkipRemainingClassBehaviors; }
            if (m_IsPathing && m_aiActor.PathComplete) {
                if (PointReachedPauseTime > 0) {
                    m_pauseTimer = PointReachedPauseTime;
                } else {
                    m_pauseTimer = Random.Range(4, 6);
                }
                m_IsPathing = false;
                return BehaviorResult.SkipAllRemainingBehaviors;
            } else if (m_IsPathing) {
                m_aiActor.ClearPath();
                if (PointReachedPauseTime > 0) {
                    m_pauseTimer = PointReachedPauseTime;
                } else {
                    m_pauseTimer = Random.Range(4, 6);
                }
                m_IsPathing = false;
                return BehaviorResult.SkipAllRemainingBehaviors;
            }
            if (!m_IsPathing && m_repathTimer <= 0) {
                m_repathTimer = PathInterval;
                RoomHandler roomHandler = m_aiActor.ParentRoom;
                if (roomHandler == null) { return BehaviorResult.Continue; }
                m_targetPos = roomHandler.GetRandomAvailableCell(new IntVector2?(m_aiActor.Clearance), new CellTypes?(m_aiActor.PathableTiles), false, new CellValidator(SimpleCellValidator));
                if (!m_targetPos.HasValue | !SimpleCellValidator(m_targetPos.Value)) { return BehaviorResult.Continue; }
                m_aiActor.PathfindToPosition(m_targetPos.Value.ToCenterVector2());
                m_IsPathing = true;
                m_pauseTimer = PathingTime;
            }
            return BehaviorResult.SkipRemainingClassBehaviors;
        }
        
        private bool SimpleCellValidator(IntVector2 c) {
            for (int X = 0; X < m_aiActor.Clearance.x; X++) {
                for (int Y = 0; Y < m_aiActor.Clearance.y; Y++) {
                    if (GameManager.Instance.Dungeon.data.isTopWall(c.x + X, c.y + Y)) { return false; }
                    if (GameManager.Instance.Dungeon.data.isAnyFaceWall(c.x + X, c.y + Y)) { return false; }
                }
            }
            return true;
        }
    }
}

