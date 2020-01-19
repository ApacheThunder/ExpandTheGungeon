using System;
using UnityEngine;
using System.Reflection;
using MonoMod.RuntimeDetour;
using Dungeonator;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandAudio;
using ExpandTheGungeon.ExpandDungeonFlows;

namespace ExpandTheGungeon {

    public class ExpandTheGungeon : ETGModule {

        public static Hook GameManagerHook;
        
        public static bool isGlitchFloor = false;
        public static bool ItemAPISetup = false;

        public static GameObject TestObject;
        
        public override void Init() { }

        public override void Start() {
            
            // Init ItemAPI
            SetupItemAPI();
            // Init Prefab Databases
            ExpandPrefabs.InitCustomPrefabs();
            // Init Custom Enemy Prefabs
            ExpandCustomEnemyDatabase.InitPrefabs();
            // Init Custom Room Prefabs
            ExpandRoomPrefabs.InitCustomRooms();
            // Init Custom DungeonFlow(s)
            ExpandDungeonFlow.InitDungeonFlows();

            // Init Custom Textures
            /*RatGrenade.Init();
            BlueShotGunMan.Init();
            Bullat.Init();
            BulletMan.Init();
            BulletManBandana.Init();
            RedShotGunMan.Init();*/
            
            // Modified version of Anywhere mod
            DungeonFlowModule.Install();

            ExpandSharedHooks.InstallRequiredHooks();

            GameManager.Instance.OnNewLevelFullyLoaded += ExpandObjectMods.Instance.InitSpecialMods;

            AudioResourceLoader.InitAudio();
            
            ETGModConsole.Commands.AddGroup("expand", delegate (string[] e) {
                ETGModConsole.Log("[ExpandTheGungeon] The following options are available for ExpandTheGungeon:\n", false);
                string[] AvailableCommands = ETGModConsole.Commands.GetGroup("ExpandTheGungeon").GetAllUnitNames();
                foreach (string Command in AvailableCommands) { ETGModConsole.Log(Command + "\n", false); }
            });

            ETGModConsole.Commands.GetGroup("expand").AddUnit("test", delegate (string[] e) {
                // GameObject soundObject = new GameObject("SoundSource");
                // soundObject.transform.position = GameManager.Instance.BestActivePlayer.transform.position;
                // AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", soundObject);
                // AkSoundEngine.PostEvent("Play_VO_bombshee_death_01", soundObject);
                // ETGModConsole.Log(TestObject.name);
                PlayerController CurrentPlayer = GameManager.Instance.PrimaryPlayer;
                /*Dungeon dungeon = GameManager.Instance.Dungeon;

                if (dungeon && CurrentPlayer) {
                    RoomHandler DestinationRoom = null;
                    foreach (RoomHandler room in dungeon.data.rooms) {
                        if (!string.IsNullOrEmpty(room.GetRoomName())) {
                            if (room.GetRoomName().StartsWith(ExpandRoomPrefabs.SecretRatEntranceRoom.name)) {
                                DestinationRoom = room;
                                break;
                            }
                        }
                    }
                    if (DestinationRoom != null) {
                        CurrentPlayer.WarpToPoint((new Vector2(8, 8) + DestinationRoom.area.basePosition.ToVector2()), doFollowers: true);
                    }
                }*/
                /*GameObject m_CachedAIActorObject = new GameObject("TestEnemy") { layer = 28 };                
                m_CachedAIActorObject.transform.position = (CurrentPlayer.transform.position + new Vector3(0, 2));
                m_CachedAIActorObject.transform.parent = CurrentPlayer.GetAbsoluteParentRoom().hierarchyParent;
                tk2dSprite newSprite = m_CachedAIActorObject.AddComponent<tk2dSprite>();
                ExpandUtility.DuplicateSprite(newSprite, EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").gameObject.GetComponent<tk2dSprite>());*/

                // AIActor newEnemy = ExpandUtility.GenerateAIActorTemplate(m_CachedAIActorObject, "Test Enemy", Guid.NewGuid().ToString());

                /*ExpandGlitchedEnemies glitchedEnemyDatabase = new ExpandGlitchedEnemies();
                glitchedEnemyDatabase.SpawnGlitchedPlayerAsEnemy(CurrentPlayer.GetAbsoluteParentRoom(), new IntVector2(3, 3), true);
                glitchedEnemyDatabase = null;*/

                // LootEngine.SpawnItem(CorruptedJunk.CorruptedJunkObject, (CurrentPlayer.transform.position + Vector3.one), Vector2.zero, 0, doDefaultItemPoof: true);
                // Rooms for floor 4.
            });
            
            ETGModConsole.Commands.GetGroup("expand").AddUnit("list_items", delegate (string[] e) {
                ETGModConsole.Log("Custom Items: ", false);
                foreach (string str in itemList) { ETGModConsole.Log("    " + str, false); }
            });

            ETGModConsole.Commands.GetGroup("expand").AddUnit("debug", delegate (string[] e) {
                if (!ExpandStats.debugMode) {
                    ExpandStats.debugMode = true;
                    ETGModConsole.Log("[ExpandTheGungeon] Installing RoomHandler.OnEntered Hook....");
                    ExpandSharedHooks.enterRoomHook = new Hook(
                        typeof(RoomHandler).GetMethod("OnEntered", BindingFlags.NonPublic | BindingFlags.Instance),
                        typeof(ExpandSharedHooks).GetMethod("EnteredNewRoomHook", BindingFlags.NonPublic | BindingFlags.Instance),
                        typeof(RoomHandler)
                    );
                } else {
                    if (ExpandStats.debugMode) {
                        ExpandStats.debugMode = false;
                        if (ExpandSharedHooks.enterRoomHook != null) {
                            ETGModConsole.Log("[ExpandTheGungeon] Uninstalling RoomHandler.OnEntered Hook....");
                            ExpandSharedHooks.enterRoomHook.Dispose(); ExpandSharedHooks.enterRoomHook = null;
                        }
                    }
                }                
                ETGModConsole.Log("[ExpandTheGungeon] Debug Stats: ", false);
                ETGModConsole.Log("Debug Stats: " + ExpandStats.debugMode.ToString(), false);
                ETGModConsole.Log("Debug Exceptions: " + ExpandStats.debugMode.ToString(), false);
            });

            ETGModConsole.Commands.GetGroup("expand").AddUnit("debug_dumpcurrentroomlayout", delegate (string[] e) {                
                RoomDebug.DumpCurrentRoomLayout();
            });

            ETGModConsole.Commands.GetGroup("expand").AddUnit("debug_dumpallroomlayouts", delegate (string[] e) {
                ETGModConsole.Log("Saving room layouts to PNG files. Please wait...");
                foreach (WeightedRoom wRoom in ExpandPrefabs.CastleRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.SewersRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.Gungeon_RoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.AbbeyRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.MinesRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.CatacombsRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.ForgeRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                foreach (WeightedRoom wRoom in ExpandPrefabs.BulletHellRoomTable.includedRooms.elements) {
                    if (wRoom.room != null) { RoomDebug.LogRoomToPNGFile(wRoom.room); }
                }
                ETGModConsole.Log("Room dump process complete!");
            });

            ETGModConsole.Commands.GetGroup("expand").AddUnit("debug_clearroom", delegate (string[] e) {
                RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;
                if (currentRoom != null) {
                    System.Collections.Generic.List<AIActor> enemies = currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear);

                    if (enemies != null && enemies.Count > 0) {
                        for (int i = 0; i < enemies.Count; i++) {
                            currentRoom.DeregisterEnemy(enemies[i]);
                            UnityEngine.Object.Destroy(enemies[i].gameObject);
                        }
                    }
                }
            });
        }

        private void GameManager_Awake(Action<GameManager> orig, GameManager self) {
            orig(self);
            self.OnNewLevelFullyLoaded += ExpandObjectMods.Instance.InitSpecialMods;
        }

        public void SetupItemAPI() {
            if (!ItemAPISetup) {
                try {
                    Tools.Init();
                    ItemBuilder.Init();
                    BabyGoodHammer.Init();
                    CorruptionBomb.Init();
                    ExpandRedScarf.Init();
                    TableTechAssassin.Init();
                    CorruptedJunk.Init();
                    ItemAPISetup = true;
                } catch (Exception e2) {
                    Tools.PrintException(e2, "FF0000");
                }
            }
        }

        private string[] itemList = new string[] {
            "Baby Good Hammer",
            "Corruption Bomb"
        };

        public override void Exit() { }
    }
}

