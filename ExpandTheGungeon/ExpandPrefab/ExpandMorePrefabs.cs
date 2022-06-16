using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;


namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandMorePrefabs : ExpandPrefabs {

        public static void InitMoreCustomPrefabs(AssetBundle expandSharedAssets1, AssetBundle sharedAssets, AssetBundle sharedAssets2, AssetBundle braveResources, AssetBundle enemiesBase) {

            /*Dungeon TutorialDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Tutorial");
            Dungeon CastleDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Castle");
            Dungeon GungeonDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Gungeon");
            Dungeon SewerDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Sewer");
            Dungeon MinesDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Mines");
            Dungeon CathedralDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Cathedral");
            Dungeon BulletHellDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_BulletHell");
            Dungeon ForgeDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Forge");
            Dungeon CatacombsDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Catacombs");
            Dungeon NakatomiDungeonPrefab = DungeonDatabase.GetOrLoadByName("base_nakatomi");*/
            Dungeon ratDungeon = DungeonDatabase.GetOrLoadByName("base_resourcefulrat");

            EXFoyerWarpDoor = expandSharedAssets1.LoadAsset<GameObject>("EXFoyerWarpDoor");
            tk2dSprite m_EXFoyerWarpDoorSprite = SpriteSerializer.AddSpriteToObject(EXFoyerWarpDoor, EXFoyerCollection, "foyerdoor_open_01");
            m_EXFoyerWarpDoorSprite.HeightOffGround = -2.25f;
            
            tk2dSpriteAnimator m_FoyerWarpDoorAnimator = ExpandUtility.GenerateSpriteAnimator(EXFoyerWarpDoor);

            List<string> m_FoyerDoorOpen = new List<string>() {
                "foyerdoor_open_01",
                "foyerdoor_open_02",
                "foyerdoor_open_03",
                "foyerdoor_open_04",
                "foyerdoor_open_05",
                "foyerdoor_open_06",
                "foyerdoor_open_07",
                "foyerdoor_open_08",
                "foyerdoor_open_09",
                "foyerdoor_open_10",
                "foyerdoor_open_11",
                "foyerdoor_open_12",
                "foyerdoor_open_13",
            };

            List<string> m_FoyerDoorClose = new List<string>() {
                "foyerdoor_open_13",
                "foyerdoor_open_12",
                "foyerdoor_open_11",
                "foyerdoor_open_10",
                "foyerdoor_open_09",
                "foyerdoor_open_08",
                "foyerdoor_open_07",
                "foyerdoor_open_06",
                "foyerdoor_open_05",
                "foyerdoor_open_04",
                "foyerdoor_open_03",
                "foyerdoor_open_02",
                "foyerdoor_open_01",
            };
            ExpandUtility.AddAnimation(m_FoyerWarpDoorAnimator, EXFoyerCollection.GetComponent<tk2dSpriteCollectionData>(), m_FoyerDoorOpen, "open", frameRate: 12);
            ExpandUtility.AddAnimation(m_FoyerWarpDoorAnimator, EXFoyerCollection.GetComponent<tk2dSpriteCollectionData>(), m_FoyerDoorClose, "close", frameRate: 12);

            ExpandUtility.GenerateOrAddToRigidBody(EXFoyerWarpDoor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(17, 24));
            ExpandUtility.GenerateOrAddToRigidBody(EXFoyerWarpDoor, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(17, 24), offset: new IntVector2(48, 0));
            ExpandUtility.GenerateOrAddToRigidBody(EXFoyerWarpDoor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 8), offset: new IntVector2(0, 24));
            ExpandUtility.GenerateOrAddToRigidBody(EXFoyerWarpDoor, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 8), offset: new IntVector2(48, 24));
            ExpandUtility.GenerateOrAddToRigidBody(EXFoyerWarpDoor, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(31, 13), offset: new IntVector2(17, 18));
            EXFoyerWarpDoor.GetComponent<SpeculativeRigidbody>().HasTriggerCollisions = true;
            EXFoyerWarpDoor.GetComponent<SpeculativeRigidbody>().PixelColliders[4].IsTrigger = true;

            EXFoyerWarpDoor.GetComponent<SpeculativeRigidbody>().PixelColliders[0].Enabled = false;
            EXFoyerWarpDoor.GetComponent<SpeculativeRigidbody>().PixelColliders[1].Enabled = false;
            EXFoyerWarpDoor.GetComponent<SpeculativeRigidbody>().PixelColliders[2].Enabled = false;
            EXFoyerWarpDoor.GetComponent<SpeculativeRigidbody>().PixelColliders[3].Enabled = false;
            EXFoyerWarpDoor.GetComponent<SpeculativeRigidbody>().PixelColliders[4].Enabled = false;


            EXFoyerWarpDoor.AddComponent<ExpandWarpManager>();


            EXFoyerTrigger = expandSharedAssets1.LoadAsset<GameObject>("EXFoyerTrigger");
            tk2dSprite m_FoyerTriggerSprite = SpriteSerializer.AddSpriteToObject(EXFoyerTrigger, EXFoyerCollection, "floortrigger_idle_01", tk2dBaseSprite.PerpendicularState.FLAT);
            m_FoyerTriggerSprite.HeightOffGround = -1.74f;
            EXFoyerTrigger.AddComponent<ExpandCasinoWarpTrigger>();


            EXCasinoHub = expandSharedAssets1.LoadAsset<GameObject>("EXCasino_Hub");
            EXCasino_HatRack = expandSharedAssets1.LoadAsset<GameObject>("EXCasino_HatRack");
            EXCasino_Litter_Cans = expandSharedAssets1.LoadAsset<GameObject>("EXCasino_Litter_Cans");
            EXCasino_Litter_Paper = expandSharedAssets1.LoadAsset<GameObject>("EXCasino_Litter_Paper");

            GameObject m_EXCasinoHubRoomPrefab = EXCasinoHub.transform.Find("Room_Prefab").gameObject;
            GameObject m_CasinoFloor = m_EXCasinoHubRoomPrefab.transform.Find("casino_hub_floor").gameObject;
            GameObject m_CasinoWalls = m_EXCasinoHubRoomPrefab.transform.Find("casino_hub_backwall").gameObject;
            GameObject m_CasinoBorder = m_EXCasinoHubRoomPrefab.transform.Find("casino_hub_border").gameObject;
            GameObject m_CasinoPokerTable = EXCasinoHub.transform.Find("casino_poker_table").gameObject;
            GameObject m_CasinoPokerTableProps = m_CasinoPokerTable.transform.Find("tableprops").gameObject;
            GameObject m_CasinoPokerTableProps2 = m_CasinoPokerTable.transform.Find("tableprops2").gameObject;
            GameObject m_CasinoPokerTableShadow = m_CasinoPokerTable.transform.Find("shadow").gameObject;
                        

            tk2dSprite m_CasinoFloorSprite = SpriteSerializer.AddSpriteToObject(m_CasinoFloor, EXFoyerCollection, "casino_hub_floor_001", tk2dBaseSprite.PerpendicularState.FLAT);
            m_CasinoFloorSprite.HeightOffGround = -1.75f;
            tk2dSprite m_CasinoWallsSprite = SpriteSerializer.AddSpriteToObject(m_CasinoWalls, EXFoyerCollection, "casino_hub_backwall_001");
            m_CasinoWallsSprite.HeightOffGround = -1.73f;
            tk2dSprite m_CasinoBorderSprite = SpriteSerializer.AddSpriteToObject(m_CasinoBorder, EXFoyerCollection, "casino_hub_border_001", tk2dBaseSprite.PerpendicularState.FLAT);
            m_CasinoBorderSprite.HeightOffGround = 4;

            tk2dSprite m_CasinoPokerTableSprite = SpriteSerializer.AddSpriteToObject(m_CasinoPokerTable, EXFoyerCollection, "casino_poker_table_001", tk2dBaseSprite.PerpendicularState.FLAT);
            m_CasinoPokerTableSprite.HeightOffGround = 0;
            tk2dSprite m_CasinoPokerTablePropsSprite = SpriteSerializer.AddSpriteToObject(m_CasinoPokerTableProps, EXFoyerCollection, "casino_poker_table_props_001", tk2dBaseSprite.PerpendicularState.FLAT);
            m_CasinoPokerTablePropsSprite.HeightOffGround = 0.2f;
            m_CasinoPokerTableProps.SetActive(false);
            tk2dSprite m_CasinoPokerTableProps2Sprite = SpriteSerializer.AddSpriteToObject(m_CasinoPokerTableProps2, EXFoyerCollection, "casino_poker_table_props_002", tk2dBaseSprite.PerpendicularState.FLAT);
            m_CasinoPokerTableProps2Sprite.HeightOffGround = 0.2f;
            tk2dSprite m_CasinoPokerTableShadowSprite = SpriteSerializer.AddSpriteToObject(m_CasinoPokerTableShadow, EXFoyerCollection, "casino_poker_table_shadow", tk2dBaseSprite.PerpendicularState.FLAT);
            m_CasinoPokerTableShadowSprite.HeightOffGround = -1.73f;
            m_CasinoPokerTableShadowSprite.usesOverrideMaterial = true;
            m_CasinoPokerTableShadowSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;


            tk2dSprite m_EXCasino_HatRackSprite = SpriteSerializer.AddSpriteToObject(EXCasino_HatRack, EXFoyerCollection, "casino_hatrack_001");
            tk2dSprite m_EXCasino_LitterCansSprite = SpriteSerializer.AddSpriteToObject(EXCasino_Litter_Cans, EXFoyerCollection, "casino_litter_cans_001", tk2dBaseSprite.PerpendicularState.FLAT);
            tk2dSprite m_EXCasino_LitterPaperSprite = SpriteSerializer.AddSpriteToObject(EXCasino_Litter_Paper, EXFoyerCollection, "casino_litter_paper_001", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXCasino_HatRackSprite.HeightOffGround = -1.25f;
            m_EXCasino_LitterCansSprite.HeightOffGround = -1.7f;
            m_EXCasino_LitterPaperSprite.HeightOffGround = -1.7f;
            m_EXCasino_HatRackSprite.usesOverrideMaterial = true;
            m_EXCasino_HatRackSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;
            m_EXCasino_LitterCansSprite.usesOverrideMaterial = true;
            m_EXCasino_LitterCansSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;
            m_EXCasino_LitterPaperSprite.usesOverrideMaterial = true;
            m_EXCasino_LitterPaperSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;


            ExpandUtility.GenerateOrAddToRigidBody(EXCasinoHub, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 304), offset: new IntVector2(-10, -32));
            ExpandUtility.GenerateOrAddToRigidBody(EXCasinoHub, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(16, 304), offset: new IntVector2(262, -32));
            ExpandUtility.GenerateOrAddToRigidBody(EXCasinoHub, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(111, 48), offset: new IntVector2(151, -32));
            ExpandUtility.GenerateOrAddToRigidBody(EXCasinoHub, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(111, 48), offset: new IntVector2(6, -32));
            ExpandUtility.GenerateOrAddToRigidBody(EXCasinoHub, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(256, 16), offset: new IntVector2(6, 256));
            ExpandUtility.GenerateOrAddToRigidBody(EXCasino_HatRack, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(12, 13), offset: new IntVector2(5, 2));
            ExpandUtility.GenerateOrAddToRigidBody(EXCasino_HatRack, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(12, 9), offset: new IntVector2(5, 15));
            ExpandUtility.GenerateOrAddToRigidBody(m_CasinoPokerTable, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(41, 45), offset: new IntVector2(2, 0));
            

            ExpandCasinoRoomController m_CasinoRoomController = EXCasinoHub.AddComponent<ExpandCasinoRoomController>();

            GameObject m_EXCasinoGame_Punchout = EXCasinoHub.transform.Find("casinogame_punchout").gameObject;
            tk2dSprite m_EXCasinoGamePunchoutSprite = SpriteSerializer.AddSpriteToObject(m_EXCasinoGame_Punchout, EXFoyerCollection, "cabinet_covered_001");
            m_EXCasinoGamePunchoutSprite.HeightOffGround = -1.65f;

            GameObject m_EXCasinoGame_PunchoutShadow = m_EXCasinoGame_Punchout.transform.Find("shadow").gameObject;
            tk2dSprite m_EXCasinoGamePunchoutShadowSprite = SpriteSerializer.AddSpriteToObject(m_EXCasinoGame_PunchoutShadow, EXFoyerCollection, "cabinet_shadow_001", tk2dBaseSprite.PerpendicularState.FLAT);
            m_EXCasinoGamePunchoutShadowSprite.HeightOffGround = -1.7f;
            m_EXCasinoGamePunchoutShadowSprite.usesOverrideMaterial = true;
            m_EXCasinoGamePunchoutShadowSprite.renderer.material.shader = GameManager.Instance.RewardManager.A_Chest.gameObject.transform.Find("Shadow").gameObject.GetComponent<tk2dSprite>().renderer.material.shader;

            ExpandUtility.GenerateOrAddToRigidBody(m_EXCasinoGame_Punchout, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(27, 15), offset: new IntVector2(4, 0));
            ExpandUtility.GenerateOrAddToRigidBody(m_EXCasinoGame_Punchout, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(23, 9), offset: new IntVector2(6, 15));
            

            List<string> m_PunchoutArcade_Idle = new List<string>() {
                "cabinet_idle_001",
                "cabinet_idle_002",
                "cabinet_idle_003",
                "cabinet_idle_004",
                "cabinet_idle_005",
                "cabinet_idle_006",
                "cabinet_idle_007",
                "cabinet_idle_008",
                "cabinet_idle_009",
                "cabinet_idle_010",
                "cabinet_idle_011",
                "cabinet_idle_012",
            };
            List<string> m_PunchoutArcade_Sleep = new List<string>() {
                "cabinet_sleep_001",
                "cabinet_sleep_002",
                "cabinet_sleep_003",
                "cabinet_sleep_004",
                "cabinet_sleep_005",
                "cabinet_sleep_006",
                "cabinet_sleep_007",
                "cabinet_sleep_008",
                "cabinet_sleep_009",
                "cabinet_sleep_010",
                "cabinet_sleep_011",
                "cabinet_sleep_012",
                "cabinet_sleep_013",
                "cabinet_sleep_014",
            };
            List<string> m_PunchoutArcade_Interact = new List<string>() {
                "cabinet_interact_001",
                "cabinet_interact_002",
                "cabinet_interact_003",
                "cabinet_interact_004",
            };
            List<string> m_PunchoutArcade_Fight = new List<string>() {
                "cabinet_fight_001",
                "cabinet_fight_002",
                "cabinet_fight_003",
                "cabinet_fight_004",
                "cabinet_fight_005",
                "cabinet_fight_006",
                "cabinet_fight_007",
                "cabinet_fight_008",
                "cabinet_fight_009",
                "cabinet_fight_010",
                "cabinet_fight_011",
                "cabinet_fight_012",
                "cabinet_fight_013",
                "cabinet_fight_014"
            };

            
            tk2dSpriteAnimator m_EXCasinoArcadeGameAnimator = ExpandUtility.GenerateSpriteAnimator(m_EXCasinoGame_Punchout, playAutomatically: true);

            ExpandUtility.AddAnimation(m_EXCasinoArcadeGameAnimator, EXFoyerCollection.GetComponent<tk2dSpriteCollectionData>(), m_PunchoutArcade_Idle, "idle", tk2dSpriteAnimationClip.WrapMode.Loop, 10);
            ExpandUtility.AddAnimation(m_EXCasinoArcadeGameAnimator, EXFoyerCollection.GetComponent<tk2dSpriteCollectionData>(), m_PunchoutArcade_Interact, "interact", tk2dSpriteAnimationClip.WrapMode.Once, 6);
            ExpandUtility.AddAnimation(m_EXCasinoArcadeGameAnimator, EXFoyerCollection.GetComponent<tk2dSpriteCollectionData>(), m_PunchoutArcade_Fight, "fight", tk2dSpriteAnimationClip.WrapMode.Once, 10);
            ExpandUtility.AddAnimation(m_EXCasinoArcadeGameAnimator, EXFoyerCollection.GetComponent<tk2dSpriteCollectionData>(), m_PunchoutArcade_Sleep, "sleep", tk2dSpriteAnimationClip.WrapMode.Loop, 10);

            m_CasinoRoomController.CasinoGame_Punchout = m_EXCasinoGame_Punchout.AddComponent<ExpandCasinoGameController>();
            
            EXCasinoArcadeGameScanlineFX = expandSharedAssets1.LoadAsset<GameObject>("EXArcadeGameScanlineFX");
            ExpandScreenFXController m_EXCasinoArcadeGameScanlineFXController = EXCasinoArcadeGameScanlineFX.AddComponent<ExpandScreenFXController>();
            m_EXCasinoArcadeGameScanlineFXController.UseCorruptionAmbience = false;
            m_EXCasinoArcadeGameScanlineFXController.shaderType = ExpandScreenFXController.ShaderType.CRT;

            GameObject m_EXCasinoArcadeGameScanlineFXAdditionalPass = EXCasinoArcadeGameScanlineFX.transform.Find("additionalPass").gameObject;
            ExpandScreenFXController m_EXCasinoArcadeGameScanlineFXController2 = m_EXCasinoArcadeGameScanlineFXAdditionalPass.AddComponent<ExpandScreenFXController>();
            m_EXCasinoArcadeGameScanlineFXController2.UseCorruptionAmbience = false;
            m_EXCasinoArcadeGameScanlineFXController2.shaderType = ExpandScreenFXController.ShaderType.Scanlines;

            
            EXRatDoor_4xLocks = expandSharedAssets1.LoadAsset<GameObject>("EXRatJailDoor4x");
            tk2dSprite EXRatDoor4xLocksSprite = SpriteSerializer.AddSpriteToObject(EXRatDoor_4xLocks, ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour.gameObject.GetComponent<tk2dSprite>().Collection, ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour.gameObject.GetComponent<tk2dSprite>().spriteId, tk2dBaseSprite.PerpendicularState.PERPENDICULAR);
            EXRatDoor4xLocksSprite.HeightOffGround = -1.5f;
            ExpandUtility.GenerateSpriteAnimator(EXRatDoor_4xLocks, ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour.gameObject.GetComponent<tk2dSpriteAnimator>().Library, 143);

            SpeculativeRigidbody m_EXRatDoor4xLocksRigidBody = EXRatDoor_4xLocks.AddComponent<SpeculativeRigidbody>();
            ExpandUtility.DuplicateComponent(m_EXRatDoor4xLocksRigidBody, ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour.gameObject.GetComponent<SpeculativeRigidbody>());
            m_EXRatDoor4xLocksRigidBody.PixelColliders = new List<PixelCollider>();
            ExpandUtility.GenerateOrAddToRigidBody(EXRatDoor_4xLocks, CollisionLayer.LowObstacle, PixelCollider.PixelColliderGeneration.Manual, true, true, false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 16), offset: IntVector2.Zero);
            ExpandUtility.GenerateOrAddToRigidBody(EXRatDoor_4xLocks, CollisionLayer.HighObstacle, PixelCollider.PixelColliderGeneration.Manual, true, true, false, UsesPixelsAsUnitSize: true, dimensions: new IntVector2(32, 16), offset: new IntVector2(0, 16));

            InteractableDoorController m_Rat4xDoorController = EXRatDoor_4xLocks.AddComponent<InteractableDoorController>();
            m_Rat4xDoorController.placeableWidth = 2;
            m_Rat4xDoorController.placeableHeight = 2;
            m_Rat4xDoorController.difficulty = DungeonPlaceableBehaviour.PlaceableDifficulty.BASE;
            m_Rat4xDoorController.isPassable = true;
            m_Rat4xDoorController.OpensAutomaticallyOnUnlocked = true;
            m_Rat4xDoorController.WorldLocks = new List<InteractableLock>();

            for (int i = 0; i < 4; i++) {
                GameObject m_ChildLock = EXRatDoor_4xLocks.transform.Find("Lock" + i).gameObject;
                tk2dSprite m_ChildLockSprite = SpriteSerializer.AddSpriteToObject(m_ChildLock, ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour.gameObject.transform.Find("Lock").gameObject.GetComponent<tk2dSprite>().Collection, ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour.gameObject.transform.Find("Lock").gameObject.GetComponent<tk2dSprite>().spriteId, tk2dBaseSprite.PerpendicularState.PERPENDICULAR);
                if (i == 0 | i == 2) {
                    m_ChildLockSprite.HeightOffGround = 1;
                } else {
                    m_ChildLockSprite.HeightOffGround = -1.5f;
                }
                ExpandUtility.GenerateSpriteAnimator(m_ChildLock, ratDungeon.PatternSettings.flows[0].AllNodes[13].overrideExactRoom.placedObjects[1].nonenemyBehaviour.gameObject.transform.Find("Lock").gameObject.GetComponent<tk2dSpriteAnimator>().Library, 53, playAutomatically: true);
                InteractableLock m_LockController = m_ChildLock.AddComponent<InteractableLock>();
                m_LockController.JailCellKeyId = -1;
                m_LockController.Suppress = false;
                m_LockController.IdleAnimName = "rat_lock_open_idle";
                m_LockController.UnlockAnimName = "rat_lock_open";
                m_LockController.NoKeyAnimName = string.Empty;
                m_LockController.SpitAnimName = string.Empty;
                m_LockController.BustedAnimName = string.Empty;
                m_LockController.lockMode = InteractableLock.InteractableLockMode.RAT_REWARD;
                m_Rat4xDoorController.WorldLocks.Add(m_LockController);
            }

            /*TutorialDungeonPrefab = null;
            CastleDungeonPrefab = null;
            GungeonDungeonPrefab = null;
            SewerDungeonPrefab = null;
            MinesDungeonPrefab = null
            CathedralDungeonPrefab = null;
            BulletHellDungeonPrefab = null;
            ForgeDungeonPrefab = null;
            CatacombsDungeonPrefab = null;
            NakatomiDungeonPrefab = null;*/
            ratDungeon = null;
        }
    }
}

