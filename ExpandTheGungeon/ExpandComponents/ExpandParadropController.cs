using UnityEngine;
using System.Collections;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandParadropController : BraveBehaviour {

        public ExpandParadropController() {
            // This component is meant added to an active instance so Update doesn't do anything until external code sets this to true.
            // This is so any of it's properties can be changed before Update runs. ;)
            Configured = false; 
            StartsIntheAir = false;
            ParentObjectExplodyBarrel = false;
            UseLandingVFX = false;
            UseObjectSizeOverride = false;
            IsItemCrate = false;

            LandingPositionOffset = -1;
            DropHeightHorizontalOffset = 0;
            StartHeight = 10;
            PopupSpeed = 0.5f;
            PopUpHeight = 6;
            DropSpeed = 3;
            MaxSwayAngle = 15f;
            SwaySpeed = 2;
            OverrideObjectSize = Vector2.one;
            ItemDropID = 78;

            m_ParadropStarted = false;
            m_Initialized = false;
            switchState = State.Wait;
            m_startScale = 0.25f;
        }

        public bool Configured;
        public bool StartsIntheAir;
        public bool ParentObjectExplodyBarrel;
        public bool UseLandingVFX;
        public bool UseObjectSizeOverride;
        public float LandingPositionOffset;
        public float DropHeightHorizontalOffset;
        public float StartHeight;
        public float PopupSpeed;
        public float PopUpHeight;
        public float DropSpeed;
        public float MaxSwayAngle;
        public float SwaySpeed;
        public Vector2 OverrideObjectSize;
        public int ItemDropID;
        public bool IsItemCrate;

        private bool m_ParadropStarted;
        private bool m_Initialized;
        private enum State { Wait, PopIntoAir, ParaDrop, End };
        private State switchState;
        private float m_CachedSpriteZDepth;
        private AIActor m_ParentEnemy;
        private Vector2 m_cachedScale;
        private Vector2 m_cachedPosition;
        private Vector2 m_CachedPositionOffset;
        private float m_startScale;
        private Quaternion m_CachedRotation;
        private int m_CachedLayer;
        

        private float m_maxSway;
        
        private GameObject m_Parachute;
        private tk2dSprite m_ParachuteSprite;
        private tk2dSpriteAnimator m_ParachuteSpriteAnimator;

        private GameObject m_LandingVFX;
        private GameObject m_ItemBoxAnchor;

        private Transform m_Anchor;
        
        private ExplosionData m_CachedExplosionData;
        
        public void SetObjectScale(Vector2 scale) {
            transform.localScale = scale.ToVector3ZUp(1f);
            if (specRigidbody) {
                specRigidbody.UpdateCollidersOnScale = true;
                specRigidbody.RegenerateColliders = true;
            }
        }

        private void Start() {
            // Delay initial configuration for ItemCrate version to avoid possible misalignment since GameObjects get instantiated initially from zero position.
            // Item crate object comes with this controller pre-attached. So position may not be correct frame 1. ;)
            if (!Configured && IsItemCrate) {
                StartCoroutine(DelayedAutoConfiguration());
                return;
            }
            m_ParentEnemy = aiActor;
            m_CachedRotation = transform.rotation;
            m_cachedPosition = transform.position;
            m_CachedLayer = gameObject.layer;
            m_CachedSpriteZDepth = sprite.HeightOffGround;
            m_maxSway = MaxSwayAngle;

            if (knockbackDoer) { knockbackDoer.SetImmobile(true, "Paradrop Protection"); }

            if (m_ParentEnemy) { m_cachedScale = m_ParentEnemy.EnemyScale; } else { m_cachedScale = Vector2.one; }

            if (m_ParentEnemy && m_ParentEnemy.HasShadow && m_ParentEnemy.ShadowObject) {
                m_ParentEnemy.ShadowObject.GetComponent<tk2dSprite>().renderer.enabled = false;
            }
            
            if (m_ParentEnemy) {
                m_ParentEnemy.behaviorSpeculator.enabled = false;
                if (m_ParentEnemy.aiAnimator) { m_ParentEnemy.aiAnimator.FacingDirection = -90; }
                if (m_ParentEnemy.aiShooter) {
                    m_ParentEnemy.aiShooter.AimAtPoint(m_ParentEnemy.CenterPosition - new Vector2(0, 2));
                    m_ParentEnemy.aiShooter.ToggleGunAndHandRenderers(false, "ParaDrop");
                }
            }

            if (specRigidbody && !IsItemCrate) { specRigidbody.CollideWithOthers = false; }
            if (healthHaver) {
                healthHaver.PreventAllDamage = true;
                healthHaver.IsVulnerable = false;
            }

            if (ParentObjectExplodyBarrel && !IsItemCrate) { m_CachedExplosionData = ExpandUtility.GenerateExplosionData(); }

            // MeshRenderer pre-disabled via added components in Unity Editor for The Item Drop host object.
            if (!IsItemCrate) { sprite.renderer.enabled = false; }

            m_Parachute = Instantiate(ExpandPrefabs.EX_Parachute, Vector3.zero, Quaternion.identity);
            m_ParachuteSpriteAnimator = m_Parachute.GetComponent<tk2dSpriteAnimator>();
            m_ParachuteSprite = m_Parachute.GetComponent<tk2dSprite>();
            m_ParachuteSprite.renderer.enabled = false;
            if (IsItemCrate) {
                m_ParachuteSprite.HeightOffGround += 3f;
                m_ParachuteSprite.UpdateZDepth();
            }


            // Despite my absolute shit math skills I somehow got this right. :P
            // (this correctly sets the parachute above the host object's sprite)
            float ParachuteXSize = (m_ParachuteSprite.GetBounds().size.x / 2);
            float ObjectUnitCenterX = m_cachedPosition.x;
            float ObjectUnitCenterY = m_cachedPosition.y;

            if (UseObjectSizeOverride) {
                ObjectUnitCenterX += OverrideObjectSize.x;
                ObjectUnitCenterY += OverrideObjectSize.y;
            } else if (specRigidbody) {
                specRigidbody.Reinitialize(); // Make sure rigidbody colliders are at current object's position
                ObjectUnitCenterX = specRigidbody.GetPixelCollider(ColliderType.HitBox).UnitCenter.x;
                ObjectUnitCenterY = specRigidbody.GetPixelCollider(ColliderType.HitBox).UnitTopCenter.y;
            }

            m_Parachute.transform.position = new Vector2((ObjectUnitCenterX - ParachuteXSize), ObjectUnitCenterY);
            
            m_Parachute.layer = gameObject.layer;
            
            if (IsItemCrate) {
                GameObject EXItemBoxAnchor = new GameObject("EX ItemBox Anchor") { layer = gameObject.layer };
                EXItemBoxAnchor.transform.position = m_ParachuteSprite.WorldBottomCenter;
                m_ItemBoxAnchor = EXItemBoxAnchor;
                EXItemBoxAnchor.transform.SetParent(m_Parachute.transform);
                gameObject.transform.SetParent(EXItemBoxAnchor.transform);
            } else {
                gameObject.transform.SetParent(m_Parachute.transform);
            }

            // m_Anchor = Instantiate(ExpandPrefabs.EX_ParadropAnchor, m_ParachuteSprite.WorldBottomCenter, Quaternion.identity).transform;
            m_Anchor = new GameObject("EXParaDropAnchor") { layer = gameObject.layer }.transform;
            m_Anchor.transform.position = m_ParachuteSprite.WorldBottomCenter;
            m_Parachute.transform.SetParent(m_Anchor);
            
            m_CachedPositionOffset = (m_Anchor.position - m_cachedPosition.ToVector3ZUp());
            
            switchState = State.PopIntoAir;
            m_Initialized = true;
        }

        private IEnumerator DelayedAutoConfiguration() {
            Configured = true;
            yield return new WaitForSeconds(0.01f);
            Start(); // Call Start() again but this time Configured is set first so it will finish Start() as usual.
            yield break;
        }
        
        private IEnumerator SpawnItem(int itemID) {
            GameObject ItemToSpawn = PickupObjectDatabase.GetById(itemID).gameObject;
            Vector3? SpawnPosition = null;
            Vector3 ItemSpawnOffset = new Vector3(-0.5f, 0.5f, 0);
            Vector3 GunSpawnOffset = new Vector3(0, 0.5f, 0);
            if (specRigidbody) {
                SpawnPosition = specRigidbody.GetPixelCollider(ColliderType.Any).UnitCenter;
            } else {
                SpawnPosition = sprite.WorldCenter.ToVector3ZUp();
            }
            yield return null;
            if (ItemToSpawn.GetComponent<Gun>()) {
                GameObject newGun = Instantiate(ItemToSpawn);
                newGun.GetComponent<tk2dBaseSprite>().PlaceAtPositionByAnchor((SpawnPosition.Value + GunSpawnOffset), tk2dBaseSprite.Anchor.MiddleCenter);
                Gun gunComponent = newGun.GetComponent<Gun>();
                gunComponent.Initialize(null);
                gunComponent.DropGun(0.5f);
            } else {
                LootEngine.SpawnItem(ItemToSpawn, (SpawnPosition.Value + ItemSpawnOffset), Vector2.zero, 0f, false, false, false);
            }
            spriteAnimator.PlayAndDestroyObject("bustopen");
            yield break;
        }
        
        private IEnumerator HandlePopup() {
            float elapsed = 0f;
            Vector2 startOffset = Vector2.zero;
            if (specRigidbody) {
                startOffset = (m_cachedPosition + m_CachedPositionOffset + new Vector2(m_ParachuteSprite.GetBounds().size.x / 2, 0));
            } else if (UseObjectSizeOverride) {
                startOffset = (m_cachedPosition + m_CachedPositionOffset + new Vector2(OverrideObjectSize.x, 0));
            } else {
                startOffset = (m_cachedPosition + m_CachedPositionOffset + new Vector2(sprite.GetBounds().size.x / 2, 0));
            }
            Vector2 HeighestPointPosition = (startOffset + new Vector2(DropHeightHorizontalOffset, PopUpHeight));
            int cachedLayer = gameObject.layer;
            int cachedOutlineLayer = cachedLayer;
            while (elapsed < PopupSpeed) {
                elapsed += BraveTime.DeltaTime;
                if (!StartsIntheAir) { m_Anchor.position = Vector2.Lerp(startOffset, HeighestPointPosition, (elapsed / PopupSpeed)); }
                SetObjectScale(Vector2.Lerp(new Vector2(m_startScale, m_startScale), m_cachedScale, (elapsed / PopupSpeed)));
                yield return null;
            }
            gameObject.layer = cachedLayer;
            SpriteOutlineManager.ChangeOutlineLayer(sprite, cachedOutlineLayer);
            switchState = State.ParaDrop;
            yield break;
        }

        private IEnumerator HandleDrop() {
            float elapsed = 0f;
            Vector2 startPosition = m_Anchor.position;
            Vector2 landingPosition = (m_cachedPosition + new Vector2(m_CachedPositionOffset.x, m_CachedPositionOffset.y + LandingPositionOffset));
            m_ParachuteSpriteAnimator.Play("ParachuteDeploy");
            yield return null;
            while (m_ParachuteSpriteAnimator.IsPlaying("ParachuteDeploy")) { yield return null; }
            while (elapsed < DropSpeed) {
                elapsed += BraveTime.DeltaTime;
                m_Anchor.position = Vector2.Lerp(startPosition, landingPosition, (elapsed / DropSpeed));
                yield return null;
            }
            if (m_LandingVFX) { SpawnManager.Despawn(m_LandingVFX); }
            switchState = State.End;
            yield break;
        }
        
        private void Update() {
            if (!Configured) { return; }
            if (m_Initialized && healthHaver && healthHaver.IsDead) {
                StopAllCoroutines();
                if (m_LandingVFX) { SpawnManager.Despawn(m_LandingVFX); }
                m_Anchor.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));
                m_Anchor.gameObject.SetLayerRecursively(m_CachedLayer);
                m_Parachute.transform.DetachChildren();
                m_Anchor.DetachChildren();
                sprite.DetachRenderer(m_ParachuteSprite);
                m_ParachuteSpriteAnimator.PlayAndDestroyObject("ParachuteLanded");
                Destroy(m_Anchor.gameObject);
                switchState = State.Wait;
            }
            switch (switchState) {
                case State.Wait:
                    return;
                case State.PopIntoAir:
                    sprite.renderer.enabled = true;                
                    if (UseLandingVFX) {
                        Vector3 m_VFXPosition = (m_cachedPosition + new Vector2(m_CachedPositionOffset.x, m_CachedPositionOffset.y + LandingPositionOffset));
                        m_LandingVFX = SpawnManager.SpawnVFX(ExpandAssets.LoadOfficialAsset<GameObject>("EmergencyCrate", ExpandAssets.AssetSource.BraveResources).GetComponent<EmergencyCrateController>().landingTargetSprite, m_VFXPosition, Quaternion.identity);
                        m_LandingVFX.transform.position -= new Vector3(0, m_LandingVFX.GetComponentInChildren<tk2dSprite>().GetBounds().size.y / 2);
                        m_LandingVFX.GetComponentInChildren<tk2dSprite>().UpdateZDepth();
                        UseLandingVFX = false;
                    }
                    m_Anchor.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));
                    if (StartsIntheAir) { m_Anchor.position += new Vector3(DropHeightHorizontalOffset, StartHeight); }
                    StartCoroutine(HandlePopup());
                    switchState = State.Wait;
                    return;
                case State.ParaDrop:
                    if (!m_ParadropStarted) {
                        m_ParadropStarted = true;
                        m_ParachuteSprite.SetSprite("EX_Parachute_Open_01"); 
                        m_ParachuteSpriteAnimator.Play("ParachuteDeploy");
                        m_ParachuteSprite.renderer.enabled = true;
                        StartCoroutine(HandleDrop());
                    }
                    Quaternion Clockwise = Quaternion.Euler(new Vector3(0, 0, m_maxSway));
                    Quaternion CounterClockwise = Quaternion.Euler(new Vector3(0, 0, -m_maxSway));
                    float Lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * SwaySpeed));
                    m_Anchor.localRotation = Quaternion.Lerp(Clockwise, CounterClockwise, Lerp);
                    if (IsItemCrate) {                        
                        Quaternion BoxClockwise = Quaternion.Euler(new Vector3(0, 0, -(m_maxSway / 1.75f)));
                        Quaternion BoxCounterClockwise = Quaternion.Euler(new Vector3(0, 0, (m_maxSway / 1.75f)));
                        m_ItemBoxAnchor.transform.localRotation = Quaternion.Lerp(BoxClockwise, BoxCounterClockwise, Lerp);
                    }
                    return;
                case State.End:
                    switchState = State.Wait; // Do this first encase somehow there's an exception. Will stay in wait state instead of endlessly causing exceptions. :P
                    if (knockbackDoer) { knockbackDoer.SetImmobile(false, "Paradrop Protection"); }
                    m_Anchor.gameObject.SetLayerRecursively(m_CachedLayer);
                    m_Parachute.transform.DetachChildren();
                    if (IsItemCrate) {
                        m_Parachute.SetLayerRecursively(LayerMask.NameToLayer("Unoccluded"));
                        m_ItemBoxAnchor.transform.DetachChildren();
                        Destroy(m_ItemBoxAnchor);
                    }
                    m_Anchor.DetachChildren();
                    sprite.DetachRenderer(m_ParachuteSprite);
                    m_ParachuteSpriteAnimator.PlayAndDestroyObject("ParachuteLanded");
                    transform.rotation = m_CachedRotation;
                    if (specRigidbody) {
                        // Item crate's collision only used to help with setting up correct offsets for parent objects.
                        // If it's item crate this setting will be left to it's starting value of false.
                        if (!IsItemCrate) { specRigidbody.CollideWithOthers = true; }
                        specRigidbody.UpdateColliderPositions();
                        specRigidbody.Reinitialize();
                    }
                    if (m_ParentEnemy) {
                        if (m_ParentEnemy.HasShadow && m_ParentEnemy.ShadowObject) {
                            m_ParentEnemy.ShadowObject.GetComponent<tk2dSprite>().renderer.enabled = true;
                        }
                        if (m_ParentEnemy.aiShooter) { m_ParentEnemy.aiShooter.ToggleGunAndHandRenderers(true, "ParaDrop"); }
                        m_ParentEnemy.behaviorSpeculator.enabled = true;
                        m_ParentEnemy.HasBeenEngaged = true;
                        m_ParentEnemy.behaviorSpeculator.PostAwakenDelay = 0;
                        m_ParentEnemy.behaviorSpeculator.RemoveDelayOnReinforce = true;
                    }
                    if (healthHaver) {
                        healthHaver.PreventAllDamage = false;
                        healthHaver.IsVulnerable = true;
                    }
                    Destroy(m_Anchor.gameObject);
                    if (ParentObjectExplodyBarrel && !IsItemCrate) {
                        spriteAnimator.PlayAndDestroyObject("explode");
                        if (m_CachedExplosionData != null) {
                            Exploder.Explode(sprite.WorldCenter, m_CachedExplosionData, Vector2.zero, null, true, CoreDamageTypes.None, false);
                        }
                    }
                    if (!ParentObjectExplodyBarrel && !IsItemCrate) { Destroy(this); }
                    if (IsItemCrate) {
                        sprite.renderer.sortingLayerName = "Background";
                        sprite.HeightOffGround = -1.5f;
                        sprite.UpdateZDepth();
                        StartCoroutine(SpawnItem(ItemDropID));
                    }
                    return;
            }
        }


        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

