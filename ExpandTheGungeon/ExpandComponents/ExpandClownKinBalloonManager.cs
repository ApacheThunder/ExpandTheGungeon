using System.Collections;
using ExpandTheGungeon.ExpandPrefab;
using UnityEngine;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandClownKinBalloonManager : CustomEngageDoer {

        public ExpandClownKinBalloonManager() {
            BalloonOffset = new Vector3(1, 2);
            GlobalBalloonFloatHeight = 3f;
            ShuffleBalloonColors = true;
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

            m_isFinished = false;
        }

        public override bool IsFinished { get { return m_isFinished; } }

        public Vector3 BalloonOffset;

        public float GlobalBalloonFloatHeight;

        public List<GameObject> BalloonPrefabs;

        public bool ShuffleBalloonColors;
        
        private bool m_isFinished;

        private string[] m_confettiNames;

        private List<ExpandBalloonController> m_Balloons;
               

        public void Awake() {
            
            if (BalloonPrefabs == null | BalloonPrefabs.Count < 3 ) {
                ETGModConsole.Log("ExpandTheGungeon: WARNING: ExpandClownKinBalloonManager's Balloon Prefab list null or does not contain at least 3 prefabs!");
                return;
            }
            
            if (ShuffleBalloonColors) { BalloonPrefabs = BalloonPrefabs.Shuffle(); }
            
            m_Balloons = new List<ExpandBalloonController>() {
                Instantiate(BalloonPrefabs[0], transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>(),
                Instantiate(BalloonPrefabs[1], transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>(),
                Instantiate(BalloonPrefabs[2], transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>()
            };
            
            if (m_Balloons != null && m_Balloons.Count > 0) {
                // Left Balloon
                m_Balloons[0].gameObject.transform.position += BalloonOffset;
                m_Balloons[0].BalloonOffset = BalloonOffset;
                m_Balloons[0].BalloonSprite = m_Balloons[0].gameObject.GetComponent<tk2dSprite>();
                m_Balloons[0].BalloonFloatHeight = GlobalBalloonFloatHeight - 0.8f;
                m_Balloons[0].BalloonFloatHorizontalOffset -= 0.4f;
                m_Balloons[0].FloatUpOnDeathSpeed = Random.Range(2, 2.5f);
                // Center Balloon
                m_Balloons[1].gameObject.transform.position += (BalloonOffset + new Vector3(0, 0, 1));
                m_Balloons[1].BalloonOffset = (BalloonOffset + new Vector3(0, 0, 1));
                m_Balloons[1].BalloonSprite = m_Balloons[1].gameObject.GetComponent<tk2dSprite>();
                m_Balloons[1].BalloonFloatHeight = GlobalBalloonFloatHeight;
                m_Balloons[1].FloatUpOnDeathSpeed = Random.Range(2.75f, 3.5f);
                m_Balloons[1].DoBlankOnPop = true;
                // Right Balloon
                m_Balloons[2].gameObject.transform.position += BalloonOffset;
                m_Balloons[2].BalloonOffset = BalloonOffset;
                m_Balloons[2].BalloonSprite = m_Balloons[2].gameObject.GetComponent<tk2dSprite>();
                m_Balloons[2].BalloonFloatHeight = GlobalBalloonFloatHeight - 0.82f;
                m_Balloons[2].BalloonFloatHorizontalOffset += 0.4f;
                m_Balloons[2].FloatUpOnDeathSpeed = Random.Range(2, 2.5f);

                m_Balloons[0].Initialize(aiActor);
                m_Balloons[1].Initialize(aiActor);
                m_Balloons[2].Initialize(aiActor);
            }

            AkSoundEngine.PostEvent("Play_OBJ_prize_won_01", gameObject);
            DoConfetti(aiActor.transform.position);
        }

        public void Start() { }

        public void Update() {
            
            /*if (IsFinished && aiActor && aiActor.healthHaver && aiActor.healthHaver.IsDead) {
                
            }*/

            /*bool PlayerInRoom = false;
            foreach (PlayerController playerController in GameManager.Instance.AllPlayers) {
                if (playerController.CurrentRoom == aiActor.ParentRoom) { PlayerInRoom = true; }
            }
            if (PlayerInRoom) {

            }*/
        }

        public override void StartIntro() { StartCoroutine(DoIntro()); }

        private IEnumerator DoIntro() {
            // aiActor.enabled = false;
            // behaviorSpeculator.enabled = false;
            // aiActor.ToggleRenderers(false);
            // aiActor.IsGone = true;
            // healthHaver.IsVulnerable = false;
            // knockbackDoer.SetImmobile(true, "ExpandWallMimicManager");
            // yield return null;
            // aiActor.ToggleRenderers(false);
            m_isFinished = true;
            yield break;
        }
        
        private void DoConfetti(Vector2 startPosition) {
            for (int i = 0; i < 16; i++) {
                GameObject ConfettiObject = (GameObject)ResourceCache.Acquire(BraveUtility.RandomElement(m_confettiNames));
                if (ConfettiObject) {
                    WaftingDebrisObject component = Instantiate(ConfettiObject).GetComponent<WaftingDebrisObject>();
                    if (component) {
                        component.sprite.PlaceAtPositionByAnchor(startPosition.ToVector3ZUp(0f) + new Vector3(0.5f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                        Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
                        insideUnitCircle.y = -Mathf.Abs(insideUnitCircle.y);
                        component.Trigger(insideUnitCircle.ToVector3ZUp(1.5f) * UnityEngine.Random.Range(0.5f, 2f), 0.5f, 0f);
                    }
                }
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

