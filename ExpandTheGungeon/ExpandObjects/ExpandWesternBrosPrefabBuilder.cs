using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandObjects
{
    public enum WestBros
    {
        Angel = 0,
        Nome = 1,
        Tuc = 2
    }

    public class ExpandWesternBrosPrefabBuilder : ExpandCustomEnemyDatabase
    {
        public static GameObject WestBrosAngelPrefab;
        public static GameObject WestBrosNomePrefab;
        public static GameObject WestBrosTucPrefab;

        public static GameObject WestBrosAngelHatPrefab;
        public static GameObject WestBrosNomeHatPrefab;
        public static GameObject WestBrosTucHatPrefab;

        public static GameObject WestBrosHandPrefab;

        public static string WestBrosAngelGUID;
        public static string WestBrosNomeGUID;
        public static string WestBrosTucGUID;

        public static int WestBrosAngelGunID = -1;
        public static int WestBrosNomeGunID = -1;
        public static int WestBrosTucGunID = -1;

        // saving these as static references probably doesn't matter, but I'm not risking it to remove them and get crashes again

        public static PickupObject WestBrosGun;
        public static tk2dSpriteCollectionData Collection;
        public static tk2dSpriteAnimation Animation;
        public static AIActor Shades;
        public static DebrisObject ShadesDebris;

        public static void BuildWestBrosBossPrefabs(AssetBundle assetBundle)
        {
            // all 3 west bros animation sets are actually in their 3 cut guns (752,753,754), they all contain the same ones, so we just take the first one
            // 752 is nome
            // 753 is tuc
            // 754 is angel
            WestBrosGun = PickupObjectDatabase.GetById(752);

            Collection = WestBrosGun.gameObject.GetComponent<tk2dSprite>().Collection;
            Animation = WestBrosGun.gameObject.GetComponent<tk2dSpriteAnimator>().Library;
            
            Shades = ExpandCustomEnemyDatabase.GetOrLoadByGuid_Orig("c00390483f394a849c36143eb878998f");
            ShadesDebris = Shades.GetComponentInChildren<ExplosionDebrisLauncher>().debrisSources[0];

            SetupHand(assetBundle, out WestBrosHandPrefab, Collection);

            BuildWestBrosHatPrefab(assetBundle, out WestBrosAngelHatPrefab, WestBros.Angel, Collection, ShadesDebris);
            BuildWestBrosHatPrefab(assetBundle, out WestBrosNomeHatPrefab, WestBros.Nome, Collection, ShadesDebris);
            BuildWestBrosHatPrefab(assetBundle, out WestBrosTucHatPrefab, WestBros.Tuc, Collection, ShadesDebris);

            BuildWestBrosBossPrefab(assetBundle, out WestBrosAngelPrefab, WestBros.Angel, false, Collection, Animation);
            BuildWestBrosBossPrefab(assetBundle, out WestBrosNomePrefab, WestBros.Nome, true, Collection, Animation, true);
            BuildWestBrosBossPrefab(assetBundle, out WestBrosTucPrefab, WestBros.Tuc, true, Collection, Animation);
        }

        private static void SetupHand(AssetBundle assetBundle, out GameObject handPrefab, tk2dSpriteCollectionData collection)
        {
            var texture = assetBundle.LoadAsset<Texture2D>("Western_Bros_Hand");

            tk2dSpriteDefinition spriteDefinition = SpriteBuilder.ConstructDefinition(texture);
            spriteDefinition.name = texture.name;

            // change the sprite definition so the sprite is centered, so it can be flipped without offsets
            spriteDefinition.boundsDataCenter = Vector3.zero;
            spriteDefinition.untrimmedBoundsDataCenter = Vector3.zero;

            float val = 0.2f;

            spriteDefinition.boundsDataExtents = new Vector3(val * 2, val * 2, 0);
            spriteDefinition.untrimmedBoundsDataExtents = new Vector3(val * 2, val * 2, 0);

            spriteDefinition.position0 = new Vector3(-val, -val, 0);
            spriteDefinition.position1 = new Vector3(val, -val, 0);
            spriteDefinition.position2 = new Vector3(-val, val, 0);
            spriteDefinition.position3 = new Vector3(val, val, 0);

            SpriteBuilder.AddSpriteToCollection(spriteDefinition, collection);

            handPrefab = assetBundle.LoadAsset<GameObject>("WestBroHandObject");

            var sprite = handPrefab.AddComponent<tk2dSprite>();
            sprite.SetSprite(collection, texture.name);

            handPrefab.AddComponent<PlayerHandController>();
        }

        private static void BuildWestBrosHatPrefab(AssetBundle assetBundle, out GameObject outObject, WestBros whichBro, tk2dSpriteCollectionData spriteCollection, DebrisObject broDebris)
        {
            outObject = assetBundle.LoadAsset<GameObject>($"WestBrosHat_{whichBro}");

            string hatSpriteName = null;

            switch (whichBro)
            {
                case WestBros.Angel:
                    hatSpriteName = "hat_angel";
                    break;

                case WestBros.Nome:
                    hatSpriteName = "hat_nome";
                    break;

                case WestBros.Tuc:
                    hatSpriteName = "hat_tuco";
                    break;
            }

            tk2dSprite hatSprite = outObject.AddComponent<tk2dSprite>();
            hatSprite.SetSprite(spriteCollection, hatSpriteName);
            hatSprite.SortingOrder = 0;

            ExpandUtility.GenerateSpriteAnimator(outObject);

            DebrisObject debrisObject = outObject.AddComponent<DebrisObject>();

            // this is set seperately because we use DeclaredOnly for the reflection field copying and Priority is inherited from EphemeralObject
            debrisObject.Priority = broDebris.Priority;

            ExpandUtility.ReflectionShallowCopyFields(debrisObject, broDebris, (BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        }

        // TODO add summon vfx (maybe summon_vfx is not even the correct animation)

        // TODO what about hit_left and hit_right?

        // TODO anger and charge are technically a loop section

        // understanding the 'The Good, the Bad and the Ugly' reference:
        // Angel is 'Angel Eyes': The Bad, a ruthless, confident, borderline-sadistic mercenary, takes a pleasure in killing and always finishes a job for which he is paid
        // Nome is 'Blondie' (the Man with No Name): the Good, a taciturn, confident bounty hunter, teams up with Tuco, and Angel Eyes temporarily, to find the buried gold
        // Tuc is 'Tuco Benedicto Pacífico Juan María Ramírez': the Ugly, a fast-talking, comically oafish yet also cunning, cagey, resilient and resourceful Mexican bandit

        // TODO remove at some point, I got tired of writing 'ToString()'
        public static void Log(object o, bool debug = false)
        {
            ETGModConsole.Log(o != null ? o.ToString() : "null", debug);
        }

        private static void BuildWestBrosBossPrefab(AssetBundle assetBundle, out GameObject outObject, WestBros whichBro, bool isSmiley, tk2dSpriteCollectionData sourceSpriteCollection, tk2dSpriteAnimation sourceAnimations, bool keepIntroDoer = false)
        {
            GameObject prefab = GetOrLoadByGuid_Orig(isSmiley ? "ea40fcc863d34b0088f490f4e57f8913" : "c00390483f394a849c36143eb878998f").gameObject;
            // Smiley
            // Shades
            // outObject = UnityEngine.Object.Instantiate(prefab, null, false);
            outObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);

            try
            {
                string name = $"West Bros {whichBro}";

                outObject.SetActive(false);
                outObject.name = name;

                AIActor actor = outObject.GetComponent<AIActor>();

                actor.healthHaver.overrideBossName = "Western Bros";

                actor.EnemyId = UnityEngine.Random.Range(100000, 999999);
                actor.ActorName = name;

                EncounterTrackable Encounterable = outObject.GetComponent<EncounterTrackable>();

                switch (whichBro)
                {
                    case WestBros.Angel:
                        actor.EnemyGuid = "275354563e244f558be87fcff4b07f9f";
                        Encounterable.EncounterGuid = "7d6e1faf682d4402b29535020313f383";
                        break;

                    case WestBros.Nome:
                        actor.EnemyGuid = "3a1a33a905bb4b669e7d798f20674c4c";
                        Encounterable.EncounterGuid = "78cb8889dc884dd9b3aafe64558d858e";
                        break;

                    case WestBros.Tuc:
                        actor.EnemyGuid = "d2e7ea9ea9a444cebadd3bafa0832cd1";
                        Encounterable.EncounterGuid = "1df53371ce084dafb46f6bcd5a6c1c5f";
                        break;
                }

                // TODO at some distant point in time
                Encounterable.journalData.PrimaryDisplayName = name;
                Encounterable.journalData.NotificationPanelDescription = name;
                Encounterable.journalData.AmmonomiconFullEntry = name;

                // x BroController
                // x BulletBroDeathController
                // x BulletBrosIntroDoer
                // x BulletBroSeekTargetBehavior // movement behaviour
                //   BulletBroRepositionBehavior // completely unused

                var oldBroController = outObject.GetComponent<BroController>();
                var newBroController = outObject.AddComponent<ExpandWesternBroController>();

                newBroController.enrageAnim = oldBroController.enrageAnim;
                newBroController.enrageAnimTime = oldBroController.enrageAnimTime;
                newBroController.enrageHealToPercent = oldBroController.enrageHealToPercent;
                newBroController.overheadVfx = oldBroController.overheadVfx;
                newBroController.postEnrageMoveSpeed = oldBroController.postEnrageMoveSpeed;

                newBroController.whichBro = whichBro;
                newBroController.postSecondEnrageMoveSpeed = newBroController.postEnrageMoveSpeed;

                Destroy(oldBroController);

                Destroy(outObject.GetComponent<BulletBroDeathController>());
                outObject.AddComponent<ExpandWesternBroDeathController>();

                var newMovementBehavior = new ExpandWesternBroSeekTargetBehavior();
                var oldMovementBehavior = actor.behaviorSpeculator.MovementBehaviors.First() as BulletBroSeekTargetBehavior;

                newMovementBehavior.CustomRange = oldMovementBehavior.CustomRange;
                newMovementBehavior.PathInterval = oldMovementBehavior.PathInterval;
                newMovementBehavior.StopWhenInRange = oldMovementBehavior.StopWhenInRange;

                actor.behaviorSpeculator.MovementBehaviors = new List<MovementBehaviorBase>() { newMovementBehavior };

                // only smiley has a bossIntroDoer, so the stuff after this would null reference if done with shade
                if (isSmiley)
                {
                    if (!keepIntroDoer)
                    {
                        Destroy(outObject.GetComponent<BulletBrosIntroDoer>());
                        Destroy(outObject.GetComponent<GenericIntroDoer>());
                    }
                    else
                    {
                        // BulletBrosIntroDoer is a SpecificIntroDoer; it does not inherent from GenericIntroDoer, it requires it to be present
                        BulletBrosIntroDoer bulletBrosIntroDoer = outObject.GetComponent<BulletBrosIntroDoer>();

                        // destroy it so we can add our own
                        Destroy(bulletBrosIntroDoer);

                        GenericIntroDoer genericIntroDoer = outObject.GetComponent<GenericIntroDoer>();

                        genericIntroDoer.portraitSlideSettings.bossNameString = "Western Bros";
                        genericIntroDoer.portraitSlideSettings.bossSubtitleString = "Triple Tap";
                        genericIntroDoer.portraitSlideSettings.bossQuoteString = string.Empty;

                        genericIntroDoer.portraitSlideSettings.bossArtSprite = assetBundle.LoadAsset<Texture2D>("WesternBrosBossCard");

                        genericIntroDoer.triggerType = GenericIntroDoer.TriggerType.PlayerEnteredRoom;
                        genericIntroDoer.OnIntroFinished = () => { };

                        ExpandWesternBroIntroDoer specificIntroDoer = outObject.AddComponent<ExpandWesternBroIntroDoer>();
                    }
                }

                var animationsAsset = assetBundle.LoadAsset<GameObject>($"WestBrosAnimations_{whichBro}");

                List<tk2dSpriteAnimationClip> animationClips = new List<tk2dSpriteAnimationClip>();

                foreach (tk2dSpriteAnimationClip clip in sourceAnimations.clips)
                {
                    if (clip.name.StartsWith($"{whichBro.ToString().ToLower()}_"))
                    {
                        animationClips.Add(ExpandUtility.DuplicateAnimationClip(clip));
                    }
                }

                List<tk2dSpriteAnimationClip> clipsToAdd = new List<tk2dSpriteAnimationClip>();

                foreach (tk2dSpriteAnimationClip clip in animationClips)
                {
                    clip.name = clip.name.Replace($"{whichBro.ToString().ToLower()}_", string.Empty);

                    clip.name = clip.name.Replace("dash_front", "dash_forward");
                    clip.name = clip.name.Replace("move_front", "move_forward");

                    if (clip.name.EndsWith("forward_left_prime"))
                    {
                        clip.frames[5].eventAudio = "Play_ENM_bullet_dash_01";
                        clip.frames[5].triggerEvent = true;
                    }
                    else if (clip.name.EndsWith("forward_right_prime"))
                    {
                        clip.frames[5].eventAudio = "Play_ENM_bullet_dash_01";
                        clip.frames[5].triggerEvent = true;
                    }
                    else if (clip.name.EndsWith("back_right_prime"))
                    {
                        clip.frames[5].eventAudio = "Play_ENM_bullet_dash_01";
                        clip.frames[5].triggerEvent = true;
                    }
                    else if (clip.name.EndsWith("back_left_prime"))
                    {
                        clip.frames[5].eventAudio = "Play_ENM_bullet_dash_01";
                        clip.frames[5].triggerEvent = true;
                    }
                    else if (clip.name.EndsWith("move_back_right"))
                    {
                        clip.frames[2].eventAudio = "PLay_FS_ENM";
                        clip.frames[2].triggerEvent = true;
                    }
                    else if (clip.name.EndsWith("move_back_left"))
                    {
                        clip.frames[2].eventAudio = "PLay_FS_ENM";
                        clip.frames[2].triggerEvent = true;
                    }
                    else if (clip.name.EndsWith("move_forward_left"))
                    {
                        clip.frames[2].eventAudio = "PLay_FS_ENM";
                        clip.frames[2].triggerEvent = true;
                    }
                    else if (clip.name.EndsWith("move_forward_right"))
                    {
                        clip.frames[2].eventAudio = "PLay_FS_ENM";
                        clip.frames[2].triggerEvent = true;
                    }
                    else if (clip.name.EndsWith("anger"))
                    {
                        // Original anger clips on Shades/Smiley is over 8 frames long and was set to loop specific section while West bros are only 2 frames and loops entire clip more then once.
                        // I have to extend this and set a loop section to make sure anger SFX only plays once.
                        tk2dSpriteAnimationFrame[] angerFrames = new tk2dSpriteAnimationFrame[] 
                        {
                            DuplicateAnimationFrame(clip.frames[0]),
                            DuplicateAnimationFrame(clip.frames[1]),
                            DuplicateAnimationFrame(clip.frames[0]),
                            DuplicateAnimationFrame(clip.frames[1])
                        };
                        clip.frames = angerFrames;
                        clip.wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
                        clip.loopStart = 2;
                        clip.frames[0].eventAudio = "Play_BOSS_bulletbros_anger_01";
                        clip.frames[0].triggerEvent = true;
                    }
                    else if (clip.name == "death_right")
                    {
                        clip.name = "die";

                        tk2dSpriteAnimationClip dieFrontRight = ExpandUtility.DuplicateAnimationClip(clip);
                        dieFrontRight.name = "die_front_right";
                        clipsToAdd.Add(dieFrontRight);
                    }
                    else if (clip.name == "death_left")
                    {
                        clip.name = "die_left";

                        tk2dSpriteAnimationClip dieFrontLeft = ExpandUtility.DuplicateAnimationClip(clip);
                        dieFrontLeft.name = "die_front_left";
                        clipsToAdd.Add(dieFrontLeft);
                    }
                    else if (clip.name == "idle_front")
                    {
                        clip.name = "idle";

                        tk2dSpriteAnimationClip bigShot = ExpandUtility.DuplicateAnimationClip(clip);
                        bigShot.wrapMode = tk2dSpriteAnimationClip.WrapMode.Once;
                        bigShot.name = "big_shot";
                        clipsToAdd.Add(bigShot);

                        tk2dSpriteAnimationClip charge = ExpandUtility.DuplicateAnimationClip(clip);
                        charge.wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop;
                        charge.name = "charge";
                        clipsToAdd.Add(charge);

                        // not really necessary since it's nuked further down
                        tk2dSpriteAnimationClip appear = ExpandUtility.DuplicateAnimationClip(clip);
                        appear.wrapMode = tk2dSpriteAnimationClip.WrapMode.Once;
                        appear.name = "appear";
                        clipsToAdd.Add(appear);

                        tk2dSpriteAnimationClip introguntoggle = ExpandUtility.DuplicateAnimationClip(clip);
                        introguntoggle.wrapMode = tk2dSpriteAnimationClip.WrapMode.Once;
                        introguntoggle.name = "intro2";
                        clipsToAdd.Add(introguntoggle);

                        introguntoggle.frames[0].eventInfo = "guntoggle";
                        introguntoggle.frames[0].triggerEvent = true;
                    }
                    else if (clip.name == "summon")
                    {
                        clip.name = "whistle";
                        clip.wrapMode = tk2dSpriteAnimationClip.WrapMode.Once;
                        // the summon vfx don't look right
                        // clip.frames[0].eventVfx = "summon_vfx";
                        // clip.frames[0].triggerEvent = true;
                    }
                    else if (clip.name == "pound")
                    {
                        clip.name = "jump_attack";
                        clip.frames[0].eventAudio = "Play_EX_BOSS_westbros_slam_01"; // Uses modified sound that plays faster on the first half to account for west bros having faster animation.
                        clip.frames[0].triggerEvent = true;
                    }
                    else if (clip.name == "intro")
                    {
                        clip.wrapMode = tk2dSpriteAnimationClip.WrapMode.Once;
                        // clip.frames[(clip.frames.Length - 1)].eventInfo = "guntoggle";
                        // clip.frames[(clip.frames.Length - 1)].triggerEvent = true;
                        // this is setup in case we want the intro to continue looping during the boss card instead of the idle animation
                        // requires to change the intro doer so it sets finished to true once it reaches the first loop

                        //if (whichBro == WestBros.Nome)
                        //{
                        //    // nome's intro animation wasn't looping at the end like the others
                        //    var list = clip.frames.ToList();

                        //    list.RemoveAt(20);
                        //    list.RemoveAt(20);

                        //    clip.frames = list.ToArray();

                        //    clip.loopStart = 23;

                        //    clip.frames[23] = clip.frames[0];
                        //    clip.frames[24] = clip.frames[1];
                        //    clip.frames[25] = clip.frames[2];
                        //    clip.frames[26] = clip.frames[3];
                        //}
                        //else if (whichBro == WestBros.Tuc)
                        //{
                        //    // tuc's intro animation was off by one frame compared to the others
                        //    var list = clip.frames.ToList();

                        //    list.RemoveAt(15);

                        //    clip.frames = list.ToArray();

                        //    clip.loopStart = 21;
                        //}
                    }
                }

                // disables all attacks for debugging
                //foreach (var item in (actor.behaviorSpeculator.AttackBehaviors.First() as AttackBehaviorGroup).AttackBehaviors)
                //{
                //    item.Probability = 0f;
                //}

                animationClips.AddRange(clipsToAdd);

                tk2dSpriteAnimation spriteAnimation = animationsAsset.AddComponent<tk2dSpriteAnimation>();

                spriteAnimation.clips = animationClips.ToArray();

                tk2dSpriteAnimator spriteAnimator = outObject.GetComponent<tk2dSpriteAnimator>();

                spriteAnimator.Library = spriteAnimation;

                tk2dSprite sprite = outObject.GetComponent<tk2dSprite>();
                sprite.SetSprite(sourceSpriteCollection, $"BB_{whichBro.ToString().ToLower()}_idle_front_001");
                sprite.PlaceAtPositionByAnchor(new Vector3(0f, 0f), tk2dBaseSprite.Anchor.LowerCenter);

                AIAnimator animator = outObject.GetComponent<AIAnimator>();
                // removes the 'appear' animation
                animator.OtherAnimations.RemoveAt(0);
                
                outObject.transform.Find("shadow").localPosition += new Vector3(1.52f,0.02f,0);
                
                AIShooter shooter = outObject.GetComponent<AIShooter>();

                string m_CachedShooter = JsonUtility.ToJson(shooter);
                // Caching the current localPosition of gunAttachPoint as it's going to get nuked soon.
                Vector3 m_CachedGunAttachPointPosition = shooter.gunAttachPoint.localPosition;
                
                // AIShooter has some stuff setup prior to game object getting disabled. This ensures old stuff gets properly nuked.
                GunInventory m_inventory = ReflectionHelpers.ReflectGetField<GunInventory>(typeof(AIShooter), "m_inventory", shooter);
                List<PlayerHandController> m_attachedHands = ReflectionHelpers.ReflectGetField<List<PlayerHandController>>(typeof(AIShooter), "m_attachedHands", shooter);

                // Removes old Smiley/Shades gun sprite renderers from main renderer
                for (int i = 0; i < m_inventory.AllGuns.Count; i++) {
                    sprite.DetachRenderer(m_inventory.AllGuns[i].GetSprite());
                    Destroy(m_inventory.AllGuns[i].gameObject);
                }
                sprite.DetachRenderer(m_inventory.CurrentGun.GetSprite());
                Destroy(m_inventory.CurrentGun.gameObject);


                // Gets rid of old hands
                // AIShooter had also attached the hand's renderer's to the gun's sprite but since the previous gun was nuked it doesn't matter.
                foreach (PlayerHandController hand in m_attachedHands)
                {
                    Destroy(hand.gameObject);
                }


                Destroy(shooter.gunAttachPoint.gameObject);
                Destroy(shooter);

                // Now that old AIShooter is gone we can add a new one.
                // This time host object is already inactive so don't have to worry about some unneeded trash hanging around. :P
                shooter = outObject.AddComponent<AIShooter>();
                // Restore some fields the previous AIShooter had.
                JsonUtility.FromJsonOverwrite(m_CachedShooter, shooter);

                // Setup new GunAttachPoint to avoid using any remnent of old AIShooter.
                GameObject GunAttachPoint = new GameObject("GunAttachPoint") { layer = 0 };
                GunAttachPoint.transform.localPosition = m_CachedGunAttachPointPosition;
                GunAttachPoint.transform.SetParent(outObject.transform);

                shooter.gunAttachPoint = GunAttachPoint.transform;
                shooter.handObject = WestBrosHandPrefab.GetComponent<PlayerHandController>();


                // I don't know why they were on to begin with
                // Commented this out. Now that AIShooter is proper nuked and rebuilt, we don't have to do this anymore. :D
                // shooter.ToggleHandRenderers(false, "BroController");

                DebrisObject hatPrefab = null;

                switch (whichBro)
                {
                    case WestBros.Angel:
                        shooter.gunAttachPoint.position = new Vector3(-1.05f, 0.5f);
                        shooter.equippedGunId = WestBrosAngelGunID;
                        WestBrosAngelGUID = actor.EnemyGuid;
                        hatPrefab = WestBrosAngelHatPrefab.GetComponent<DebrisObject>();
                        break;
                    case WestBros.Nome:
                        shooter.gunAttachPoint.position = new Vector3(-1.05f, 0.4f);
                        shooter.equippedGunId = WestBrosNomeGunID;
                        WestBrosNomeGUID = actor.EnemyGuid;
                        hatPrefab = WestBrosNomeHatPrefab.GetComponent<DebrisObject>();
                        break;
                    case WestBros.Tuc:
                        shooter.gunAttachPoint.position = new Vector3(-1.05f, 0.5f);
                        shooter.equippedGunId = WestBrosTucGunID;
                        WestBrosTucGUID = actor.EnemyGuid;
                        hatPrefab = WestBrosTucHatPrefab.GetComponent<DebrisObject>();
                        break;
                }

                if (shooter.equippedGunId == -1)
                {
                    ETGModConsole.Log("The West Bros Gun ID should have been set at this point already, but it wasn't. Assigning fallback gun.");
                    shooter.equippedGunId = isSmiley ? 35 : 22;
                }

                // shooter.RegenerateCache();

                var hatLauncher = outObject.GetComponentInChildren<ExplosionDebrisLauncher>();

                // basically changes smiley's debris launcher into shades'
                hatLauncher.specifyArcDegrees = false;
                hatLauncher.minShards = 1;
                hatLauncher.maxShards = 1;

                hatLauncher.debrisSources = new DebrisObject[] { hatPrefab };

                // move the ring of bullets spawned by jumping
                var shootPoint = outObject.transform.Find("shoot point");
                shootPoint.position += new Vector3(1.5f, 0);

                // move the actual pixel colliders
                var rigidbody = outObject.GetComponent<SpeculativeRigidbody>();

                foreach (var item in rigidbody.PixelColliders)
                {
                    item.ManualOffsetX = 32;
                    item.Regenerate(outObject.transform);
                }

                // TODO balance
                //actor.healthHaver.ForceSetCurrentHealth(1000);
                //actor.healthHaver.SetHealthMaximum(1000);

                actor.RegenerateCache();

                AddEnemyToDatabase(outObject, actor.EnemyGuid, true);
                FakePrefab.MarkAsFakePrefab(outObject);
                DontDestroyOnLoad(outObject);
            }
            catch (Exception e)
            {
                ETGModConsole.Log($"Error setting up the western bro {whichBro}: " + e.ToString());
            }
        }

        public static tk2dSpriteAnimationFrame DuplicateAnimationFrame (tk2dSpriteAnimationFrame source)
        {
            return new tk2dSpriteAnimationFrame()
            {   
                spriteCollection = source.spriteCollection,
                spriteId = source.spriteId,
                invulnerableFrame = source.invulnerableFrame,
                groundedFrame = source.groundedFrame,
                requiresOffscreenUpdate = source.requiresOffscreenUpdate,
                eventAudio = source.eventAudio,
                eventVfx = source.eventVfx,
                eventStopVfx = source.eventStopVfx,
                eventLerpEmissive = source.eventLerpEmissive,
                eventLerpEmissiveTime = source.eventLerpEmissiveTime,
                eventLerpEmissivePower = source.eventLerpEmissivePower,
                forceMaterialUpdate = source.forceMaterialUpdate,
                finishedSpawning = source.finishedSpawning,
                triggerEvent = source.triggerEvent,
                eventInfo = source.eventInfo,
                eventInt = source.eventInt,
                eventFloat = source.eventFloat,
                eventOutline = source.eventOutline
            };
        }
    }
}

