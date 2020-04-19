using System;
using System.Collections;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandObjects;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandRickRollChest : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandRickRollChest() {

            BaseOutlineColor = Color.black;
            IsBroken = false;

            m_configured = false;

            pickedUp = false;

        }

        public Color BaseOutlineColor;
        public bool pickedUp;
        public bool IsBroken;

        public string openAnimName;
        public string breakAnimName;

        public GameObject RickRollAnimationObject;
        public GameObject MinimapIconPrefab;


        private bool m_configured;

        
        private GameObject minimapIconInstance;

        private RoomHandler m_room;
        private RoomHandler m_registeredIconRoom;

        public void ConfigureOnPlacement(RoomHandler room) {
            m_room = room;
            Initialize();
            if (!m_configured) { RegisterChestOnMinimap(room); }
            m_configured = true;
        }

        private void Awake() {            
            SpriteOutlineManager.AddOutlineToSprite(sprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            MajorBreakable majorBreakable = base.majorBreakable;
            if (base.majorBreakable.DamageReduction > 1000f) { base.majorBreakable.ReportZeroDamage = true; }
            // base.majorBreakable.InvulnerableToEnemyBullets = true;
        }

        protected void Initialize() {
            specRigidbody.Initialize();
            specRigidbody.PreventPiercing = true;

            MajorBreakable component = GetComponent<MajorBreakable>();
            if (component != null) {
                MajorBreakable majorBreakable = component;
                majorBreakable.OnBreak = (Action)Delegate.Combine(majorBreakable.OnBreak, new Action(OnBroken));
            }
            IntVector2 intVector = specRigidbody.UnitBottomLeft.ToIntVector2(VectorConversions.Floor);
            IntVector2 intVector2 = specRigidbody.UnitTopRight.ToIntVector2(VectorConversions.Floor);
            for (int i = intVector.x; i <= intVector2.x; i++) {
                for (int j = intVector.y; j <= intVector2.y; j++) {
                    GameManager.Instance.Dungeon.data[new IntVector2(i, j)].isOccupied = true;
                }
            }

            BecomeRainbow();
        }

        private void BecomeRainbow() { sprite.renderer.material.shader = ShaderCache.Acquire("Brave/Internal/RainbowChestShader"); }

        public void Interact(PlayerController player) {
            if (!pickedUp) { Open(player); }
            return;
        }

        public void RegisterChestOnMinimap(RoomHandler r) {
            m_registeredIconRoom = r;
            GameObject iconPrefab = MinimapIconPrefab ?? (BraveResources.Load("Global Prefabs/Minimap_Treasure_Icon", ".prefab") as GameObject);
            minimapIconInstance = Minimap.Instance.RegisterRoomIcon(r, iconPrefab, false);
        }

        public void DeregisterChestOnMinimap() {
            if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }
        }

        public void Open(PlayerController player) {
            if (player) {
                if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }
                pickedUp = true;
                // IsOpen = true;
                m_room.DeregisterInteractable(this);
                MajorBreakable component = GetComponent<MajorBreakable>();
                if (component != null) { component.usesTemporaryZeroHitPointsState = false; }
                AkSoundEngine.PostEvent("play_obj_chest_open_01", gameObject);
                spriteAnimator.Play(openAnimName);
                player.TriggerItemAcquisition();
                StartCoroutine(DoRickRoll());
            }
        }


        private IEnumerator DoRickRoll() {
            yield return new WaitForSeconds(0.1f);
            GameObject m_RickRollInstance = Instantiate(RickRollAnimationObject, (base.transform.position + new Vector3(0.1f, 0.5f, 0)), Quaternion.identity);
            int cachedLayer = m_RickRollInstance.layer;
            int cachedOutlineLayer = cachedLayer;
            m_RickRollInstance.layer = LayerMask.NameToLayer("Unpixelated");
            m_RickRollInstance.transform.localScale = new Vector2(0.2f, 0.2f).ToVector3ZUp(1f);
            // cachedOutlineLayer = SpriteOutlineManager.ChangeOutlineLayer(m_RickRollInstance.GetComponent<tk2dSprite>(), LayerMask.NameToLayer("Unpixelated"));
            // if (m_RickRollInstance.GetComponent<tk2dSprite>()) {
            if (sprite) {
                sprite.HeightOffGround = -1f;
                sprite.UpdateZDepth();
                // m_RickRollInstance.GetComponent<tk2dSprite>().HeightOffGround = 1.01f;
                // m_RickRollInstance.GetComponent<tk2dSprite>().UpdateZDepth();
            }
            MajorBreakable m_MajorBreakable = GetComponent<MajorBreakable>();
            if (m_MajorBreakable) { m_MajorBreakable.TemporarilyInvulnerable = true; }
            tk2dSpriteAnimator m_RickRollAnimator = m_RickRollInstance.GetComponent<tk2dSpriteAnimator>();
            m_RickRollAnimator.Play("RickRollAnimation_Rise");
            while (m_RickRollAnimator.IsPlaying("RickRollAnimation_Rise")) { yield return null; }
            m_RickRollAnimator.Play("RickRollAnimation");
            AkSoundEngine.PostEvent("Play_EX_RickRollMusic_01", gameObject);
            yield return new WaitForSeconds(10);
            Destroy(m_RickRollInstance);
            m_MajorBreakable.TemporarilyInvulnerable = false;
            if (m_MajorBreakable) {
                MajorBreakable majorBreakable = m_MajorBreakable;
                majorBreakable.OnBreak = (Action)Delegate.Remove(majorBreakable.OnBreak, new Action(OnBroken));
            }
            spriteAnimator.Play(breakAnimName);
            specRigidbody.enabled = false;
            IsBroken = true;
            Transform shadowTransform = transform.Find("ChestShadow");
            if (shadowTransform != null) { Destroy(shadowTransform.gameObject); }
            pickedUp = true;
            if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }
            m_room.DeregisterInteractable(this);
            Exploder.DoDefaultExplosion(sprite.WorldCenter, Vector2.zero, null, false, CoreDamageTypes.None, false);
            yield break;
        }


        private void OnBroken() {
            spriteAnimator.Play(breakAnimName);
            specRigidbody.enabled = false;
            IsBroken = true;
            Transform shadowTransform = transform.Find("ChestShadow");
            if (shadowTransform != null) { Destroy(shadowTransform.gameObject); }
            if (!pickedUp) {
                pickedUp = true;                
                m_room.DeregisterInteractable(this);
                if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }                
                bool flag2 = GameStatsManager.Instance.GetFlag(GungeonFlags.ITEMSPECIFIC_GOLD_JUNK);
                float num = GameManager.Instance.RewardManager.ChestDowngradeChance;
                float num2 = GameManager.Instance.RewardManager.ChestHalfHeartChance;
                float num3 = GameManager.Instance.RewardManager.ChestExplosionChance;
                float num4 = GameManager.Instance.RewardManager.ChestJunkChance;
                float num5 = (!flag2) ? 0f : 0.005f;
                bool flag3 = GameStatsManager.Instance.GetFlag(GungeonFlags.ITEMSPECIFIC_SER_JUNKAN_UNLOCKED);
                float num6 = (!flag3 || Chest.HasDroppedSerJunkanThisSession) ? 0f : GameManager.Instance.RewardManager.ChestJunkanUnlockedChance;
                if (GameManager.Instance.PrimaryPlayer && GameManager.Instance.PrimaryPlayer.carriedConsumables.KeyBullets > 0) {
                    num4 *= GameManager.Instance.RewardManager.HasKeyJunkMultiplier;
                }
                if (SackKnightController.HasJunkan()) {
                    num4 *= GameManager.Instance.RewardManager.HasJunkanJunkMultiplier;
                    num5 *= 3f;
                }                
                num4 -= num5;
                float num7 = num5 + num + num2 + num3 + num4 + num6;
                float num8 = UnityEngine.Random.value * num7;
                if (num8 > num + num2 + num4 + num6) {
                    Exploder.DoDefaultExplosion(sprite.WorldCenter, Vector2.zero, null, false, CoreDamageTypes.None, false);
                }
            }
        }

        public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.AddOutlineToSprite(sprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public float GetDistanceToPoint(Vector2 point) {
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }

        public float GetOverrideMaxDistance() { return -1f; }


        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }
        
        protected override void OnDestroy() {
            base.OnDestroy();
            AkSoundEngine.PostEvent("Stop_SND_OBJ", gameObject);
            AkSoundEngine.PostEvent("Stop_EX_RickRollMusic_01", gameObject);
        }
    }

}

