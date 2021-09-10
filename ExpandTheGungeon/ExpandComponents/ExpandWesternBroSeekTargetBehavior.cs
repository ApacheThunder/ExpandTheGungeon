using System.Linq;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents
{
    public class ExpandWesternBroSeekTargetBehavior : MovementBehaviorBase
    {
        public bool StopWhenInRange;

        public float CustomRange;

        public float PathInterval;

        private float m_repathTimer;

        private ExpandWesternBroController m_otherBro;

        public override float DesiredCombatDistance
        {
            get
            {
                return this.CustomRange;
            }
        }

        public override void Upkeep()
        {
            base.Upkeep();
            base.DecrementTimer(ref this.m_repathTimer, false);
        }

        public override BehaviorResult Update()
        {
            SpeculativeRigidbody targetRigidbody = this.m_aiActor.TargetRigidbody;

            if (!(targetRigidbody != null))
            {
                return BehaviorResult.Continue;
            }

            float desiredCombatDistance = this.m_aiActor.DesiredCombatDistance;

            if (this.StopWhenInRange && this.m_aiActor.DistanceToTarget <= desiredCombatDistance)
            {
                this.m_aiActor.ClearPath();

                return BehaviorResult.Continue;
            }

            if (this.m_repathTimer <= 0f)
            {
                Vector2 targetPosition;

                if (!this.m_otherBro)
                {
                    m_otherBro = ExpandWesternBroController.GetOtherWesternBros(this.m_aiActor).FirstOrDefault();
                }

                if (!this.m_otherBro)
                {
                    targetPosition = targetRigidbody.UnitCenter;
                }
                else
                {
                    Vector2 unitCenter = this.m_aiActor.TargetRigidbody.specRigidbody.GetUnitCenter(ColliderType.HitBox);
                    Vector2 unitCenter2 = this.m_aiActor.specRigidbody.UnitCenter;
                    Vector2 unitCenter3 = this.m_otherBro.specRigidbody.UnitCenter;
                    float num = (unitCenter2 - unitCenter).ToAngle();
                    float num2 = (unitCenter3 - unitCenter).ToAngle();
                    float num3 = (num + num2) / 2f;
                    float angle;

                    if (BraveMathCollege.ClampAngle180(num - num3) > 0f)
                    {
                        angle = num3 + 90f;
                    }
                    else
                    {
                        angle = num3 - 90f;
                    }

                    targetPosition = unitCenter + BraveMathCollege.DegreesToVector(angle, 1f) * this.DesiredCombatDistance;
                }

                this.m_aiActor.PathfindToPosition(targetPosition, null, true, null, null, null, false);
                this.m_repathTimer = this.PathInterval;
            }

            return BehaviorResult.SkipRemainingClassBehaviors;
        }
    }
}