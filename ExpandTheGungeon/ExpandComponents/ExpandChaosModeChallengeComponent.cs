using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using System;
using System.Collections;
using Pathfinding;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandChaosChallengeComponent : ChallengeModifier {

        public ExpandChaosChallengeComponent() {
            DisplayName = "Apache Thunder's Revenge!";
            AlternateLanguageDisplayName = string.Empty;
            AtlasSpriteName = "Rat's_Revenge_icon_001";
            ValidInBossChambers = true;

            AlreadyIgnoredForRoomClearList = new List<string>() {
                "6ad1cafc268f4214a101dca7af61bc91", // rat
                "14ea47ff46b54bb4a98f91ffcffb656d", // rat_candle
                "1386da0f42fb4bcabc5be8feb16a7c38", // snake
                "76bc43539fc24648bff4568c75c686d1", // chicken
                "c2f902b7cbe745efb3db4399927eab34", // skusket_head
                "8b43a5c59b854eb780f9ab669ec26b7a", // dragun_egg_slimeguy
                "d1c9781fdac54d9e8498ed89210a0238", // tiny_blobulord
                "95ea1a31fc9e4415a5f271b9aedf9b15", // robots_past_critter_1
                "42432592685e47c9941e339879379d3a", // robots_past_critter_2
                "4254a93fc3c84c0dbe0a8f0dddf48a5a", // robots_past_critter_3
                "b5e699a0abb94666bda567ab23bd91c4", // bullet_kings_toadie
                "02a14dec58ab45fb8aacde7aacd25b01", // old_kings_toadie
                "566ecca5f3b04945ac6ce1f26dedbf4f", // mine_flayers_claymore
                "78a8ee40dff2477e9c2134f6990ef297", // mine_flayers_bell
                "0ff278534abb4fbaaa65d3f638003648" // poopulons_corn
            };

            PotEnemiesBannedRooms = new List<string>() {
                "Tutorial_Room_007_bosstime",
                "ResourcefulRatRoom01",
                "MetalGearRatRoom01",
                "DraGunRoom01",
                "DemonWallRoom01"
            };

            KillOnRoomClearList = new List<string> {
                "4538456236f64ea79f483784370bc62f", // fusebot
                "be0683affb0e41bbb699cb7125fdded6", // mouser
                "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                "98fdf153a4dd4d51bf0bafe43f3c77ff", // tazie
                "d4dd2b2bbda64cc9bcec534b4e920518", // bullet_kings_toadie_revenge
                "42be66373a3d4d89b91a35c9ff8adfec", // blobulin
                "b8103805af174924b578c98e95313074", // poisbulin
                "c2f902b7cbe745efb3db4399927eab34", // skusket_head
                "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                "2feb50a6a40f4f50982e89fd276f6f15", // bullat
                "2d4f8b5404614e7d8b235006acde427a", // shotgat
                "b4666cb6ef4f4b038ba8924fd8adf38f", // grenat
                "7ec3e8146f634c559a7d58b19191cd43", // spirat
                "226fd90be3a64958a5b13cb0a4f43e97", // musket_kin
                "566ecca5f3b04945ac6ce1f26dedbf4f", // mine_flayers_claymore
                "cf27dd464a504a428d87a8b2560ad40a", // tombstoner
                "f905765488874846b7ff257ff81d6d0c" // fungun
            };

            RoomEnemyGUIDList = new List<string> {
                "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                "d4a9836f8ab14f3fadd0f597438b1f1f", // mutant_bullet_kin
                "05891b158cd542b1a5f3df30fb67a7ff", // arrow_head
                "f155fd2759764f4a9217db29dd21b7eb", // mountain_cube
                "9b2cf2949a894599917d4d391a0b7394", // high_gunjurer
                "a9cc6a4e9b3d46ea871e70a03c9f77d4", // marines_past_imp
                "1cec0cdf383e42b19920787798353e46", // black_skusket
                "95ec774b5a75467a9ab05fa230c0c143", // skullmet
                "5288e86d20184fa69c91ceb642d31474", // gummy
                "d8a445ea4d944cc1b55a40f22821ae69", // muzzle_flare
                "43426a2e39584871b287ac31df04b544", // wizbang
                "8b4a938cdbc64e64822e841e482ba3d2", // jammomancer
                "ba657723b2904aa79f9e51bce7d23872", // jamerlengo
                "2ebf8ef6728648089babb507dec4edb7", // brown_chest_mimic
                "8bb5578fba374e8aae8e10b754e61d62", // cardinal
                "37340393f97f41b2822bc02d14654172", // creech
                "062b9b64371e46e195de17b6f10e47c8", // bloodbulon
                "044a9f39712f456597b9762893fbc19c", // shotgrub
                "5f3abc2d561b4b9c9e72b879c6f10c7e", // fallen_bullet_kin
                "c4fba8def15e47b297865b18e36cbef8", // gunjurer
                "f38686671d524feda75261e469f30e0b", // ammoconda_ball
                "a446c626b56d4166915a4e29869737fd", // chance_bullet_kin
                "699cd24270af4cd183d671090d8323a1", // key_bullet_kin
                "57255ed50ee24794b7aac1ac3cfb8a95", // gun_cultist
                "022d7c822bc146b58fe3b0287568aaa2", // blizzbulon
                "56f5a0f2c1fc4bc78875aea617ee31ac", // spectre
                "56fb939a434140308b8f257f0f447829", // lore_gunjurer
                "1bd8e49f93614e76b140077ff2e33f2b", // ashen_shotgun_kin
                "1a78cfb776f54641b832e92c44021cf2", // ashen_bullet_kin
                "844657ad68894a4facb1b8e1aef1abf9", // hooded_bullet
                "3f6d6b0c4a7c4690807435c7b37c35a5", // agonizer
                "383175a55879441d90933b5c4e60cf6f", // spectre_gun_nut
                "b1770e0f1c744d9d887cc16122882b4f", // executioner
                "3e98ccecf7334ff2800188c417e67c15", // killithid
                "c5b11bfc065d417b9c4d03a5e385fe2c", // professional
                "2752019b770f473193b08b4005dc781f", // veteran_shotgun_kin
                "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
                "19b420dec96d4e9ea4aebc3398c0ba7a", // bombshee
                "98ca70157c364750a60f5e0084f9d3e2", // phaser_spider
                "1bc2a07ef87741be90c37096910843ab", // chancebulon
                "45192ff6d6cb43ed8f1a874ab6bef316", // misfire_beast
                "12a054b8a6e549dcac58a82b89e319e5", // robots_past_terminator
                "556e9f2a10f9411cb9dbfd61e0e0f1e1", // convicts_past_soldier
                "226fd90be3a64958a5b13cb0a4f43e97", // musket_kin
                "df4e9fedb8764b5a876517431ca67b86", // bullet_kin_gal_titan_boss
                "1f290ea06a4c416cabc52d6b3cf47266", // bullet_kin_titan_boss
                "c4cf0620f71c4678bb8d77929fd4feff", // bullet_kin_titan
                "6f818f482a5c47fd8f38cce101f6566c", // bullet_kin_pirate
                "143be8c9bbb84e3fb3ab98bcd4cf5e5b", // bullet_kin_fish
                "06f5623a351c4f28bc8c6cda56004b80", // bullet_kin_fish_blue
                "ff4f54ce606e4604bf8d467c1279be3e", // bullet_kin_broccoli
                "39e6f47a16ab4c86bec4b12984aece4c", // bullet_kin_knight
                "f020570a42164e2699dcf57cac8a495c", // bullet_kin_kaliber
                "37de0df92697431baa47894a075ba7e9", // bullet_kin_candle
                "5861e5a077244905a8c25c2b7b4d6ebb", // bullet_kin_cowboy
                "906d71ccc1934c02a6f4ff2e9c07c9ec", // bullet_kin_officetie
                "9eba44a0ea6c4ea386ff02286dd0e6bd", // bullet_kin_officesuit
                "2b6854c0849b4b8fb98eb15519d7db1c", // bullet_kin_mech
                "05cb719e0178478685dc610f8b3e8bfc", // bullet_kin_vest
                "5f15093e6f684f4fb09d3e7e697216b4", // dynamite_kin_office
                "d4f4405e0ff34ab483966fd177f2ece3", // cylinder
                "534f1159e7cf4f6aa00aeea92459065e", // cylinder_red
                "80ab6cd15bfc46668a8844b2975c6c26", // gunzookie_office
                "981d358ffc69419bac918ca1bdf0c7f7", // bullat_gargoyle
                "e861e59012954ab2b9b6977da85cb83c", // snake_office
                "3b0bd258b4c9432db3339665cc61c356", // cactus_kin
                "4b21a913e8c54056bc05cafecf9da880", // gigi_parrot
                "78e0951b097b46d89356f004dda27c42", // tablet_bookllet
                "216fd3dfb9da439d9bd7ba53e1c76462", // necronomicon_bookllet
                "ddf12a4881eb43cfba04f36dd6377abb", // cowboy_shotgun_kin
                "86dfc13486ee4f559189de53cfb84107", // pirate_shotgun_kin
                "9215d1a221904c7386b481a171e52859" // lead_maiden_fridge
            };

            EnemySpawnOdds = 0.55f;
            EnemyResizeOdds = 0.6f;
            EnemySpawnDelay = 0.54f;
            BonusEnemySpawnOdds = 0.75f;
            AirDropExplodeChances = 0.25f;
            
            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("brave_resources_001");
            m_LootCratePrefab = assetBundle.LoadAsset<GameObject>("EmergencyCrate");
            assetBundle = null;
        }
        
        public List<string> AlreadyIgnoredForRoomClearList;
        public List<string> PotEnemiesBannedRooms;
        public List<string> KillOnRoomClearList;
        public List<string> RoomEnemyGUIDList;
        public List<string> PotEnemiesList;
        public List<string> PotPestGUIDList;

        public float EnemySpawnOdds;
        public float EnemyResizeOdds;
        public float EnemySpawnDelay;
        public float BonusEnemySpawnOdds;
        public float AirDropExplodeChances;
        
        private GameObject m_LootCratePrefab;

        private tk2dSpriteCollectionData BulletManMonochromeCollection;
        private tk2dSpriteCollectionData BulletManUpsideDownCollection;

        private IEnumerator Start() {
            
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER | GameManager.Instance.IsLoadingLevel | Dungeon.IsGenerating) { yield break; }
            if (ChallengeManager.Instance.ChallengeMode == ChallengeModeType.None) { yield break; }

            BulletManMonochromeCollection = ExpandUtility.BuildSpriteCollection(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").sprite.Collection, ExpandPrefabs.BulletManMonochromeTexture, null, ShaderCache.Acquire("tk2d/BlendVertexColorUnlitTilted"), false);
            BulletManUpsideDownCollection = ExpandUtility.BuildSpriteCollection(EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5").sprite.Collection, ExpandPrefabs.BulletManUpsideDownTexture, null, null, false);

            AlreadyIgnoredForRoomClearList.Add(ExpandCustomEnemyDatabase.RatGrenadeGUID); // rat_granade
            
            PotPestGUIDList = new List<string>() {
                "6ad1cafc268f4214a101dca7af61bc91", // rat
                "14ea47ff46b54bb4a98f91ffcffb656d", // rat_candle
                ExpandCustomEnemyDatabase.RatGrenadeGUID,
                "1386da0f42fb4bcabc5be8feb16a7c38", // snake
                "8b43a5c59b854eb780f9ab669ec26b7a", // dragun_egg_slimeguy
                "d1c9781fdac54d9e8498ed89210a0238", // tiny_blobulord
            };

            PotEnemiesList = new List<string> {
                "6ad1cafc268f4214a101dca7af61bc91", // rat
                "14ea47ff46b54bb4a98f91ffcffb656d", // rat_candle
                ExpandCustomEnemyDatabase.RatGrenadeGUID,
                "76bc43539fc24648bff4568c75c686d1", // chicken
                "1386da0f42fb4bcabc5be8feb16a7c38", // snake
                "2feb50a6a40f4f50982e89fd276f6f15", // bullat
                "b4666cb6ef4f4b038ba8924fd8adf38f", // grenat
                "2d4f8b5404614e7d8b235006acde427a", // shotgat
                "7ec3e8146f634c559a7d58b19191cd43", // spirat
                "d4dd2b2bbda64cc9bcec534b4e920518", // bullet_kings_toadie_revenge
                "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                "95ea1a31fc9e4415a5f271b9aedf9b15", // robots_past_critter_1
                "42432592685e47c9941e339879379d3a", // robots_past_critter_2
                "4254a93fc3c84c0dbe0a8f0dddf48a5a", // robots_past_critter_3
                "b5e699a0abb94666bda567ab23bd91c4", // bullet_kings_toadie
                "02a14dec58ab45fb8aacde7aacd25b01", // old_kings_toadie
                "566ecca5f3b04945ac6ce1f26dedbf4f", // mine_flayers_claymore
                "78a8ee40dff2477e9c2134f6990ef297", // mine_flayers_bell
                "8b43a5c59b854eb780f9ab669ec26b7a", // dragun_egg_slimeguy
                "d1c9781fdac54d9e8498ed89210a0238", // tiny_blobulord
                "4538456236f64ea79f483784370bc62f", // fusebot
                "b8103805af174924b578c98e95313074", // poisbulin
                "be0683affb0e41bbb699cb7125fdded6", // mouser
                "42be66373a3d4d89b91a35c9ff8adfec", // blobulin
                "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                "98fdf153a4dd4d51bf0bafe43f3c77ff", // tazie
                "226fd90be3a64958a5b13cb0a4f43e97" // musket_kin            
            };


            RoomHandler currentRoom = GameManager.Instance.BestActivePlayer.CurrentRoom;
            if (currentRoom == null) { yield break; }
            
            FlippableCover[] AllTables = FindObjectsOfType<FlippableCover>();

            if (AllTables != null) {
                if (AllTables.Length > 0) {
                    foreach (FlippableCover table in AllTables) {
                        if (table.transform.position.GetAbsoluteRoom() != null && table.transform.position.GetAbsoluteRoom() == currentRoom && table.gameObject.GetComponent<ExpandKickableObject>() == null) {
                            if (UnityEngine.Random.value <= 0.6f) {
                                table.gameObject.AddComponent<ExpandKickableObject>();
                                currentRoom.RegisterInteractable(table.gameObject.GetComponent<ExpandKickableObject>());
                                currentRoom.TransferInteractableOwnershipToDungeon(table.gameObject.GetComponent<ExpandKickableObject>());
                            }
                            if (UnityEngine.Random.value <= 0.45f) { BecomeHologram(table, BraveUtility.RandomBool()); }
                        }
                    }
                }
            }

            yield return null;

            PlaceRandomCrap(currentRoom);

            yield return null;

            List<MinorBreakable> AllBreakables = StaticReferenceManager.AllMinorBreakables;
            if (AllBreakables != null && !currentRoom.GetRoomName().StartsWith("BulletBrosRoom")) {
                if (AllBreakables.Count > 0) {
                    foreach (MinorBreakable breakable in AllBreakables) {
                        if (breakable.CenterPoint.GetAbsoluteRoom() == currentRoom) {
                            if (breakable && !breakable.IsBroken && !breakable.IgnoredForPotShotsModifier && !breakable.name.ToLower().StartsWith("stamp_tabletop")) { AddOrRemoveActionForPot(breakable); }
                            if (UnityEngine.Random.value <= 0.35f) { BecomeHologram(breakable, BraveUtility.RandomBool()); }
                        }
                    }
                }
            }

            yield return null;

            if (m_LootCratePrefab != null && currentRoom.GetRoomName() != null) {
                PlayerController SelectedPlayer = GameManager.Instance.PrimaryPlayer;
                IntVector2? RoomVectorForEnemy = FindRandomDropLocation(currentRoom, new IntVector2(3, 3));
                if (RoomVectorForEnemy!= null) {
                    if (RoomVectorForEnemy.HasValue) {
                        if (UnityEngine.Random.value < BonusEnemySpawnOdds) {
                            if (UnityEngine.Random.value < AirDropExplodeChances) {
                                ExpandUtility.SpawnParaDrop(currentRoom, RoomVectorForEnemy.Value.ToVector3());
                            } else {
                                ExpandUtility.SpawnParaDrop(currentRoom, RoomVectorForEnemy.Value.ToVector3(), EnemyDrop: BraveUtility.RandomElement(RoomEnemyGUIDList));
                            }
                            // SpawnAirDrop(m_LootCratePrefab, SelectedPlayer, RoomVectorForEnemy.Value, null, 1f, AirDropExplodeChances, false, false, BraveUtility.RandomElement(RoomEnemyGUIDList));
                        }
                    }
                }
            }

            yield return null;

            List<AIActor> activeEnemies = currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear);
            if (activeEnemies != null) {
                if (activeEnemies.Count > 0) {
                    foreach (AIActor enemy in activeEnemies) {
                        if (enemy.GetAbsoluteParentRoom() == currentRoom) {
                            if (UnityEngine.Random.value <= EnemyResizeOdds && !enemy.gameObject.GetComponent<ExpandParadropController>()) {
                                MakeTinyOrBig(enemy);
                            }
                            if (UnityEngine.Random.value <= 0.3 && !enemy.healthHaver.IsBoss) {
                                ExpandShaders.Instance.BecomeHologram(enemy, BraveUtility.RandomBool());
                            } else if (UnityEngine.Random.value <= 0.15f && !enemy.healthHaver.IsBoss) {
                                ExpandShaders.Instance.BecomeRainbow(enemy);
                            } else if (!enemy.healthHaver.IsBoss) {
                                MaybeSwapEnemySprites(enemy);
                            }
                        }
                    }
                }
            }

            yield break;
        }


        private void SpawnEnemyOnBreak(MinorBreakable breakable) {
            if (breakable.CenterPoint.GetAbsoluteRoom() != null) {
                if(breakable.CenterPoint.GetAbsoluteRoom().IsSealed && UnityEngine.Random.value <= EnemySpawnOdds) {
                    RoomHandler currentRoom = breakable.CenterPoint.GetAbsoluteRoom();
                    StartCoroutine(DelayedEnemySpawn(EnemySpawnDelay, currentRoom, breakable.CenterPoint, PotEnemiesList));
                }
            }
        }

        private void SpawnCrittersOnBreak(MinorBreakable breakable) {
            if (breakable.CenterPoint.GetAbsoluteRoom() != null) {
                if(breakable.CenterPoint.GetAbsoluteRoom().IsSealed && UnityEngine.Random.value <= EnemySpawnOdds) {
                    RoomHandler currentRoom = breakable.CenterPoint.GetAbsoluteRoom();
                    StartCoroutine(DelayedEnemySpawn(EnemySpawnDelay, currentRoom, breakable.CenterPoint, ExpandLists.PotCritterGUIDList));
                }
            }
        }

        private void SpawnPestsOnBreak(MinorBreakable breakable) {
            if (breakable.CenterPoint.GetAbsoluteRoom() != null) {
                if(breakable.CenterPoint.GetAbsoluteRoom().IsSealed && UnityEngine.Random.value <= EnemySpawnOdds) {
                    RoomHandler currentRoom = breakable.CenterPoint.GetAbsoluteRoom();
                    StartCoroutine(DelayedEnemySpawn(EnemySpawnDelay, currentRoom, breakable.CenterPoint, PotPestGUIDList));
                }
            }
        }

        private void SpawnTombstonesOnBreak(MinorBreakable breakable) {
            if (breakable.CenterPoint.GetAbsoluteRoom() != null) {
                if(breakable.CenterPoint.GetAbsoluteRoom().IsSealed && UnityEngine.Random.value <= EnemySpawnOdds) {
                    RoomHandler currentRoom = breakable.CenterPoint.GetAbsoluteRoom();
                    StartCoroutine(DelayedEnemySpawn(EnemySpawnDelay, currentRoom, breakable.CenterPoint, new List<string>() { "cf27dd464a504a428d87a8b2560ad40a" })); // tombstoner
                }
            }
        }

        private void SpawnPoisbuloidsOnBreak(MinorBreakable breakable) {
            if (breakable.CenterPoint.GetAbsoluteRoom() != null) {
                if(breakable.CenterPoint.GetAbsoluteRoom().IsSealed && UnityEngine.Random.value <= EnemySpawnOdds) {
                    RoomHandler currentRoom = breakable.CenterPoint.GetAbsoluteRoom();
                    StartCoroutine(DelayedEnemySpawn(EnemySpawnDelay, currentRoom, breakable.CenterPoint, new List<string>() { "fe3fe59d867347839824d5d9ae87f244" })); // posbuloid
                }
            }
        }

        private void SpawnMushroomOnBreak(MinorBreakable breakable) {
            if (breakable.CenterPoint.GetAbsoluteRoom() != null) {
                if(breakable.CenterPoint.GetAbsoluteRoom().IsSealed && UnityEngine.Random.value <= EnemySpawnOdds) {
                    RoomHandler currentRoom = breakable.CenterPoint.GetAbsoluteRoom();
                    StartCoroutine(DelayedEnemySpawn(EnemySpawnDelay, currentRoom, breakable.CenterPoint, new List<string>() { "f905765488874846b7ff257ff81d6d0c" })); // fungun
                }
            }
        }

        private void SpawnRubberBallOnBreak(MinorBreakable breakable) {
            if (breakable.CenterPoint.GetAbsoluteRoom() != null) {
                if(breakable.CenterPoint.GetAbsoluteRoom().IsSealed && UnityEngine.Random.value <= EnemySpawnOdds) {
                    RoomHandler currentRoom = breakable.CenterPoint.GetAbsoluteRoom();
                    StartCoroutine(DelayedEnemySpawn(EnemySpawnDelay, currentRoom, breakable.CenterPoint, new List<string>() { "226fd90be3a64958a5b13cb0a4f43e97" })); // musketkin
                }
            }
        }

        private void AddOrRemoveActionForPot(MinorBreakable breakable, bool install = true) {
            if (breakable.name.ToLower().Contains("skull") | breakable.name.ToLower().Contains("urn") |
                breakable.name.ToLower().Contains("wine_stand") | breakable.name.ToLower().Contains("armor") |
                breakable.name.ToLower().Contains("globe") | breakable.name.ToLower().Contains("mines_stamp_overturned_cart") |
                breakable.name.ToLower().Contains("pot"))
            {
                if (install) {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnEnemyOnBreak));
                } else {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Remove(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnEnemyOnBreak));
                }                
            } else if (breakable.name.ToLower().Contains("bush") | breakable.name.ToLower().Contains("bottle")) {
                if (install) {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnCrittersOnBreak));
                } else {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Remove(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnCrittersOnBreak));
                }
            } else if (breakable.name.ToLower().Contains("crate") | breakable.name.ToLower().Contains("barrel") | breakable.name.ToLower().Contains("ice")) {
                if (install) {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnPestsOnBreak));
                } else {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Remove(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnPestsOnBreak));
                }
            } else if (breakable.name.ToLower().Contains("tombstone")) {
                if (install) {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnTombstonesOnBreak));
                } else {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Remove(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnTombstonesOnBreak));
                }
            } else if (breakable.name.ToLower().Contains("yellow drum")) {
                if (install) {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnPoisbuloidsOnBreak));
                } else {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Remove(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnPoisbuloidsOnBreak));
                }
            } else if (breakable.name.ToLower().Contains("purple drum")) {
                if (install) {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnMushroomOnBreak));
                } else {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Remove(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnMushroomOnBreak));
                }
            } else if (breakable.name.ToLower().Contains("blue drum")) {
                if (install) {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnMushroomOnBreak));
                } else {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Remove(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnMushroomOnBreak));
                }
            } else {
                if (install) {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnCrittersOnBreak));
                } else {
                    breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Remove(breakable.OnBreakContext, new Action<MinorBreakable>(SpawnCrittersOnBreak));
                }
            }
        }
        
        private void SpawnAirDrop(GameObject lootCratePrefab, PlayerController player, IntVector2 roomVector, GenericLootTable overrideTable = null, float EnemyOdds = 0f, float ExplodeOdds = 0f, bool playSoundFX = true, bool usePlayerPosition = true, string EnemyGUID = "01972dee89fc4404a5c408d50007dad5") {

            EmergencyCrateController lootCrate = Instantiate(lootCratePrefab, roomVector.ToVector2().ToVector3ZUp(1f), Quaternion.identity).GetComponent<EmergencyCrateController>();
            if (lootCrate == null) { return; }
            if (player.CurrentRoom.ExtantEmergencyCrate == lootCrate.gameObject) { return; }
            if (player.CurrentRoom.ExtantEmergencyCrate != null) { return; }

            Dungeon dungeon = GameManager.Instance.Dungeon;            
            RoomHandler currentRoom = player.CurrentRoom;
            IntVector2 currentRoomSize = currentRoom.area.dimensions;
            int RoomSizeX = currentRoomSize.x;
            int RoomSizeY = currentRoomSize.y;

            lootCrate.ChanceToExplode = ExplodeOdds;
            lootCrate.ChanceToSpawnEnemy = EnemyOdds;          
            lootCrate.EnemyPlaceable = m_CustomEnemyPlacable(EnemyGUID);
          

            if (overrideTable != null) { lootCrate.gunTable = overrideTable; }

            IntVector2 bestRewardLocation = new IntVector2(1, 1);

            if (RoomSizeX < 8 && RoomSizeY < 8) { currentRoomSize = bestRewardLocation; }

            if (!usePlayerPosition) {
                bestRewardLocation = player.CurrentRoom.GetBestRewardLocation(currentRoom.area.dimensions, RoomHandler.RewardLocationStyle.CameraCenter, true);
                lootCrate.Trigger(new Vector3(-5f, -5f, -5f), bestRewardLocation.ToVector3() + new Vector3(15f, 15f, 15f), player.CurrentRoom, overrideTable == null);
                GameManager.Instance.Dungeon.data[bestRewardLocation].PreventRewardSpawn = true;
            } else {
                bestRewardLocation = player.CurrentRoom.GetBestRewardLocation(new IntVector2(1, 1), RoomHandler.RewardLocationStyle.CameraCenter, true);
                lootCrate.Trigger(new Vector3(-5f, -5f, -5f), bestRewardLocation.ToVector3() + new Vector3(15f, 15f, 15f), player.CurrentRoom, overrideTable == null);
                GameManager.Instance.Dungeon.data[bestRewardLocation].PreventRewardSpawn = true;
            }

            dungeon.data[bestRewardLocation].PreventRewardSpawn = true;
            player.CurrentRoom.ExtantEmergencyCrate = lootCrate.gameObject;
            if (playSoundFX) { AkSoundEngine.PostEvent("Play_OBJ_supplydrop_activate_01", player.CurrentRoom.ExtantEmergencyCrate); }
            return;
        }

        private void PlaceRandomCrap(RoomHandler currentRoom, bool debugMode = false) {
            
            PlayerController player = GameManager.Instance.BestActivePlayer;
            Dungeon dungeon = GameManager.Instance.Dungeon;
            
            if (dungeon.IsGlitchDungeon) { return; }
            if (currentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS) { return; }
            if (currentRoom.GetRoomName() == null) { return; }
            
            bool RatCorpsePlaced = false;
            bool BabyDragunPlaced = false;
            int RandomObjectsPlaced = 0;
            int RandomObjectsSkipped = 0;
            int MaxRandomObjectsForRoom = UnityEngine.Random.Range(2, 5);            

            List<GameObject> TableObjects = new List<GameObject>();
            List<GameObject> KickableDrumObjects = new List<GameObject>();
            List<GameObject> InteractableNPCs = new List<GameObject>();
            List<GameObject> NonInteractableObjects = new List<GameObject>();

            List<DungeonPlaceable> CoffinObjects = new List<DungeonPlaceable>();
            List<DungeonPlaceable> MiscPlacables = new List<DungeonPlaceable>();

            List<IntVector2> NonCachedList = new List<IntVector2>();
            List<IntVector2> SharedCachedList = new List<IntVector2>();
            List<IntVector2> CachedNPCs = new List<IntVector2>();
            List<IntVector2> CachedVFXObjects = new List<IntVector2>();
            List<IntVector2> CachedChests = new List<IntVector2>();

            

            if (debugMode)ETGModConsole.Log("[DEBUG] Clearing object list for preoperation of new floor...", true);
            TableObjects.Clear();
            KickableDrumObjects.Clear();
            InteractableNPCs.Clear();
            NonInteractableObjects.Clear();
            CoffinObjects.Clear();
            MiscPlacables.Clear();

            if (debugMode)ETGModConsole.Log("[DEBUG] Building KickableDrumObjects list...", true);
            KickableDrumObjects.Add(ExpandObjectDatabase.RedDrum);
            KickableDrumObjects.Add(ExpandObjectDatabase.YellowDrum);
            KickableDrumObjects.Add(ExpandObjectDatabase.WaterDrum);
            if (debugMode)ETGModConsole.Log("[DEBUG] Building TableObjects list...", true);
            TableObjects.Add(ExpandObjectDatabase.TableHorizontal);
            TableObjects.Add(ExpandObjectDatabase.TableVertical);
            TableObjects.Add(ExpandObjectDatabase.TableHorizontalStone);
            TableObjects.Add(ExpandObjectDatabase.TableVerticalStone);
            CoffinObjects.Add(ExpandObjectDatabase.CoffinHorizontal);
            CoffinObjects.Add(ExpandObjectDatabase.CoffinVertical);            
            if (debugMode)ETGModConsole.Log("[DEBUG] Building NPC list...", true);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCOldMan);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCGunMuncher);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCEvilMuncher);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCMonsterManuel);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCVampire);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCGuardLeft);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCGuardRight);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCTruthKnower);
            InteractableNPCs.Add(ExpandObjectDatabase.NPCSynergrace);
            InteractableNPCs.Add(ExpandObjectDatabase.CultistBaldBowLeft);
            if (debugMode)ETGModConsole.Log("[DEBUG] Building NonInteractableObjects list...", true);
            NonInteractableObjects.Add(ExpandObjectDatabase.AmygdalaNorth);
            NonInteractableObjects.Add(ExpandObjectDatabase.AmygdalaSouth);
            NonInteractableObjects.Add(ExpandObjectDatabase.AmygdalaWest);
            NonInteractableObjects.Add(ExpandObjectDatabase.AmygdalaEast);
            NonInteractableObjects.Add(ExpandObjectDatabase.SpaceFog);
            NonInteractableObjects.Add(ExpandObjectDatabase.LockedDoor);
            // NonInteractableObjects.Add(LockedJailDoor);
            NonInteractableObjects.Add(ExpandObjectDatabase.SpikeTrap);
            NonInteractableObjects.Add(ExpandObjectDatabase.FlameTrap);
            NonInteractableObjects.Add(ExpandObjectDatabase.FakeTrap);
            NonInteractableObjects.Add(ExpandObjectDatabase.PlayerCorpse);
            NonInteractableObjects.Add(ExpandObjectDatabase.TimefallCorpse);
            NonInteractableObjects.Add(ExpandObjectDatabase.HangingPot);
            NonInteractableObjects.Add(ExpandObjectDatabase.IceBomb);
            NonInteractableObjects.Add(ExpandObjectDatabase.DoorsVertical);
            NonInteractableObjects.Add(ExpandObjectDatabase.DoorsHorizontal);
            NonInteractableObjects.Add(ExpandObjectDatabase.BigDoorsVertical);
            NonInteractableObjects.Add(ExpandObjectDatabase.BigDoorsHorizontal);
            NonInteractableObjects.Add(ExpandObjectDatabase.CultistBaldBowBackLeft);
            NonInteractableObjects.Add(ExpandObjectDatabase.CultistBaldBowBackRight);
            NonInteractableObjects.Add(ExpandObjectDatabase.CultistBaldBowBack);
            NonInteractableObjects.Add(ExpandObjectDatabase.CultistHoodBowBack);
            NonInteractableObjects.Add(ExpandObjectDatabase.CultistHoodBowLeft);
            NonInteractableObjects.Add(ExpandObjectDatabase.CultistHoodBowRight);
            // NonInteractableObjects.Add(ExpandObjectDatabase.TallGrassStrip);
            NonInteractableObjects.Add(ExpandObjectDatabase.RatTrapDoorIcon);
            NonInteractableObjects.Add(ExpandPrefabs.MouseTrap1);
            NonInteractableObjects.Add(ExpandPrefabs.MouseTrap2);
            NonInteractableObjects.Add(ExpandPrefabs.MouseTrap3);
            NonInteractableObjects.Add(ExpandPrefabs.PlayerLostRatNote);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_01);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_02);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_03);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_04);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_05);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_06);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_07);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_08);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_09);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_10);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_11);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_12);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_13);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_14);
            NonInteractableObjects.Add(ExpandObjectDatabase.ConvictPastCrowdNPC_15);
            if (debugMode) ETGModConsole.Log("[DEBUG] Building MiscPlacables list...", true);
            MiscPlacables.Add(ExpandObjectDatabase.Sarcophogus);
            MiscPlacables.Add(ExpandObjectDatabase.CursedPot);
            MiscPlacables.Add(ExpandObjectDatabase.GodRays);
            // MiscPlacables.Add(PitTrap);
            // MiscPlacables.Add(SpecialTraps);
            InteractableNPCs.Add(ExpandPrefabs.MimicNPC);            
            
            
            PrototypeDungeonRoom.RoomCategory roomCategory = currentRoom.area.PrototypeRoomCategory;

            if (debugMode && !string.IsNullOrEmpty(currentRoom.GetRoomName())) {
                ETGModConsole.Log("[DEBUG] Preparing to check/place objects in room: " + currentRoom.GetRoomName(), true);
                ETGModConsole.Log("[DEBUG] Current Room Cetegory: " + roomCategory, true);
            }                
            
            try {
                if (debugMode) ETGModConsole.Log("[DEBUG] Current Room is valid for random objects. Continuing...", true);
                if (debugMode) ETGModConsole.Log("[DEBUG] Preparing to check/place objects in room: " + currentRoom.GetRoomName(), false);
                if (debugMode) ETGModConsole.Log("[DEBUG] Shuffling Object lists...", true);
                KickableDrumObjects = KickableDrumObjects.Shuffle();
                TableObjects = TableObjects.Shuffle();
                CoffinObjects = CoffinObjects.Shuffle();
                InteractableNPCs = InteractableNPCs.Shuffle();
                NonInteractableObjects = NonInteractableObjects.Shuffle();
                MiscPlacables = MiscPlacables.Shuffle();
                RatCorpsePlaced = false;
                if (debugMode)ETGModConsole.Log("[DEBUG] Clearing cached object placement lists for new room...", true);
                NonCachedList.Clear();
                CachedNPCs.Clear();
                CachedChests.Clear();
                SharedCachedList.Clear();
                CachedVFXObjects.Clear();
                
                for (int loopCount = 0; loopCount < MaxRandomObjectsForRoom; ++loopCount) {
                    if ((RandomObjectsPlaced + RandomObjectsSkipped) >= MaxRandomObjectsForRoom) {
                        if (debugMode)ETGModConsole.Log("[DEBUG] Max Object Placement Reached. Ending object placement mode...", true);
                        break;
                    }
                    if (debugMode)ETGModConsole.Log("[DEBUG] Setting up random placement vectors for current room...", true);
                    IntVector2 RandomHammerIntVector2 = GetRandomAvailableCellForPlacable(dungeon, currentRoom, NonCachedList, false, false);
                    IntVector2 RandomChestVectorAlt = GetRandomAvailableCellForPlacable(dungeon, currentRoom, CachedNPCs, true, false);
                    IntVector2 RandomVFXVector = GetRandomAvailableCellForPlacable(dungeon, currentRoom, NonCachedList, false, true);
                    IntVector2 RandomNPCVector = GetRandomAvailableCellForNPC(dungeon, currentRoom, CachedNPCs, true);
                    IntVector2 RandomRatNPCVector = GetRandomAvailableCellForNPC(dungeon, currentRoom, CachedNPCs, true);
                    IntVector2 RandomTableVector = GetRandomAvailableCellForPlacable(dungeon, currentRoom, SharedCachedList, true, false, 2);
                    IntVector2 RandomDrumVector = GetRandomAvailableCellForPlacable(dungeon, currentRoom, SharedCachedList, true, false);
                    IntVector2 RandomMiscObjectVector = GetRandomAvailableCellForPlacable(dungeon, currentRoom, SharedCachedList, true, true, 2);
                    IntVector2 RandomCurseMirrorVector = GetRandomAvailableCellForNPC(dungeon, currentRoom, CachedNPCs, true);
                    IntVector2 RandomMiscPlacableVector = GetRandomAvailableCellForPlacable(dungeon, currentRoom, SharedCachedList, true, true, 2);
                    IntVector2 RandomSpecialTrapsVector = GetRandomAvailableCellForPlacable(dungeon, currentRoom, SharedCachedList, true, false, 2);
                    IntVector2 RandomSarcophogusVector = GetRandomAvailableCellForPlacable(dungeon, currentRoom, SharedCachedList, true, false, 2);
                    IntVector2 RandomBabyDragunVector = GetRandomAvailableCellForPlacable(dungeon, currentRoom, SharedCachedList, true, false, 2);
                
                
                    if (RandomNPCVector != IntVector2.Zero) {
                        if (UnityEngine.Random.value <= 0.03) {
                            GameObject SelectedNPCObject = BraveUtility.RandomElement(InteractableNPCs).gameObject;
                            if (debugMode)ETGModConsole.Log("[DEBUG] Attempting to place NPC object: " + SelectedNPCObject, true);
                            bool isMimicNPC = false;
                            if (SelectedNPCObject.name.StartsWith(ExpandPrefabs.MimicNPC.name)) { isMimicNPC = true; }
                
                            GameObject Random_InteractableNPC = DungeonPlaceableUtility.InstantiateDungeonPlaceable(SelectedNPCObject, currentRoom, RandomNPCVector, isMimicNPC);
                            
                            if (Random_InteractableNPC) {
                                Random_InteractableNPC.transform.parent = currentRoom.hierarchyParent;
                                if (isMimicNPC) {
                                    Destroy(Random_InteractableNPC.GetComponent<MysteryMimicManController>());
                                    isMimicNPC = false;
                                }                                                    
                                TalkDoerLite RandomNPCComponent = Random_InteractableNPC.GetComponent<TalkDoerLite>();
                                RandomNPCComponent.transform.position.XY().GetAbsoluteRoom().RegisterInteractable(RandomNPCComponent);
                                SpeculativeRigidbody InteractableRigidNPC = RandomNPCComponent.GetComponentInChildren<SpeculativeRigidbody>();
                                InteractableRigidNPC.Initialize();
                                InteractableRigidNPC.PrimaryPixelCollider.Enabled = false;
                                InteractableRigidNPC.HitboxPixelCollider.Enabled = false;
                                InteractableRigidNPC.CollideWithOthers = false;
                                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(InteractableRigidNPC, null, false);
                                RandomObjectsPlaced++;
                                if (debugMode)ETGModConsole.Log("[DEBUG] Success!", true);
                            } else { RandomObjectsSkipped++; }
                        }
                    } else { if (UnityEngine.Random.value <= 0.25)RandomObjectsSkipped++; }
                
                    if (!RatCorpsePlaced && RandomRatNPCVector != IntVector2.Zero) {
                        if (UnityEngine.Random.value <= 0.05) {
                            GameObject SelectedRatNPCObject = ExpandPrefabs.RatCorpseNPC;
                            if (debugMode) ETGModConsole.Log("[DEBUG] Attempting to place rat object: " + SelectedRatNPCObject, true);
                            GameObject RatCorpseObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(SelectedRatNPCObject, currentRoom, RandomRatNPCVector, false);
                            TalkDoerLite RatNPCComponent = RatCorpseObject.GetComponent<TalkDoerLite>();
                            // Spawn him in already dead state. Don't want player to accidently start convo with him during combat.
                            // Player would likely die by the time the rat finishes his long sob story. :P
                            RatNPCComponent.playmakerFsm.SetState("Set Mode");
                            if (RatNPCComponent) {
                                currentRoom.RegisterInteractable(RatNPCComponent);
                                currentRoom.TransferInteractableOwnershipToDungeon(RatNPCComponent);
                            }
                            // ExpandUtility.AddHealthHaver(RatNPCComponent.gameObject, 40, flashesOnDamage: false, exploderSpawnsItem: BraveUtility.RandomBool());
                            ExpandUtility.GenerateHealthHaver(RatNPCComponent.gameObject, 40, flashesOnDamage: false, exploderSpawnsItem: BraveUtility.RandomBool(), isCorruptedObject: false, isRatNPC: true);
                            RatCorpsePlaced = true;
                            RandomObjectsPlaced++;
                            if (debugMode) ETGModConsole.Log("[DEBUG] Success!", true);
                        }
                    } else { if (UnityEngine.Random.value <= 0.25) RandomObjectsSkipped++; }
                    
                    if (RandomTableVector != IntVector2.Zero) {
                        if (BraveUtility.RandomBool()) {
                            GameObject RandomTableObject = BraveUtility.RandomElement(TableObjects).gameObject;
                            if (debugMode)ETGModConsole.Log("[DEBUG] Attempting to place Table Object: " + RandomTableObject, true);
                            GameObject RandomTable = DungeonPlaceableUtility.InstantiateDungeonPlaceable(RandomTableObject, currentRoom, RandomTableVector, false);
                            
                            if (RandomTable) {
                                RandomTable.transform.parent = currentRoom.hierarchyParent;
                                FlippableCover RandomTableCoverComponent = RandomTable.GetComponent<FlippableCover>();
                                RandomTable.AddComponent<ExpandKickableObject>();
                                ExpandKickableObject RandomTableKickableComponent = RandomTable.GetComponent<ExpandKickableObject>();
                                currentRoom.RegisterInteractable(RandomTableCoverComponent);
                                currentRoom.RegisterInteractable(RandomTableKickableComponent);
                                SpeculativeRigidbody TableComponentInChildren = RandomTable.GetComponentInChildren<SpeculativeRigidbody>();
                                RandomTableCoverComponent.ConfigureOnPlacement(currentRoom);
                                TableComponentInChildren.Initialize();
                                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(TableComponentInChildren, null, false);
                                if (UnityEngine.Random.value <= 0.4f) { BecomeHologram(RandomTableCoverComponent, BraveUtility.RandomBool()); }
                                RandomObjectsPlaced++;
                                if (debugMode)ETGModConsole.Log("[DEBUG] Success!", true);
                            } else { RandomObjectsSkipped++; }
                        } else {
                            if (UnityEngine.Random.value <= 0.25) {
                                if (debugMode)ETGModConsole.Log("[DEBUG] Attempting to place Table Object: " + ExpandObjectDatabase.FoldingTable.name, true);
                                GameObject portableTableInstance = DungeonPlaceableUtility.InstantiateDungeonPlaceable(ExpandObjectDatabase.FoldingTable, currentRoom, RandomTableVector, false);
                                portableTableInstance.transform.parent = currentRoom.hierarchyParent;
                                portableTableInstance.AddComponent<ExpandKickableObject>();
                                FlippableCover portableTableCoverComponent = portableTableInstance.GetComponent<FlippableCover>();
                                ExpandKickableObject portableTableKickableComponent = portableTableInstance.GetComponent<ExpandKickableObject>();
                                currentRoom.RegisterInteractable(portableTableCoverComponent);
                                currentRoom.RegisterInteractable(portableTableKickableComponent);
                                SpeculativeRigidbody TableComponentInChildren = portableTableInstance.GetComponentInChildren<SpeculativeRigidbody>();
                                portableTableCoverComponent.ConfigureOnPlacement(currentRoom);
                                TableComponentInChildren.Initialize();
                                if (UnityEngine.Random.value <= 0.4f) { BecomeHologram(portableTableCoverComponent, BraveUtility.RandomBool()); }
                                if (debugMode)ETGModConsole.Log("[DEBUG] Success!", true);
                                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(TableComponentInChildren, null, false);
                            } else {
                                if (UnityEngine.Random.value <= 0.3) {
                                    GameObject BrazierPlaced = ExpandObjectDatabase.Brazier.InstantiateObject(currentRoom, RandomTableVector);
                                    BrazierPlaced.transform.parent = currentRoom.hierarchyParent;
                                    BrazierPlaced.AddComponent<ExpandKickableObject>();
                                    BrazierController BrazierComponent = BrazierPlaced.GetComponent<BrazierController>();
                                    ExpandKickableObject BrazierKickableComponent = BrazierPlaced.GetComponent<ExpandKickableObject>();
                                    SpeculativeRigidbody BrazierComponentInChildren = BrazierPlaced.GetComponentInChildren<SpeculativeRigidbody>();
                                    currentRoom.RegisterInteractable(BrazierComponent);
                                    currentRoom.RegisterInteractable(BrazierKickableComponent);
                                    BrazierComponentInChildren.Initialize();
                                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(BrazierComponentInChildren, null, false);
                                } else {
                                    GameObject CoffinObject = BraveUtility.RandomElement(CoffinObjects).InstantiateObject(currentRoom, RandomTableVector);
                                    CoffinObject.transform.parent = currentRoom.hierarchyParent;
                                    CoffinObject.AddComponent<ExpandKickableObject>();
                                    FlippableCover CoffinComponent = CoffinObject.GetComponent<FlippableCover>();
                                    ExpandKickableObject CoffinKickableComponent = CoffinObject.GetComponent<ExpandKickableObject>();
                                    SpeculativeRigidbody CoffinComponentInChildren = CoffinObject.GetComponentInChildren<SpeculativeRigidbody>();
                                    currentRoom.RegisterInteractable(CoffinComponent);
                                    currentRoom.RegisterInteractable(CoffinKickableComponent);
                                    CoffinComponentInChildren.Initialize();
                                    if (UnityEngine.Random.value <= 0.4f) { BecomeHologram(CoffinComponent, BraveUtility.RandomBool()); }
                                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(CoffinComponentInChildren, null, false);
                                }
                            }
                       }
                   } else { if (UnityEngine.Random.value <= 0.25)RandomObjectsSkipped++; }
                
                    if (RandomDrumVector != IntVector2.Zero) {
                        if (BraveUtility.RandomBool()) {
                            GameObject SelectedDrumObject = BraveUtility.RandomElement(KickableDrumObjects).gameObject;
                            if (debugMode)ETGModConsole.Log("[DEBUG] Attempting to place Drum object: " + SelectedDrumObject, true);
                            GameObject RandomDrumObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(SelectedDrumObject, currentRoom, RandomDrumVector, false);
                            if (RandomDrumObject) {
                                RandomDrumObject.transform.parent = currentRoom.hierarchyParent;
                                KickableObject DrumComponent = RandomDrumObject.GetComponent<KickableObject>();
                                currentRoom.RegisterInteractable(DrumComponent);
                                DrumComponent.ConfigureOnPlacement(currentRoom);
                                SpeculativeRigidbody InteractableRigidDrum = DrumComponent.GetComponentInChildren<SpeculativeRigidbody>();
                                InteractableRigidDrum.Initialize();
                                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(InteractableRigidDrum, null, false);
                                RandomObjectsPlaced++;
                                if (debugMode)ETGModConsole.Log("[DEBUG] Success!", true);
                            }
                        } else {
                            if (UnityEngine.Random.value <= 0.4) {
                                if (debugMode) ETGModConsole.Log("[DEBUG] Attempting to place Exploding Drum/Barrel object.", true);
                                GameObject SelectedExplodyBarrel = ExpandObjectDatabase.ExplodyBarrel.InstantiateObject(currentRoom, RandomDrumVector);
                                if (SelectedExplodyBarrel) {
                                    SelectedExplodyBarrel.transform.parent = currentRoom.hierarchyParent;
                                    KickableObject ExplodyBarrelComponent = SelectedExplodyBarrel.GetComponentInChildren<KickableObject>();
                                    currentRoom.RegisterInteractable(ExplodyBarrelComponent);
                                    SpeculativeRigidbody InteractableRigidExplody = ExplodyBarrelComponent.GetComponentInChildren<SpeculativeRigidbody>();
                                    InteractableRigidExplody.Initialize();
                                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(InteractableRigidExplody, null, false);
                                }
                                if (debugMode) ETGModConsole.Log("[DEBUG] Success!", true);
                            }
                        }
                    } else { if (UnityEngine.Random.value <= 0.3)RandomObjectsSkipped++; }
                
                    if (RandomMiscObjectVector != IntVector2.Zero) {
                        GameObject SelectedNonInteractable = BraveUtility.RandomElement(NonInteractableObjects).gameObject;
                        if (debugMode)ETGModConsole.Log("[DEBUG] Attempting to place Misc Object: " + SelectedNonInteractable, true);                                         
                                                                
                        GameObject PlacedMiscObject = DungeonPlaceableUtility.InstantiateDungeonPlaceable(SelectedNonInteractable, currentRoom, RandomMiscObjectVector, false);
                        PlacedMiscObject.transform.parent = currentRoom.hierarchyParent;
                        if (SelectedNonInteractable == ExpandObjectDatabase.CultistBaldBowBack | SelectedNonInteractable == ExpandObjectDatabase.CultistBaldBowBackLeft |
                            SelectedNonInteractable == ExpandObjectDatabase.CultistBaldBowBackRight | SelectedNonInteractable == ExpandObjectDatabase.CultistBaldBowBack |
                            SelectedNonInteractable == ExpandObjectDatabase.CultistHoodBowBack | SelectedNonInteractable == ExpandObjectDatabase.CultistHoodBowLeft |
                            SelectedNonInteractable == ExpandObjectDatabase.CultistHoodBowRight | SelectedNonInteractable == ExpandObjectDatabase.NPCHeartDispenser)
                        {
                            SpeculativeRigidbody SelectedNonInteractableRigidBody = PlacedMiscObject.GetComponentInChildren<SpeculativeRigidbody>();
                            SelectedNonInteractableRigidBody.PrimaryPixelCollider.Enabled = false;                                                
                        }
                        if (SelectedNonInteractable == ExpandObjectDatabase.LockedDoor) {
                            SpeculativeRigidbody SelectedDoorRigidBody = PlacedMiscObject.GetComponentInChildren<SpeculativeRigidbody>();
                            SelectedDoorRigidBody.CollideWithOthers = false;
                        }
                        if (SelectedNonInteractable == ExpandPrefabs.PlayerLostRatNote) {
                            if (PlacedMiscObject) {
                                NoteDoer RatNote = PlacedMiscObject.GetComponent<NoteDoer>();
                                currentRoom.RegisterInteractable(RatNote);
                            }
                        }
                       
                        if (SelectedNonInteractable == ExpandObjectDatabase.IceBomb) {
                            if (PlacedMiscObject) {
                                IPlayerInteractable[] IceBombInterfacesInChildren = PlacedMiscObject.GetInterfacesInChildren<IPlayerInteractable>();
                                for (int i = 0; i < IceBombInterfacesInChildren.Length; i++) { currentRoom.RegisterInteractable(IceBombInterfacesInChildren[i]); }
                                SpeculativeRigidbody IceBombRigidBody = PlacedMiscObject.GetComponentInChildren<SpeculativeRigidbody>();
                                IceBombRigidBody.Initialize();
                                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(IceBombRigidBody, null, false);
                            }
                        }
                        
                        if (debugMode)ETGModConsole.Log("[DEBUG] Success!", true);
                        RandomObjectsPlaced++;
                    } else { RandomObjectsSkipped++; }
                
                    if (RandomMiscPlacableVector != IntVector2.Zero && UnityEngine.Random.value <= 0.3) {
                        DungeonPlaceable SelectedMiscPlacable = BraveUtility.RandomElement(MiscPlacables);
                        GameObject MiscPlacableObject = SelectedMiscPlacable.InstantiateObject(currentRoom, RandomMiscPlacableVector);
                        if (SelectedMiscPlacable == ExpandObjectDatabase.Sarcophogus) {
                            SpeculativeRigidbody SarcophogusRigidBody = MiscPlacableObject.GetComponentInChildren<SpeculativeRigidbody>();
                            SarcophogusRigidBody.CollideWithOthers = false;
                        }
                        if (BraveUtility.RandomBool()) { RandomObjectsPlaced++; }
                    }  
                    
                    if (RandomBabyDragunVector != IntVector2.Zero && UnityEngine.Random.value <= 0.08) {
                        GameObject babyDragun = DungeonPlaceableUtility.InstantiateDungeonPlaceable(ExpandPrefabs.NPCBabyDragunChaos, currentRoom, RandomMiscObjectVector, false);
                        if (babyDragun) { babyDragun.AddComponent<ExpandBabyDragunComponent>(); }
                        if (!BabyDragunPlaced) { BabyDragunPlaced = true; }
                    }
                }
            } catch (Exception ex) {
                if (debugMode)ETGModConsole.Log("[DEBUG] Exception while setting up or placing objects for current room: " + currentRoom.GetRoomName(), true);
                if (debugMode)ETGModConsole.Log("[DEBUG] Skipping current room...", true);
                if (ExpandSettings.debugMode) { ETGModConsole.Log(ex.Message + ex.StackTrace + ex.Source, debugMode); }
                return;
            }

            
            if (debugMode)ETGModConsole.Log("[DEBUG] Finished placing objects. Preparing to exit...", true);
            if (ExpandSettings.debugMode) {
                ETGModConsole.Log("[DEBUG] Max Number of Objects assigned to room: " + MaxRandomObjectsForRoom, false);
                ETGModConsole.Log("[DEBUG] Number of Objects placed: " + RandomObjectsPlaced, false);
                ETGModConsole.Log("[DEBUG] Number of Objects skipped: " + RandomObjectsSkipped, false);
                if (RandomObjectsPlaced <= 0) { ETGModConsole.Log("[DEBUG] Error: No Objects have been placed.", false); }
            }
            if (debugMode)ETGModConsole.Log("[DEBUG] Clearing all lists before exit...", true);
            NonCachedList.Clear();
            CachedNPCs.Clear();
            SharedCachedList.Clear();
            CachedVFXObjects.Clear();
            TableObjects.Clear();
            KickableDrumObjects.Clear();
            InteractableNPCs.Clear();
            NonInteractableObjects.Clear();
        }

        private void MakeTinyOrBig(AIActor aiActor, bool delayed = false) {
            if (string.IsNullOrEmpty(aiActor.EnemyGuid)) { return; }
            if (aiActor.name.ToLower().StartsWith("glitched") | aiActor.name.ToLower().EndsWith("(clone)(clone)")) { return; }
            if (ExpandLists.BannedEnemyGUIDList.Contains(aiActor.EnemyGuid)) { return; }
            if (aiActor.GetComponentInParent<BossStatueController>() != null) { return; }
            try {
                if (aiActor.transform.position.GetAbsoluteRoom().GetRoomName().ToLower().StartsWith("doublebeholsterroom01")) { return; }
            } catch (Exception) { return; }
            int currentFloor = GameManager.Instance.CurrentFloor;
            
            if (UnityEngine.Random.value <= 0.5f) {
                // Make them tiny bois :P
                // Don't make cursed bois tiny. It can be a bit much to get hurt by tiny bois that are cursed. :P 
                if (aiActor.IsBlackPhantom) {
                    aiActor.StartCoroutine(ResizeEnemy(aiActor, new Vector2(1.5f, 1.5f), false, true, delayed));
                } else {
                    aiActor.StartCoroutine(ResizeEnemy(aiActor, new Vector2(0.5f, 0.5f), false, false, delayed));
                }
            } else {
                // Make them big bois :P
                aiActor.StartCoroutine(ResizeEnemy(aiActor, new Vector2(1.5f, 1.5f), false, delayed));
            }
            aiActor.placeableWidth += 2;
            aiActor.placeableHeight += 2;            
            return;
        }
        
        private void EnemyScale(AIActor aiActor, Vector2 ScaleVector) {
            aiActor.transform.localScale = ScaleVector.ToVector3ZUp(1f);
            aiActor.HasShadow = false;
            // aiActor.CorpseObject.transform.localScale = actorSize.ToVector3ZUp(1f);
            int cachedLayer = aiActor.gameObject.layer;
            int cachedOutlineLayer = cachedLayer;
            aiActor.gameObject.layer = LayerMask.NameToLayer("Unpixelated");
            cachedOutlineLayer = SpriteOutlineManager.ChangeOutlineLayer(aiActor.sprite, LayerMask.NameToLayer("Unpixelated"));

            if (aiActor.specRigidbody) {
                SpeculativeRigidbody specRigidbody = aiActor.GetComponent<SpeculativeRigidbody>();
                // if (specRigidbody == null) { return; }
                specRigidbody.transform.localScale = ScaleVector.ToVector3ZUp(1f);
                for (int i = 0; i < specRigidbody.PixelColliders.Count; i++) {
                    specRigidbody.PixelColliders[i].Regenerate(specRigidbody.transform, true, true);
                }
                specRigidbody.UpdateCollidersOnScale = true;
                specRigidbody.RegenerateColliders = true;
                specRigidbody.ForceRegenerate(true, true);
                specRigidbody.RegenerateCache();
            }
            ExpandUtility.CorrectForWalls(aiActor);
        }

        private void BecomeHologram(MinorBreakable breakable, bool isGreen = false) {
            try {
                if (breakable == null) { return; }

                tk2dBaseSprite sprite = null;
                try {
                    if (!(breakable.sprite is tk2dBaseSprite)) return;
                    sprite = breakable.sprite;
                } catch { };
                if (sprite == null) { return; }  
                    
                if (string.IsNullOrEmpty(breakable.gameObject.name)) { return; }                
        
                if (breakable.gameObject.GetComponent<PlayerController>() != null ) { return; }
                if (breakable.gameObject.GetComponentInChildren<PlayerController>() != null) { return; }
                if (breakable.gameObject.transform != null && breakable.gameObject.transform.position.GetAbsoluteRoom() != null) {
                    if (breakable.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith("doublebeholsterroom01")) { return; }
                    if (breakable.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith("bossstatuesroom01")) { return; }
                    if (breakable.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith("boss foyer")) { return; }
                    if (GameManager.Instance.Dungeon.data.Entrance != null) {
                        if (breakable.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith(GameManager.Instance.Dungeon.data.Entrance.GetRoomName())) {
                            return;
                        }
                    }
                }
                
                ExpandShaders.Instance.ApplyHologramShader(sprite, isGreen, true);

                if (breakable.gameObject.GetComponent<SpeculativeRigidbody>() != null) {
                    SpeculativeRigidbody CurrentObjectRigidBody = breakable.gameObject.GetComponent<SpeculativeRigidbody>();
                    CurrentObjectRigidBody.RegisterSpecificCollisionException(GameManager.Instance.PrimaryPlayer.specRigidbody);
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        CurrentObjectRigidBody.specRigidbody.RegisterSpecificCollisionException(GameManager.Instance.SecondaryPlayer.specRigidbody);
                    }
                } else if (breakable.GetComponentInChildren<SpeculativeRigidbody>() != null) {
                    SpeculativeRigidbody CurrentObjectRigidBody = breakable.GetComponentInChildren<SpeculativeRigidbody>();
                    CurrentObjectRigidBody.RegisterSpecificCollisionException(GameManager.Instance.PrimaryPlayer.specRigidbody);
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        CurrentObjectRigidBody.specRigidbody.RegisterSpecificCollisionException(GameManager.Instance.SecondaryPlayer.specRigidbody);
                    }
                }

            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("Exception Caught at [BecomeHologram] in ExpandChaosModeChallengeComponent!", false);
                    ETGModConsole.Log(ex.Message + ex.Source, false);
                    ETGModConsole.Log(ex.StackTrace, false);
                }
                return;
            }
        }

        private void BecomeHologram(FlippableCover table, bool isGreen = false) {
            try {
                if (table == null) { return; }

                tk2dBaseSprite sprite = null;
                try {
                    if (!(table.sprite is tk2dBaseSprite)) return;
                    sprite = table.sprite;
                } catch { };
                if (sprite == null) { return; }  
                    
                if (string.IsNullOrEmpty(table.gameObject.name)) { return; }                
        
                if (table.gameObject.GetComponent<PlayerController>() != null ) { return; }
                if (table.gameObject.GetComponentInChildren<PlayerController>() != null) { return; }
                if (table.gameObject.transform != null && table.gameObject.transform.position.GetAbsoluteRoom() != null) {
                    if (table.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith("doublebeholsterroom01")) { return; }
                    if (table.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith("bossstatuesroom01")) { return; }
                    if (table.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith("boss foyer")) { return; }
                    if (GameManager.Instance.Dungeon.data.Entrance != null) {
                        if (table.gameObject.transform.position.GetAbsoluteRoom().GetRoomName().StartsWith(GameManager.Instance.Dungeon.data.Entrance.GetRoomName())) {
                            return;
                        }
                    }
                }
                
                ExpandShaders.Instance.ApplyHologramShader(sprite, isGreen, true);

                if (table.gameObject.GetComponent<SpeculativeRigidbody>() != null) {
                    SpeculativeRigidbody CurrentObjectRigidBody = table.gameObject.GetComponent<SpeculativeRigidbody>();
                    CurrentObjectRigidBody.RegisterSpecificCollisionException(GameManager.Instance.PrimaryPlayer.specRigidbody);
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        CurrentObjectRigidBody.specRigidbody.RegisterSpecificCollisionException(GameManager.Instance.SecondaryPlayer.specRigidbody);
                    }
                } else if (table.GetComponentInChildren<SpeculativeRigidbody>() != null) {
                    SpeculativeRigidbody CurrentObjectRigidBody = table.GetComponentInChildren<SpeculativeRigidbody>();
                    CurrentObjectRigidBody.RegisterSpecificCollisionException(GameManager.Instance.PrimaryPlayer.specRigidbody);
                    if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                        CurrentObjectRigidBody.specRigidbody.RegisterSpecificCollisionException(GameManager.Instance.SecondaryPlayer.specRigidbody);
                    }
                }

            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("Exception Caught at [BecomeHologram] in ExpandChaosModeChallengeComponent!", false);
                    ETGModConsole.Log(ex.Message + ex.Source, false);
                    ETGModConsole.Log(ex.StackTrace, false);
                }
                return;
            }
        }

        private void MaybeSwapEnemySprites(AIActor targetActor) {
            if (UnityEngine.Random.value > 0.35f) { return; }

            if (targetActor.EnemyGuid == "128db2f0781141bcb505d8f00f9e4d47" | targetActor.EnemyGuid == "b54d89f9e802455cbb2b8a96a31e8259") {
                // IntVector2 targetSpawnPosition = (targetActor.gameObject.transform.PositionVector2().ToIntVector2() - targetActor.GetAbsoluteParentRoom().area.basePosition);
                IntVector2 targetSpawnPosition = (targetActor.gameObject.transform.PositionVector2().ToIntVector2());

                if (targetActor.EnemyGuid == "128db2f0781141bcb505d8f00f9e4d47") {	        		
                    AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(ExpandCustomEnemyDatabase.BootlegShotgunManRedGUID), targetSpawnPosition, targetActor.GetAbsoluteParentRoom(), true, AIActor.AwakenAnimationType.Spawn, true);
                    targetActor.GetAbsoluteParentRoom().DeregisterEnemy(targetActor);
                    Destroy(targetActor.gameObject);
	        	} else {
                    AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(ExpandCustomEnemyDatabase.BootlegShotgunManBlueGUID), targetSpawnPosition, targetActor.GetAbsoluteParentRoom(), true, AIActor.AwakenAnimationType.Spawn, true);
                    targetActor.GetAbsoluteParentRoom().DeregisterEnemy(targetActor);
                    Destroy(targetActor.gameObject);
                }
	        	
	        	return;
	        }

	        if (targetActor.EnemyGuid == "01972dee89fc4404a5c408d50007dad5" | targetActor.EnemyGuid == "db35531e66ce41cbb81d507a34366dfe") {
                int Selector = UnityEngine.Random.Range(0, 3);
                if (Selector == 0) {
	        		ExpandUtility.ApplyCustomTexture(targetActor, prebuiltCollection: BulletManMonochromeCollection, overrideShader: ShaderCache.Acquire("tk2d/BlendVertexColorUnlitTilted"));
	        		targetActor.OverrideDisplayName = ("1-Bit " + targetActor.GetActorName());
	        		targetActor.ActorName += "ALT";
	        		return;
	        	} else if (Selector == 1){
                    ExpandUtility.ApplyCustomTexture(targetActor, prebuiltCollection: BulletManUpsideDownCollection);
	        		targetActor.OverrideDisplayName = ("Bizarro " + targetActor.GetActorName());
	        		targetActor.ActorName += "ALT";
	        		return;
	        	} else if (Selector > 1) {
                    // IntVector2 targetSpawnPosition = (targetActor.gameObject.transform.PositionVector2().ToIntVector2() - targetActor.GetAbsoluteParentRoom().area.basePosition);
                    IntVector2 targetSpawnPosition = (targetActor.gameObject.transform.PositionVector2().ToIntVector2());
                    if (targetActor.EnemyGuid == "db35531e66ce41cbb81d507a34366dfe") {
                        AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID), targetSpawnPosition, targetActor.GetAbsoluteParentRoom(), true, AIActor.AwakenAnimationType.Spawn, true);
                    } else {
                        AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(ExpandCustomEnemyDatabase.BootlegBulletManGUID), targetSpawnPosition, targetActor.GetAbsoluteParentRoom(), true, AIActor.AwakenAnimationType.Spawn, true);
                    }
                    targetActor.GetAbsoluteParentRoom().DeregisterEnemy(targetActor);
                    Destroy(targetActor.gameObject);
                }
	        }

	        if (targetActor.EnemyGuid == "88b6b6a93d4b4234a67844ef4728382c") {
                IntVector2 targetSpawnPosition = (targetActor.gameObject.transform.PositionVector2().ToIntVector2() - targetActor.GetAbsoluteParentRoom().area.basePosition);
                AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID), targetSpawnPosition, targetActor.GetAbsoluteParentRoom(), true, AIActor.AwakenAnimationType.Spawn, true);
                targetActor.GetAbsoluteParentRoom().DeregisterEnemy(targetActor);
                Destroy(targetActor.gameObject);
                return;
	        }
            return;
        }

        private IEnumerator DelayedEnemySpawn(float delay, RoomHandler currentRoom, Vector2 spawnPosition, List<string> GUIDList) {
            if (!currentRoom.IsSealed) { yield break; }
            if (currentRoom.GetRoomName() != null) {
                if (PotEnemiesBannedRooms.Contains(currentRoom.GetRoomName())) { yield break; }
            }
            
            yield return new WaitForSeconds(delay);

            if (!currentRoom.IsSealed) { yield break; }

            yield return null;

            AIActor spawnedActor = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(BraveUtility.RandomElement(GUIDList)), spawnPosition, currentRoom, correctForWalls: true); ;
            
            yield return null;

            if (!AlreadyIgnoredForRoomClearList.Contains(spawnedActor.EnemyGuid)) {
                if (!spawnedActor.IgnoreForRoomClear) { spawnedActor.IgnoreForRoomClear = true; }
                if (!spawnedActor.PreventBlackPhantom) {
                    spawnedActor.PreventBlackPhantom = true;
                    if (spawnedActor.IsBlackPhantom) { spawnedActor.UnbecomeBlackPhantom(); }
                }
            }
            if (KillOnRoomClearList.Contains(spawnedActor.EnemyGuid)) {
                spawnedActor.gameObject.AddComponent<KillOnRoomClear>();
                KillOnRoomClear killOnRoomClear = spawnedActor.GetComponent<KillOnRoomClear>();
                if (killOnRoomClear) {
                    killOnRoomClear.overrideDeathAnim = string.Empty;
                    killOnRoomClear.preventExplodeOnDeath = false;
                }
            }

            if (UnityEngine.Random.value <= EnemyResizeOdds) { MakeTinyOrBig(spawnedActor, true); }

            if (UnityEngine.Random.value <= 0.35f) {
                ExpandShaders.Instance.BecomeHologram(spawnedActor, BraveUtility.RandomBool());
            } else if (UnityEngine.Random.value <= 0.15f) {
                ExpandShaders.Instance.BecomeRainbow(spawnedActor);
            } else {
                MaybeSwapEnemySprites(spawnedActor);
            }

            yield break;
        }

        private IEnumerator ResizeEnemy(AIActor target, Vector2 ScaleValue, bool onlyDoRescale = true, bool isBigEnemy = false, bool delayed = false) {
            if (target == null | ScaleValue == null) { yield break; }

            if (delayed) { yield return new WaitForSeconds(0.8f); }

            HealthHaver targetHealthHaver = target.GetComponent<HealthHaver>();
            float knockBackValue = 2f;

            int cachedLayer = target.gameObject.layer;
            int cachedOutlineLayer = cachedLayer;
            target.gameObject.layer = LayerMask.NameToLayer("Unpixelated");
            cachedOutlineLayer = SpriteOutlineManager.ChangeOutlineLayer(target.sprite, LayerMask.NameToLayer("Unpixelated"));
            if (!onlyDoRescale) {
                if (target.knockbackDoer) {
                    if (isBigEnemy) {
                        target.knockbackDoer.weight *= knockBackValue;
                    } else {
                        target.knockbackDoer.weight /= knockBackValue;
                    }
                }
                if (!isBigEnemy && targetHealthHaver != null && !onlyDoRescale) {
                    if (!targetHealthHaver.IsBoss && !ExpandLists.DontDieOnCollisionWhenTinyGUIDList.Contains(target.EnemyGuid)) {
                        target.DiesOnCollison = true;
                        target.EnemySwitchState = "Blobulin";
                    }

                    target.CollisionDamage = 0f;
                    target.CollisionDamageTypes = 0;
                    target.PreventFallingInPitsEver = false;
                    target.PreventBlackPhantom = true;

                    if (targetHealthHaver.IsBoss) {
                        if (targetHealthHaver != null) { targetHealthHaver.SetHealthMaximum(targetHealthHaver.GetMaxHealth() / 1.5f, null, false); }
                    } else if (targetHealthHaver != null && !onlyDoRescale) {
                        target.BaseMovementSpeed *= 1.15f;
                        target.MovementSpeed *= 1.15f;
                        if (targetHealthHaver != null) { targetHealthHaver.SetHealthMaximum(targetHealthHaver.GetMaxHealth() / 2f, null, false); }
                    }
                    target.OverrideDisplayName = ("Tiny " + target.GetActorName());
                } else if (isBigEnemy && targetHealthHaver != null && !onlyDoRescale) {
                    if (!target.IsFlying && !targetHealthHaver.IsBoss && !ExpandLists.OverrideFallIntoPitsList.Contains(target.EnemyGuid)) {
                        target.PreventFallingInPitsEver = true;
                    }
                    if (targetHealthHaver.IsBoss) {
                        targetHealthHaver.SetHealthMaximum(targetHealthHaver.GetMaxHealth() * 1.2f, null, false);
                    } else {
                        target.BaseMovementSpeed /= 1.25f;
                        target.MovementSpeed /= 1.25f;
                        targetHealthHaver.SetHealthMaximum(targetHealthHaver.GetMaxHealth() * 1.5f, null, false);
                    }
                    target.OverrideDisplayName = ("Big " + target.GetActorName());
                }
            }
            Vector2 startScale = target.EnemyScale;
            float elapsed = 0f;
            float ShrinkTime = 0.5f;
            while (elapsed < ShrinkTime) {
                elapsed += BraveTime.DeltaTime;
                target.EnemyScale = Vector2.Lerp(startScale, ScaleValue, elapsed / ShrinkTime);
                if (target.specRigidbody) {
                    target.specRigidbody.UpdateCollidersOnScale = true;
                    target.specRigidbody.RegenerateColliders = true;
                }
                yield return null;
            }
            yield return new WaitForSeconds(1.5f);
            ExpandUtility.CorrectForWalls(target);
            yield break;
        }

        private DungeonPlaceable m_CustomEnemyPlacable(string EnemyGUID = "01972dee89fc4404a5c408d50007dad5", bool forceBlackPhantom = false) {
            DungeonPlaceableVariant EnemyVariant = new DungeonPlaceableVariant();
            EnemyVariant.percentChance = 1f;
            EnemyVariant.unitOffset = Vector2.zero;
            EnemyVariant.enemyPlaceableGuid = EnemyGUID;
            EnemyVariant.pickupObjectPlaceableId = -1;
            EnemyVariant.forceBlackPhantom = forceBlackPhantom;
            EnemyVariant.addDebrisObject = false;
            EnemyVariant.prerequisites = null;
            EnemyVariant.materialRequirements = null;

            List<DungeonPlaceableVariant> EnemyTiers = new List<DungeonPlaceableVariant>();
            EnemyTiers.Add(EnemyVariant);

            DungeonPlaceable m_cachedPlacable = ScriptableObject.CreateInstance<DungeonPlaceable>();
            m_cachedPlacable.name = "CustomEnemyPlacable";
            m_cachedPlacable.width = 1;
            m_cachedPlacable.height = 1;
            m_cachedPlacable.roomSequential = false;
            m_cachedPlacable.respectsEncounterableDifferentiator = false;
            m_cachedPlacable.UsePrefabTransformOffset = false;
            m_cachedPlacable.MarkSpawnedItemsAsRatIgnored = false;
            m_cachedPlacable.DebugThisPlaceable = false;
            m_cachedPlacable.variantTiers = EnemyTiers;

            return m_cachedPlacable;
        }

        private IntVector2? FindRandomDropLocation(RoomHandler currentRoom, IntVector2 Clearance, float maxDistanceFromPlayer = 8f, bool markLocationAsOccupied = false) {
            CellValidator cellValidator = delegate (IntVector2 pos) {
                for (int j = 0; j < GameManager.Instance.AllPlayers.Length; j++) {
                    if (Vector2.Distance(GameManager.Instance.AllPlayers[j].CenterPosition, pos.ToCenterVector2()) < maxDistanceFromPlayer) { return false; }
                }
                return true;
            };

            IntVector2? randomAvailableCell = currentRoom.GetRandomAvailableCell(new IntVector2?(Clearance), new CellTypes?(CellTypes.FLOOR), false, cellValidator);

            if (randomAvailableCell.HasValue) {
                CellData cellData = GameManager.Instance.Dungeon.data[randomAvailableCell.Value];
                if (cellData.parentRoom == currentRoom && cellData.type == CellType.FLOOR && !cellData.isOccupied && !cellData.containsTrap && !cellData.isOccludedByTopWall) {
                    if (markLocationAsOccupied) { cellData.isOccupied = true; }
                    return randomAvailableCell;
                } else {
                    // return null;
                    return randomAvailableCell;
                }
            } else {
                return GameManager.Instance.PrimaryPlayer.CenterPosition.ToIntVector2();
            }
        }

        private IntVector2 GetRandomAvailableCellForPlacable(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, bool useCachedList, bool allowPlacingOverPits = false, int gridSnap = 1) {
            if (!useCachedList | validCellsCached == null) { validCellsCached = new List<IntVector2>(); }
            if (validCellsCached.Count <= 0) {
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + height;
                        if (X % gridSnap == 0 && Y % gridSnap == 0) {
                            if (allowPlacingOverPits) {
                                if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                                    !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                                    !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                                    !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                                    !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                                    !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                                    !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                                    !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                                    !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                                    !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied)
                                {
                                    validCellsCached.Add(new IntVector2(X, Y));
                                }
                            } else {
                                if (!dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) &&
                                    !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) &&
                                    !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) &&
                                    !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) &&
                                    !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) &&
                                    !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                                    !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                                    !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                                    !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                                    !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied &&
                                    !dungeon.data.isPit(X - 2, Y + 2) && !dungeon.data.isPit(X - 1, Y + 2) && !dungeon.data.isPit(X, Y + 2) && !dungeon.data.isPit(X + 1, Y + 2) && !dungeon.data.isPit(X + 2, Y + 2) &&
                                    !dungeon.data.isPit(X - 2, Y + 1) && !dungeon.data.isPit(X - 1, Y + 1) && !dungeon.data.isPit(X, Y + 1) && !dungeon.data.isPit(X + 1, Y + 1) && !dungeon.data.isPit(X + 2, Y + 1) &&
                                    !dungeon.data.isPit(X - 2, Y) && !dungeon.data.isPit(X - 1, Y) && !dungeon.data.isPit(X, Y) && !dungeon.data.isPit(X + 1, Y) && !dungeon.data.isPit(X + 2, Y) &&
                                    !dungeon.data.isPit(X - 2, Y - 1) && !dungeon.data.isPit(X - 1, Y - 1) && !dungeon.data.isPit(X, Y - 1) && !dungeon.data.isPit(X + 1, Y - 1) && !dungeon.data.isPit(X + 2, Y - 1) &&
                                    !dungeon.data.isPit(X - 2, Y - 2) && !dungeon.data.isPit(X - 1, Y - 2) && !dungeon.data.isPit(X, Y - 2) && !dungeon.data.isPit(X + 1, Y - 2) && !dungeon.data.isPit(X + 2, Y - 2))
                                {
                                    validCellsCached.Add(new IntVector2(X, Y));
                                }
                            }
                        }
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                IntVector2 RegisteredCell = (SelectedCell);
                if (useCachedList) dungeon.data[RegisteredCell].isOccupied = true;
                validCellsCached.Remove(SelectedCell);
                return (SelectedCell - currentRoom.area.basePosition);
            } else { return IntVector2.Zero; }
        }

        private IntVector2 GetRandomAvailableCellForNPC(Dungeon dungeon, RoomHandler currentRoom, List<IntVector2> validCellsCached, bool useCachedList) {
            if (!useCachedList | validCellsCached == null) {
                validCellsCached = new List<IntVector2>();
                validCellsCached.Clear();
            }
            if (validCellsCached.Count <= 0) {
                for (int Width = -1; Width <= currentRoom.area.dimensions.x; Width++) {
                    for (int height = -1; height <= currentRoom.area.dimensions.y; height++) {
                        int X = currentRoom.area.basePosition.x + Width;
                        int Y = currentRoom.area.basePosition.y + height;
                        if (!dungeon.data.isWall(X - 3, Y + 3) && !dungeon.data.isWall(X - 2, Y + 3) && !dungeon.data.isWall(X - 1, Y + 3) && !dungeon.data.isWall(X, Y + 3) && !dungeon.data.isWall(X + 1, Y + 3) && !dungeon.data.isWall(X + 2, Y + 3) && !dungeon.data.isWall(X + 3, Y + 3) &&
                            !dungeon.data.isWall(X - 3, Y + 2) && !dungeon.data.isWall(X - 2, Y + 2) && !dungeon.data.isWall(X - 1, Y + 2) && !dungeon.data.isWall(X, Y + 2) && !dungeon.data.isWall(X + 1, Y + 2) && !dungeon.data.isWall(X + 2, Y + 2) && !dungeon.data.isWall(X + 3, Y + 2) &&
                            !dungeon.data.isWall(X - 3, Y + 1) && !dungeon.data.isWall(X - 2, Y + 1) && !dungeon.data.isWall(X - 1, Y + 1) && !dungeon.data.isWall(X, Y + 1) && !dungeon.data.isWall(X + 1, Y + 1) && !dungeon.data.isWall(X + 2, Y + 1) && !dungeon.data.isWall(X + 3, Y + 1) &&
                            !dungeon.data.isWall(X - 3, Y) && !dungeon.data.isWall(X - 2, Y) && !dungeon.data.isWall(X - 1, Y) && !dungeon.data.isWall(X, Y) && !dungeon.data.isWall(X + 1, Y) && !dungeon.data.isWall(X + 2, Y) && !dungeon.data.isWall(X + 3, Y) &&
                            !dungeon.data.isWall(X - 3, Y - 1) && !dungeon.data.isWall(X - 2, Y - 1) && !dungeon.data.isWall(X - 1, Y - 1) && !dungeon.data.isWall(X, Y - 1) && !dungeon.data.isWall(X + 1, Y - 1) && !dungeon.data.isWall(X + 2, Y - 1) && !dungeon.data.isWall(X + 3, Y - 1) &&
                            !dungeon.data.isWall(X - 3, Y - 2) && !dungeon.data.isWall(X - 2, Y - 2) && !dungeon.data.isWall(X - 1, Y - 2) && !dungeon.data.isWall(X, Y - 2) && !dungeon.data.isWall(X + 1, Y - 2) && !dungeon.data.isWall(X + 2, Y - 2) && !dungeon.data.isWall(X + 3, Y - 2) &&
                            !dungeon.data.isPit(X - 3, Y + 3) && !dungeon.data.isPit(X - 2, Y + 3) && !dungeon.data.isPit(X - 1, Y + 3) && !dungeon.data.isPit(X, Y + 3) && !dungeon.data.isPit(X + 1, Y + 3) && !dungeon.data.isPit(X + 2, Y + 3) && !dungeon.data.isPit(X + 3, Y + 3) &&
                            !dungeon.data.isPit(X - 3, Y + 2) && !dungeon.data.isPit(X - 2, Y + 2) && !dungeon.data.isPit(X - 1, Y + 2) && !dungeon.data.isPit(X, Y + 2) && !dungeon.data.isPit(X + 1, Y + 2) && !dungeon.data.isPit(X + 2, Y + 2) && !dungeon.data.isPit(X + 3, Y + 2) &&
                            !dungeon.data.isPit(X - 3, Y + 1) && !dungeon.data.isPit(X - 2, Y + 1) && !dungeon.data.isPit(X - 1, Y + 1) && !dungeon.data.isPit(X, Y + 1) && !dungeon.data.isPit(X + 1, Y + 1) && !dungeon.data.isPit(X + 2, Y + 1) && !dungeon.data.isPit(X + 3, Y + 1) &&
                            !dungeon.data.isPit(X - 3, Y) && !dungeon.data.isPit(X - 2, Y) && !dungeon.data.isPit(X - 1, Y) && !dungeon.data.isPit(X, Y) && !dungeon.data.isPit(X + 1, Y) && !dungeon.data.isPit(X + 2, Y) && !dungeon.data.isPit(X + 3, Y) &&
                            !dungeon.data.isPit(X - 3, Y - 1) && !dungeon.data.isPit(X - 2, Y - 1) && !dungeon.data.isPit(X - 1, Y - 1) && !dungeon.data.isPit(X, Y - 1) && !dungeon.data.isPit(X + 1, Y - 1) && !dungeon.data.isPit(X + 2, Y - 1) && !dungeon.data.isPit(X + 3, Y - 1) &&
                            !dungeon.data.isPit(X - 3, Y - 2) && !dungeon.data.isPit(X - 2, Y - 2) && !dungeon.data.isPit(X - 1, Y - 2) && !dungeon.data.isPit(X, Y - 2) && !dungeon.data.isPit(X + 1, Y - 2) && !dungeon.data.isPit(X + 2, Y - 2) && !dungeon.data.isPit(X + 3, Y - 2) &&
                            !dungeon.data.isPit(X - 3, Y - 3) && !dungeon.data.isPit(X - 2, Y - 3) && !dungeon.data.isPit(X - 1, Y - 3) && !dungeon.data.isPit(X, Y - 3) && !dungeon.data.isPit(X + 1, Y - 3) && !dungeon.data.isPit(X + 2, Y - 3) && !dungeon.data.isPit(X + 3, Y - 3) &&
                            !dungeon.data[X - 2, Y + 2].isOccupied && !dungeon.data[X - 1, Y + 2].isOccupied && !dungeon.data[X, Y + 2].isOccupied && !dungeon.data[X + 1, Y + 2].isOccupied && !dungeon.data[X + 2, Y + 2].isOccupied &&
                            !dungeon.data[X - 2, Y + 1].isOccupied && !dungeon.data[X - 1, Y + 1].isOccupied && !dungeon.data[X, Y + 1].isOccupied && !dungeon.data[X + 1, Y + 1].isOccupied && !dungeon.data[X + 2, Y + 1].isOccupied &&
                            !dungeon.data[X - 2, Y].isOccupied && !dungeon.data[X - 1, Y].isOccupied && !dungeon.data[X, Y].isOccupied && !dungeon.data[X + 1, Y].isOccupied && !dungeon.data[X + 2, Y].isOccupied &&
                            !dungeon.data[X - 2, Y - 1].isOccupied && !dungeon.data[X - 1, Y - 1].isOccupied && !dungeon.data[X, Y - 1].isOccupied && !dungeon.data[X + 1, Y - 1].isOccupied && !dungeon.data[X + 2, Y - 1].isOccupied &&
                            !dungeon.data[X - 2, Y - 2].isOccupied && !dungeon.data[X - 1, Y - 2].isOccupied && !dungeon.data[X, Y - 2].isOccupied && !dungeon.data[X + 1, Y - 2].isOccupied && !dungeon.data[X + 2, Y - 2].isOccupied)
                        {
                            validCellsCached.Add(new IntVector2(X, Y));
                        }
                    }
                }
            }
            if (validCellsCached.Count > 0) {
                IntVector2 SelectedCell = BraveUtility.RandomElement(validCellsCached);
                IntVector2 RegisteredCell = (SelectedCell);
                dungeon.data[RegisteredCell].isOccupied = true;
                validCellsCached.Remove(SelectedCell);
                return (SelectedCell - currentRoom.area.basePosition);
            } else { return IntVector2.Zero; }
        }

        private void Update() { }

        private void LateUpdate() { }

        private void OnDestroy() {
            if (Dungeon.IsGenerating || !GameManager.HasInstance || GameManager.Instance.IsLoadingLevel || !GameManager.Instance.PrimaryPlayer || GameManager.Instance.PrimaryPlayer.CurrentRoom == null) { return; }
            RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;
            foreach (MinorBreakable breakable in StaticReferenceManager.AllMinorBreakables) {
                if (breakable && breakable.CenterPoint.GetAbsoluteRoom() == currentRoom) { AddOrRemoveActionForPot(breakable, false); }
            }
        }

    }
}

