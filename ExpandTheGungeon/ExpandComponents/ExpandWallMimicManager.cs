using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandWallMimicManager : CustomEngageDoer, IPlaceConfigurable {
        
        public ExpandWallMimicManager() {
            SpawnEnemyMode = false;
            GlitchedEnemyMode = false;
            CursedBrickMode = false;
            SkipPlayerCheck = false;

            m_AssetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
            WallDisappearVFX = m_AssetBundle.LoadAsset<GameObject>("VFX_Dust_Explosion");
            if (SpawnEnemyList == null) {
                SpawnEnemyList = new List<string> {
                    "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                    "05891b158cd542b1a5f3df30fb67a7ff", // arrow_head
                    "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                    "8bb5578fba374e8aae8e10b754e61d62", // cardinal
                    "f905765488874846b7ff257ff81d6d0c", // fungun
                    "37340393f97f41b2822bc02d14654172", // creech
                    "c182a5cb704d460d9d099a47af49c913", // pot_fairy
                    "5f3abc2d561b4b9c9e72b879c6f10c7e", // fallen_bullet_kin
                    "1bd8e49f93614e76b140077ff2e33f2b", // ashen_shotgun_kin
                    "1a78cfb776f54641b832e92c44021cf2", // ashen_bullet_kin
                    "2752019b770f473193b08b4005dc781f", // veteran_shotgun_kin
                    "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
                    "d4dd2b2bbda64cc9bcec534b4e920518", // bullet_kings_toadie_revenge
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
                    "05cb719e0178478685dc610f8b3e8bfc", // bullet_kin_vest
                    "e861e59012954ab2b9b6977da85cb83c", // snake_office
                    "3b0bd258b4c9432db3339665cc61c356", // cactus_kin
                    "4b21a913e8c54056bc05cafecf9da880", // gigi_parrot
                    "78e0951b097b46d89356f004dda27c42", // tablet_bookllet
                    "216fd3dfb9da439d9bd7ba53e1c76462", // necronomicon_bookllet
                    // "a446c626b56d4166915a4e29869737fd", // chance_bullet_kin
                    // "699cd24270af4cd183d671090d8323a1", // key_bullet_kin
                    "9b2cf2949a894599917d4d391a0b7394", // high_gunjurer
                    "RATCORPSE"
                };
            }            
            m_AssetBundle = null;
            m_isHidden = true;
            m_isFriendlyMimic = false;
            m_failedWallConfigure = false;
            m_isGlitched = false;
            m_ItemDropOdds = 0.18f;
            m_FriendlyMimicOdds = 0.15f;
            m_spawnEnemyOdds = 0.3f;
            m_glitchOdds = 0.08f;
        }


        public List<string> SpawnEnemyList;

        public bool CursedBrickMode;
        public bool SpawnEnemyMode;
        public bool GlitchedEnemyMode;
        public bool SkipPlayerCheck;

        public tk2dSpriteCollectionData DungeonCollectionOverride;
        public Dungeon SourceDungeonForTileSetOverride;

        private AssetBundle m_AssetBundle;
        private GameObject WallDisappearVFX;

        protected bool m_playerTrueSight;
        private bool m_isHidden;
        private bool m_isFinished;
        private bool m_configured;
        private bool m_isFriendlyMimic;
        private bool m_isGlitched;
        private bool m_failedWallConfigure;
        
        private float m_collisionKnockbackStrength;
        private float m_ItemDropOdds;
        private float m_FriendlyMimicOdds;
        private float m_spawnEnemyOdds;
        private float m_glitchOdds;

        private GameObject m_fakeWall;
        private GameObject m_fakeCeiling;

        private Vector3 m_startingPos;

        private IntVector2 pos1;
        private IntVector2 pos2;

        private DungeonData.Direction m_facingDirection;

        private GunHandController[] m_hands;

        private GoopDoer m_goopDoer;

        protected bool CanAwaken { get { return m_isHidden && !PassiveItem.IsFlagSetAtAll(typeof(MimicRingItem)); } }

        public void Awake() {
            ObjectVisibilityManager visibilityManager = base.visibilityManager;
            visibilityManager.OnToggleRenderers = (Action)Delegate.Combine(visibilityManager.OnToggleRenderers, new Action(OnToggleRenderers));
            aiActor.IsGone = true;
        }

        public void Start() {
            
            if (!m_configured) {
                ConfigureOnPlacement(GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(transform.position.IntXY(VectorConversions.Ceil)));
            }
            
            PlayerController player = GameManager.Instance.PrimaryPlayer;
            PlayerController player2 = GameManager.Instance.SecondaryPlayer;

            if ((player && player.HasPassiveItem(CorruptedJunk.CorruptedJunkID)) | (player2 && player2.HasPassiveItem(CorruptedJunk.CorruptedJunkID))) {
                GlitchedEnemyMode = true;
            }
           
            if (GlitchedEnemyMode && UnityEngine.Random.value <= m_glitchOdds) { m_isGlitched = true; }
            
            transform.position = m_startingPos;
            specRigidbody.Reinitialize();
            aiAnimator.LockFacingDirection = true;
            aiAnimator.FacingDirection = DungeonData.GetAngleFromDirection(m_facingDirection);
            if (!m_failedWallConfigure) {
                m_fakeWall = ExpandUtility.GenerateWallMesh(m_facingDirection, pos1, "Mimic Wall", null, true, m_isGlitched, DungeonCollectionOverride, SourceDungeonForTileSetOverride);
                if (aiActor.ParentRoom != null) { m_fakeWall.transform.parent = aiActor.ParentRoom.hierarchyParent; }
                m_fakeWall.transform.position = pos1.ToVector3().WithZ(pos1.y - 2) + Vector3.down;
                if (m_facingDirection == DungeonData.Direction.SOUTH) {
                StaticReferenceManager.AllShadowSystemDepthHavers.Add(m_fakeWall.transform);
                } else if (m_facingDirection == DungeonData.Direction.WEST) {
                    m_fakeWall.transform.position = m_fakeWall.transform.position + new Vector3(-0.1875f, 0f);
                }
                m_fakeCeiling = ExpandUtility.GenerateRoomCeilingMesh(GetCeilingTileSet(pos1, pos2, m_facingDirection), "Mimic Ceiling", null, true, m_isGlitched, DungeonCollectionOverride, SourceDungeonForTileSetOverride);
                if (aiActor.ParentRoom != null) { m_fakeCeiling.transform.parent = aiActor.ParentRoom.hierarchyParent; }
                m_fakeCeiling.transform.position = pos1.ToVector3().WithZ(pos1.y - 4);
                if (m_facingDirection == DungeonData.Direction.NORTH) {
                    m_fakeCeiling.transform.position += new Vector3(-1f, 0f);
                } else if (m_facingDirection == DungeonData.Direction.SOUTH) {
                    m_fakeCeiling.transform.position += new Vector3(-1f, 2f);
                } else if (m_facingDirection == DungeonData.Direction.EAST) {
                    m_fakeCeiling.transform.position += new Vector3(-1f, 0f);
                }
                m_fakeCeiling.transform.position = m_fakeCeiling.transform.position.WithZ(m_fakeCeiling.transform.position.y - 5f);
                for (int i = 0; i < specRigidbody.PixelColliders.Count; i++) { specRigidbody.PixelColliders[i].Enabled = false; }
                if (m_facingDirection == DungeonData.Direction.NORTH) {
                    specRigidbody.PixelColliders.Add(PixelCollider.CreateRectangle(CollisionLayer.LowObstacle, 38, 38, 32, 8, true));
                    specRigidbody.PixelColliders.Add(PixelCollider.CreateRectangle(CollisionLayer.HighObstacle, 38, 54, 32, 8, true));
                } else if (m_facingDirection == DungeonData.Direction.SOUTH) {
                    specRigidbody.PixelColliders.Add(PixelCollider.CreateRectangle(CollisionLayer.LowObstacle, 38, 38, 32, 16, true));
                    specRigidbody.PixelColliders.Add(PixelCollider.CreateRectangle(CollisionLayer.HighObstacle, 38, 54, 32, 16, true));
                } else if (m_facingDirection == DungeonData.Direction.WEST || m_facingDirection == DungeonData.Direction.EAST) {
                    specRigidbody.PixelColliders.Add(PixelCollider.CreateRectangle(CollisionLayer.LowObstacle, 46, 38, 16, 32, true));
                    specRigidbody.PixelColliders.Add(PixelCollider.CreateRectangle(CollisionLayer.HighObstacle, 46, 38, 16, 32, true));
                }
                specRigidbody.ForceRegenerate(null, null);
            }            
            aiActor.HasDonePlayerEnterCheck = true;
            m_collisionKnockbackStrength = aiActor.CollisionKnockbackStrength;
            aiActor.CollisionKnockbackStrength = 0f;
            aiActor.CollisionDamage = 0f;
            m_goopDoer = GetComponent<GoopDoer>();
        }

        public void Update() {
            bool PlayerInRoom = false;
            if (!SkipPlayerCheck) {
                foreach (PlayerController playerController in GameManager.Instance.AllPlayers) {
                    if (playerController.CurrentRoom == aiActor.ParentRoom) { PlayerInRoom = true; }
                }
            }
            if ((SkipPlayerCheck | PlayerInRoom) && CanAwaken) {
                Vector2 vector = specRigidbody.PixelColliders[0].UnitBottomLeft;
                Vector2 vector2 = vector;
                if (!m_failedWallConfigure) { vector = specRigidbody.PixelColliders[2].UnitBottomLeft; }
                if (m_facingDirection == DungeonData.Direction.SOUTH) {
                    vector += new Vector2(0f, -1.5f);
                    vector2 += new Vector2(2f, 0f);
                } else if (m_facingDirection == DungeonData.Direction.NORTH) {
                    vector += new Vector2(0f, 1f);
                    vector2 += new Vector2(2f, 3f);
                } else if (m_facingDirection == DungeonData.Direction.WEST) {
                    vector += new Vector2(-1.5f, 0f);
                    vector2 += new Vector2(0f, 2f);
                } else if (m_facingDirection == DungeonData.Direction.EAST) {
                    vector += new Vector2(1f, 0f);
                    vector2 += new Vector2(2.5f, 2f);
                }
                bool flag = false;
                foreach (PlayerController playerController in GameManager.Instance.AllPlayers) {
                    if (playerController.CanDetectHiddenEnemies) {
                        flag = true;
                        if (!m_playerTrueSight) { m_playerTrueSight = true; aiActor.ToggleRenderers(true); }
                    }
                    if (playerController && playerController.healthHaver.IsAlive && !playerController.IsGhost) {
                        Vector2 unitCenter = playerController.specRigidbody.GetUnitCenter(ColliderType.Ground);
                        if (unitCenter.IsWithin(vector, vector2)) {
                            if (m_goopDoer) {
                                Vector2 vector3 = specRigidbody.PixelColliders[2].UnitCenter;
                                if (m_facingDirection == DungeonData.Direction.NORTH) { vector3 += Vector2.up; }
                                DeadlyDeadlyGoopManager goopManagerForGoopType = DeadlyDeadlyGoopManager.GetGoopManagerForGoopType(m_goopDoer.goopDefinition);
                                goopManagerForGoopType.TimedAddGoopArc(vector3, 3f, 90f, DungeonData.GetIntVector2FromDirection(m_facingDirection).ToVector2(), 0.2f, null);
                            }
                            StartCoroutine(BecomeMimic());
                        }
                    }
                }
                if (!flag && m_playerTrueSight) { m_playerTrueSight = false; aiActor.ToggleRenderers(false); }
            }
        }

        private IEnumerator BecomeMimic() {
            PlayerController player = GameManager.Instance.PrimaryPlayer;
            PlayerController player2 = GameManager.Instance.SecondaryPlayer;

            if (!SpawnEnemyMode) {
                if (player) {
                    if (player.HasPassiveItem(293) && player.HasPassiveItem(CursedBrick.CursedBrickID)) { SpawnEnemyMode = true; }
                }
                if (player2) {
                    if (player2.HasPassiveItem(293) && player2.HasPassiveItem(CursedBrick.CursedBrickID)) { SpawnEnemyMode = true; }
                }
            }

            if (m_hands == null) { StartCoroutine(DoIntro()); }
            
            if (SpawnEnemyMode) { m_ItemDropOdds += 0.1f; }

            m_isHidden = false;
            SpeculativeRigidbody specRigidbody = this.specRigidbody;
            specRigidbody.OnRigidbodyCollision = (SpeculativeRigidbody.OnRigidbodyCollisionDelegate)Delegate.Remove(specRigidbody.OnRigidbodyCollision, new SpeculativeRigidbody.OnRigidbodyCollisionDelegate(HandleRigidbodyCollision));
            SpeculativeRigidbody specRigidbody2 = this.specRigidbody;
            specRigidbody2.OnBeamCollision = (SpeculativeRigidbody.OnBeamCollisionDelegate)Delegate.Remove(specRigidbody2.OnBeamCollision, new SpeculativeRigidbody.OnBeamCollisionDelegate(HandleBeamCollision));
            AIAnimator tongueAnimator = aiAnimator.ChildAnimator;
            tongueAnimator.renderer.enabled = true;
            tongueAnimator.spriteAnimator.enabled = true;
            AIAnimator spitAnimator = tongueAnimator.ChildAnimator;
            spitAnimator.renderer.enabled = true;
            spitAnimator.spriteAnimator.enabled = true;
            tongueAnimator.PlayUntilFinished("spawn", false, null, -1f, false);
            float delay = tongueAnimator.CurrentClipLength;
            float timer = 0f;
            bool hasPlayedVFX = false;
            while (timer < delay) {
                yield return null;
                timer += BraveTime.DeltaTime;
                if (!hasPlayedVFX && delay - timer < 0.1f) {
                    hasPlayedVFX = true;
                    if (WallDisappearVFX) {
                        Vector2 zero = Vector2.zero;
                        Vector2 zero2 = Vector2.zero;
                        DungeonData.Direction facingDirection = m_facingDirection;
                        if (facingDirection != DungeonData.Direction.SOUTH) {
                            if (facingDirection != DungeonData.Direction.EAST) {
                                if (facingDirection == DungeonData.Direction.WEST) {
                                    zero = new Vector2(0f, -1f);
                                    zero2 = new Vector2(0f, 1f);
                                }
                            } else {
                                zero = new Vector2(0f, -1f);
                                zero2 = new Vector2(0f, 1f);
                            }
                        } else {
                            zero = new Vector2(0f, -1f);
                            zero2 = new Vector2(0f, 1f);
                        }
                        Vector2 min = Vector2.Min(pos1.ToVector2(), pos2.ToVector2()) + zero;
                        Vector2 max = Vector2.Max(pos1.ToVector2(), pos2.ToVector2()) + new Vector2(1f, 1f) + zero2;
                        for (int i = 0; i < 5; i++) {
                            Vector2 v = BraveUtility.RandomVector2(min, max, new Vector2(0.25f, 0.25f)) + new Vector2(0f, 1f);
                            GameObject gameObject = SpawnManager.SpawnVFX(WallDisappearVFX, v, Quaternion.identity);
                            tk2dBaseSprite tk2dBaseSprite = (!gameObject) ? null : gameObject.GetComponent<tk2dBaseSprite>();
                            if (tk2dBaseSprite) {
                                tk2dBaseSprite.HeightOffGround = 8f;
                                tk2dBaseSprite.UpdateZDepth();
                            }
                        }
                    }
                }
            }
            if (!m_failedWallConfigure && SpawnEnemyMode) { 
                if (aiActor.ParentRoom != null && SpawnEnemyList != null && SpawnEnemyList.Count > 0 && UnityEngine.Random.value <= m_spawnEnemyOdds) {
                    
                    int count2 = this.specRigidbody.PixelColliders.Count;
                    this.specRigidbody.PixelColliders.RemoveAt(count2 - 1);
                    this.specRigidbody.PixelColliders.RemoveAt(count2 - 2);
                    StaticReferenceManager.AllShadowSystemDepthHavers.Remove(m_fakeWall.transform);
                    Destroy(m_fakeWall);
                    Destroy(m_fakeCeiling);
                    
                    Vector3 targetPosForSpawn = m_startingPos + DungeonData.GetIntVector2FromDirection(m_facingDirection).ToVector3();
                    while (timer < delay) {
                        aiAnimator.LockFacingDirection = true;
                        aiAnimator.FacingDirection = DungeonData.GetAngleFromDirection(m_facingDirection);
                        yield return null;
                        timer += BraveTime.DeltaTime;
                        transform.position = Vector3.Lerp(m_startingPos, targetPosForSpawn, Mathf.InverseLerp(0.42f, 0.58f, timer));
                        this.specRigidbody.Reinitialize();
                    }
                    yield return null;
                    Vector3 FinalSpawnLocation = transform.position;
                    Vector3 VFXExplosionLocation = transform.position;
                    Vector2 VFXExplosionSource = Vector2.zero;
                    DungeonData.Direction CurrentDirection = m_facingDirection;
                    if (CurrentDirection == DungeonData.Direction.WEST) {
                        FinalSpawnLocation += new Vector3(2.5f, 3.5f);
                        VFXExplosionLocation += new Vector3(3.5f, 3.5f);
                        VFXExplosionSource = new Vector2(1, 0);
                    } else if (CurrentDirection == DungeonData.Direction.EAST) {
                        FinalSpawnLocation += new Vector3(4f, 3.5f);
                        VFXExplosionLocation += new Vector3(3f, 3.5f);                    
                    } else if (CurrentDirection == DungeonData.Direction.NORTH) {
                        FinalSpawnLocation += new Vector3(3.5f, 4f);
                        VFXExplosionLocation += new Vector3(3.5f, 3f);
                        VFXExplosionSource = new Vector2(0, 1);
                    } else if (CurrentDirection == DungeonData.Direction.SOUTH) {
                        FinalSpawnLocation += new Vector3(3.5f, 1.5f);
                        VFXExplosionLocation += new Vector3(3.5f, 2.5f);
                    }
                    yield return null;
                    string SelectedEnemy = BraveUtility.RandomElement(SpawnEnemyList);
                    ExplosionData wallMimicExplosionData = new ExplosionData();
                    wallMimicExplosionData.CopyFrom(GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultExplosionData);
                    wallMimicExplosionData.damage = 0f;
                    wallMimicExplosionData.force /= 1.6f;

                    if (SelectedEnemy != "RATCORPSE") {
                        Exploder.Explode(VFXExplosionLocation, wallMimicExplosionData, VFXExplosionSource, ignoreQueues: true, damageTypes: CoreDamageTypes.None);                    
                        GameObject SpawnVFXObject = Instantiate((GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Spawn_Poof"));

                        if (SpawnVFXObject) {
                            tk2dBaseSprite SpawnVFXObjectComponent = SpawnVFXObject.GetComponent<tk2dBaseSprite>();
                            SpawnVFXObjectComponent.PlaceAtPositionByAnchor(FinalSpawnLocation + new Vector3(0f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                            SpawnVFXObjectComponent.HeightOffGround = 1f;
                            SpawnVFXObjectComponent.UpdateZDepth();
                        }

                        AIActor glitchActor = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(SelectedEnemy), FinalSpawnLocation, aiActor.ParentRoom, true, AIActor.AwakenAnimationType.Awaken, true);

                        if (glitchActor) { 
                            PickupObject.ItemQuality targetGlitchEnemyItemQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.C : PickupObject.ItemQuality.D) : PickupObject.ItemQuality.B;
                            GenericLootTable glitchEnemyLootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                            PickupObject glitchEnemyItem = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetGlitchEnemyItemQuality, glitchEnemyLootTable, false);

                            // if (BraveUtility.RandomBool()) { ChaosUtility.MakeCompanion(glitchActor); }
                            
                            if (glitchEnemyItem) { glitchActor.AdditionalSafeItemDrops.Add(glitchEnemyItem); }

                            if (m_isGlitched) {
                                float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                                float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
                                float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
                                float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
                                float RandomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

                                targetGlitchEnemyItemQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.B : PickupObject.ItemQuality.C) : PickupObject.ItemQuality.A;
                                glitchEnemyLootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                                glitchEnemyItem = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetGlitchEnemyItemQuality, glitchEnemyLootTable, false);
                                if (glitchEnemyItem) { aiActor.AdditionalSafeItemDrops.Add(glitchEnemyItem); }

                                ExpandShaders.Instance.ApplyGlitchShader(glitchActor.sprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                            }
                            
                            if (glitchActor.ParentRoom != null && !glitchActor.ParentRoom.IsSealed) { glitchActor.IgnoreForRoomClear = true; }
                        } else {
                            // AIActor is null! Time to bail out of this and continue as normal wall mimic!
                            goto IL_ESCAPE;
                        }
                    } else {
                        Exploder.Explode(VFXExplosionLocation, wallMimicExplosionData, VFXExplosionSource, ignoreQueues: true, damageTypes: CoreDamageTypes.None);
                        GameObject SpawnVFXObject = Instantiate((GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Spawn_Poof"));
                        tk2dBaseSprite SpawnVFXObjectComponent = SpawnVFXObject.GetComponent<tk2dBaseSprite>();
                        SpawnVFXObjectComponent.PlaceAtPositionByAnchor(FinalSpawnLocation + new Vector3(0f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                        SpawnVFXObjectComponent.HeightOffGround = 1f;
                        SpawnVFXObjectComponent.UpdateZDepth();
                        GameObject spawnedRatCorpseObject = Instantiate(ExpandPrefabs.RatCorpseNPC, FinalSpawnLocation, Quaternion.identity);
                        TalkDoerLite talkdoerComponent = spawnedRatCorpseObject.GetComponent<TalkDoerLite>();
                        talkdoerComponent.transform.position.XY().GetAbsoluteRoom().RegisterInteractable(talkdoerComponent);
                        talkdoerComponent.transform.position.XY().GetAbsoluteRoom().TransferInteractableOwnershipToDungeon(talkdoerComponent);
                        talkdoerComponent.playmakerFsm.SetState("Set Mode");
                        ExpandUtility.AddHealthHaver(talkdoerComponent.gameObject, 60, flashesOnDamage: false, exploderSpawnsItem: true);
                    }
                    yield return null;
                    Destroy(gameObject);
                    yield break;
                }
            }
            IL_ESCAPE:
            PickupObject.ItemQuality targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.C : PickupObject.ItemQuality.D) : PickupObject.ItemQuality.B;
            GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
            PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
            if (item) {
                if (CursedBrickMode) {
                    if (UnityEngine.Random.value <= m_ItemDropOdds | m_isGlitched) {
                        aiActor.AdditionalSafeItemDrops.Add(item);
                    } else {                        
                        aiActor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(70));
                        if (BraveUtility.RandomBool()) { aiActor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(70)); }
                        if (SpawnEnemyMode) { aiActor.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(70)); }
                    }
                    if (SpawnEnemyMode && UnityEngine.Random.value <= m_FriendlyMimicOdds) { m_isFriendlyMimic = true; }
                    if (m_isGlitched) {
                        float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                        float RandomDispFloat = UnityEngine.Random.Range(0.1f, 0.16f);
                        float RandomDispIntensityFloat = UnityEngine.Random.Range(0.1f, 0.4f);
                        float RandomColorProbFloat = UnityEngine.Random.Range(0.05f, 0.2f);
                        float RandomColorIntensityFloat = UnityEngine.Random.Range(0.1f, 0.25f);

                        targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.B : PickupObject.ItemQuality.C) : PickupObject.ItemQuality.A;
                        lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                        item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                        if (item) { aiActor.AdditionalSafeItemDrops.Add(item);}
                        
                        ExpandShaders.Instance.ApplyGlitchShader(sprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                    }
                } else {
                    aiActor.AdditionalSafeItemDrops.Add(item);
                }                
            }
            if (CursedBrickMode && SpawnEnemyMode && UnityEngine.Random.value <= m_FriendlyMimicOdds) { m_isFriendlyMimic = true; }
            aiActor.enabled = true;            
            behaviorSpeculator.enabled = true;            
            if (aiActor.ParentRoom != null && aiActor.ParentRoom.IsSealed && !m_isFriendlyMimic) { aiActor.IgnoreForRoomClear = false; }
            // if (m_isFriendlyMimic) { ExpandUtility.MakeCompanion(aiActor); }
            if (m_isFriendlyMimic) { aiActor.ApplyEffect(GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultPermanentCharmEffect, 1f, null); }
            if (!m_failedWallConfigure) {
                int count = this.specRigidbody.PixelColliders.Count;
                for (int j = 0; j < count - 2; j++) { this.specRigidbody.PixelColliders[j].Enabled = true; }
                this.specRigidbody.PixelColliders.RemoveAt(count - 1);
                this.specRigidbody.PixelColliders.RemoveAt(count - 2);
                StaticReferenceManager.AllShadowSystemDepthHavers.Remove(m_fakeWall.transform);
                Destroy(m_fakeWall);
                Destroy(m_fakeCeiling);
            } else {
                int count = this.specRigidbody.PixelColliders.Count;
                for (int j = 0; j < count; j++) { this.specRigidbody.PixelColliders[j].Enabled = true; }
            }
            for (int k = 0; k < m_hands.Length; k++) { m_hands[k].gameObject.SetActive(true); }
            aiActor.ToggleRenderers(true);
            if (aiShooter) { aiShooter.ToggleGunAndHandRenderers(true, "ExpandWallMimicManager"); }
            aiActor.IsGone = false;
            healthHaver.IsVulnerable = true;
            aiActor.State = AIActor.ActorState.Normal;
            for (int l = 0; l < m_hands.Length; l++) { m_hands[l].gameObject.SetActive(false); }
            m_isFinished = true;
            delay = 0.58f;
            timer = 0f;
            Vector3 targetPos = m_startingPos + DungeonData.GetIntVector2FromDirection(m_facingDirection).ToVector3();
            while (timer < delay) {
                aiAnimator.LockFacingDirection = true;
                aiAnimator.FacingDirection = DungeonData.GetAngleFromDirection(m_facingDirection);
                yield return null;
                timer += BraveTime.DeltaTime;
                transform.position = Vector3.Lerp(m_startingPos, targetPos, Mathf.InverseLerp(0.42f, 0.58f, timer));
                this.specRigidbody.Reinitialize();
            }
            aiAnimator.LockFacingDirection = false;
            knockbackDoer.SetImmobile(false, "ExpandWallMimicManager");
            aiActor.CollisionDamage = 0.5f;
            aiActor.CollisionKnockbackStrength = m_collisionKnockbackStrength;            
            yield break;
        }        

        public override void StartIntro() { StartCoroutine(DoIntro()); }

        private IEnumerator DoIntro() {
            aiActor.enabled = false;
            behaviorSpeculator.enabled = false;
            aiActor.ToggleRenderers(false);
            aiActor.IsGone = true;
            healthHaver.IsVulnerable = false;
            knockbackDoer.SetImmobile(true, "ExpandWallMimicManager");
            m_hands = GetComponentsInChildren<GunHandController>();
            for (int i = 0; i < m_hands.Length; i++) { m_hands[i].gameObject.SetActive(false); }
            yield return null;
            aiActor.ToggleRenderers(false);
            if (aiShooter) { aiShooter.ToggleGunAndHandRenderers(false, "ExpandWallMimicManager"); }
            SpeculativeRigidbody specRigidbody = this.specRigidbody;
            specRigidbody.OnRigidbodyCollision = (SpeculativeRigidbody.OnRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnRigidbodyCollision, new SpeculativeRigidbody.OnRigidbodyCollisionDelegate(HandleRigidbodyCollision));
            SpeculativeRigidbody specRigidbody2 = this.specRigidbody;
            specRigidbody2.OnBeamCollision = (SpeculativeRigidbody.OnBeamCollisionDelegate)Delegate.Combine(specRigidbody2.OnBeamCollision, new SpeculativeRigidbody.OnBeamCollisionDelegate(HandleBeamCollision));
            for (int j = 0; j < m_hands.Length; j++) { m_hands[j].gameObject.SetActive(false); }
            yield break;
        }        

        private void HandleRigidbodyCollision(CollisionData rigidbodyCollision) {
            if (CanAwaken && rigidbodyCollision.OtherRigidbody.projectile) { StartCoroutine(BecomeMimic()); }
        }

        private void HandleBeamCollision(BeamController beamController) { if (CanAwaken) { StartCoroutine(BecomeMimic()); } }

        private void OnToggleRenderers() {
            if (m_isHidden && aiActor) {
                if (aiActor.sprite) { aiActor.sprite.renderer.enabled = false; }
                if (aiActor.ShadowObject) { aiActor.ShadowObject.GetComponent<Renderer>().enabled = false; }
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            Vector2 vector = transform.position.XY() + new Vector2(specRigidbody.GroundPixelCollider.ManualOffsetX / 16f, specRigidbody.GroundPixelCollider.ManualOffsetY / 16f);
            Vector2 vector2 = vector.ToIntVector2(VectorConversions.Round).ToVector2();
            transform.position += (vector2 - vector).ToVector3ZUp();
            pos1 = vector2.ToIntVector2(VectorConversions.Floor);
            pos2 = pos1 + IntVector2.Right;
            m_facingDirection = GetFacingDirection(pos1, pos2);
            if (m_facingDirection == DungeonData.Direction.WEST) {
                pos1 = pos2;
                m_startingPos = transform.position + new Vector3(1f, 0f);
            } else if (m_facingDirection == DungeonData.Direction.EAST) {
                pos2 = pos1;
                m_startingPos = transform.position;
            } else {
                m_startingPos = transform.position + new Vector3(0.5f, 0f);
            }
            try {
                CellData cellData = GameManager.Instance.Dungeon.data[pos1];
                CellData cellData2 = GameManager.Instance.Dungeon.data[pos2];
                cellData.isSecretRoomCell = true;
                cellData2.isSecretRoomCell = true;
                cellData.forceDisallowGoop = true;
                cellData2.forceDisallowGoop = true;
                cellData.cellVisualData.preventFloorStamping = true;
                cellData2.cellVisualData.preventFloorStamping = true;
                cellData.isWallMimicHideout = true;
                cellData2.isWallMimicHideout = true;
                if (m_facingDirection == DungeonData.Direction.WEST || m_facingDirection == DungeonData.Direction.EAST) {
                    GameManager.Instance.Dungeon.data[pos1 + IntVector2.Up].isSecretRoomCell = true;
                }
            } catch (Exception ex) {
                Debug.Log("[DEBUG] Warning: Exception caught during ExpandWallMimicManager.ConfigureOnPlacement!\nLikely due to Wall Mimic spawning in an area besides a normal Wall Mimic Hideout!");
                Debug.LogException(ex);
                m_failedWallConfigure = true;
            }
            m_configured = true;
        }

        private DungeonData.Direction GetFacingDirection(IntVector2 pos1, IntVector2 pos2) {
            DungeonData data = GameManager.Instance.Dungeon.data;
            if (data.isWall(pos1 + IntVector2.Down) && data.isWall(pos1 + IntVector2.Up)) { return DungeonData.Direction.EAST; }
            if (data.isWall(pos2 + IntVector2.Down) && data.isWall(pos2 + IntVector2.Up)) { return DungeonData.Direction.WEST; }
            if (data.isWall(pos1 + IntVector2.Down) && data.isWall(pos2 + IntVector2.Down)) { return DungeonData.Direction.NORTH; }
            if (data.isWall(pos1 + IntVector2.Up) && data.isWall(pos2 + IntVector2.Up)) { return DungeonData.Direction.SOUTH; }
            Debug.LogError("Not able to determine the direction of a wall mimic! Using random direction instead!");            
            // return DungeonData.Direction.SOUTH;
            return BraveUtility.RandomElement(new List<DungeonData.Direction>() {
                    DungeonData.Direction.EAST,
                    DungeonData.Direction.WEST,
                    DungeonData.Direction.NORTH,
                    DungeonData.Direction.SOUTH
                }
            );
        }

        private HashSet<IntVector2> GetCeilingTileSet(IntVector2 pos1, IntVector2 pos2, DungeonData.Direction facingDirection) {
            IntVector2 intVector;
            IntVector2 intVector2;
            if (facingDirection == DungeonData.Direction.NORTH) {
                intVector = pos1 + new IntVector2(-1, 0);
                intVector2 = pos2 + new IntVector2(1, 1);
            } else if (facingDirection == DungeonData.Direction.SOUTH) {
                intVector = pos1 + new IntVector2(-1, 2);
                intVector2 = pos2 + new IntVector2(1, 3);
            } else if (facingDirection == DungeonData.Direction.EAST) {
                intVector = pos1 + new IntVector2(-1, 0);
                intVector2 = pos2 + new IntVector2(0, 3);
            } else {
                if (facingDirection != DungeonData.Direction.WEST) { return null; }
                intVector = pos1 + new IntVector2(0, 0);
                intVector2 = pos2 + new IntVector2(1, 3);
            }
            HashSet<IntVector2> hashSet = new HashSet<IntVector2>();
            for (int i = intVector.x; i <= intVector2.x; i++) {
                for (int j = intVector.y; j <= intVector2.y; j++) {
                    IntVector2 item = new IntVector2(i, j);
                    hashSet.Add(item);
                }
            }
            return hashSet;
        }

        public override bool IsFinished { get { return m_isFinished; } }

        protected override void OnDestroy() {
            if (base.visibilityManager) {
                ObjectVisibilityManager visibilityManager = base.visibilityManager;
                visibilityManager.OnToggleRenderers = (Action)Delegate.Remove(visibilityManager.OnToggleRenderers, new Action(OnToggleRenderers));
            }
            base.OnDestroy();
        }
    }
}

