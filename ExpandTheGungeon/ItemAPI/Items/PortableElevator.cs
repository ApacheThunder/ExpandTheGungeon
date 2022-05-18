using Dungeonator;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

    public class PortableElevator : PlayerItem {
        
        public static int PortableElevatorID;

        public static GameObject PortableElevatorOBJ;

        public static void Init(AssetBundle expandSharedAssets1) {
            PortableElevatorOBJ = expandSharedAssets1.LoadAsset<GameObject>("Portable Elevator");
            SpriteSerializer.AddSpriteToObject(PortableElevatorOBJ, ExpandPrefabs.EXItemCollection, "portable_elevator");

            PortableElevator portableelevator = PortableElevatorOBJ.AddComponent<PortableElevator>();
            string shortDesc = "Make a Hasty Exit";
            string longDesc = "Sometimes you just need to escape. A one time use item Lunk found but he dropped it while exploring the Gungeon because he was too busy looking at his map.";
            ItemBuilder.SetupItem(portableelevator, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(portableelevator, ItemBuilder.CooldownType.Damage, 275f);
            portableelevator.quality = ItemQuality.B;
                        
            if (!ExpandSettings.EnableEXItems) { portableelevator.quality = ItemQuality.EXCLUDED; }

            List<string> spritePaths = new List<string>() {
                "portable_elevator",
                "portable_elevator",
                "portable_elevator_pushed",
                "portable_elevator_pushed",
                "portable_elevator"
            };
            
            ExpandUtility.GenerateSpriteAnimator(PortableElevatorOBJ);

            tk2dSpriteAnimator portableElevatorAnimator = PortableElevatorOBJ.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(portableElevatorAnimator, ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), spritePaths, "Activate", frameRate: 2);

            PortableElevatorID = portableelevator.PickupObjectId;
        }




        public PortableElevator() {
            ValidDestinations = new List<string>() { "tt_office", "tt_phobos", "tt_space", };

            maxDistance = 10;

            m_WasUsed = false;

            m_currentDistance = 5f;
        }


        public List<string> ValidDestinations;
        public float maxDistance;

        public GameObject reticleQuad;        
        
        private PlayerController m_currentUser;
        private tk2dBaseSprite m_extantReticleQuad;
        private ExpandReticleRiserEffect m_ReticleEffect;

        private bool m_WasUsed;

        private float m_currentAngle;
        private float m_currentDistance;


        public override bool CanBeUsed(PlayerController user) { return (IsUsableRightNow(user) && base.CanBeUsed(user)); }

        private bool IsUsableRightNow(PlayerController user) {
            if (!user | user.IsInCombat | user.CurrentRoom == null | user.CurrentRoom.IsSealed) { return false; }
            if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON) { return false; }
            if (!GameStatsManager.Instance.AllCorePastsBeaten() && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON) {
                return false;
            }
            return true;
        }

        protected bool CanDropElevatorHere(PlayerController Owner, Vector2 centerOffset) {
            if (Owner && Owner.IsInCombat) { return false; }

            for (int X = 0; X < 4; X++) {
                for (int Y = 0; Y < 5; Y++) {
                    Vector2 currentOffset = (centerOffset + new Vector2(X, Y));
                    
                    bool ValidPositionForPlayer = Owner.IsValidPlayerPosition(currentOffset);

                    if (!ValidPositionForPlayer) { return false; }

                    CellData cellData = GameManager.Instance.Dungeon.data[currentOffset.ToIntVector2(VectorConversions.Floor)];
                    if (cellData == null) { return false; }
                    if (cellData.type != CellType.FLOOR) { return false; }
                    // if (cellData.isOccupied) { return false; }
                    if (cellData.parentRoom == null | Owner.CurrentRoom == null | cellData.parentRoom != Owner.CurrentRoom) { return false; }
                }
            }
            return true;
        }


        public override void Update() {
            base.Update();
            if (IsCurrentlyActive) {
                if (m_extantReticleQuad) {
                    UpdateReticlePosition();
                } else {
                    IsCurrentlyActive = false;
                    ClearCooldowns();
                }
            }
        }

        private void UpdateReticlePosition() {
            if (BraveInput.GetInstanceForPlayer(m_currentUser.PlayerIDX).IsKeyboardAndMouse(false)) {
                Vector2 a = m_currentUser.unadjustedAimPoint.XY();
                Vector2 v = a - m_extantReticleQuad.GetBounds().extents.XY();
                m_extantReticleQuad.transform.position = v;
            } else {
                BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(m_currentUser.PlayerIDX);
                Vector2 a2 = m_currentUser.CenterPosition + (Quaternion.Euler(0f, 0f, m_currentAngle) * Vector2.right).XY() * m_currentDistance;
                a2 += instanceForPlayer.ActiveActions.Aim.Vector * 8f * BraveTime.DeltaTime;
                m_currentAngle = BraveMathCollege.Atan2Degrees(a2 - m_currentUser.CenterPosition);
                m_currentDistance = Vector2.Distance(a2, m_currentUser.CenterPosition);
                m_currentDistance = Mathf.Min(m_currentDistance, maxDistance);
                a2 = m_currentUser.CenterPosition + (Quaternion.Euler(0f, 0f, m_currentAngle) * Vector2.right).XY() * m_currentDistance;
                Vector2 v2 = a2 - m_extantReticleQuad.GetBounds().extents.XY();
                m_extantReticleQuad.transform.position = v2;
            }
            
            if (CanDropElevatorHere(m_currentUser, (m_extantReticleQuad.gameObject.transform.position + new Vector3(1f, 1f)))) {
                m_extantReticleQuad.SetSprite("portable_elevator_reticle_green");
                if (m_ReticleEffect) { m_ReticleEffect.CurrentSpriteName = "portable_elevator_reticle_green"; }
            } else {
                m_extantReticleQuad.SetSprite("portable_elevator_reticle_red");
                if (m_ReticleEffect) { m_ReticleEffect.CurrentSpriteName = "portable_elevator_reticle_red"; }
            }
        }

        protected override void OnPreDrop(PlayerController user) {
            base.OnPreDrop(user);
            if (m_extantReticleQuad) { Destroy(m_extantReticleQuad.gameObject); }
            if (m_WasUsed) { Destroy(gameObject); }
        }

        protected override void DoEffect(PlayerController user) {
            IsCurrentlyActive = true;
            m_currentUser = user;
            GameObject reticleObject = Instantiate(ExpandPrefabs.EXPortableElevator_Reticle);
            m_extantReticleQuad = reticleObject.GetComponent<tk2dBaseSprite>();
            m_ReticleEffect = reticleObject.GetComponent<ExpandReticleRiserEffect>();
            m_currentAngle = BraveMathCollege.Atan2Degrees(m_currentUser.unadjustedAimPoint.XY() - m_currentUser.CenterPosition);
            m_currentDistance = 5f;
            UpdateReticlePosition();
            spriteAnimator.Play("Activate");
        }

        protected override void DoActiveEffect(PlayerController user) {
            if (user && user.CurrentRoom != null) {
                if (!CanDropElevatorHere(user, (m_extantReticleQuad.gameObject.transform.position + new Vector3(1f, 1f)))) {
                    AkSoundEngine.PostEvent("Play_OBJ_purchase_unable_01", user.gameObject);
                    return;
                }
                Vector2 cachedPosition = m_extantReticleQuad.gameObject.transform.position + new Vector3(0, 0.25f);
                if (m_extantReticleQuad) { Destroy(m_extantReticleQuad.gameObject); }
                IsCurrentlyActive = true;
                AkSoundEngine.PostEvent("Play_OBJ_computer_boop_01", user.gameObject);
                DoElevatorDrop(cachedPosition, user.CurrentRoom);
                IsCurrentlyActive = false;
                m_WasUsed = true;
                user.DropActiveItem(this);
            }
        }
        

        private void DoElevatorDrop(Vector2 currentTarget, RoomHandler targetRoom) {
            GameObject m_ElevatorOBJ = Instantiate(ExpandPrefabs.EXPortableElevator_Departure, currentTarget, Quaternion.identity);
            ExpandNewElevatorController m_ElevatorController = m_ElevatorOBJ.GetComponent<ExpandNewElevatorController>();
            m_ElevatorController.OverrideFloorName = BraveUtility.RandomElement(ValidDestinations.Shuffle());
            m_ElevatorController.ConfigureOnPlacement(targetRoom);

            if(Random.value <= 0.004f) {
                if (m_ElevatorOBJ.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                    foreach (tk2dBaseSprite baseSprite in m_ElevatorOBJ.GetComponentsInChildren<tk2dBaseSprite>(true)) {
                        ExpandShaders.Instance.ApplyGlitchShader(baseSprite);
                    }
                }
            }
            m_ElevatorController.IsGlitchElevator = true;
        }

        protected override void OnDestroy() {
            if (m_extantReticleQuad) { Destroy(m_extantReticleQuad.gameObject); }
            base.OnDestroy();
        }
    }
}

