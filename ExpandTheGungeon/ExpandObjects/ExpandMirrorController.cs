using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandMirrorController : DungeonPlaceableBehaviour, IPlayerInteractable, IPlaceConfigurable {

        public ExpandMirrorController() {            
            sprite = gameObject.GetComponent<MirrorController>().sprite;
            spriteAnimator = gameObject.GetComponent<MirrorController>().spriteAnimator;
            MirrorSprite = gameObject.GetComponent<MirrorController>().MirrorSprite;
            PlayerReflection = gameObject.GetComponent<MirrorController>().PlayerReflection;
            CoopPlayerReflection = gameObject.GetComponent<MirrorController>().CoopPlayerReflection;
            ChestReflection = gameObject.GetComponent<MirrorController>().ChestReflection;
            ShatterSystem = gameObject.GetComponent<MirrorController>().ShatterSystem;

            Destroy(gameObject.GetComponent<MirrorController>());

            spawnBellosChest = true;
            isGlitched = false;
            CURSE_EXPOSED = 1f;
        }

        public MirrorDweller PlayerReflection;
        public MirrorDweller CoopPlayerReflection;
        public MirrorDweller ChestReflection;

        public tk2dBaseSprite ChestSprite;
        public tk2dBaseSprite MirrorSprite;

        public GameObject ShatterSystem;
        public Chest MirrorChest;

        public float CURSE_EXPOSED;
        public bool spawnBellosChest;
        public bool isGlitched;

        private RoomHandler m_ParentRoom;

        private void Start() {    
                    
            PlayerReflection.TargetPlayer = GameManager.Instance.PrimaryPlayer;
            PlayerReflection.MirrorSprite = MirrorSprite;   
            if (!isGlitched) {
                if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                    CoopPlayerReflection.TargetPlayer = GameManager.Instance.SecondaryPlayer;
                    CoopPlayerReflection.MirrorSprite = MirrorSprite;
                } else {
                    CoopPlayerReflection.gameObject.SetActive(false);
                }
            } else {
                PlayerReflection.gameObject.SetActive(false);
                CoopPlayerReflection.gameObject.SetActive(false);
                tk2dBaseSprite[] AllMirrorSprites = gameObject.GetComponents<tk2dBaseSprite>();
                if (AllMirrorSprites != null && AllMirrorSprites.Length > 0) { ExpandShaders.Instance.ApplyGlitchShader(AllMirrorSprites[0]); }
            }

            IntVector2 MirrorChestPosition = (base.transform.position.IntXY(VectorConversions.Round) + new IntVector2(0, -2) - m_ParentRoom.area.basePosition);
            if (spawnBellosChest) {
                MirrorChest = ExpandUtility.GenerateChest(MirrorChestPosition, m_ParentRoom, PickupObject.ItemQuality.A, 0, false);
                MirrorChest.forceContentIds = new List<int>() { 435, 493 };
            } else {
                MirrorChest = ExpandUtility.GenerateChest(MirrorChestPosition, m_ParentRoom, null, -1f);
            }
            MirrorChest.PreventFuse = true;
            SpriteOutlineManager.RemoveOutlineFromSprite(MirrorChest.sprite, false);
            Transform transform = MirrorChest.gameObject.transform.Find("Shadow");
            if (transform) { MirrorChest.ShadowSprite = transform.GetComponent<tk2dSprite>(); }
            MirrorChest.IsMirrorChest = true;
            MirrorChest.ConfigureOnPlacement(m_ParentRoom);
            m_ParentRoom.RegisterInteractable(MirrorChest);
            if (spawnBellosChest) { MirrorChest.DeregisterChestOnMinimap(); }
            if (MirrorChest.majorBreakable) { MirrorChest.majorBreakable.TemporarilyInvulnerable = true; }
            ChestSprite = MirrorChest.sprite;
            ChestSprite.renderer.enabled = false;
            ChestReflection.TargetSprite = ChestSprite;
            ChestReflection.MirrorSprite = MirrorSprite;
            SpeculativeRigidbody specRigidbody = MirrorSprite.specRigidbody;
            specRigidbody.OnRigidbodyCollision = (SpeculativeRigidbody.OnRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnRigidbodyCollision, new SpeculativeRigidbody.OnRigidbodyCollisionDelegate(HandleRigidbodyCollisionWithMirror));
            MinorBreakable componentInChildren = GetComponentInChildren<MinorBreakable>();
            componentInChildren.OnlyBrokenByCode = true;
            componentInChildren.heightOffGround = 4f;

            IPlayerInteractable[] TableInterfacesInChildren = GameObjectExtensions.GetInterfacesInChildren<IPlayerInteractable>(gameObject);
            for (int i = 0; i < TableInterfacesInChildren.Length; i++) { if (!m_ParentRoom.IsRegistered(TableInterfacesInChildren[i])) { m_ParentRoom.RegisterInteractable(TableInterfacesInChildren[i]); } }
            // Destroy(gameObject.GetComponent<MirrorController>());

            // SpeculativeRigidbody InteractableRigidMirror = gameObject.GetComponent<SpeculativeRigidbody>();
            // InteractableRigidMirror.Initialize();
            // PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(InteractableRigidMirror, null, false);
        }


        private void HandleRigidbodyCollisionWithMirror(CollisionData rigidbodyCollision) {
            if (rigidbodyCollision.OtherRigidbody.projectile) {
                GetAbsoluteParentRoom().DeregisterInteractable(this);
                if (rigidbodyCollision.OtherRigidbody.projectile.Owner is PlayerController) {
                    StartCoroutine(HandleShatter(rigidbodyCollision.OtherRigidbody.projectile.Owner as PlayerController, true));
                } else {
                    StartCoroutine(HandleShatter(GameManager.Instance.PrimaryPlayer, true));
                }
            }
        }

        public float GetDistanceToPoint(Vector2 point) {
            Bounds bounds = ChestSprite.GetBounds();
            bounds.SetMinMax(bounds.min + ChestSprite.transform.position, bounds.max + ChestSprite.transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }

        public void OnEnteredRange(PlayerController interactor) { }

        public void OnExitRange(PlayerController interactor) {
            MirrorDweller[] componentsInChildren = ChestReflection.GetComponentsInChildren<MirrorDweller>(true);
            for (int i = 0; i < componentsInChildren.Length; i++) {
                if (componentsInChildren[i].UsesOverrideTintColor) { componentsInChildren[i].renderer.enabled = false; }
            }
        }

        public void Interact(PlayerController interactor) {
            ChestSprite.GetComponent<Chest>().ForceOpen(interactor);
            MirrorDweller[] componentsInChildren = ChestReflection.GetComponentsInChildren<MirrorDweller>(true);
            for (int i = 0; i < componentsInChildren.Length; i++) {
                if (componentsInChildren[i].UsesOverrideTintColor) { componentsInChildren[i].renderer.enabled = false; }
            }
            GetAbsoluteParentRoom().DeregisterInteractable(this);
            StartCoroutine(HandleShatter(interactor, false));
            for (int j = 0; j < interactor.passiveItems.Count; j++) {
                if (interactor.passiveItems[j] is YellowChamberItem) { break; }
            }
        }

        private IEnumerator HandleShatter(PlayerController interactor, bool skipInitialWait = false) {
            if (!skipInitialWait) { yield return new WaitForSeconds(0.5f); }
            if (this) {
                AkSoundEngine.PostEvent("Play_OBJ_crystal_shatter_01", gameObject);
                AkSoundEngine.PostEvent("Play_OBJ_pot_shatter_01", gameObject);
                AkSoundEngine.PostEvent("Play_OBJ_glass_shatter_01", gameObject);
            }
            StatModifier curse = new StatModifier();
            curse.statToBoost = PlayerStats.StatType.Curse;
            curse.amount = CURSE_EXPOSED;
            curse.modifyType = StatModifier.ModifyMethod.ADDITIVE;
            if (!interactor) { interactor = GameManager.Instance.PrimaryPlayer; }
            if (interactor) {
                interactor.ownerlessStatModifiers.Add(curse);
                interactor.stats.RecalculateStats(interactor, false, false);
            }
            MinorBreakable childBreakable = GetComponentInChildren<MinorBreakable>();
            if (childBreakable) {
                childBreakable.Break();
                while (childBreakable) { yield return null; }
            }
            tk2dSpriteAnimator eyeBall = GetComponentInChildren<tk2dSpriteAnimator>();
            if (eyeBall) { eyeBall.Play("haunted_mirror_eye"); }
            if (ShatterSystem) { ShatterSystem.SetActive(true); }
            yield return new WaitForSeconds(2.5f);
            if (ShatterSystem) { ShatterSystem.GetComponent<ParticleSystem>().Pause(false); }
            yield break;
        }

        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }

        public float GetOverrideMaxDistance() { return -1f; }

        public void ConfigureOnPlacement(RoomHandler room) {            
            m_ParentRoom = room;
            m_ParentRoom.OptionalDoorTopDecorable = (ResourceCache.Acquire("Global Prefabs/Purple_Lantern") as GameObject);            
        }
    }
}

