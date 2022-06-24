namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCasinoPropAnimationHandler : BraveBehaviour {

        public ExpandCasinoPropAnimationHandler() {
            FidgetAnimations = new string[] { "blink", "sigh", };
            IdleAnimation = "idle";
            MaxFidgetWaitTime = 8;
            MaxFidgetWaitTime = 12;

            m_Timer = 10;
        }

        public float MinFidgetWaitTime;
        public float MaxFidgetWaitTime;
        public string[] FidgetAnimations;
        public string IdleAnimation;

        private float m_Timer;
        private string m_CurrentAnimation;
        

        private void Awake() {
            m_Timer = UnityEngine.Random.Range(MinFidgetWaitTime, MaxFidgetWaitTime);
        }

        private void Update() {
            m_Timer -= BraveTime.DeltaTime;

            if (m_Timer <= 0) {
                m_Timer = UnityEngine.Random.Range(MinFidgetWaitTime, MaxFidgetWaitTime);
                m_CurrentAnimation = BraveUtility.RandomElement(FidgetAnimations);
                spriteAnimator.Play(m_CurrentAnimation);
            } else {
                if (!string.IsNullOrEmpty(m_CurrentAnimation) && !spriteAnimator.IsPlaying(m_CurrentAnimation)) {
                    m_CurrentAnimation = IdleAnimation;
                    spriteAnimator.Play(IdleAnimation);
                }
            }
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

