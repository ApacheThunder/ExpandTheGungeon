using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using System;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandComponents;
using Dungeonator;

namespace ExpandTheGungeon.ItemAPI {
    
    public class ClownFriend : PassiveItem {
                
        public static GameObject ClownFriendObject;
        public static int ClownFriendID;

        public static void Init(AssetBundle expandSharedAssets1) {

            ClownFriendObject = expandSharedAssets1.LoadAsset<GameObject>("Clown Friend");
            SpriteSerializer.AddSpriteToObject(ClownFriendObject, ExpandPrefabs.EXItemCollection, "clownfriend");

            ClownFriend clownFriendItem = ClownFriendObject.AddComponent<ClownFriend>();
            clownFriendItem.CompanionGuid = ExpandCustomEnemyDatabase.ClownkinAltGUID;
            // clownFriendItem.CompanionPastGuid = string.Empty;
            // clownFriendItem.UsesAlternatePastPrefab = false;
            // clownFriendItem.Synergies = new CompanionTransformSynergy[0];
            // clownFriendItem.PreventRespawnOnFloorLoad = false;
            // clownFriendItem.BabyGoodMimicOrbitalOverridden = false;

            string shortDesc = "Fool's Company...";
            string longDesc = "A wondering Clownkin has learned the art of the balloon gun. He decided to follow you on your journey...for some reason...";

            ItemBuilder.SetupItem(clownFriendItem, shortDesc, longDesc, "ex");
            clownFriendItem.quality = ItemQuality.A;
            if (!ExpandSettings.EnableEXItems) { clownFriendItem.quality = ItemQuality.EXCLUDED; }

            ExpandLists.CompanionItems.Add(ClownFriendObject);

            ClownFriendID = clownFriendItem.PickupObjectId;
        }

        public static void PostInit() {
            ClownFriendObject.GetComponent<ClownFriend>().BalloonPrefabs = new GameObject[] {
                ExpandPrefabs.EX_GreenBalloon,
                ExpandPrefabs.EX_BlueBalloon,
                ExpandPrefabs.EX_YellowBalloon,
                ExpandPrefabs.EX_PinkBalloon,
            };
        }

        public ClownFriend() {
            CompanionGuid = ExpandCustomEnemyDatabase.ClownkinAltGUID;
            BalloonRespawnTimer = 30;
            BalloonWasPopped = false;

            m_PickedUp = false;
            m_SynergyObtained = false;

            m_confettiNames = new string[] {
                "Global VFX/Confetti_Blue_001",
                "Global VFX/Confetti_Yellow_001",
                "Global VFX/Confetti_Green_001"
            };

            m_Timer = BalloonRespawnTimer;
        }
        
        [EnemyIdentifier]
        public string CompanionGuid;

        public float BalloonRespawnTimer;

        public GameObject[] BalloonPrefabs;

        [NonSerialized]
        public bool BalloonWasPopped;
        [NonSerialized]
        public ExpandBalloonController PlayerBalloon;

        [NonSerialized]
        private bool m_PickedUp;
        [NonSerialized]
        private bool m_SynergyObtained;
        [NonSerialized]
        private float m_Timer;
        [NonSerialized]
        private GameObject m_extantCompanion;
        [NonSerialized]
        private string[] m_confettiNames;
        
                
        private void CreateCompanion(PlayerController owner) {
            string guid = CompanionGuid;
            
            AIActor companion = EnemyDatabase.GetOrLoadByGuid(guid);
            if (!companion) { return; }
            Vector3 vector = owner.transform.position;
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER) { vector += new Vector3(1.125f, -0.3125f, 0f); }
            GameObject extantCompanion2 = Instantiate(companion.gameObject, vector, Quaternion.identity);
            m_extantCompanion = extantCompanion2;
            CompanionController orAddComponent = m_extantCompanion.GetOrAddComponent<CompanionController>();
            orAddComponent.Initialize(owner);
            if (orAddComponent.specRigidbody) {
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
            }
        }

        private void DoConfetti(Vector2 startPosition, int ConfettiCount = 8) {
            for (int i = 0; i < ConfettiCount; i++) {
                GameObject ConfettiObject = (GameObject)ResourceCache.Acquire(BraveUtility.RandomElement(m_confettiNames));
                if (ConfettiObject) {
                    WaftingDebrisObject component = Instantiate(ConfettiObject).GetComponent<WaftingDebrisObject>();
                    if (component) {
                        component.sprite.PlaceAtPositionByAnchor(startPosition.ToVector3ZUp(0f) + new Vector3(0.5f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                        Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
                        insideUnitCircle.y = -Mathf.Abs(insideUnitCircle.y);
                        component.Trigger(insideUnitCircle.ToVector3ZUp(1.5f) * UnityEngine.Random.Range(0.5f, 2f), 0.5f, 0f);
                    }
                }
            }
        }

        public void RespawnBalloon(PlayerController player) {
            if (!player) { return; }
            if (PlayerBalloon) { Destroy(PlayerBalloon.gameObject); }
            
            PlayerBalloon = Instantiate(BraveUtility.RandomElement(BalloonPrefabs), player.gameObject.transform.position, Quaternion.identity).GetComponent<ExpandBalloonController>();
            if (PlayerBalloon) {
                Vector3 BalloonOffset = new Vector3(1, 2);
                PlayerBalloon.gameObject.transform.position += (BalloonOffset + new Vector3(0, 0, 1));
                PlayerBalloon.BalloonOffset = (BalloonOffset + new Vector3(0, 0, 1));
                PlayerBalloon.BalloonSprite = PlayerBalloon.gameObject.GetComponent<tk2dSprite>();
                PlayerBalloon.BalloonFloatHeight = 3;
                PlayerBalloon.FloatUpOnDeathSpeed = UnityEngine.Random.Range(2.75f, 3.5f);
                PlayerBalloon.DoBlankOnPop = true;
                PlayerBalloon.BlankChance = -1;
                PlayerBalloon.ParentClownkinController = null;
                PlayerBalloon.ParentClownFriendItem = this;
                PlayerBalloon.PopsOnProjectileHit = true;
                PlayerBalloon.BlankVFXPrefab = BraveResources.Load<GameObject>("Global VFX/BlankVFX_Ghost", ".prefab");
                PlayerBalloon.Initialize(player);
            }
        }

        private void DestroyCompanion() {
            if (!m_extantCompanion) { return; }
            Destroy(m_extantCompanion);
            m_extantCompanion = null;
        }

        protected override void Update() {
            if (!m_owner | !m_pickedUp | Dungeon.IsGenerating) { return; }
            if (m_owner.HasPassiveItem(ClownBullets.ClownBulletsID) && !m_SynergyObtained) {
                m_SynergyObtained = true;
                if (!BalloonWasPopped) {
                    RespawnBalloon(m_owner);
                    AkSoundEngine.PostEvent("Play_OBJ_prize_won_01", m_owner.gameObject);
                    DoConfetti(m_owner.gameObject.transform.position);
                }
            } else if (!m_owner.HasPassiveItem(ClownBullets.ClownBulletsID)) {
                m_SynergyObtained = false;
                if (PlayerBalloon) { Destroy(PlayerBalloon.gameObject); }
            }
            if (!m_SynergyObtained | !BalloonWasPopped) { return; }
            m_Timer -= BraveTime.DeltaTime;
            if (m_Timer < 0) {
                m_Timer = BalloonRespawnTimer;
                BalloonWasPopped = false;
                if (m_owner.HasPassiveItem(ClownBullets.ClownBulletsID)) {
                    RespawnBalloon(m_owner);
                    AkSoundEngine.PostEvent("Play_OBJ_prize_won_01", m_owner.gameObject);
                    DoConfetti(m_owner.gameObject.transform.position);
                }
                return;
            }
            base.Update();
        }

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            player.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Combine(player.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
            CreateCompanion(player);
            if (m_SynergyObtained && !BalloonWasPopped) { RespawnBalloon(player); }
        }

        private void HandleNewFloor(PlayerController obj) {
            DestroyCompanion();
            CreateCompanion(obj);
        }

        public override DebrisObject Drop(PlayerController player) {
            DestroyCompanion();
            player.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Remove(player.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
            DebrisObject drop = base.Drop(player);
            if (drop) {
                ClownFriend component = drop.gameObject.GetComponent<ClownFriend>();
                if (component) {
                    component.m_PickedUp = true;
                    if (component.PlayerBalloon) { Destroy(component.PlayerBalloon.gameObject); }
                }
            }
            return drop;
        }
        

        public override void MidGameSerialize(List<object> data) {
            base.MidGameSerialize(data);
            data.Add(m_PickedUp);
        }

        public override void MidGameDeserialize(List<object> data) {
            base.MidGameDeserialize(data);
            if (data.Count == 1) { m_PickedUp = (bool)data[0]; }
        }

        protected override void OnDestroy() {
            if (m_owner != null) {
                PlayerController owner = m_owner;
                owner.OnNewFloorLoaded = (Action<PlayerController>)Delegate.Remove(owner.OnNewFloorLoaded, new Action<PlayerController>(HandleNewFloor));
            }
            if (PlayerBalloon) { Destroy(PlayerBalloon.gameObject); }
            DestroyCompanion();
            base.OnDestroy();
        }
    }
}

