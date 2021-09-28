using System;
using System.Collections;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;


namespace ExpandTheGungeon.ExpandComponents {
    public class ExpandBellyWoodCrestEntranceController : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandBellyWoodCrestEntranceController() {

            TargetRoomName = string.Empty;

            ItemID = -1;

            BaseOutlineColor = Color.black;
            
            m_Interacted = false;
        }

        public string TargetRoomName;

        public int ItemID;
        public Color BaseOutlineColor;

        private bool m_Interacted;

        private RoomHandler m_ParentRoom;
        private RoomHandler m_TargetRoom;

        private GameObject m_TargetDoor;

        public void Start() {

            sprite.HeightOffGround = -0.5f;
            sprite.UpdateZDepth();

            TargetRoomName = ExpandRoomPrefabs.Expand_Gungeon_HiddenMonsterRoom.name;

            foreach (RoomHandler Room in GameManager.Instance.Dungeon.data.rooms) {
                if (!string.IsNullOrEmpty(Room.GetRoomName()) && Room.GetRoomName().StartsWith(TargetRoomName)) {
                    m_TargetRoom = Room;
                    break;
                }
            }

            if (m_TargetRoom == null) {
                // Target Room not found! Self Destruct Sequence Activated!
                Destroy(this);
                return;
            }

            if (FindObjectsOfType<GameObject>() == null | FindObjectsOfType<GameObject>().Length <= 0) {
                return;
            }

            foreach (GameObject obj in FindObjectsOfType<GameObject>()) {
                if (!string.IsNullOrEmpty(obj.name) && obj.name.StartsWith(ExpandPrefabs.Sarco_Door.name)) {
                    m_TargetDoor = obj;
                    return;
                }
            }

        }

        // private void Awake() { }
        // private void Update() { }

        public void Interact(PlayerController player) {
            if (!m_Interacted && player && ItemID != -1 && player.HasPassiveItem(ItemID) && m_ParentRoom != null && m_TargetRoom != null) { TakeItem(player); }
            return;
        }        

        private void TakeItem(PlayerController player) {
            
            if (PickupObjectDatabase.GetById(ItemID) && (PickupObjectDatabase.GetById(ItemID).sprite as tk2dSprite) != null && player.HasPassiveItem(ItemID)) {
                m_Interacted = true;
            } else {
                return;
            }

            GameObject m_SubSpriteObject = new GameObject("Item Display Object", new Type[] { typeof(tk2dSprite) }) { layer = 0 };
            m_SubSpriteObject.transform.position = (transform.position + new Vector3(0.35f, 1.3f));
            ExpandUtility.DuplicateSprite(m_SubSpriteObject.GetComponent<tk2dSprite>(), (PickupObjectDatabase.GetById(ItemID).sprite as tk2dSprite));
            if (m_ParentRoom != null) { m_SubSpriteObject.transform.parent = m_ParentRoom.hierarchyParent; }
            m_SubSpriteObject.GetComponent<tk2dSprite>().HeightOffGround = 3f;
            m_SubSpriteObject.GetComponent<tk2dSprite>().UpdateZDepth();
            player.RemovePassiveItem(ItemID);
            
            if (m_TargetDoor && m_TargetDoor.GetComponent<tk2dSpriteAnimator>() && m_TargetDoor.GetComponent<SpeculativeRigidbody>()) {
                
                AkSoundEngine.PostEvent("Play_OBJ_plate_press_01", gameObject);
                StartCoroutine(DelayedDoorOpen(m_TargetDoor));
            }

            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
        }

        private IEnumerator DelayedDoorOpen(GameObject targetObject) {
            yield return new WaitForSeconds(0.5f);
            targetObject.GetComponent<tk2dSpriteAnimator>().Play("Sarco_Door_Open");
            AkSoundEngine.PostEvent("Play_OBJ_hugedoor_open_01", targetObject);
            while (targetObject.GetComponent<tk2dSpriteAnimator>().IsPlaying("Sarco_Door_Open")) { yield return null; }

            if (targetObject.GetComponent<SpeculativeRigidbody>().PixelColliders.Count > 2) {
                targetObject.GetComponent<SpeculativeRigidbody>().PixelColliders[0].Enabled = false;
                targetObject.GetComponent<SpeculativeRigidbody>().PixelColliders[1].Enabled = false;
            }

            GameObject m_NewWarpWing = new GameObject("Belly Warp Wing Thing") { layer = 0 };
            m_NewWarpWing.transform.position = m_TargetDoor.transform.position;
            ExpandUtility.GenerateOrAddToRigidBody(m_NewWarpWing, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, UsesPixelsAsUnitSize: true, offset: new IntVector2(7, 12), dimensions: new IntVector2(38, 16));
            ExpandWarpManager WarpWingDoor = m_NewWarpWing.AddComponent<ExpandWarpManager>();
            WarpWingDoor.TargetRoom = m_TargetRoom;
            WarpWingDoor.IsOpenForTeleport = true;
            yield return null;
            Destroy(this);
            yield break;
        }

        public void ConfigureOnPlacement(RoomHandler room) { m_ParentRoom = room; }

        public void OnEnteredRange(PlayerController interactor) {
            if (!this | m_Interacted | ItemID == -1 | !interactor | !interactor.HasPassiveItem(ItemID) | m_TargetRoom == null) { return; }
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this | m_Interacted | ItemID == -1 | !interactor | !interactor.HasPassiveItem(ItemID) | m_TargetRoom == null) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
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
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
}


