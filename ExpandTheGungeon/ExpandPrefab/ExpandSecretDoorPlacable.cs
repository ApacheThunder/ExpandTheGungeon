using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ItemAPI;
using UnityEngine;

namespace ExpandTheGungeon.ExpandPrefab {

    public static class ExpandSecretDoorPrefabs {

        public static GameObject EXSecretDoorMinimapIcon;

        public static GameObject EXSecretDoor;
        public static GameObject EXSecretDoor_Normal;
        public static GameObject EXSecretDoorDestination;
        public static GameObject EXSecretDoor_Frame_Top;
        public static GameObject EXSecretDoor_Frame_Bottom;
        public static GameObject EXSecretDoor_Background;
        public static GameObject EXSecretDoor_Light;
        public static GameObject EXSecretDoor_Lock;
        

        public static void InitPrefabs(AssetBundle expandSharedAssets1) {
            
            List<string> m_DoorOpenSprites = new List<string>() {
                "EXSecretDoor_Open_00",
                "EXSecretDoor_Open_01",
                "EXSecretDoor_Open_02",
                "EXSecretDoor_Open_03",
                "EXSecretDoor_Open_04",
                "EXSecretDoor_Open_05",
                "EXSecretDoor_Open_06",
                "EXSecretDoor_Open_07"
            };

            List<string> m_DoorCloseSprites = new List<string>() {
                "EXSecretDoor_Close_00",
                "EXSecretDoor_Close_01",
                "EXSecretDoor_Close_02",
                "EXSecretDoor_Close_03",
                "EXSecretDoor_Close_04",
                "EXSecretDoor_Close_05",
                "EXSecretDoor_Close_06",
                "EXSecretDoor_Close_07",
            };

            EXSecretDoorMinimapIcon = expandSharedAssets1.LoadAsset<GameObject>("EXSecretDoor_MinimapIcon");
            ItemBuilder.AddSpriteToObject(EXSecretDoorMinimapIcon, expandSharedAssets1.LoadAsset<Texture2D>("EXSecretDoor_MinimapIcon"));

            EXSecretDoor = expandSharedAssets1.LoadAsset<GameObject>("EX Secret Door Entrance");            
            EXSecretDoor_Frame_Top = EXSecretDoor.transform.Find("EX Secret Door Top").gameObject;
            EXSecretDoor_Frame_Bottom = EXSecretDoor.transform.Find("EX Secret Door Bottom").gameObject;
            EXSecretDoor_Background = EXSecretDoor.transform.Find("EX Secret Door Background").gameObject;
            EXSecretDoor_Light = EXSecretDoor.transform.Find("EX Secret Door Light").gameObject;
            EXSecretDoor_Frame_Top.layer = LayerMask.NameToLayer("FG_Critical");
            EXSecretDoor_Frame_Bottom.layer = LayerMask.NameToLayer("FG_Critical");
            EXSecretDoor_Background.layer = LayerMask.NameToLayer("FG_Critical");
            EXSecretDoor_Light.layer = LayerMask.NameToLayer("FG_Critical");
            EXSecretDoor_Frame_Top.transform.parent = EXSecretDoor.transform;
            EXSecretDoor_Frame_Bottom.transform.parent = EXSecretDoor.transform;
            EXSecretDoor_Background.transform.parent = EXSecretDoor.transform;
            EXSecretDoor_Light.transform.parent = EXSecretDoor.transform;

            ItemBuilder.AddSpriteToObject(EXSecretDoor, expandSharedAssets1.LoadAsset<Texture2D>("EXSecretDoor_Open_00"));
            ItemBuilder.AddSpriteToObject(EXSecretDoor_Frame_Top, expandSharedAssets1.LoadAsset<Texture2D>("EXSecretDoor_Frame_Top"));
            ItemBuilder.AddSpriteToObject(EXSecretDoor_Frame_Bottom, expandSharedAssets1.LoadAsset<Texture2D>("EXSecretDoor_Frame_Bottom"));
            ItemBuilder.AddSpriteToObject(EXSecretDoor_Background, expandSharedAssets1.LoadAsset<Texture2D>("EXSecretDoor_Background"));
            ItemBuilder.AddSpriteToObject(EXSecretDoor_Light, expandSharedAssets1.LoadAsset<Texture2D>("EXSecretDoor_Light_Red"));


            tk2dSprite m_DoorBorderTopSprite = EXSecretDoor_Frame_Top.GetComponent<tk2dSprite>();
            m_DoorBorderTopSprite.HeightOffGround = 3;
            SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>("EXSecretDoor_Frame_NoDecal_Top"), m_DoorBorderTopSprite.Collection);

            tk2dSprite m_DoorBorderBottomSprite = EXSecretDoor_Frame_Bottom.GetComponent<tk2dSprite>();
            m_DoorBorderBottomSprite.HeightOffGround = -0.5f;
            
            tk2dSprite m_DoorSprite = EXSecretDoor.GetComponent<tk2dSprite>();
            m_DoorSprite.HeightOffGround = -1.5f;

            tk2dSprite m_DoorBackgroundSprite = EXSecretDoor_Background.GetComponent<tk2dSprite>();
            m_DoorBackgroundSprite.HeightOffGround = -2f;

            tk2dSprite m_DoorLightSprite = EXSecretDoor_Light.GetComponent<tk2dSprite>();
            m_DoorLightSprite.HeightOffGround = 3.5f;
               
            SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>("EXSecretDoor_Light_Green"), m_DoorLightSprite.Collection);
            
            foreach (string spriteName in m_DoorOpenSprites) {
                if (spriteName != "EXSecretDoor_Open_00") {
                    SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_DoorSprite.Collection);
                }
            }
            foreach (string spriteName in m_DoorCloseSprites) { SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(spriteName), m_DoorSprite.Collection); }

            ExpandUtility.GenerateSpriteAnimator(EXSecretDoor, ClipFps: 10);

            tk2dSpriteAnimator m_DoorAnimator = EXSecretDoor.GetComponent<tk2dSpriteAnimator>();
            ExpandUtility.AddAnimation(m_DoorAnimator, m_DoorSprite.Collection, m_DoorOpenSprites, "door_open", frameRate: 10);
            ExpandUtility.AddAnimation(m_DoorAnimator, m_DoorSprite.Collection, m_DoorCloseSprites, "door_close", frameRate: 10);

            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 64), offset: new IntVector2(16, 0));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 14));
            ExpandUtility.GenerateOrAddToRigidBody(EXSecretDoor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, CanBeCarried: false, IsTrigger: true, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 32), offset: new IntVector2(16, 12));

            ExpandSecretDoorPlacable m_SecretDoorComponent = EXSecretDoor.AddComponent<ExpandSecretDoorPlacable>();
            m_SecretDoorComponent.DoorTopBorderObject = EXSecretDoor_Frame_Top;
            m_SecretDoorComponent.DoorBottomBorderObject = EXSecretDoor_Frame_Bottom;
            m_SecretDoorComponent.DoorBackgroundObject = EXSecretDoor_Background;
            m_SecretDoorComponent.DoorLightObject = EXSecretDoor_Light;
            
            GameObject m_RatLock = ExpandPrefabs.RatJailDoor.GetComponent<InteractableDoorController>().WorldLocks[0].gameObject;
            
            EXSecretDoor_Lock = EXSecretDoor.transform.Find("EX Secret Door Lock").gameObject;
            tk2dSprite EXLockSprite = EXSecretDoor_Lock.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(EXLockSprite, m_RatLock.GetComponent<tk2dSprite>());
            EXLockSprite.HeightOffGround = -0.3f;

            tk2dSpriteAnimator m_EXLockAnimator = ExpandUtility.DuplicateSpriteAnimator(EXSecretDoor_Lock, m_RatLock.GetComponent<tk2dSpriteAnimator>(), true);

            InteractableLock m_EXLock = EXSecretDoor_Lock.AddComponent<InteractableLock>();
            m_EXLock.Suppress = m_RatLock.GetComponent<InteractableLock>().Suppress;
            m_EXLock.lockMode = InteractableLock.InteractableLockMode.RESOURCEFUL_RAT;
            m_EXLock.JailCellKeyId = m_RatLock.GetComponent<InteractableLock>().JailCellKeyId;
            m_EXLock.IdleAnimName = m_RatLock.GetComponent<InteractableLock>().IdleAnimName;
            m_EXLock.UnlockAnimName = m_RatLock.GetComponent<InteractableLock>().UnlockAnimName;
            m_EXLock.NoKeyAnimName = m_RatLock.GetComponent<InteractableLock>().NoKeyAnimName;
            m_EXLock.SpitAnimName = m_RatLock.GetComponent<InteractableLock>().SpitAnimName;
            m_EXLock.BustedAnimName = m_RatLock.GetComponent<InteractableLock>().BustedAnimName;

            m_SecretDoorComponent.Lock = m_EXLock;
            EXSecretDoor_Lock.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            
            EXSecretDoorDestination = UnityEngine.Object.Instantiate(EXSecretDoor);
            EXSecretDoorDestination.name = "EX Secret Door (Exit)";
            ExpandSecretDoorExitPlacable m_ExitDoorComponent = EXSecretDoorDestination.AddComponent<ExpandSecretDoorExitPlacable>();
            m_ExitDoorComponent.MinimapIcon = ExpandPrefabs.exit_room_basic.associatedMinimapIcon;
                
            m_ExitDoorComponent.DoorBackgroundObject = EXSecretDoorDestination.GetComponent<ExpandSecretDoorPlacable>().DoorBackgroundObject;
            m_ExitDoorComponent.DoorBottomBorderObject = EXSecretDoorDestination.GetComponent<ExpandSecretDoorPlacable>().DoorBottomBorderObject;
            m_ExitDoorComponent.DoorLightObject = EXSecretDoorDestination.GetComponent<ExpandSecretDoorPlacable>().DoorLightObject;
            m_ExitDoorComponent.DoorTopBorderObject = EXSecretDoorDestination.GetComponent<ExpandSecretDoorPlacable>().DoorTopBorderObject;
            UnityEngine.Object.Destroy(EXSecretDoorDestination.GetComponent<ExpandSecretDoorPlacable>().Lock.gameObject);
            UnityEngine.Object.Destroy(EXSecretDoorDestination.GetComponent<ExpandSecretDoorPlacable>());


            Dungeon Base_Castle = DungeonDatabase.GetOrLoadByName("Base_Castle");
            GameObject m_NormalLock = (Base_Castle.PatternSettings.flows[0].sharedInjectionData[1].InjectionData[0].exactRoom.placedObjects[0].nonenemyBehaviour as SecretFloorInteractableController).WorldLocks[0].gameObject;

            // Setup copy with no rat decal and normal lock instead of rat lock. A general purpose version of the Mini-Elevator.
            EXSecretDoor_Normal = UnityEngine.Object.Instantiate(EXSecretDoor);
            EXSecretDoor_Normal.name = "EX Secret Door 2 (Entrance)";

            Vector3 Lock2PositionOffset = (EXSecretDoor_Normal.transform.position + new Vector3(1.22f, 0.34f));

            ExpandSecretDoorPlacable m_SecretDoor2Component = EXSecretDoor_Normal.GetComponent<ExpandSecretDoorPlacable>();
            m_SecretDoor2Component.isHollowsElevator = false;
            UnityEngine.Object.Destroy(m_SecretDoor2Component.Lock.gameObject);
            m_SecretDoor2Component.DoorTopBorderObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Frame_NoDecal_Top");
            GameObject m_LockObject2 = UnityEngine.Object.Instantiate(m_NormalLock, Lock2PositionOffset, Quaternion.identity);
            m_LockObject2.gameObject.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            m_LockObject2.GetComponent<InteractableLock>().sprite.HeightOffGround = 1.7f;
            m_LockObject2.GetComponent<InteractableLock>().sprite.UpdateZDepth();
            // m_LockObject2.SetActive(false);
            m_LockObject2.transform.SetParent(EXSecretDoor_Normal.transform, true);
            m_SecretDoor2Component.Lock = m_LockObject2.GetComponent<InteractableLock>();


            /*m_ExitDoorComponent.DoorBackgroundObject.SetActive(false);
            m_ExitDoorComponent.DoorBottomBorderObject.SetActive(false);
            m_ExitDoorComponent.DoorLightObject.SetActive(false);
            m_ExitDoorComponent.DoorTopBorderObject.SetActive(false);*/
            EXSecretDoor_Normal.SetActive(false);
            EXSecretDoorDestination.SetActive(false);
            FakePrefab.MarkAsFakePrefab(EXSecretDoor_Normal);
            FakePrefab.MarkAsFakePrefab(EXSecretDoorDestination);
            UnityEngine.Object.DontDestroyOnLoad(EXSecretDoor_Normal);
            UnityEngine.Object.DontDestroyOnLoad(EXSecretDoorDestination);

            Base_Castle = null;
            m_NormalLock = null;
        }

    }

    public class ExpandSecretDoorPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable {
        
        public ExpandSecretDoorPlacable() {
            placeableWidth = 4;
            placeableHeight = 4;
            difficulty = PlaceableDifficulty.BASE;
            isPassable = true;
            
            spawnUnlocked = false;
            isHollowsElevator = true;

            m_IsRecievingPlayer = false;
            m_Opened = false;
            m_Disabled = false;
            m_InUse = false;
            hasBeenSetup = false;

            m_IsInMovment = false;
            m_AutoClosed = false;
        }

        public bool spawnUnlocked;
        public bool isHollowsElevator;      

        public InteractableLock Lock;
        public GameObject MinimapIcon;
        public GameObject DoorTopBorderObject;
        public GameObject DoorBottomBorderObject;
        public GameObject DoorBackgroundObject;
        public GameObject DoorLightObject;

        public ExpandSecretDoorExitPlacable m_DestinationDoor;
        public Vector3? m_Destination;
        public bool hasBeenSetup;
        public bool m_IsRecievingPlayer;
        public bool m_Opened;
        public bool m_Disabled;
        public bool m_InUse;

        
        public PlayerController m_RecievedPlayer;

        private RoomHandler m_parentRoom;
        private bool m_IsInMovment;
        private bool m_AutoClosed;

        private IEnumerator Start() {

            while (!hasBeenSetup) { yield return null; }
            
            if (!m_Destination.HasValue) {
                if (ExpandStats.debugMode) { ETGModConsole.Log("[ExpandTheGungeon] [" + gameObject.name + "] ERROR: Destination Door was not found!"); }
                m_Disabled = true;
            }

            if (!m_Disabled) {
                if (specRigidbody) {
                    specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                }
            }
            
            m_parentRoom.RegisterInteractable(Lock);

            /*if (isHollowsElevator) {
                List<IntVector2> CachedPositions = new List<IntVector2>();
                IntVector2 RandomGlitchEnemyPosition1 = ExpandObjectDatabase.GetRandomAvailableCellForPlacable(GameManager.Instance.Dungeon, m_parentRoom, CachedPositions, false, true);
                IntVector2 RandomGlitchEnemyPosition2 = ExpandObjectDatabase.GetRandomAvailableCellForPlacable(GameManager.Instance.Dungeon, m_parentRoom, CachedPositions, false, true);
                IntVector2 RandomGlitchEnemyPosition3 = ExpandObjectDatabase.GetRandomAvailableCellForPlacable(GameManager.Instance.Dungeon, m_parentRoom, CachedPositions, false, true);
                ExpandGlitchedEnemies m_GlitchEnemyDatabase = new ExpandGlitchedEnemies();
                m_GlitchEnemyDatabase.SpawnGlitchedRat(m_parentRoom, RandomGlitchEnemyPosition1);
                m_GlitchEnemyDatabase.SpawnGlitchedRat(m_parentRoom, RandomGlitchEnemyPosition2);
                m_GlitchEnemyDatabase.SpawnGlitchedRat(m_parentRoom, RandomGlitchEnemyPosition3);
                m_GlitchEnemyDatabase = null;
                CachedPositions.Clear();
            }*/
            
            if (MinimapIcon) {
                Minimap.Instance.RegisterRoomIcon(m_parentRoom, MinimapIcon, false);
            }

            yield break;
        }
        
        private void HandleTriggerCollision(SpeculativeRigidbody Rigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_InUse | m_IsRecievingPlayer | Lock.IsLocked) { return; }
            PlayerController player = Rigidbody.GetComponent<PlayerController>();
            if (player && !m_IsRecievingPlayer) { StartCoroutine(Trigger(player)); }
        }

        private IEnumerator Trigger(PlayerController player) {
            if (m_InUse | m_IsRecievingPlayer) { yield break; }            
            if (m_DestinationDoor) {
                player.CurrentInputState = PlayerInputState.NoInput;
                m_DestinationDoor.m_IsRecievingPlayer = true;
                m_DestinationDoor.m_RecievedPlayer = player;
                Close();
                m_DestinationDoor.Close(true);
                m_DestinationDoor.m_Opened = false;
                while (spriteAnimator.IsPlaying("door_close") && m_DestinationDoor.m_InUse) { yield return null; }                
                Pixelator.Instance.FadeToBlack(0.25f, false, 0f);
                yield return new WaitForSeconds(0.35f);
                player.WarpToPointAndBringCoopPartner(m_Destination.Value, false, true);
                if (player.GetAbsoluteParentRoom() != null) {
                    while (player.GetAbsoluteParentRoom() == m_parentRoom) { yield return null; }
                }
                m_DestinationDoor.m_InUse = false;
                m_InUse = false;
            }
            yield break;
        }

        private void LateUpdate() {

            if (GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) { return; }

            HandleAutoMovement();

            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != m_parentRoom | m_InUse)) { return; }
                       
            if (!m_Opened && !m_InUse && !Lock.IsLocked) { Open(); }
        }

        private void HandleAutoMovement() {
            if (!hasBeenSetup | Lock.IsLocked | Lock.IsBusted | m_InUse | m_IsRecievingPlayer | m_RecievedPlayer | m_IsInMovment) { return; }
            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != m_parentRoom && m_AutoClosed) { return; }
            if (m_parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { return; }

            PlayerController m_ActivePlayerClosestToPoint = GameManager.Instance.GetActivePlayerClosestToPoint((gameObject.transform.position.XY() + new Vector2(0.5f, 0)), true);
            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == m_parentRoom) {
                if (Vector2.Distance((gameObject.transform.position.XY() + new Vector2(0.5f, 0)), m_ActivePlayerClosestToPoint.CenterPosition) < 3.5f) {
                    AutoOpen();
                } else {
                    AutoClose();
                }
            } else {
                AutoClose();
            }
        }

        public void Open(bool silentOpen = false) {
            m_Opened = true;
            m_AutoClosed = false;
            m_IsInMovment = true;
            StartCoroutine(HandleDoorOpen(silentOpen));
            return;
        }

        public void Close(bool silentClose = false) {
            m_InUse = true;
            m_AutoClosed = true;
            m_IsInMovment = true;
            StartCoroutine(HandleDoorClose(silentClose));
            return;
        }

        private IEnumerator HandleDoorOpen(bool SilentOpen) {
            if (m_IsRecievingPlayer) {
                Pixelator.Instance.FadeToBlack(0.25f, true, 0f);
                yield return new WaitForSeconds(0.65f);
            }
            if (m_parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && !m_IsRecievingPlayer) {
                while (m_parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { yield return null; }
            }
            if (m_IsRecievingPlayer && Lock.IsLocked) {
                Lock.ForceUnlock();
                while (Lock.IsLocked) { yield return null; }
            }
            if (!SilentOpen) { AkSoundEngine.PostEvent("Play_OBJ_cardoor_open_01", gameObject); }
            if (DoorLightObject && DoorLightObject.GetComponent<tk2dSprite>()) { DoorLightObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Light_Green"); }
            spriteAnimator.Play("door_open");
            while (spriteAnimator.IsPlaying("door_open")) { yield return null; }
            specRigidbody.PixelColliders[0].Enabled = false;
            sprite.HeightOffGround = -1.5f;
            sprite.UpdateZDepth();
            if (m_RecievedPlayer) {
                if (!SilentOpen) { AkSoundEngine.PostEvent("Play_EX_ElevatorBell_01", gameObject); }
                m_RecievedPlayer.CurrentInputState = PlayerInputState.AllInput;
                m_RecievedPlayer = null;
            }
            yield return new WaitForSeconds(5f);
            m_IsRecievingPlayer = false;
            m_InUse = false;
            m_IsInMovment = false;
            yield break;
        }

        private IEnumerator HandleDoorClose(bool SilentClose) {
            if (!SilentClose) { AkSoundEngine.PostEvent("Play_OBJ_cardoor_close_01", gameObject); }
            if (DoorLightObject && DoorLightObject.GetComponent<tk2dSprite>()) { DoorLightObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Light_Red"); }
            spriteAnimator.Play("door_close");
            while (spriteAnimator.IsPlaying("door_close")) { yield return null; }
            specRigidbody.PixelColliders[0].Enabled = true;
            sprite.HeightOffGround = 3f;
            sprite.UpdateZDepth();            
            while (m_InUse) { yield return null; }
            m_InUse = false;
            m_Opened = false;
            sprite.HeightOffGround = -1.5f;
            sprite.UpdateZDepth();
            m_IsInMovment = false;
            yield break;
        }

        private void AutoOpen() {
            if (m_IsInMovment | !m_AutoClosed) { return; }
            m_AutoClosed = false;
            m_IsInMovment = true;
            GameManager.Instance.StartCoroutine(HandleDoorAutoOpen());
        }

        private void AutoClose() {
            if (m_IsInMovment | m_AutoClosed) { return; }
            m_AutoClosed = true;
            m_IsInMovment = true;
            GameManager.Instance.StartCoroutine(HandleDoorAutoClose());
        }

        private IEnumerator HandleDoorAutoOpen() {
            if (DoorLightObject && DoorLightObject.GetComponent<tk2dSprite>()) { DoorLightObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Light_Green"); }
            spriteAnimator.Play("door_open");
            RoomHandler PlayerParentRoom = GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom();
            if (PlayerParentRoom != null && PlayerParentRoom == m_parentRoom) { AkSoundEngine.PostEvent("Play_OBJ_cardoor_open_01", gameObject); }
            while (spriteAnimator.IsPlaying("door_open")) { yield return null; }
            specRigidbody.PixelColliders[0].Enabled = false;
            if (sprite.HeightOffGround != -1.5f) {
                sprite.HeightOffGround = -1.5f;
                sprite.UpdateZDepth();
            }
            m_IsInMovment = false;
            yield break;
        }

        private IEnumerator HandleDoorAutoClose() {
            if (DoorLightObject && DoorLightObject.GetComponent<tk2dSprite>()) { DoorLightObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Light_Red"); }
            RoomHandler PlayerParentRoom = GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom();
            if (PlayerParentRoom != null && PlayerParentRoom == m_parentRoom) { AkSoundEngine.PostEvent("Play_OBJ_cardoor_close_01", gameObject); }
            spriteAnimator.Play("door_close");
            while (spriteAnimator.IsPlaying("door_clsoe")) { yield return null; }
            specRigidbody.PixelColliders[0].Enabled = true;
            if (sprite.HeightOffGround != -1.5f) {
                sprite.HeightOffGround = -1.5f;
                sprite.UpdateZDepth();
            }
            m_IsInMovment = false;
            yield break;
        }


        public void ConfigureOnPlacement(RoomHandler room) {
            m_parentRoom = room;

            DoorTopBorderObject.transform.parent = room.hierarchyParent;
            DoorBottomBorderObject.transform.parent = room.hierarchyParent;
            DoorBackgroundObject.transform.parent = room.hierarchyParent;

            if (spawnUnlocked) { Lock.ForceUnlock(); }
            
            gameObject.transform.SetParent(GameManager.Instance.Dungeon.gameObject.transform, true);
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }

    public class ExpandSecretDoorExitPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public ExpandSecretDoorExitPlacable() {
            placeableWidth = 4;
            placeableHeight = 4;
            difficulty = PlaceableDifficulty.BASE;
            isPassable = true;
            
            m_IsRecievingPlayer = false;

            hasBeenSetup = false;
            m_Opened = false;
            m_Disabled = false;
            m_InUse = false;

            m_IsInMovment = false;
            m_AutoClosed = false;
        }
        
        public bool hasBeenSetup;

        public GameObject MinimapIcon;
        public GameObject DoorTopBorderObject;
        public GameObject DoorBottomBorderObject;
        public GameObject DoorBackgroundObject;
        public GameObject DoorLightObject;

        public ExpandSecretDoorPlacable m_DestinationDoor;
        public Vector3? m_Destination;
        public bool m_IsRecievingPlayer;
        public bool m_Opened;
        public bool m_Disabled;
        public bool m_InUse;
        
        public PlayerController m_RecievedPlayer;

        private RoomHandler m_parentRoom;
        private bool m_IsInMovment;
        private bool m_AutoClosed;

        private IEnumerator Start() {

            while (!hasBeenSetup) { yield return null; }
            
            if (!m_Destination.HasValue) {
                if (ExpandStats.debugMode) { ETGModConsole.Log("[ExpandTheGungeon] [" + gameObject.name + "] ERROR: Destination Door was not found!"); }
                m_Disabled = true;
            }

            if (!m_Disabled) {
                if (specRigidbody) {
                    specRigidbody.OnTriggerCollision = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(specRigidbody.OnTriggerCollision, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerCollision));
                }
            }
                        
            SetupExitElevator();

            if (MinimapIcon) { Minimap.Instance.RegisterRoomIcon(m_parentRoom, MinimapIcon, false); }

            yield break;
        }
        

        private void SetupExitElevator() {

            if (m_DestinationDoor && !m_DestinationDoor.isHollowsElevator) { return; }

            ElevatorDepartureController targetElevator = null;
            if (FindObjectsOfType<ElevatorDepartureController>() != null) {
                foreach (ElevatorDepartureController elevator in FindObjectsOfType<ElevatorDepartureController>()) {
                    if (elevator.gameObject.transform.position.GetAbsoluteRoom() != null && elevator.gameObject.transform.position.GetAbsoluteRoom() == m_parentRoom) {
                        targetElevator = elevator;
                    }
                }
            }
            if (targetElevator != null) {
                targetElevator.gameObject.AddComponent<ExpandElevatorDepartureManager>();
                ExpandElevatorDepartureManager expandElevatorComponent = targetElevator.gameObject.GetComponent<ExpandElevatorDepartureManager>();
                expandElevatorComponent.UsesOverrideTargetFloor = true;
                expandElevatorComponent.OverrideTargetFloor = GlobalDungeonData.ValidTilesets.WESTGEON;
            }
            
            return;
        }

        
        private void HandleTriggerCollision(SpeculativeRigidbody Rigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            if (m_InUse | m_IsRecievingPlayer) { return; }
            PlayerController player = Rigidbody.GetComponent<PlayerController>();
            if (player && !m_IsRecievingPlayer) { StartCoroutine(Trigger(player)); }
        }

        private IEnumerator Trigger(PlayerController player) {
            if (m_InUse | m_IsRecievingPlayer) { yield break; }
            if (m_DestinationDoor) {
                player.CurrentInputState = PlayerInputState.NoInput;
                m_DestinationDoor.m_IsRecievingPlayer = true;
                m_DestinationDoor.m_RecievedPlayer = player;
                Close();
                m_DestinationDoor.Close(true);
                m_DestinationDoor.m_Opened = false;
                while (spriteAnimator.IsPlaying("door_close") && m_DestinationDoor.m_InUse) { yield return null; }                
                Pixelator.Instance.FadeToBlack(0.25f, false, 0f);
                yield return new WaitForSeconds(0.35f);
                player.WarpToPointAndBringCoopPartner(m_Destination.Value, false, true);
                if (player.GetAbsoluteParentRoom() != null) {
                    while (player.GetAbsoluteParentRoom() == m_parentRoom) { yield return null; }
                }
                m_DestinationDoor.m_InUse = false;
                m_InUse = false;
            }
            yield break;
        }

        private void LateUpdate() {

            if (GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) { return; }

            HandleAutoMovement();

            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != m_parentRoom | m_InUse)) { return; }
                        
            if (!m_Opened && !m_InUse) { Open(); }
        }

        private void HandleAutoMovement() {
            if (!hasBeenSetup | m_IsInMovment | m_InUse | m_IsRecievingPlayer | m_RecievedPlayer) { return; }
            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != m_parentRoom && m_AutoClosed) { return; }

            PlayerController m_ActivePlayerClosestToPoint = GameManager.Instance.GetActivePlayerClosestToPoint((gameObject.transform.position.XY() + new Vector2(0.5f, 0)), true);

            if (GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() != null && GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom() == m_parentRoom) {
                if (Vector2.Distance((gameObject.transform.position.XY() + new Vector2(0.5f, 0)), m_ActivePlayerClosestToPoint.CenterPosition) < 3.5f) {
                    AutoOpen();
                } else {
                    AutoClose();
                }
            } else {
                AutoClose();
            }
            return;
        }

        public void Open(bool silentOpen = false) {
            m_Opened = true;
            m_AutoClosed = false;
            m_IsInMovment = true;
            StartCoroutine(HandleDoorOpen(silentOpen));
            return;
        }

        public void Close(bool silentClose = false) {
            m_InUse = true;
            m_AutoClosed = true;
            m_IsInMovment = true;
            StartCoroutine(HandleDoorClose(silentClose));
            return;
        }

        private IEnumerator HandleDoorOpen(bool SilentOpen) {
            if (m_IsRecievingPlayer) {
                Pixelator.Instance.FadeToBlack(0.25f, true, 0f);
                yield return new WaitForSeconds(0.65f);
            }

            if (m_parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear) && !m_IsRecievingPlayer) {
                while (m_parentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { yield return null; }
            }
            if (!SilentOpen) { AkSoundEngine.PostEvent("Play_OBJ_cardoor_open_01", gameObject); }
            if (DoorLightObject && DoorLightObject.GetComponent<tk2dSprite>()) { DoorLightObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Light_Green"); }
            spriteAnimator.Play("door_open");
            while (spriteAnimator.IsPlaying("door_open")) { yield return null; }
            specRigidbody.PixelColliders[0].Enabled = false;
            sprite.HeightOffGround = -1.5f;
            sprite.UpdateZDepth();
            if (m_RecievedPlayer) {
                if (!SilentOpen) { AkSoundEngine.PostEvent("Play_EX_ElevatorBell_01", gameObject); }
                m_RecievedPlayer.CurrentInputState = PlayerInputState.AllInput;
                m_RecievedPlayer = null;
            }
            yield return new WaitForSeconds(5f);
            m_IsRecievingPlayer = false;
            m_InUse = false;
            m_IsInMovment = false;
            yield break;
        }
        
        private IEnumerator HandleDoorClose(bool SilentClose) {            
            if (!SilentClose) { AkSoundEngine.PostEvent("Play_OBJ_cardoor_close_01", gameObject); }
            if (DoorLightObject && DoorLightObject.GetComponent<tk2dSprite>()) { DoorLightObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Light_Red"); }
            spriteAnimator.Play("door_close");
            while (spriteAnimator.IsPlaying("door_close")) { yield return null; }
            specRigidbody.PixelColliders[0].Enabled = true;
            sprite.HeightOffGround = 3f;
            sprite.UpdateZDepth();
            while (m_InUse) { yield return null; }            
            m_Opened = false;
            sprite.HeightOffGround = -1.5f;
            sprite.UpdateZDepth();
            m_IsInMovment = false;
            yield break;
        }

        private void AutoOpen() {
            if (m_IsInMovment | !m_AutoClosed) { return; }
            m_AutoClosed = false;
            m_IsInMovment = true;
            GameManager.Instance.StartCoroutine(HandleDoorAutoOpen());
        }

        private void AutoClose() {
            if (m_IsInMovment | m_AutoClosed) { return; }
            m_AutoClosed = true;
            m_IsInMovment = true;
            GameManager.Instance.StartCoroutine(HandleDoorAutoClose());
        }

        private IEnumerator HandleDoorAutoOpen() {
            if (DoorLightObject && DoorLightObject.GetComponent<tk2dSprite>()) { DoorLightObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Light_Green"); }
            RoomHandler PlayerParentRoom = GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom();
            if (PlayerParentRoom != null && PlayerParentRoom == m_parentRoom) { AkSoundEngine.PostEvent("Play_OBJ_cardoor_open_01", gameObject); }
            spriteAnimator.Play("door_open");
            while (spriteAnimator.IsPlaying("door_open")) { yield return null; }
            specRigidbody.PixelColliders[0].Enabled = false;
            if (sprite.HeightOffGround != -1.5f) {
                sprite.HeightOffGround = -1.5f;
                sprite.UpdateZDepth();
            }
            m_IsInMovment = false;
            yield break;
        }

        private IEnumerator HandleDoorAutoClose() {
            if (DoorLightObject && DoorLightObject.GetComponent<tk2dSprite>()) { DoorLightObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Light_Red"); }
            RoomHandler PlayerParentRoom = GameManager.Instance.PrimaryPlayer.GetAbsoluteParentRoom();
            if (PlayerParentRoom != null && PlayerParentRoom == m_parentRoom) { AkSoundEngine.PostEvent("Play_OBJ_cardoor_close_01", gameObject); }
            spriteAnimator.Play("door_close");
            while (spriteAnimator.IsPlaying("door_clsoe")) { yield return null; }
            specRigidbody.PixelColliders[0].Enabled = true;
            if (sprite.HeightOffGround != -1.5f) {
                sprite.HeightOffGround = -1.5f;
                sprite.UpdateZDepth();
            }
            m_IsInMovment = false;
            yield break;
        }


        public void ConfigureOnPlacement(RoomHandler room) {
            m_parentRoom = room;

            if (GameManager.Instance.Dungeon.tileIndices.tilesetId != GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                DoorTopBorderObject.GetComponent<tk2dSprite>().SetSprite("EXSecretDoor_Frame_NoDecal_Top");
            }

            DoorTopBorderObject.transform.parent = room.hierarchyParent;
            DoorBottomBorderObject.transform.parent = room.hierarchyParent;
            DoorBackgroundObject.transform.parent = room.hierarchyParent;
            
            // gameObject.transform.parent = GameManager.Instance.Dungeon.gameObject.transform;
            gameObject.transform.SetParent(ETGModMainBehaviour.Instance.gameObject.transform, true);
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

