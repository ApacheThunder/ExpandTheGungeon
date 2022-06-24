using System;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using System.Collections;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ExpandUtilities;
using Dungeonator;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCasinoWarpTrigger : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandCasinoWarpTrigger() {
            TargetSpawnPosition = new Vector3(-3, 2);
            TargetSpawnPosition2 = new Vector3(6, 1);

            TargetDoorOpenAnim = "open";
            TargetDoorCloseAnim = "close";

            m_Interacted = false;
        }
        
        public string TargetDoorOpenAnim;
        public string TargetDoorCloseAnim;
        public GameObject TargetSpawnObject;
        public Vector3 TargetSpawnPosition;
        public Vector3 TargetSpawnPosition2;


        [NonSerialized]
        private bool m_Interacted;
        [NonSerialized]
        GameObject m_SpawnedObject;
        [NonSerialized]
        GameObject m_SpawnedObject2;
        [NonSerialized]
        GameObject m_SpawnedObject3;
        [NonSerialized]
        GameObject m_SpawnedObject4;
        [NonSerialized]
        private tk2dSpriteAnimator m_TargetAnimator;
        [NonSerialized]
        private SpeculativeRigidbody m_TargetRigidBody;
        [NonSerialized]
        private SpeculativeRigidbody m_TargetRigidBody2;
        [NonSerialized]
        private ExpandWarpManager m_FoyerDoorWarp;
        [NonSerialized]
        private ExpandWarpManager m_FoyerDoorWarp2;
        [NonSerialized]
        private RoomHandler m_TargetRoom;
        [NonSerialized]
        private RoomHandler m_ParentRoom;



        /*private void Awake() { }*/
        private void Update() {
            /*if (!GameManager.Instance | GameManager.Instance.PrimaryPlayer) { return; }
            if (GameManager.Instance.PrimaryPlayer && GameStatsManager.Instance.GetPlayerStatValue(TrackedStats.)) {

            }*/
        }
        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;
        }

        public void Interact(PlayerController player) {
            if (!m_Interacted && player) {
                m_Interacted = true;
                if (!m_SpawnedObject) {
                    if (m_SpawnedObject2) { Destroy(m_SpawnedObject2); }
                    m_SpawnedObject = Instantiate(TargetSpawnObject, (gameObject.transform.position + TargetSpawnPosition), Quaternion.identity);
                    m_TargetAnimator = m_SpawnedObject.GetComponent<tk2dSpriteAnimator>();
                    m_FoyerDoorWarp = m_SpawnedObject.GetComponent<ExpandWarpManager>();
                    m_TargetRigidBody = m_SpawnedObject.GetComponent<SpeculativeRigidbody>();
                    m_FoyerDoorWarp.TargetPoint = (new IntVector2(1, 2) + TargetSpawnPosition2.ToIntVector2());
                    if (m_TargetRoom == null) {
                        m_TargetRoom = ExpandUtility.AddCustomRuntimeRoom(GameManager.Instance.Dungeon, new IntVector2(17, 20), ExpandPrefabs.EXCasinoHub, roomWorldPositionOverride: new IntVector2(0, 60));
                    }
                    m_FoyerDoorWarp.TargetRoom = m_TargetRoom;
                    m_FoyerDoorWarp.ConfigureOnPlacement(m_ParentRoom);
                    if (!m_SpawnedObject3) {
                        if (m_SpawnedObject4) { Destroy(m_SpawnedObject4); }
                        m_SpawnedObject3 = Instantiate(TargetSpawnObject, (m_TargetRoom.area.basePosition.ToVector3() + TargetSpawnPosition2 + new Vector3(0.4f, 0)), Quaternion.identity);
                        m_FoyerDoorWarp2 = m_SpawnedObject3.GetComponent<ExpandWarpManager>();
                        m_TargetRigidBody2 = m_SpawnedObject3.GetComponent<SpeculativeRigidbody>();
                        m_FoyerDoorWarp2.TargetPoint = (m_SpawnedObject.transform.position.ToIntVector2() - m_ParentRoom.area.basePosition) + new IntVector2(1, -1);
                        m_FoyerDoorWarp2.TargetRoom = m_ParentRoom;
                        m_FoyerDoorWarp2.sprite.SetSprite("foyerdoor_open_13");
                        m_FoyerDoorWarp2.ConfigureOnPlacement(m_TargetRoom);
                        m_SpawnedObject4 = new GameObject("EXFoyerWarpDoor Field 2");
                        m_SpawnedObject4.transform.position = m_SpawnedObject3.transform.position;
                        tk2dSprite m_EXFoyerWarpDoorSprite2 = SpriteSerializer.AddSpriteToObject(m_SpawnedObject4, ExpandPrefabs.EXFoyerCollection, "foyerdoor_field_01");
                        ExpandShaders.ApplyHologramShader(m_EXFoyerWarpDoorSprite2);
                        m_EXFoyerWarpDoorSprite2.HeightOffGround = -1f;
                        m_EXFoyerWarpDoorSprite2.UpdateZDepth();
                        foreach (PixelCollider collider in m_TargetRigidBody2.PixelColliders) { collider.Enabled = true; }
                        m_FoyerDoorWarp2.IsOpenForTeleport = true;
                    }
                    StartCoroutine(HandleDoorOpen());
                    sprite.SetSprite("floortrigger_active_01");
                } else {
                    if (m_FoyerDoorWarp) { m_FoyerDoorWarp.IsOpenForTeleport = false; }
                    StartCoroutine(HandleDoorClose());
                    sprite.SetSprite("floortrigger_idle_01");
                }

            }
            return;
        }

        private IEnumerator HandleDoorOpen() {
            m_TargetAnimator.Play(TargetDoorOpenAnim);
            yield return null;
            AkSoundEngine.PostEvent("Play_OBJ_cardoor_open_01", m_SpawnedObject);
            foreach (PixelCollider collider in m_TargetRigidBody.PixelColliders) { collider.Enabled = true; }
            while (m_TargetAnimator.IsPlaying(TargetDoorOpenAnim)) { yield return null; }
            m_SpawnedObject2 = new GameObject("EXFoyerWarpDoor Field");
            tk2dSprite m_EXFoyerWarpDoorSprite = SpriteSerializer.AddSpriteToObject(m_SpawnedObject2, ExpandPrefabs.EXFoyerCollection, "foyerdoor_field_01");
            yield return null;
            ExpandShaders.ApplyHologramShader(m_EXFoyerWarpDoorSprite);
            m_EXFoyerWarpDoorSprite.HeightOffGround = -1f;
            yield return null;
            m_SpawnedObject2.transform.position = m_SpawnedObject.transform.position;
            m_EXFoyerWarpDoorSprite.UpdateZDepth();
            m_FoyerDoorWarp.IsOpenForTeleport = true;
            m_Interacted = false;
            yield break;
        }

        private IEnumerator HandleDoorClose() {
            Destroy(m_SpawnedObject2);
            foreach (PixelCollider collider in m_TargetRigidBody.PixelColliders) { collider.Enabled = false; }
            AkSoundEngine.PostEvent("Play_OBJ_cardoor_close_01", m_SpawnedObject);
            m_TargetAnimator.PlayAndDestroyObject(TargetDoorCloseAnim);
            yield return null;
            while (m_SpawnedObject | m_SpawnedObject2) { yield return null; }
            m_Interacted = false;
            yield break;
        }
                
        public void OnEnteredRange(PlayerController interactor) {
            if (!this | m_Interacted) { return; }
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            sprite.UpdateZDepth();
        }

        public float GetDistanceToPoint(Vector2 point) {
            if (m_Interacted) { return float.PositiveInfinity; }
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
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

