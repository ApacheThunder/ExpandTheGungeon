using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandSpriteMirror : BraveBehaviour {

        public ExpandSpriteMirror() {
            IsLocalMirror = true;
            SpriteHasAnimator = true;
            OutlineThatSprite = false;
            DestroyOnHide = true;
            DestroyOnTimer = true;
            DestroyTimout = 2;
            SpriteOutlineColor = Color.black;
            
            SpriteNamesToHideOn = new string[] {
                "hammer_up_right_015",
                "hammer_left_right_015"
            };

            SpriteNamesToShowOn = new string[] {
                "hammer_slam_right_001",
                "hammer_slam_right_002",
                "hammer_slam_right_003",
                "hammer_slam_right_004",
                "hammer_slam_right_005",
                "hammer_slam_right_006",
                "hammer_slam_right_007",
                "hammer_up_left_001",
                "hammer_up_left_002",
                "hammer_up_left_003",
                "hammer_up_left_004",
                "hammer_up_left_005",
                "hammer_up_left_006",
                "hammer_up_left_007",
                "hammer_up_left_008",
                "hammer_up_left_009",
                "hammer_up_left_010",
                "hammer_up_left_011",
                "hammer_up_left_012",
                "hammer_up_left_013",
                "hammer_up_left_014",
                "hammer_up_right_001",
                "hammer_up_right_002",
                "hammer_up_right_003",
                "hammer_up_right_004",
                "hammer_up_right_005",
                "hammer_up_right_006",
                "hammer_up_right_007",
                "hammer_up_right_008",
                "hammer_up_right_009",
                "hammer_up_right_010",
                "hammer_up_right_011",
                "hammer_up_right_012",
                "hammer_up_right_013",
                "hammer_up_right_014",
                "hammer_slam_left_001",
                "hammer_slam_left_002",
                "hammer_slam_left_003",
                "hammer_slam_left_004",
                "hammer_slam_left_005",
                "hammer_slam_left_006",
                "hammer_slam_left_007"
            };

            IsActive = false;
        }

        public bool OutlineThatSprite;
        public bool IsLocalMirror;
        public bool SpriteHasAnimator;
        public bool IsActive;
        public bool DestroyOnHide;
        public bool DestroyOnTimer;
        public float DestroyTimout;
        public Color SpriteOutlineColor;
        public tk2dSprite SpriteToMirror;

        public string[] SpriteNamesToHideOn;
        public string[] SpriteNamesToShowOn;

        private tk2dSprite m_CachedMirrorSprite;
        private Transform m_CachedParent;
        private SpeculativeRigidbody RigidBodyToWatch;
        private bool m_InitComplete;
        private float m_Timer;

        private bool InitComplete {
            get { return m_InitComplete; }
            set {
                IsActive = value;
                m_InitComplete = value;
            }
        }
        
        public void AttachMirror(Transform ParentTransform, tk2dSpriteCollectionData spriteCollectionOverride = null, Vector3? InitialOffset = null, bool alreadyMiddleCenter = false, bool useHitbox = false, bool isLocalMirror = true) {
            if (m_InitComplete) return;

            if (DestroyOnTimer) m_Timer = DestroyTimout;

            IsLocalMirror = isLocalMirror;

            m_CachedParent = ParentTransform;
            Vector3 offset = Vector3.zero;
            if (InitialOffset.HasValue) offset = InitialOffset.Value;

            SpriteToMirror = ParentTransform.gameObject.GetComponent<tk2dSprite>();

            m_CachedMirrorSprite = gameObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_CachedMirrorSprite, SpriteToMirror);
            if (spriteCollectionOverride)m_CachedMirrorSprite.Collection = spriteCollectionOverride;            

            if (OutlineThatSprite)SpriteOutlineManager.AddOutlineToSprite(m_CachedMirrorSprite, SpriteOutlineColor);

            RigidBodyToWatch = m_CachedParent.gameObject.GetComponent<SpeculativeRigidbody>();

            Vector3 a = (!useHitbox || !RigidBodyToWatch || RigidBodyToWatch.HitboxPixelCollider == null) ? SpriteToMirror.WorldCenter.ToVector3ZUp(0f) : RigidBodyToWatch.HitboxPixelCollider.UnitCenter.ToVector3ZUp(0f);
            if (!alreadyMiddleCenter) {
                m_CachedMirrorSprite.PlaceAtPositionByAnchor(a + offset, tk2dBaseSprite.Anchor.MiddleCenter);
            } else {
                m_CachedMirrorSprite.transform.position = a + offset;
            }
            
            if (IsLocalMirror && m_CachedParent && m_CachedParent != transform) transform.parent = m_CachedParent;

            if (IsLocalMirror)SpriteToMirror.AttachRenderer(m_CachedMirrorSprite);

            if (IsLocalMirror && !alreadyMiddleCenter) {
                transform.localPosition = transform.localPosition.QuantizeFloor(0.0625f);
            } else if (!alreadyMiddleCenter) {
                transform.position = transform.position.QuantizeFloor(0.0625f);
            }
            m_CachedMirrorSprite.IsPerpendicular = SpriteToMirror.IsPerpendicular;
            InitComplete = true;
            ShowMirror();
        }

        public void ShowMirror() {
            m_CachedMirrorSprite.renderer.enabled = true;
            SpriteToMirror.renderer.enabled = false;
            if (OutlineThatSprite) {
                SpriteOutlineManager.AddOutlineToSprite(m_CachedMirrorSprite, SpriteOutlineColor);
                SpriteOutlineManager.RemoveOutlineFromSprite(SpriteToMirror);
                SpriteToMirror.UpdateZDepth();
            }
            m_CachedMirrorSprite.UpdateZDepth();
            IsActive = true;
        }

        public void HideMirror() {
            IsActive = false;
            m_CachedMirrorSprite.renderer.enabled = false;
            if (OutlineThatSprite) {
                SpriteOutlineManager.RemoveOutlineFromSprite(m_CachedMirrorSprite);
                SpriteOutlineManager.AddOutlineToSprite(SpriteToMirror, SpriteOutlineColor);
                SpriteToMirror.UpdateZDepth();
            }
            if (DestroyOnHide)DestroyMirror(DestroyOnHide);
        }

        public void DestroyMirror(bool alreadyHidden) {
            if (alreadyHidden) {
                Destroy(m_CachedMirrorSprite.gameObject);
                return;
            }
            if (OutlineThatSprite) {
                SpriteOutlineManager.RemoveOutlineFromSprite(m_CachedMirrorSprite);
                SpriteOutlineManager.AddOutlineToSprite(SpriteToMirror, SpriteOutlineColor);
                SpriteToMirror.UpdateZDepth();
            }
            if (IsLocalMirror)SpriteToMirror.DetachRenderer(m_CachedMirrorSprite);
            Destroy(m_CachedMirrorSprite.gameObject);
        }

        public void Update() {
            if (IsActive) {
                if (SpriteHasAnimator) {
                    if (m_CachedMirrorSprite.GetCurrentSpriteDef() != SpriteToMirror.GetCurrentSpriteDef()) {
                        m_CachedMirrorSprite.SetSprite(SpriteToMirror.GetCurrentSpriteDef().name);
                        if (SpriteNamesToHideOn != null && SpriteNamesToHideOn.Length > 0) {
                            foreach (string name in SpriteNamesToHideOn) {
                                if (SpriteToMirror.GetCurrentSpriteDef().name.StartsWith(name)) {
                                    if (DestroyOnHide) {
                                        HideMirror();
                                    } else {
                                        m_CachedMirrorSprite.renderer.enabled = false;
                                    }
                                    return;
                                }
                            }
                        }
                        if (SpriteNamesToShowOn != null && SpriteNamesToShowOn.Length > 0) {
                            foreach (string name in SpriteNamesToShowOn) {
                                if (SpriteToMirror.GetCurrentSpriteDef().name.StartsWith(name)) {
                                    m_CachedMirrorSprite.renderer.enabled = true;
                                    return;
                                }
                            }
                        }
                    }
                }
                if (m_CachedMirrorSprite.HeightOffGround != SpriteToMirror.HeightOffGround) {
                    m_CachedMirrorSprite.HeightOffGround = SpriteToMirror.HeightOffGround;
                    m_CachedMirrorSprite.UpdateZDepth();
                }
            }
        }

        public void LateUpdate() {
            if (!IsActive) return;
            if (!IsLocalMirror) m_CachedMirrorSprite.gameObject.transform.position = m_CachedParent.position;
            if (DestroyOnTimer) {
                m_Timer -= BraveTime.DeltaTime;
                if (m_Timer <= 0) {
                    IsActive = false;
                    Destroy(m_CachedMirrorSprite.gameObject);
                    return;
                }
            }
        }
    }
}

