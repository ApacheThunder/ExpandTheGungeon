using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandSpriteBobber : BraveBehaviour {

        public ExpandSpriteBobber() {
            IsLocalBob = true;
            IsBobbin = false;
            BobOnMovement = true;
            OutlineThatBob = true;
            BobOutlineColor = Color.black;
            BobUpIntensity = 0.08f;
            BobDownIntensity = 0.08f;
            BobDirection = new Vector3(0, 0.025f, 0);
            bobType = BobType.Standard;
            ImpactVFXFootprint = new Vector2(2f, 0.2f);
            ImpactVFXOffset = new Vector2(0.4f, 0.1f);

            SlamScreenShakeSettings = new ScreenShakeSettings() {
                magnitude = 0.6f,
                speed = 10f,
                time = 0.15f,
                falloff = 0.5f,
                direction = new Vector2(0, -1),
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium
            };

            ImpactSoundFX = new List<string>() { "Play_EX_ChestSlam_01", "Play_EX_ChestSlam_02" };
            
            BobberHasAnimator = true;
            m_BobInitComplete = false;
        }

        // public bool DoRotation;
        public bool OutlineThatBob;
        public bool IsLocalBob;
        public bool IsBobbin;
        public bool BobOnMovement;
        public bool BobberHasAnimator;
        
        public float BobSpeed;
        public float BobUpIntensity;
        public float BobDownIntensity;

        public GameObject[] ImpactVFXObjects;
        List<string> ImpactSoundFX;
        public Vector2 ImpactVFXFootprint;
        public Vector2 ImpactVFXOffset;

        public Color BobOutlineColor;

        public Vector3 BobDirection;

        public enum BobType { Standard, TinyBoi, BigChonker };
        public BobType bobType;

        public ScreenShakeSettings SlamScreenShakeSettings;

        public SpeculativeRigidbody RigidBodyToWatch;
        public tk2dSprite SpriteToBob;
                
        private bool m_BobInitComplete;
        private Vector2 m_CachedPosition;
        private Transform m_CachedParent;
        private tk2dSprite m_CachedBobSprite;
        

        public void AttachBobber(Transform ParentTransform, Vector3? InitialOffset = null, bool alreadyMiddleCenter = false, bool useHitbox = false) {
            if (m_BobInitComplete) return;

            if (ImpactVFXObjects == null) {
                ImpactVFXObjects = new GameObject[] {
                    ExpandAssets.LoadOfficialAsset<GameObject>("VFX_Dust_Ring_Generic_001", ExpandAssets.AssetSource.SharedAuto1),
                    // ExpandAssets.LoadOfficialAsset<GameObject>("VFX_Bow_Dust", ExpandAssets.AssetSource.SharedAuto1),
                    // ExpandAssets.LoadOfficialAsset<GameObject>("VFX_SecretDoorPoof_Vertical", ExpandAssets.AssetSource.SharedAuto2)
                };
            }

            switch (bobType) {
                case BobType.Standard:
                    BobUpIntensity = 0.08f;
                    BobDownIntensity = 0.08f;
                    BobDirection = new Vector3(0, 0.025f, 0);
                    ImpactVFXFootprint = new Vector2(2f, 0.2f);
                    break;
                case BobType.TinyBoi:
                    BobUpIntensity = 0.065f;
                    BobDownIntensity = 0.065f;
                    BobDirection = new Vector3(0, 0.022f, 0);
                    ImpactVFXFootprint = new Vector2(1, 1);
                    ImpactSoundFX = new List<string>() { "Play_WPN_woodbeam_impact_01", "Play_WPN_woodbeam_impact_02" };
                    break;
                case BobType.BigChonker:
                    BobUpIntensity = 0.15f;
                    BobDownIntensity = 0.15f;
                    BobDirection = new Vector3(0, 0.015f, 0);
                    ImpactVFXFootprint = new Vector2(4, 4);
                    ImpactSoundFX = new List<string>() { "Play_EX_ChestSlam_03", "Play_EX_ChestSlam_04" };
                    break;
            }

            m_CachedParent = ParentTransform;
            m_CachedPosition = transform.position;
            Vector3 offset = Vector3.zero;
            if (InitialOffset.HasValue) offset = InitialOffset.Value;

            SpriteToBob = ParentTransform.gameObject.GetComponent<tk2dSprite>();

            m_CachedBobSprite = gameObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_CachedBobSprite, SpriteToBob);

            if (OutlineThatBob)SpriteOutlineManager.AddOutlineToSprite(m_CachedBobSprite, BobOutlineColor);

            RigidBodyToWatch = m_CachedParent.gameObject.GetComponent<SpeculativeRigidbody>();

            Vector3 a = (!useHitbox || !RigidBodyToWatch || RigidBodyToWatch.HitboxPixelCollider == null) ? SpriteToBob.WorldCenter.ToVector3ZUp(0f) : RigidBodyToWatch.HitboxPixelCollider.UnitCenter.ToVector3ZUp(0f);
            if (!alreadyMiddleCenter) {
                m_CachedBobSprite.PlaceAtPositionByAnchor(a + offset, tk2dBaseSprite.Anchor.MiddleCenter);
            } else {
                m_CachedBobSprite.transform.position = a + offset;
            }
            
            if (m_CachedParent && m_CachedParent != transform) transform.parent = m_CachedParent;

            SpriteToBob.AttachRenderer(m_CachedBobSprite);
            
            if (!alreadyMiddleCenter) transform.localPosition = transform.localPosition.QuantizeFloor(0.0625f);
            
            if (IsLocalBob) m_CachedPosition = transform.localPosition;
                                    
            m_BobInitComplete = true;
            IsBobbin = true;
            ShowBobber();
        }

        public void ShowBobber() {
            if (!m_BobInitComplete) return;
            m_CachedBobSprite.renderer.enabled = true;
            SpriteToBob.renderer.enabled = false;
            IsBobbin = true;
            m_CachedBobSprite.UpdateZDepth();
            if (OutlineThatBob) {
                SpriteOutlineManager.AddOutlineToSprite(m_CachedBobSprite, BobOutlineColor);
                SpriteOutlineManager.RemoveOutlineFromSprite(SpriteToBob);
                SpriteToBob.UpdateZDepth();
            }
            StartCoroutine(HandleSpriteBob());
        }

        public void HideBobber() {
            if (!m_BobInitComplete) return;
            IsBobbin = false;
            StopCoroutine(HandleSpriteBob());
            if (IsLocalBob) {
                transform.localPosition = m_CachedPosition;
            } else {
                transform.position = m_CachedPosition;
            }
            m_CachedBobSprite.renderer.enabled = false;
            SpriteToBob.renderer.enabled = true;
            if (OutlineThatBob) {
                SpriteOutlineManager.RemoveOutlineFromSprite(m_CachedBobSprite);
                SpriteOutlineManager.AddOutlineToSprite(SpriteToBob, BobOutlineColor);
                SpriteToBob.UpdateZDepth();
            }
        }

        public void DestroyBobber() {
            StopCoroutine(HandleSpriteBob());
            if (OutlineThatBob) {
                SpriteOutlineManager.RemoveOutlineFromSprite(m_CachedBobSprite);
                SpriteOutlineManager.AddOutlineToSprite(SpriteToBob, BobOutlineColor);
                SpriteToBob.UpdateZDepth();
            }
            SpriteToBob.DetachRenderer(m_CachedBobSprite);
            SpriteToBob.renderer.enabled = true;
            Destroy(gameObject);
        }

        private IEnumerator HandleSpriteBob() {
            float elapsed = 0;
            while (IsBobbin) {
                if (BobOnMovement && RigidBodyToWatch?.Velocity == Vector2.zero) {
                    if (IsLocalBob) {
                        transform.localPosition = m_CachedPosition;
                    } else {
                        transform.position = m_CachedPosition;
                    }
                }
                while (BobOnMovement && RigidBodyToWatch?.Velocity == Vector2.zero) yield return null;
                elapsed = 0;
                while (elapsed < BobUpIntensity) {
                    elapsed += BraveTime.DeltaTime;
                    if (IsLocalBob) {
                        transform.localPosition += BobDirection;
                    } else {
                        transform.position += BobDirection;
                    }
                    if (BobOnMovement && RigidBodyToWatch?.Velocity == Vector2.zero)elapsed += BobUpIntensity;
                    yield return null;
                }
                elapsed = 0;
                while (elapsed < BobDownIntensity) {
                    elapsed += BraveTime.DeltaTime;
                    if (IsLocalBob) {
                        transform.localPosition -= BobDirection;
                    } else {
                        transform.position -= BobDirection;
                    }
                    if (BobOnMovement && RigidBodyToWatch?.Velocity == Vector2.zero)elapsed += BobDownIntensity;
                    yield return null;
                }
                HandleImpactVFX();
                yield return null;
            }
            yield return null;
            if (IsLocalBob) {
                transform.localPosition = m_CachedPosition;
            } else {
                transform.position = m_CachedPosition;
            }
            yield break;
        }

        private void HandleImpactVFX() {
            // AkSoundEngine.PostEvent("Stop_WPN_All", gameObject);
            // Make sure sprite is grounded.
            if (IsLocalBob) {
                transform.localPosition = m_CachedPosition;
            } else {
                transform.position = m_CachedPosition;
            }
            if (bobType == BobType.BigChonker)GameManager.Instance.MainCameraController.DoScreenShake(SlamScreenShakeSettings, (transform.position + new Vector3(0, 2f)), false);

            switch (bobType) {
                case BobType.BigChonker:
                    break;
                case BobType.TinyBoi:
                    if (ImpactVFXObjects != null && ImpactVFXObjects.Length > 0) {
                        int SpawnCount = Random.Range(2, 4);
                        if (Random.value > 0.4f) {
                            for (int i = 0; i < SpawnCount; i++) {
                                Vector2 m_ChosenSpawnPoint = transform.position;
                                m_ChosenSpawnPoint += new Vector2(Random.Range(ImpactVFXOffset.x, ImpactVFXFootprint.x), Random.Range(ImpactVFXOffset.y, ImpactVFXFootprint.y));
                                Instantiate(BraveUtility.RandomElement(ImpactVFXObjects), m_ChosenSpawnPoint, Quaternion.identity);
                            }
                        }
                    }
                    break;
                case BobType.Standard:
                    GameObject impactVFX = ExpandUtility.AttachEffect(ExpandObjectDatabase.VFXLeadMaidenMove, gameObject, new Vector3(0, -0.5f), true, false, false, true, 1.2f);
                    impactVFX.SetLayerRecursively(20);
                    break;
            }
            
            ImpactSoundFX = ImpactSoundFX.Shuffle();
            AkSoundEngine.PostEvent(BraveUtility.RandomElement(ImpactSoundFX), gameObject);
        }

        public void Update() {
            if (m_BobInitComplete && BobberHasAnimator && IsBobbin) {
                if (m_CachedBobSprite.GetCurrentSpriteDef() != SpriteToBob.GetCurrentSpriteDef()) {
                    m_CachedBobSprite.SetSprite(SpriteToBob.GetCurrentSpriteDef().name);
                }
            }
        }
        

        protected override void OnDestroy() {
            StopCoroutine(HandleSpriteBob());
            base.OnDestroy();
        }
    }
}

