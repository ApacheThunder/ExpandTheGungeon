using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;


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
            if (!m_room.IsSealed) {
                m_room.npcSealState = RoomHandler.NPCSealState.SealAll;
                m_room.SealRoom();
            }
            GameObject m_RickRollInstance = Instantiate(RickRollAnimationObject, (transform.position + new Vector3(0.1f, 0.5f, 0)), Quaternion.identity);
            int cachedLayer = m_RickRollInstance.layer;
            int cachedOutlineLayer = cachedLayer;
            m_RickRollInstance.layer = LayerMask.NameToLayer("Unpixelated");
            m_RickRollInstance.transform.localScale = new Vector2(0.2f, 0.2f).ToVector3ZUp(1f);
            // cachedOutlineLayer = SpriteOutlineManager.ChangeOutlineLayer(m_RickRollInstance.GetComponent<tk2dSprite>(), LayerMask.NameToLayer("Unpixelated"));
            MajorBreakable m_MajorBreakable = GetComponent<MajorBreakable>();
            if (m_MajorBreakable) { m_MajorBreakable.TemporarilyInvulnerable = true; }
            if (sprite) { sprite.HeightOffGround = -2f; sprite.UpdateZDepth(); }

            GameManager.Instance.StartCoroutine(SpwanEnemyAirDrop());

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
            if (m_room.npcSealState != RoomHandler.NPCSealState.SealNone) { m_room.npcSealState = RoomHandler.NPCSealState.SealNone; }
            Exploder.DoDefaultExplosion(sprite.WorldCenter, Vector2.zero, null, false, CoreDamageTypes.None, false);
            yield break;
        }

        private IEnumerator SpwanEnemyAirDrop(float delay = 0.4f) {
            Vector3 RoomOffset = m_room.area.basePosition.ToVector3();
            string EnemyGUID1 = "88b6b6a93d4b4234a67844ef4728382c"; // bandana_bullet_kin
            string EnemyGUID2 = "4d37ce3d666b4ddda8039929225b7ede"; // grenade_kin
            string EnemyGUID3 = "01972dee89fc4404a5c408d50007dad5"; // bullet_kin
            string EnemyGUID4 = "128db2f0781141bcb505d8f00f9e4d47"; // red_shotgun_kin

            if (UnityEngine.Random.value <= 0.5f) { EnemyGUID1 = ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID; }
            if (UnityEngine.Random.value <= 0.1f) { EnemyGUID2 = ExpandCustomEnemyDatabase.BootlegShotgunManBlueGUID; }
            if (UnityEngine.Random.value <= 0.5f) { EnemyGUID3 = ExpandCustomEnemyDatabase.BootlegBulletManGUID; }
            if (UnityEngine.Random.value <= 0.5f) { EnemyGUID4 = ExpandCustomEnemyDatabase.BootlegShotgunManRedGUID; }

            yield return new WaitForSeconds(delay);

            GameObject eCrateInstance1 = ExpandUtility.SpawnAirDrop(m_room, (RoomOffset + new Vector3(4, 3, 0)), null, ExpandUtility.GenerateDungeonPlacable(null, true, EnemyGUID: EnemyGUID1));
            GameObject eCrateInstance2 = ExpandUtility.SpawnAirDrop(m_room, (RoomOffset + new Vector3(4, 9, 0)), null, ExpandUtility.GenerateDungeonPlacable(null, true, EnemyGUID: EnemyGUID2), 0.2f);
            GameObject eCrateInstance3 = ExpandUtility.SpawnAirDrop(m_room, (RoomOffset + new Vector3(13, 3, 0)), null, ExpandUtility.GenerateDungeonPlacable(null, true, EnemyGUID: EnemyGUID3));
            GameObject eCrateInstance4 = ExpandUtility.SpawnAirDrop(m_room, (RoomOffset + new Vector3(13, 9, 0)), null, ExpandUtility.GenerateDungeonPlacable(null, true, EnemyGUID: EnemyGUID4), 0.2f);

            /*List<GameObject> eCrateList = new List<GameObject>();

            if (eCrateInstance1) { eCrateList.Add(eCrateInstance1); }
            if (eCrateInstance2) { eCrateList.Add(eCrateInstance1); }
            if (eCrateInstance3) { eCrateList.Add(eCrateInstance1); }
            if (eCrateInstance4) { eCrateList.Add(eCrateInstance1); }

            GameObject SelectedCrate = null;

            if (eCrateList.Count > 0) { SelectedCrate = BraveUtility.RandomElement(eCrateList); }

            if (SelectedCrate && SelectedCrate.GetComponent<EmergencyCrateController>()) {
                yield return new WaitForSeconds(2.25f);
                while (ReflectionHelpers.ReflectGetField<bool?>(typeof(EmergencyCrateController), "m_hasBeenTriggered", SelectedCrate.GetComponent<EmergencyCrateController>()).HasValue && ReflectionHelpers.ReflectGetField<bool?>(typeof(EmergencyCrateController), "m_hasBeenTriggered", SelectedCrate.GetComponent<EmergencyCrateController>()).Value) {
                    if (!SelectedCrate | !SelectedCrate.GetComponent<EmergencyCrateController>()) { break; }
                    yield return null;
                }
                yield return new WaitForSeconds(1f);
                m_room.npcSealState = RoomHandler.NPCSealState.SealNone;
            }*/
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

