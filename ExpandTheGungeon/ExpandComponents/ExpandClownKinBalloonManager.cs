using System.Collections;
using ExpandTheGungeon.ExpandPrefab;
using UnityEngine;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandClownKinBalloonManager : CustomEngageDoer {

        public ExpandClownKinBalloonManager() {
            BalloonOffset = new Vector3(1, 2);
            GlobalBalloonFloatHeight = 3f;
            PoppedBalloonRespawnCoolDown = 30;
            ShuffleBalloonColors = true;
            DoConfettiOnSpawn = true;
            IsSingleBalloon = false;
            SingleBalloonDoesBlankOnPop = false;
            BalloonPrefabs = new List<GameObject>() {
                ExpandPrefabs.EX_RedBalloon,
                ExpandPrefabs.EX_GreenBalloon,
                ExpandPrefabs.EX_BlueBalloon,
                ExpandPrefabs.EX_YellowBalloon,
                ExpandPrefabs.EX_PinkBalloon,
            };

            m_confettiNames = new string[] {
                "Global VFX/Confetti_Blue_001",
                "Global VFX/Confetti_Yellow_001",
                "Global VFX/Confetti_Green_001"
            };

            m_Timer = PoppedBalloonRespawnCoolDown;

            SingleBalloonWasPopped = false;
            m_isFinished = false;
        }

        public override bool IsFinished { get { return m_isFinished; } }

        public Vector3 BalloonOffset;

        public float GlobalBalloonFloatHeight;
        public float PoppedBalloonRespawnCoolDown;

        public List<GameObject> BalloonPrefabs;

        public bool ShuffleBalloonColors;
        public bool DoConfettiOnSpawn;
        public bool IsSingleBalloon;
        public bool SingleBalloonDoesBlankOnPop;
        
        [System.NonSerialized]
        public List<ExpandBalloonController> m_Balloons;
        [System.NonSerialized]
        public ExpandBalloonController m_Balloon;
        [System.NonSerialized]
        public bool SingleBalloonWasPopped;

        private bool m_isFinished;
        
        private string[] m_confettiNames;

        private float m_Timer;


        public void InitFX() {
            if (DoConfettiOnSpawn) {
                AkSoundEngine.PostEvent("Play_OBJ_prize_won_01", gameObject);
                DoConfetti(aiActor.transform.position);
            }

            if (BalloonPrefabs == null | BalloonPrefabs.Count < 3 ) {
                ETGModConsole.Log("ExpandTheGungeon: WARNING: ExpandClownKinBalloonManager's Balloon Prefab list null or does not contain at least 3 prefabs!");
                return;
            }
            
            if (ShuffleBalloonColors) { BalloonPrefabs = BalloonPrefabs.Shuffle(); }
            
            if (IsSingleBalloon) {
                m_Balloon = Instantiate(BraveUtility.RandomElement(BalloonPrefabs), transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>();
                if (m_Balloon) {
                    m_Balloon.gameObject.transform.position += (BalloonOffset + new Vector3(0, 0, 1));
                    m_Balloon.BalloonOffset = (BalloonOffset + new Vector3(0, 0, 1));
                    m_Balloon.BalloonSprite = m_Balloon.gameObject.GetComponent<tk2dSprite>();
                    m_Balloon.BalloonFloatHeight = GlobalBalloonFloatHeight;
                    m_Balloon.FloatUpOnDeathSpeed = Random.Range(2.75f, 3.5f);
                    m_Balloon.DoBlankOnPop = SingleBalloonDoesBlankOnPop;
                    m_Balloon.BlankChance = -1;
                    m_Balloon.ParentClownkinController = this;
                    m_Balloon.PopsOnProjectileHit = true;
                    m_Balloon.BlankVFXPrefab = BraveResources.Load<GameObject>("Global VFX/BlankVFX_Ghost", ".prefab");
                    m_Balloon.Initialize(aiActor);
                }
            } else {
                m_Balloons = new List<ExpandBalloonController>() {
                    Instantiate(BalloonPrefabs[0], transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>(),
                    Instantiate(BalloonPrefabs[1], transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>(),
                    Instantiate(BalloonPrefabs[2], transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>()
                };

                if (m_Balloons != null && m_Balloons.Count == 3) {
                    // Left Balloon
                    m_Balloons[0].gameObject.transform.position += BalloonOffset;
                    m_Balloons[0].BalloonOffset = BalloonOffset;
                    m_Balloons[0].BalloonSprite = m_Balloons[0].gameObject.GetComponent<tk2dSprite>();
                    m_Balloons[0].BalloonFloatHeight = GlobalBalloonFloatHeight - 0.8f;
                    m_Balloons[0].BalloonFloatHorizontalOffset -= 0.4f;
                    m_Balloons[0].FloatUpOnDeathSpeed = Random.Range(2, 2.5f);
                    m_Balloons[0].ParentClownkinController = this;
                    // Center Balloon
                    m_Balloons[1].gameObject.transform.position += (BalloonOffset + new Vector3(0, 0, 1));
                    m_Balloons[1].BalloonOffset = (BalloonOffset + new Vector3(0, 0, 1));
                    m_Balloons[1].BalloonSprite = m_Balloons[1].gameObject.GetComponent<tk2dSprite>();
                    m_Balloons[1].BalloonFloatHeight = GlobalBalloonFloatHeight;
                    m_Balloons[1].FloatUpOnDeathSpeed = Random.Range(2.75f, 3.5f);
                    m_Balloons[1].DoBlankOnPop = true;
                    m_Balloons[1].ParentClownkinController = this;
                    // Right Balloon
                    m_Balloons[2].gameObject.transform.position += BalloonOffset;
                    m_Balloons[2].BalloonOffset = BalloonOffset;
                    m_Balloons[2].BalloonSprite = m_Balloons[2].gameObject.GetComponent<tk2dSprite>();
                    m_Balloons[2].BalloonFloatHeight = GlobalBalloonFloatHeight - 0.82f;
                    m_Balloons[2].BalloonFloatHorizontalOffset += 0.4f;
                    m_Balloons[2].FloatUpOnDeathSpeed = Random.Range(2, 2.5f);
                    m_Balloons[2].ParentClownkinController = this;

                    m_Balloons[0].Initialize(aiActor);
                    m_Balloons[1].Initialize(aiActor);
                    m_Balloons[2].Initialize(aiActor);
                }
            }
        }
        
        public void RespawnBalloon() {
            if (m_Balloon) { return; }

            if (BalloonPrefabs == null | BalloonPrefabs.Count < 3 ) {
                ETGModConsole.Log("ExpandTheGungeon: WARNING: ExpandClownKinBalloonManager's Balloon Prefab list null or does not contain at least 3 prefabs!");
                return;
            }
            
            if (ShuffleBalloonColors) { BalloonPrefabs = BalloonPrefabs.Shuffle(); }

            m_Balloon = Instantiate(BraveUtility.RandomElement(BalloonPrefabs), transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>();
            if (m_Balloon) {
                m_Balloon.gameObject.transform.position += (BalloonOffset + new Vector3(0, 0, 1));
                m_Balloon.BalloonOffset = (BalloonOffset + new Vector3(0, 0, 1));
                m_Balloon.BalloonSprite = m_Balloon.gameObject.GetComponent<tk2dSprite>();
                m_Balloon.BalloonFloatHeight = GlobalBalloonFloatHeight;
                m_Balloon.FloatUpOnDeathSpeed = Random.Range(2.75f, 3.5f);
                m_Balloon.DoBlankOnPop = SingleBalloonDoesBlankOnPop;
                m_Balloon.BlankChance = -1;
                m_Balloon.ParentClownkinController = this;
                m_Balloon.PopsOnProjectileHit = true;
                m_Balloon.BlankVFXPrefab = BraveResources.Load<GameObject>("Global VFX/BlankVFX_Ghost", ".prefab");
                m_Balloon.Initialize(aiActor);
            }
            AkSoundEngine.PostEvent("Play_OBJ_prize_won_01", gameObject);
            DoConfetti(gameObject.transform.position);
        }

        private void DoConfetti(Vector2 startPosition) {
            for (int i = 0; i < 16; i++) {
                GameObject ConfettiObject = (GameObject)ResourceCache.Acquire(BraveUtility.RandomElement(m_confettiNames));
                if (ConfettiObject) {
                    WaftingDebrisObject component = Instantiate(ConfettiObject).GetComponent<WaftingDebrisObject>();
                    if (component) {
                        component.sprite.PlaceAtPositionByAnchor(startPosition.ToVector3ZUp(0f) + new Vector3(0.5f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                        Vector2 insideUnitCircle = Random.insideUnitCircle;
                        insideUnitCircle.y = -Mathf.Abs(insideUnitCircle.y);
                        component.Trigger(insideUnitCircle.ToVector3ZUp(1.5f) * UnityEngine.Random.Range(0.5f, 2f), 0.5f, 0f);
                    }
                }
            }
        }

        public override void StartIntro() { StartCoroutine(DoIntro()); }

        private IEnumerator DoIntro() {
            // InitFX();
            m_isFinished = true;
            yield break;
        }
        
        public void Awake() { InitFX(); }
        public void Start() { }
        public void Update() {
            if (!m_isFinished | !SingleBalloonWasPopped) { return; }
            
            m_Timer -= BraveTime.DeltaTime;

            if (m_Timer < 0) {
                m_Timer = PoppedBalloonRespawnCoolDown;
                RespawnBalloon();
                SingleBalloonWasPopped = false;
                return;
            }
        }

        protected override void OnDestroy() {
            if (m_Balloon && !m_Balloon.DoDetachAndFloatAfterTargetDeath) { Destroy(m_Balloon.gameObject); }
            if (m_Balloons != null && m_Balloons.Count > 0) {
                for (int i = 0; i < m_Balloons.Count; i++) {
                    if (!m_Balloons[i].DoDetachAndFloatAfterTargetDeath) {
                        Destroy(m_Balloons[i].gameObject);
                    }
                }
            }
            base.OnDestroy();
        }
    }
}

