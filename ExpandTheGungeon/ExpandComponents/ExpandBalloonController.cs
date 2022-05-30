using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandBalloonController : BraveBehaviour {

        public ExpandBalloonController() {
            PopAnimationName = "pop";

            DestroyOnDeath = false;
            BalloonOffset = new Vector3(1, 2);
            BalloonFloatHeight = 2.5f;
            BalloonStringZHeight = -3;
            BalloonFloatHorizontalOffset = 1f;
            IsUsingGameObjectTarget = false;
            DoDetachAndFloatAfterTargetDeath = true;
            DoBlankOnPop = false;
            PopsOnProjectileHit = false;

            BlankChance = 0.6f;
            FloatUpOnDeathSpeed = 2;

            m_BalloonPopSoundNames = new List<string>() {
                "Play_EX_BalloonPop_01",
                "Play_EX_BalloonPop_02",
                "Play_EX_BalloonPop_03"
            };

            m_IsDetachedFromOriginalTarget = false;
            IsBeingDestroyed = false;
            m_Inactive = false;
        }

        public string PopAnimationName;

        public GameActor AttachTarget;
        public GameObject AlternateAttachTarget;
        public PlayerController BlankOwner;
        
        public bool DestroyOnDeath;
        public bool IsUsingGameObjectTarget;
        public bool DoDetachAndFloatAfterTargetDeath;
        public bool DoBlankOnPop;
        public bool PopsOnProjectileHit;

        [NonSerialized]
        public bool IsBeingDestroyed;

        public Vector3 BalloonOffset;
        public float BalloonFloatHeight;
        public float BalloonFloatHorizontalOffset;
        public float BalloonStringZHeight;
        public float FloatUpOnDeathSpeed;
        public float BlankChance;

        public tk2dBaseSprite BalloonSprite;
        public tk2dSprite AltTargetSprite;


        [NonSerialized]
        public ExpandClownKinBalloonManager ParentClownkinController;
        [NonSerialized]
        public ClownFriend ParentClownFriendItem;
        [NonSerialized]
        public SpeculativeRigidbody BalloonRigidBody;
        [NonSerialized]
        public GameObject BlankVFXPrefab;

        private GameObject m_BalloonString;

        private bool m_IsDetachedFromOriginalTarget;
        private bool m_Inactive;

        private Vector2 m_currentOffset;
        private Vector3[] m_vertices;

        private Vector3? m_stringTargetAnchorPoint;
        private Vector3? m_stringBalloonAnchorPoint;

        private List<string> m_BalloonPopSoundNames;

        private Mesh m_mesh;
        private MeshFilter m_stringFilter;
        

        public void Initialize(GameActor target) {
            if (!ParentClownkinController && !ParentClownFriendItem) { return; }
            AttachTarget = target;
            m_currentOffset = BalloonOffset;
            if (!BalloonSprite) { BalloonSprite = sprite; }
            m_mesh = new Mesh();
            m_vertices = new Vector3[20];
            m_mesh.vertices = m_vertices;
            int[] array = new int[54];
            Vector2[] uv = new Vector2[20];
            int num = 0;
            for (int i = 0; i < 9; i++) {
                array[i * 6] = num;
                array[i * 6 + 1] = num + 2;
                array[i * 6 + 2] = num + 1;
                array[i * 6 + 3] = num + 2;
                array[i * 6 + 4] = num + 3;
                array[i * 6 + 5] = num + 1;
                num += 2;
            }
            m_mesh.triangles = array;
            m_mesh.uv = uv;
            m_BalloonString = new GameObject("balloon string");
            m_stringFilter = m_BalloonString.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = m_BalloonString.AddComponent<MeshRenderer>();
            meshRenderer.material = (BraveResources.Load("Global VFX/WhiteMaterial", ".mat") as Material);
            m_stringFilter.mesh = m_mesh;
            transform.position = (AttachTarget.transform.position + m_currentOffset.ToVector3ZisY(-3f));

            if (BlankChance != -1 && UnityEngine.Random.value <= BlankChance) { DoBlankOnPop = false; }

            if (DoBlankOnPop && !BlankOwner && ParentClownkinController) {
                if (!ParentClownkinController.IsSingleBalloon) {
                    if (GameManager.Instance.SecondaryPlayer && GameManager.Instance.SecondaryPlayer.HasPassiveItem(ClownBullets.ClownBulletsID)) {
                        BlankOwner = GameManager.Instance.SecondaryPlayer;
                    } else if (DoBlankOnPop && GameManager.Instance.PrimaryPlayer) {
                        BlankOwner = GameManager.Instance.PrimaryPlayer;
                    }
                }
            } else if (DoBlankOnPop && !BlankOwner && ParentClownFriendItem && ParentClownFriendItem.Owner) {
                BlankOwner = ParentClownFriendItem.Owner;
            }
            if (PopsOnProjectileHit) {
                BalloonRigidBody = ExpandUtility.GenerateOrAddToRigidBody(gameObject, CollisionLayer.EnemyBulletBlocker, PixelCollider.PixelColliderGeneration.Manual, false, true, false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(15, 17), offset: new IntVector2(1, 3));
                BalloonRigidBody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(BalloonRigidBody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(OnPreRigidBodyCollision));
            }

        }

        public void Initialize(GameObject target) {
            AlternateAttachTarget = target;
            if (AttachTarget) { AttachTarget = null; }
            if (!BalloonSprite) { BalloonSprite = sprite; }
            IsUsingGameObjectTarget = true;
            m_currentOffset = BalloonOffset;
            if (AlternateAttachTarget.GetComponent<tk2dSprite>()) { AltTargetSprite = AlternateAttachTarget.GetComponent<tk2dSprite>(); }
            m_mesh = new Mesh();
            m_vertices = new Vector3[20];
            m_mesh.vertices = m_vertices;
            int[] array = new int[54];
            Vector2[] uv = new Vector2[20];
            int num = 0;
            for (int i = 0; i < 9; i++) {
                array[i * 6] = num;
                array[i * 6 + 1] = num + 2;
                array[i * 6 + 2] = num + 1;
                array[i * 6 + 3] = num + 2;
                array[i * 6 + 4] = num + 3;
                array[i * 6 + 5] = num + 1;
                num += 2;
            }
            m_mesh.triangles = array;
            m_mesh.uv = uv;
            m_BalloonString = new GameObject("balloon string");
            m_stringFilter = m_BalloonString.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = m_BalloonString.AddComponent<MeshRenderer>();
            meshRenderer.material = (BraveResources.Load("Global VFX/WhiteMaterial", ".mat") as Material);
            m_stringFilter.mesh = m_mesh;
            transform.position = (AlternateAttachTarget.transform.position + m_currentOffset.ToVector3ZisY(-3f));
        }
        
        public void OnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            Projectile projectile = otherRigidbody.projectile;
            if (projectile && projectile.Owner && projectile.Owner is AIActor && !projectile.Owner.gameObject.GetComponent<CompanionController>()) {
                ExpandBalloonController balloonController = myRigidbody.gameObject.GetComponent<ExpandBalloonController>();
                if (balloonController) {
                    if (ParentClownkinController) {
                        if (balloonController.ParentClownkinController.aiActor && balloonController.ParentClownkinController.aiActor.CompanionOwner) {
                            BlankOwner = balloonController.ParentClownkinController.aiActor.CompanionOwner;
                        }
                        balloonController.ParentClownkinController.SingleBalloonWasPopped = true;
                    } else if (ParentClownFriendItem) {
                        if (ParentClownFriendItem.Owner) { BlankOwner = ParentClownFriendItem.Owner; }
                        ParentClownFriendItem.BalloonWasPopped = true;
                    }
                    balloonController.IsBeingDestroyed = true;
                }
            } else {
                PhysicsEngine.SkipCollision = true;
            }
        }
        

        private IEnumerator HandleFloatAndPop(GameObject target) {
            gameObject.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));
            target.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));
            m_BalloonString.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));

            float elapsed = 0f;
            Vector2 startOffset = target.transform.PositionVector2();            
            Vector2 HeighestPointPosition = (startOffset + new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(5, 10)));
            
            while (elapsed < FloatUpOnDeathSpeed) {
                elapsed += BraveTime.DeltaTime;
                target.transform.position = Vector2.Lerp(startOffset, HeighestPointPosition, (elapsed / FloatUpOnDeathSpeed));
                yield return null;
            }

            IsBeingDestroyed = true;

            yield break;
        }
        

        private void LateUpdate() {
            if (m_Inactive) { return; }

            if (IsBeingDestroyed) {
                IsBeingDestroyed = false;
                m_Inactive = true;

                if (AlternateAttachTarget) { Destroy(AlternateAttachTarget); }
                Destroy(m_BalloonString);
                if (spriteAnimator) {
                    m_BalloonPopSoundNames = m_BalloonPopSoundNames.Shuffle();
                    AkSoundEngine.PostEvent(BraveUtility.RandomElement(m_BalloonPopSoundNames), gameObject);
                    if (DoBlankOnPop && BlankOwner) { HandleBalloonPopBlank(BlankOwner); }
                    spriteAnimator.PlayAndDestroyObject(PopAnimationName);
                } else {
                    Destroy(gameObject);
                }
                return;
            }

            float stringLength = 2;

            if (!IsUsingGameObjectTarget && DoDetachAndFloatAfterTargetDeath && AttachTarget && AttachTarget is AIActor && (!AttachTarget || AttachTarget.healthHaver.IsDead)) {
                if (DestroyOnDeath) {
                    m_Inactive = true;
                    if (AlternateAttachTarget) { Destroy(AlternateAttachTarget); }
                    Destroy(m_BalloonString);
                    Destroy(gameObject);
                    return;
                }
                if (!m_IsDetachedFromOriginalTarget) {
                    m_IsDetachedFromOriginalTarget = true;
                    AttachTarget = null;

                    if (AlternateAttachTarget) { Destroy(AlternateAttachTarget); }

                    if (!AlternateAttachTarget) {
                        AlternateAttachTarget = new GameObject() { name = ("Balloon Temp Object - " + Guid.NewGuid().ToString()) };
                        AlternateAttachTarget.transform.position = (transform.position - new Vector3(0, stringLength) + new Vector3((BalloonSprite.GetBounds().size.x / 2), 0,0));
                        IsUsingGameObjectTarget = true;
                    }

                    StartCoroutine(HandleFloatAndPop(AlternateAttachTarget));
                }
                return;
            }
            
            if (m_IsDetachedFromOriginalTarget) {
                stringLength = 3;
                m_currentOffset = new Vector2(0, stringLength);
                m_stringBalloonAnchorPoint = (AlternateAttachTarget.transform.position + m_currentOffset.ToVector3ZisY(BalloonStringZHeight));
                m_stringTargetAnchorPoint = AlternateAttachTarget.transform.position + new Vector3((BalloonSprite.GetBounds().size.x / 2), 0, 0);
            } else {
                m_currentOffset = new Vector2(Mathf.Lerp(BalloonFloatHorizontalOffset - 0.5f, BalloonFloatHorizontalOffset, Mathf.PingPong(Time.realtimeSinceStartup, 3f) / 3f), Mathf.Lerp((BalloonFloatHeight - 1 + 0.33f), BalloonFloatHeight, Mathf.PingPong(Time.realtimeSinceStartup / 1.75f, 3f) / 3f));
            }

            if (IsUsingGameObjectTarget && !m_IsDetachedFromOriginalTarget) {
                if (AltTargetSprite) {
                    m_stringBalloonAnchorPoint = (AltTargetSprite.WorldCenter.ToVector3ZUp() + m_currentOffset.ToVector3ZisY(BalloonStringZHeight));
                    m_stringTargetAnchorPoint = AltTargetSprite.WorldCenter.ToVector3ZUp();
                } else {
                    m_stringBalloonAnchorPoint = (AlternateAttachTarget.transform.position + m_currentOffset.ToVector3ZisY(BalloonStringZHeight));
                    m_stringTargetAnchorPoint = AlternateAttachTarget.transform.position;
                }
            } else if (AttachTarget && !m_IsDetachedFromOriginalTarget) {
                m_stringBalloonAnchorPoint = (AttachTarget.transform.position + m_currentOffset.ToVector3ZisY(BalloonStringZHeight));
                m_stringTargetAnchorPoint = AttachTarget.CenterPosition;
            }
            
            if (!m_IsDetachedFromOriginalTarget) {
                if (AttachTarget && AttachTarget is PlayerController) {
                    PlayerHandController primaryHand = (AttachTarget as PlayerController).primaryHand;
                    if (primaryHand.renderer.enabled) { m_stringTargetAnchorPoint = primaryHand.sprite.WorldCenter; }
                }

                stringLength = Vector3.Distance(transform.position, m_stringBalloonAnchorPoint.Value);
            }

            if (m_IsDetachedFromOriginalTarget && AlternateAttachTarget) {
                transform.position = AlternateAttachTarget.transform.position + new Vector3(0, m_currentOffset.y);
            } else {
                transform.position = Vector3.MoveTowards(transform.position, m_stringBalloonAnchorPoint.Value, BraveMathCollege.UnboundedLerp(1f, 10f, stringLength / 3f) * BraveTime.DeltaTime);
            }

            if (!m_stringTargetAnchorPoint.HasValue) { return; }

            BuildMeshAlongCurve(m_stringTargetAnchorPoint.Value, m_stringTargetAnchorPoint.Value, BalloonSprite.WorldBottomCenter - new Vector2(0, 2), BalloonSprite.WorldBottomCenter);
            m_mesh.vertices = m_vertices;
            m_mesh.RecalculateBounds();
            m_mesh.RecalculateNormals();
            
            if (BalloonRigidBody) {
                BalloonRigidBody.UpdateColliderPositions();
                BalloonRigidBody.Reinitialize();
            }

            if (!IsUsingGameObjectTarget && !DoDetachAndFloatAfterTargetDeath && DestroyOnDeath && (!AttachTarget || AttachTarget.healthHaver.IsDead)) {
                Destroy(m_BalloonString);
                Destroy(gameObject);
                return;
            }
        }

        
        // This appears to create the "string" that attaches the balloon to the target object. Note the balloon it self is an otherwise normal sprite.
        private void BuildMeshAlongCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float meshWidth = 0.03125f) {
            Vector3[] vertices = m_vertices;
            Vector2? vector = null;
            for (int i = 0; i < 10; i++) {
                Vector2 vector2 = BraveMathCollege.CalculateBezierPoint(i / 9f, p0, p1, p2, p3);
                Vector2? vector3 = (i != 9) ? new Vector2?(BraveMathCollege.CalculateBezierPoint(i / 9f, p0, p1, p2, p3)) : null;
                Vector2 a = Vector2.zero;
                if (vector != null) {
                    a += (Quaternion.Euler(0f, 0f, 90f) * (vector2 - vector.Value)).XY().normalized;
                }
                if (vector3 != null) {
                    a += (Quaternion.Euler(0f, 0f, 90f) * (vector3.Value - vector2)).XY().normalized;
                }
                a = a.normalized;
                vertices[i * 2] = (vector2 + a * meshWidth).ToVector3ZisY(0f);
                vertices[i * 2 + 1] = (vector2 + -a * meshWidth).ToVector3ZisY(0f);
                vector = new Vector2?(vector2);
            }
        }
        

        protected void HandleBalloonPopBlank(PlayerController arg1) {
            if (PopsOnProjectileHit) {
                float additionalTimeAtMaxRadius = 0.25f;
                Vector2? WorldCenter = BalloonSprite.WorldCenter;
                if (!WorldCenter.HasValue && BalloonRigidBody) {
                    WorldCenter = BalloonRigidBody.UnitCenter;
                } else if (!WorldCenter.HasValue && BalloonRigidBody) {
                    return;
                }
                if (!BlankVFXPrefab) { BlankVFXPrefab = BraveResources.Load<GameObject>("Global VFX/BlankVFX_Ghost", ".prefab"); }
                AkSoundEngine.PostEvent("Play_OBJ_silenceblank_small_01", gameObject);
                GameObject silencerObject = new GameObject("silencer");
                silencerObject.transform.position = gameObject.transform.position;
                SilencerInstance silencerInstance = silencerObject.AddComponent<SilencerInstance>();
                silencerInstance.TriggerSilencer(WorldCenter.Value, 20f, 6, BlankVFXPrefab, 0f, 3f, 3f, 3f, 30f, 3f, additionalTimeAtMaxRadius, arg1, true, false);
            } else {
                arg1.ForceBlank(overrideCenter: new Vector2?(BalloonSprite.WorldCenter));
            }
        }

        protected override void OnDestroy() {
            if (BalloonRigidBody) {
                BalloonRigidBody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Remove(BalloonRigidBody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(OnPreRigidBodyCollision));
            }
            if (m_stringFilter) { Destroy(m_stringFilter.gameObject); }
            base.OnDestroy();
        }
    }
}
