using Dungeonator;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCustomEnemyBehaviorEnabler : BraveBehaviour {

        public ExpandCustomEnemyBehaviorEnabler() {
            EnemyToCloneGUID = "57255ed50ee24794b7aac1ac3cfb8a95"; // cultist
            m_Activated = false;
        }

        public string EnemyToCloneGUID;

        private bool m_Activated;
        private RoomHandler m_StartRoom;

        private void Start() { m_StartRoom = aiActor.GetAbsoluteParentRoom(); }

        private void Update() {
            if (!m_Activated) { CheckPlayerRoom(); }
        }

        private void CheckPlayerRoom() {

            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == m_StartRoom) {
                m_Activated = true;
                
                if (gameObject.GetComponent<BehaviorSpeculator>()) {
                    Destroy(this);
                    return;
                }

                AIActor CachedEnemyActor = EnemyDatabase.GetOrLoadByGuid(EnemyToCloneGUID);

                BehaviorSpeculator behaviorSpeculator = gameObject.AddComponent<BehaviorSpeculator>();
                behaviorSpeculator.OverrideBehaviors = new List<OverrideBehaviorBase>();
                behaviorSpeculator.TargetBehaviors = new List<TargetBehaviorBase>();
                behaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>();
                behaviorSpeculator.AttackBehaviors = new List<AttackBehaviorBase>();
                behaviorSpeculator.OtherBehaviors = new List<BehaviorBase>();

                if (CachedEnemyActor.behaviorSpeculator.OverrideBehaviors.Count > 0) {
                    foreach (OverrideBehaviorBase overrideBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().OverrideBehaviors) {
                        behaviorSpeculator.OverrideBehaviors.Add(overrideBehavior);
                    }
                }
                if (CachedEnemyActor.behaviorSpeculator.TargetBehaviors.Count > 0) {
                    foreach (TargetBehaviorBase targetBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().TargetBehaviors) {
                        behaviorSpeculator.TargetBehaviors.Add(targetBehavior);
                    }
                }
                if (CachedEnemyActor.behaviorSpeculator.MovementBehaviors.Count > 0) {
                    foreach (MovementBehaviorBase movementBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().MovementBehaviors) {
                        behaviorSpeculator.MovementBehaviors.Add(movementBehavior);
                    }
                }
                if (CachedEnemyActor.behaviorSpeculator.AttackBehaviors.Count > 0) {
                    foreach (AttackBehaviorBase attackBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().AttackBehaviors) {
                        behaviorSpeculator.AttackBehaviors.Add(attackBehavior);
                    }
                }
                if (CachedEnemyActor.behaviorSpeculator.OtherBehaviors.Count > 0) {
                    foreach (BehaviorBase otherBehavior in CachedEnemyActor.gameObject.GetComponent<BehaviorSpeculator>().OtherBehaviors) {
                        behaviorSpeculator.OtherBehaviors.Add(otherBehavior);
                    }
                }

                behaviorSpeculator.InstantFirstTick = CachedEnemyActor.behaviorSpeculator.InstantFirstTick;
                behaviorSpeculator.TickInterval = CachedEnemyActor.behaviorSpeculator.TickInterval;
                behaviorSpeculator.PostAwakenDelay = CachedEnemyActor.behaviorSpeculator.PostAwakenDelay;
                behaviorSpeculator.RemoveDelayOnReinforce = CachedEnemyActor.behaviorSpeculator.RemoveDelayOnReinforce;
                behaviorSpeculator.OverrideStartingFacingDirection = CachedEnemyActor.behaviorSpeculator.OverrideStartingFacingDirection;
                behaviorSpeculator.StartingFacingDirection = CachedEnemyActor.behaviorSpeculator.StartingFacingDirection;
                behaviorSpeculator.SkipTimingDifferentiator = CachedEnemyActor.behaviorSpeculator.SkipTimingDifferentiator;
                behaviorSpeculator.RegenerateCache();

                CachedEnemyActor = null;
                Destroy(this);
                return;
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

