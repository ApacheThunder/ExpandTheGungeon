using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;
using UnityEngine;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ExpandPrefab {

    public static class ExpandSecretDoorPrefabs {

        public static GameObject EXSecretDoorMinimapIcon;

        public static GameObject EXSecretDoor_Hollow;
        public static GameObject EXSecretDoor_Hollow_Unlocked;
        public static GameObject EXSecretDoor;
        public static GameObject EXSecretDoor_Unlocked;
        public static GameObject EXSecretDoorAnimation;

        private static readonly List<string> m_DoorOpenSprites = new List<string>() {
            "EXSecretDoor_Open_00",
            "EXSecretDoor_Open_01",
            "EXSecretDoor_Open_02",
            "EXSecretDoor_Open_03",
            "EXSecretDoor_Open_04",
            "EXSecretDoor_Open_05",
            "EXSecretDoor_Open_06",
            "EXSecretDoor_Open_07"
        };

        private static readonly List<string> m_DoorCloseSprites = new List<string>() {
            "EXSecretDoor_Close_00",
            "EXSecretDoor_Close_01",
            "EXSecretDoor_Close_02",
            "EXSecretDoor_Close_03",
            "EXSecretDoor_Close_04",
            "EXSecretDoor_Close_05",
            "EXSecretDoor_Close_06",
            "EXSecretDoor_Close_07",
        };

        public static void InitPrefabs(AssetBundle expandSharedAssets1) {

            EXSecretDoorAnimation = expandSharedAssets1.LoadAsset<GameObject>("EX_SecretDoor_Animation");

            tk2dSpriteAnimation DoorSpriteAnimations = EXSecretDoorAnimation.AddComponent<tk2dSpriteAnimation>();
            
            ExpandUtility.AddAnimation(DoorSpriteAnimations, ExpandPrefabs.EXSecretDoorCollection.GetComponent<tk2dSpriteCollectionData>(), m_DoorOpenSprites, "door_open", frameRate: 10);
            ExpandUtility.AddAnimation(DoorSpriteAnimations, ExpandPrefabs.EXSecretDoorCollection.GetComponent<tk2dSpriteCollectionData>(), m_DoorCloseSprites, "door_close", frameRate: 10);

            EXSecretDoorMinimapIcon = expandSharedAssets1.LoadAsset<GameObject>("EXSecretDoor_MinimapIcon");
            SpriteSerializer.AddSpriteToObject(EXSecretDoorMinimapIcon, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_MinimapIcon");

            EXSecretDoor_Hollow = expandSharedAssets1.LoadAsset<GameObject>("EX_Secret_Door_Hollow");
            GameObject EXSecretDoorHollow_Frame_Top = EXSecretDoor_Hollow.transform.Find("FrameTop").gameObject;
            GameObject EXSecretDoorHollow_Frame_Bottom = EXSecretDoor_Hollow.transform.Find("FrameBottom").gameObject;
            GameObject EXSecretDoorHollow_Background = EXSecretDoor_Hollow.transform.Find("Background").gameObject;
            GameObject EXSecretDoorHollow_Light = EXSecretDoor_Hollow.transform.Find("Light").gameObject;
            GameObject EXSecretDoorHollow_Lock = EXSecretDoor_Hollow.transform.Find("Lock").gameObject;


            tk2dSprite m_DoorHollowSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoor_Hollow, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Open_00");
            tk2dSprite m_DoorHollowBorderTopSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorHollow_Frame_Top, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Frame_Top");
            tk2dSprite m_DoorHollowBorderBottomSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorHollow_Frame_Bottom, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Frame_Bottom");
            tk2dSprite m_DoorHollowBackgroundSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorHollow_Background, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Background");
            tk2dSprite m_DoorHollowLightSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorHollow_Light, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Light_Red");
            m_DoorHollowBorderTopSprite.HeightOffGround = 3;
            m_DoorHollowBorderBottomSprite.HeightOffGround = -0.5f;
            m_DoorHollowSprite.HeightOffGround = -1.5f;
            m_DoorHollowBackgroundSprite.HeightOffGround = -2f;
            m_DoorHollowLightSprite.HeightOffGround = 3.5f;
            
            ExpandUtility.GenerateSpriteAnimator(EXSecretDoor_Hollow, DoorSpriteAnimations, ClipFps: 10);
            
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Hollow, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 64), offset: new IntVector2(16, 0));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Hollow, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 14));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Hollow, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 12));

            ExpandSecretDoorPlacable m_SecretDoorHollowComponent = EXSecretDoor_Hollow.AddComponent<ExpandSecretDoorPlacable>();
            m_SecretDoorHollowComponent.DoorTopBorderObject = EXSecretDoorHollow_Frame_Top;
            m_SecretDoorHollowComponent.DoorBottomBorderObject = EXSecretDoorHollow_Frame_Bottom;
            m_SecretDoorHollowComponent.DoorBackgroundObject = EXSecretDoorHollow_Background;
            m_SecretDoorHollowComponent.DoorLightObject = EXSecretDoorHollow_Light;
            
            GameObject m_RatLock = ExpandPrefabs.RatJailDoor.GetComponent<InteractableDoorController>().WorldLocks[0].gameObject;
            
            tk2dSprite EXLockSprite = EXSecretDoorHollow_Lock.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(EXLockSprite, m_RatLock.GetComponent<tk2dSprite>());
            ExpandUtility.DuplicateSpriteAnimator(EXSecretDoorHollow_Lock, m_RatLock.GetComponent<tk2dSpriteAnimator>());
            EXLockSprite.HeightOffGround = -0.3f;
            

            InteractableLock m_EXLockHollow = EXSecretDoorHollow_Lock.AddComponent<InteractableLock>();
            m_EXLockHollow.Suppress = m_RatLock.GetComponent<InteractableLock>().Suppress;
            m_EXLockHollow.lockMode = InteractableLock.InteractableLockMode.RAT_REWARD;
            m_EXLockHollow.JailCellKeyId = m_RatLock.GetComponent<InteractableLock>().JailCellKeyId;
            m_EXLockHollow.IdleAnimName = m_RatLock.GetComponent<InteractableLock>().IdleAnimName;
            m_EXLockHollow.UnlockAnimName = m_RatLock.GetComponent<InteractableLock>().UnlockAnimName;
            m_EXLockHollow.NoKeyAnimName = m_RatLock.GetComponent<InteractableLock>().NoKeyAnimName;
            m_EXLockHollow.SpitAnimName = m_RatLock.GetComponent<InteractableLock>().SpitAnimName;
            m_EXLockHollow.BustedAnimName = m_RatLock.GetComponent<InteractableLock>().BustedAnimName;
            m_SecretDoorHollowComponent.Lock = m_EXLockHollow;
            EXSecretDoor_Hollow.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            
            EXSecretDoor_Hollow_Unlocked = expandSharedAssets1.LoadAsset<GameObject>("EX_Secret_Door_Hollow_Unlocked");
            GameObject EXSecretDoorHollowUnlocked_Frame_Top = EXSecretDoor_Hollow_Unlocked.transform.Find("FrameTop").gameObject;
            GameObject EXSecretDoorHollowUnlocked_Frame_Bottom = EXSecretDoor_Hollow_Unlocked.transform.Find("FrameBottom").gameObject;
            GameObject EXSecretDoorHollowUnlocked_Background = EXSecretDoor_Hollow_Unlocked.transform.Find("Background").gameObject;
            GameObject EXSecretDoorHollowUnlocked_Light = EXSecretDoor_Hollow_Unlocked.transform.Find("Light").gameObject;
            
            tk2dSprite m_DoorHollow_UnlockedSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoor_Hollow_Unlocked, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Open_00");
            tk2dSprite m_DoorHollow_UnlockedBorderTopSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorHollowUnlocked_Frame_Top, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Frame_Top");
            tk2dSprite m_DoorHollow_UnlockedBorderBottomSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorHollowUnlocked_Frame_Bottom, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Frame_Bottom");
            tk2dSprite m_DoorHollow_UnlockedBackgroundSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorHollowUnlocked_Background, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Background");
            tk2dSprite m_DoorHollow_UnlockedLightSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorHollowUnlocked_Light, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Light_Red");
            m_DoorHollow_UnlockedBorderTopSprite.HeightOffGround = 3;
            m_DoorHollow_UnlockedBorderBottomSprite.HeightOffGround = -0.5f;
            m_DoorHollow_UnlockedSprite.HeightOffGround = -1.5f;
            m_DoorHollow_UnlockedBackgroundSprite.HeightOffGround = -2f;
            m_DoorHollow_UnlockedLightSprite.HeightOffGround = 3.5f;

            ExpandUtility.GenerateSpriteAnimator(EXSecretDoor_Hollow_Unlocked, DoorSpriteAnimations, ClipFps: 10);
                        
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Hollow_Unlocked, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 64), offset: new IntVector2(16, 0));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Hollow_Unlocked, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 14));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Hollow_Unlocked, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 12));

            ExpandSecretDoorPlacable m_SecretDoorHollow_UnlockedComponent = EXSecretDoor_Hollow_Unlocked.AddComponent<ExpandSecretDoorPlacable>();
            m_SecretDoorHollow_UnlockedComponent.DoorTopBorderObject = EXSecretDoorHollowUnlocked_Frame_Top;
            m_SecretDoorHollow_UnlockedComponent.DoorBottomBorderObject = EXSecretDoorHollowUnlocked_Frame_Bottom;
            m_SecretDoorHollow_UnlockedComponent.DoorBackgroundObject = EXSecretDoorHollowUnlocked_Background;
            m_SecretDoorHollow_UnlockedComponent.DoorLightObject = EXSecretDoorHollowUnlocked_Light;
            EXSecretDoor_Hollow_Unlocked.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            


            EXSecretDoor = expandSharedAssets1.LoadAsset<GameObject>("EX_Secret_Door");
            GameObject EXSecretDoor_Frame_Top = EXSecretDoor.transform.Find("FrameTop").gameObject;
            GameObject EXSecretDoor_Frame_Bottom = EXSecretDoor.transform.Find("FrameBottom").gameObject;
            GameObject EXSecretDoor_Background = EXSecretDoor.transform.Find("Background").gameObject;
            GameObject EXSecretDoor_Light = EXSecretDoor.transform.Find("Light").gameObject;
            GameObject EXSecretDoor_Lock = EXSecretDoor.transform.Find("Lock").gameObject;


            tk2dSprite m_DoorSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoor, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Open_00");
            tk2dSprite m_DoorBorderTopSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoor_Frame_Top, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Frame_NoDecal_Top");
            tk2dSprite m_DoorBorderBottomSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoor_Frame_Bottom, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Frame_Bottom");
            tk2dSprite m_DoorBackgroundSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoor_Background, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Background");
            tk2dSprite m_DoorLightSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoor_Light, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Light_Red");
            m_DoorBorderTopSprite.HeightOffGround = 3;
            m_DoorBorderBottomSprite.HeightOffGround = -0.5f;
            m_DoorSprite.HeightOffGround = -1.5f;
            m_DoorBackgroundSprite.HeightOffGround = -2f;
            m_DoorLightSprite.HeightOffGround = 3.5f;


            ExpandUtility.GenerateSpriteAnimator(EXSecretDoor, DoorSpriteAnimations, ClipFps: 10);

            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 64), offset: new IntVector2(16, 0));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 14));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 12));

            ExpandSecretDoorPlacable m_SecretDoorComponent = EXSecretDoor.AddComponent<ExpandSecretDoorPlacable>();
            m_SecretDoorComponent.DoorTopBorderObject = EXSecretDoor_Frame_Top;
            m_SecretDoorComponent.DoorBottomBorderObject = EXSecretDoor_Frame_Bottom;
            m_SecretDoorComponent.DoorBackgroundObject = EXSecretDoor_Background;
            m_SecretDoorComponent.DoorLightObject = EXSecretDoor_Light;

            Dungeon Base_Castle = DungeonDatabase.GetOrLoadByName("Base_Castle");
            GameObject m_NormalLock = (Base_Castle.PatternSettings.flows[0].sharedInjectionData[1].InjectionData[0].exactRoom.placedObjects[0].nonenemyBehaviour as SecretFloorInteractableController).WorldLocks[0].gameObject;

            tk2dSprite EXLockNormalSprite = EXSecretDoor_Lock.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(EXLockNormalSprite, m_NormalLock.GetComponent<tk2dSprite>());
            ExpandUtility.DuplicateSpriteAnimator(EXSecretDoor_Lock, m_NormalLock.GetComponent<tk2dSpriteAnimator>());
            EXLockNormalSprite.HeightOffGround = 1.7f;

            InteractableLock m_EXLockNormal = EXSecretDoor_Lock.AddComponent<InteractableLock>();
            m_EXLockNormal.Suppress = m_NormalLock.GetComponent<InteractableLock>().Suppress;
            m_EXLockNormal.lockMode = m_NormalLock.GetComponent<InteractableLock>().lockMode;
            m_EXLockNormal.JailCellKeyId = m_NormalLock.GetComponent<InteractableLock>().JailCellKeyId;
            m_EXLockNormal.IdleAnimName = m_NormalLock.GetComponent<InteractableLock>().IdleAnimName;
            m_EXLockNormal.UnlockAnimName = m_NormalLock.GetComponent<InteractableLock>().UnlockAnimName;
            m_EXLockNormal.NoKeyAnimName = m_NormalLock.GetComponent<InteractableLock>().NoKeyAnimName;
            m_EXLockNormal.SpitAnimName = m_NormalLock.GetComponent<InteractableLock>().SpitAnimName;
            m_EXLockNormal.BustedAnimName = m_NormalLock.GetComponent<InteractableLock>().BustedAnimName;

            m_SecretDoorComponent.Lock = m_EXLockNormal;
            EXSecretDoor.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
                        
            Base_Castle = null;
            m_NormalLock = null;
            
            EXSecretDoor_Unlocked = expandSharedAssets1.LoadAsset<GameObject>("EX_Secret_Door_Unlocked");
            GameObject EXSecretDoorUnlocked_Frame_Top = EXSecretDoor_Unlocked.transform.Find("FrameTop").gameObject;
            GameObject EXSecretDoorUnlocked_Frame_Bottom = EXSecretDoor_Unlocked.transform.Find("FrameBottom").gameObject;
            GameObject EXSecretDoorUnlocked_Background = EXSecretDoor_Unlocked.transform.Find("Background").gameObject;
            GameObject EXSecretDoorUnlocked_Light = EXSecretDoor_Unlocked.transform.Find("Light").gameObject;

            tk2dSprite m_Door_UnlockedSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoor_Unlocked, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Open_00");
            tk2dSprite m_Door_UnlockedBorderTopSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorUnlocked_Frame_Top, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Frame_Top");
            tk2dSprite m_Door_UnlockedBorderBottomSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorUnlocked_Frame_Bottom, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Frame_Bottom");
            tk2dSprite m_Door_UnlockedBackgroundSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorUnlocked_Background, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Background");
            tk2dSprite m_Door_UnlockedLightSprite = SpriteSerializer.AddSpriteToObject(EXSecretDoorUnlocked_Light, ExpandPrefabs.EXSecretDoorCollection, "EXSecretDoor_Light_Red");
            m_Door_UnlockedBorderTopSprite.HeightOffGround = 3;
            m_Door_UnlockedBorderBottomSprite.HeightOffGround = -0.5f;
            m_Door_UnlockedSprite.HeightOffGround = -1.5f;
            m_Door_UnlockedBackgroundSprite.HeightOffGround = -2f;
            m_Door_UnlockedLightSprite.HeightOffGround = 3.5f;

            ExpandUtility.GenerateSpriteAnimator(EXSecretDoor_Unlocked, DoorSpriteAnimations, ClipFps: 10);

            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Unlocked, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 64), offset: new IntVector2(16, 0));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Unlocked, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 14));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor_Unlocked, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 12));

            ExpandSecretDoorPlacable m_SecretDoor_UnlockedComponent = EXSecretDoor_Unlocked.AddComponent<ExpandSecretDoorPlacable>();
            m_SecretDoor_UnlockedComponent.DoorTopBorderObject = EXSecretDoorUnlocked_Frame_Top;
            m_SecretDoor_UnlockedComponent.DoorBottomBorderObject = EXSecretDoorUnlocked_Frame_Bottom;
            m_SecretDoor_UnlockedComponent.DoorBackgroundObject = EXSecretDoorUnlocked_Background;
            m_SecretDoor_UnlockedComponent.DoorLightObject = EXSecretDoorUnlocked_Light;
            EXSecretDoor_Unlocked.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
        }

    }

    public class ExpandSecretDoorPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {
        
        public ExpandSecretDoorPlacable() {
            placeableWidth = 4;
            placeableHeight = 4;
            difficulty = PlaceableDifficulty.BASE;
            isPassable = true;

            ManuallyAssigned = false;
            
            m_IsRecievingPlayer = false;
            m_Disabled = false;
            m_InUse = false;
            m_spawnUnlocked = false;
            m_WaitingForPlayer = false;
        }

        public bool ManuallyAssigned;

        [NonSerialized]
        public bool m_IsRecievingPlayer;

        [NonSerialized]
        public bool m_Disabled;

        [NonSerialized]
        public bool m_InUse;

        [NonSerialized]
        public bool m_WaitingForPlayer;

        public InteractableLock Lock;
        public GameObject MinimapIcon;
        public GameObject DoorTopBorderObject;
        public GameObject DoorBottomBorderObject;
        public GameObject DoorBackgroundObject;
        public GameObject DoorLightObject;
        
        [NonSerialized]
        public ExpandSecretDoorPlacable m_DestinationDoor;

        [NonSerialized]
        public Vector3? m_Destination;
        
        [NonSerialized]
        public PlayerController m_RecievedPlayer;

        [NonSerialized]
        private RoomHandler m_parentRoom;

        [NonSerialized]
        private bool m_spawnUnlocked;
        
        [NonSerialized]
        private tk2dSprite m_doorLightSprite;

        private bool m_IsLocked {
            get {
                if (m_spawnUnlocked) { return false; } else if (Lock && (Lock.IsBusted | Lock.IsLocked)) { return true; }
                return false;
            }    
        }

        public IEnumerator Start() {
            while (Dungeon.IsGenerating | GameManager.Instance.IsLoadingLevel) { yield return null; }

            if (!ManuallyAssigned) { TryFindOtherDoor(); }

            yield return null;
            
            if (!m_DestinationDoor | !m_Destination.HasValue) {
                ETGModConsole.Log("[ExpandTheGungeon] [" + gameObject.name + "] ERROR: Destination Door was not found!", true);
                m_Disabled = true;
                yield break;
            }

            if (!specRigidbody) { m_Disabled = true; yield break; }

            if (!m_spawnUnlocked && Lock) {
                m_parentRoom.RegisterInteractable(Lock);
                Lock.OnUnlocked = MakeReadyForPlayer;
            }

            if (MinimapIcon) {
                Minimap.Instance.RegisterRoomIcon(m_parentRoom, MinimapIcon, false);
            } else if (m_spawnUnlocked) {
                Minimap.Instance.RegisterRoomIcon(m_parentRoom, ExpandPrefabs.exit_room_basic.associatedMinimapIcon, false);
            }
            yield break;
        }
        
        private void LateUpdate() {
            if (m_Disabled && !m_InUse) { m_InUse = true; Destroy(this); return; }
            if (!m_InUse && !m_Disabled && m_IsRecievingPlayer && m_RecievedPlayer) {
                m_InUse = true;
                StartCoroutine(ReceivePlayer(m_RecievedPlayer));
                return;
            } else {
                return;
            }
        }
        

        private void TryFindOtherDoor() {
            ExpandSecretDoorPlacable[] Doors = FindObjectsOfType<ExpandSecretDoorPlacable>();
            if (Doors == null | Doors.Length <= 0) { m_Disabled = true; return; }
            foreach (ExpandSecretDoorPlacable door in Doors) {
                if (door != this) {
                    m_DestinationDoor = door;
                    m_Destination = (m_DestinationDoor.transform.position + new Vector3(1.25f, 0.6f));
                    return;
                }
            }
            m_Disabled = true;
            return;
        }


        public void ConfigureOnPlacement(RoomHandler room) {
            m_parentRoom = room;

            DoorTopBorderObject.transform.SetParent(room.hierarchyParent, true);
            DoorBottomBorderObject.transform.SetParent(room.hierarchyParent, true);
            DoorBackgroundObject.transform.SetParent(room.hierarchyParent, true);

            if (!Lock) { m_spawnUnlocked = true; }

            m_doorLightSprite = DoorLightObject.GetComponent<tk2dSprite>();

            transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform, true);

        }
        
        public void Interact(PlayerController interactor) {
            if (m_IsLocked | m_WaitingForPlayer) {
                return;
            } else if (!m_Disabled && !m_InUse && interactor && !m_parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                MakeReadyForPlayer();
            }
        }

        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) { shouldBeFlipped = false; return string.Empty; }

        public float GetOverrideMaxDistance() { return -1f; }

        public float GetDistanceToPoint(Vector2 point) {
            if (m_InUse | m_IsRecievingPlayer | m_IsLocked | m_Disabled | m_WaitingForPlayer | m_parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                return 1000f;
            } else {
                Bounds bounds = sprite.GetBounds();
                bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
                float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
                float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
                return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
            }
        }

        public void OnEnteredRange(PlayerController interactor) {
            if (!this | m_InUse | m_IsRecievingPlayer | m_WaitingForPlayer | m_IsLocked | m_Disabled) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(m_doorLightSprite, false);
            SpriteOutlineManager.AddOutlineToSprite(m_doorLightSprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            m_doorLightSprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(m_doorLightSprite, false);
            SpriteOutlineManager.AddOutlineToSprite(m_doorLightSprite, Color.black, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            m_doorLightSprite.UpdateZDepth();
        }
        

        private void HandleTriggerCollision(SpeculativeRigidbody Rigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if ((m_InUse | m_IsLocked) | !m_WaitingForPlayer) { return; }
            PlayerController player = Rigidbody.GetComponent<PlayerController>();
            PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(player);
            if (player) {
                m_InUse = true;
                if (player.IsDodgeRolling) { player.ForceStopDodgeRoll(); }
                player.SetInputOverride("Entering Elevator");
                if (otherPlayer) {
                    if (otherPlayer.IsDodgeRolling) { otherPlayer.ForceStopDodgeRoll(); }
                    otherPlayer.SetInputOverride("Entering Elevator");
                }
                if (m_doorLightSprite) { m_doorLightSprite.SetSprite("EXSecretDoor_Light_Red"); }
                if (sprite) { sprite.HeightOffGround = 3f; sprite.UpdateZDepth(); }
                specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Remove(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                StartCoroutine(SendPlayer(player));
            }
        }

        private void MakeReadyForPlayer() {
            if (m_parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { return; }
            m_WaitingForPlayer = true;
            specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
            if (m_doorLightSprite) { m_doorLightSprite.SetSprite("EXSecretDoor_Light_Green"); }
            AkSoundEngine.PostEvent("Play_OBJ_cardoor_open_01", gameObject);
            StartCoroutine(HandleOpen());
            return;
        }
                
        public void Reset() {
            specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Remove(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
            m_WaitingForPlayer = false;
        }

        private IEnumerator HandleOpen() {
            if (spriteAnimator) {
                spriteAnimator.Play("door_open");
                while (spriteAnimator.IsPlaying("door_open")) { yield return null; }
            }
            specRigidbody.PixelColliders[0].Enabled = false;
            sprite.HeightOffGround = -1.5f;
            sprite.UpdateZDepth();
            yield break;
        }
        

        private IEnumerator SendPlayer(PlayerController player) {
            if (spriteAnimator) {
                AkSoundEngine.PostEvent("Play_OBJ_cardoor_close_01", gameObject);
                spriteAnimator.Play("door_close");
                while (spriteAnimator.IsPlaying("door_close")) { yield return null; }
            }
            m_WaitingForPlayer = false;
            if (m_DestinationDoor.m_WaitingForPlayer) { m_DestinationDoor.Reset(); }
            Pixelator.Instance.FadeToBlack(0.25f, false, 0f);
            yield return new WaitForSeconds(0.35f);
            player.WarpToPointAndBringCoopPartner(m_Destination.Value, false, true);
            if (!m_DestinationDoor.enabled) { m_DestinationDoor.enabled = true; }
            m_DestinationDoor.m_RecievedPlayer = player;
            m_DestinationDoor.m_IsRecievingPlayer = true;
            specRigidbody.PixelColliders[0].Enabled = true;
            if (sprite) { sprite.HeightOffGround = -1.5f; sprite.UpdateZDepth(); }
            m_InUse = false;
            yield break;
        }
        
        private IEnumerator ReceivePlayer(PlayerController player) {
            if (sprite) { sprite.HeightOffGround = 3f; sprite.UpdateZDepth(); }
            PlayerController otherPlayer = GameManager.Instance.GetOtherPlayer(player);
            yield return null;
            if (otherPlayer) {
                otherPlayer.forceAimPoint = Vector2.down;
                otherPlayer.ForceStaticFaceDirection(Vector2.down);
            }
            player.forceAimPoint = Vector2.down;
            player.ForceStaticFaceDirection(Vector2.down);
            if (m_doorLightSprite) { m_doorLightSprite.SetSprite("EXSecretDoor_Light_Red"); }
            if (sprite) { sprite.SetSprite("EXSecretDoor_Close_07"); }
            if (!specRigidbody.PixelColliders[0].Enabled) { specRigidbody.PixelColliders[0].Enabled = true; }
            yield return new WaitForSeconds(0.3f);
            Pixelator.Instance.FadeToBlack(0.25f, true, 0f);
            yield return new WaitForSeconds(0.35f);
            if (m_IsLocked && Lock) {
                Lock.OnUnlocked = null;
                Lock.ForceUnlock();
                while (Lock.IsLocked) { yield return null; }
            }
            if (spriteAnimator) {
                AkSoundEngine.PostEvent("Play_OBJ_cardoor_open_01", gameObject);
                spriteAnimator.Play("door_open");
                while (spriteAnimator.IsPlaying("door_open")) { yield return null; }
            }
            AkSoundEngine.PostEvent("Play_EX_ElevatorBell_01", gameObject);
            if (sprite) { sprite.HeightOffGround = -1.5f; sprite.UpdateZDepth(); }
            yield return new WaitForSeconds(0.1f);
            player.ForceMoveInDirectionUntilThreshold(Vector2.down, player.CenterPosition.y - 1.5f, 0, 0.6f, new List<SpeculativeRigidbody>() { specRigidbody });
            if (otherPlayer) {
                otherPlayer.ForceMoveInDirectionUntilThreshold(Vector2.down, GameManager.Instance.GetOtherPlayer(player).CenterPosition.y - 1.5f, 0, 0.6f, new List<SpeculativeRigidbody>() { specRigidbody });
            }
            yield return new WaitForSeconds(0.6f);
            if (spriteAnimator) {
                AkSoundEngine.PostEvent("Play_OBJ_cardoor_close_01", gameObject);
                spriteAnimator.Play("door_close");
                while (spriteAnimator.IsPlaying("door_close")) { yield return null; }
            }
            if (m_doorLightSprite) { m_doorLightSprite.SetSprite("EXSecretDoor_Light_Green"); }
            player.forceAimPoint = null;
            player.ClearAllInputOverrides();
            if (otherPlayer) {
                otherPlayer.forceAimPoint = null;
                otherPlayer.ClearAllInputOverrides();
            }
            m_IsRecievingPlayer = false;
            m_InUse = false;
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

