using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ItemAPI {

	public class RockSlide : PlayerItem {

        public static int RockSlidePickupID;

        public static GameObject RockslideObject;

        public static void Init(AssetBundle expandSharedAssets1) {
            RockslideObject = expandSharedAssets1.LoadAsset<GameObject>("Rock Slide");
            SpriteSerializer.AddSpriteToObject(RockslideObject, ExpandPrefabs.EXItemCollection, "rockslide");

            RockSlide rockslide = RockslideObject.AddComponent<RockSlide>();
            string shortDesc = "Crushing Defeat";
			string longDesc = "Falling rocks are a well known threat to everyone, especially within the Gungeon.\n\nIt does not help that a long gone Gungeoneer accidentally popularized the idea of using falling debris as a weapon among the Gundead.\n\nHowever, they quickly grew tired of it, seeming too easy or effortless to eliminate Gungeoneers compared to the exhilarating experience of the combat they were forged for.";
			ItemBuilder.SetupItem(rockslide, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(rockslide, ItemBuilder.CooldownType.Damage, 275f);
            rockslide.quality = ItemQuality.B;
            if (!ExpandSettings.EnableEXItems) { rockslide.quality = ItemQuality.EXCLUDED; }

            List<string> spritePaths = new List<string>() {
                "plunger_fire_001",
                "plunger_fire_002",
                "plunger_fire_003",
                "plunger_fire_004",
                "plunger_fire_005",
                "plunger_fire_006"
            };

            /*tk2dSprite rockslidesprite = RockslideObject.GetComponent<tk2dSprite>();
            foreach (string sprite in spritePaths) { SpriteBuilder.AddSpriteToCollection(expandSharedAssets1.LoadAsset<Texture2D>(sprite), rockslidesprite.Collection); }*/

            ExpandUtility.GenerateSpriteAnimator(RockslideObject);

            tk2dSpriteAnimator rockslideAnimator = RockslideObject.GetComponent<tk2dSpriteAnimator>();

            ExpandUtility.AddAnimation(rockslideAnimator, ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), spritePaths, "Activate", frameRate: 8);

            RockSlidePickupID = rockslide.PickupObjectId;
        }
        

        public RockSlide() {
            m_PickedUp = false;
            m_Ready = true;
            m_MinesCageInObject = ExpandObjectDatabase.Mines_Cave_In;
        }

        private GameObject m_MinesCageInObject;

        private bool m_PickedUp;
        private bool m_Ready;

        public override bool CanBeUsed(PlayerController user) {
            return user.IsInCombat && base.CanBeUsed(user);
        }

        protected override void DoEffect(PlayerController user) {
            AkSoundEngine.PostEvent("Play_OBJ_detonate_push_01", user.gameObject);
            SpawnRockslides(user);
		}

        public void SpawnRockslides(PlayerController user) {
            spriteAnimator.Play("Activate");
            if (user.CurrentRoom.HasActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) {
                int EnemyCount = UnityEngine.Random.Range(1, user.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear).Count);
                // Set a cap incase there's an insane amount of enemies in the room and it chose a very large value.

                if (UnityEngine.Random.value <= 0.35f) {
                    EnemyCount = user.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear).Count;
                } else if (EnemyCount > 3 && UnityEngine.Random.value <= 0.5f) {
                    EnemyCount = 3;
                }

                if (EnemyCount > 10) { EnemyCount = 10; }                

                List<AIActor> SelectedEnemies = new List<AIActor>();
                foreach (AIActor enemy in user.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.RoomClear)) { SelectedEnemies.Add(enemy); }

                for(int i = 0; i < EnemyCount; i++) {

                    if (SelectedEnemies.Count <= 0) { break; }

                    AIActor TargetEnemy = BraveUtility.RandomElement(SelectedEnemies);

                    if (TargetEnemy && !TargetEnemy.healthHaver.IsDead) {
                        Vector2 SelectedEnemyPosition = TargetEnemy.specRigidbody.GetUnitCenter(ColliderType.Ground);
                        StartCoroutine(HandleTriggerRockSlide(user, m_MinesCageInObject, SelectedEnemyPosition));
                        SelectedEnemies.Remove(TargetEnemy);
                    }
                }
            }
        }


        private IEnumerator HandleTriggerRockSlide(PlayerController user, GameObject RockSlidePrefab, Vector2 targetPosition) {
            RoomHandler CurrentRoom = user.CurrentRoom;
            GameObject NewRockSlide = Instantiate(RockSlidePrefab, targetPosition, Quaternion.identity);
            HangingObjectController RockSlideController = NewRockSlide.GetComponent<HangingObjectController>();
            // If you don't null the trigger object, it will attempt to place the plunger somewhere. (but we don't want a plunger to appear)
            // But the room won't have the event trigger so an excpetion will happen in Start()!
            RockSlideController.triggerObjectPrefab = null;
            GameObject[] m_additionalDestroyObjects = new GameObject[] { RockSlideController.additionalDestroyObjects[1] };
            RockSlideController.additionalDestroyObjects = m_additionalDestroyObjects;
            Destroy(NewRockSlide.transform.Find("Sign").gameObject);
            RockSlideController.ConfigureOnPlacement(CurrentRoom);
            // If we attempt to immedietely trigger it, the cave in animation bugs and it happens too quickly. (there is no fall animation/delay to impact)
            // Delay just a tiny bit before activating.
            yield return new WaitForSeconds(0.01f);
            RockSlideController.Interact(user);
            m_Ready = false;
            yield break;
        }

        public override void Update() {
            base.Update();
            if (!Dungeon.IsGenerating && m_PickedUp && !m_Ready) {
                if (!IsOnCooldown) {
                    sprite.SetSprite("rockslide");
                    m_Ready = true;
                }
            }
        }

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            m_PickedUp = true;
        }

        protected override void OnPreDrop(PlayerController player) {
            base.OnPreDrop(player);
            m_PickedUp = false;
        }
        
        protected override void OnDestroy() {
            m_PickedUp = false;
            base.OnDestroy();
        }
    }
}

