﻿using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using System;
using System.Collections.Generic;
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

    public class ExpandWesternBrosPrefabBuilder
    {
        public static GameObject WestBrosAngelPrefab;
        public static GameObject WestBrosNomePrefab;
        public static GameObject WestBrosTucPrefab;

        public static GameObject WestBrosAngelHatPrefab;
        public static GameObject WestBrosNomeHatPrefab;
        public static GameObject WestBrosTucHatPrefab;

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

            BuildWestBrosHatPrefab(assetBundle, out WestBrosAngelHatPrefab, WestBros.Angel, Collection, ShadesDebris);
            BuildWestBrosHatPrefab(assetBundle, out WestBrosNomeHatPrefab, WestBros.Nome, Collection, ShadesDebris);
            BuildWestBrosHatPrefab(assetBundle, out WestBrosTucHatPrefab, WestBros.Tuc, Collection, ShadesDebris);

            BuildWestBrosBossPrefab(assetBundle, out WestBrosAngelPrefab, WestBros.Angel, false, Collection, Animation, false);
            BuildWestBrosBossPrefab(assetBundle, out WestBrosNomePrefab, WestBros.Nome, true, Collection, Animation, true);
            BuildWestBrosBossPrefab(assetBundle, out WestBrosTucPrefab, WestBros.Tuc, true, Collection, Animation, false);
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
            debrisObject.Priority = broDebris.Priority;

            ExpandUtility.ReflectionShallowCopyFields(debrisObject, broDebris, (BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
        }

        // TODO fix gun offsets :/, TODO take from intro animation

        // TODO has smiley always been broken when spawned in with the console? the first one disappears into nothingness while all new ones don't have a weapon hold animation

        // TODO give fitting hands sprite, TODO color changes when enraged, TODO you can take the hand sprite size, color and design from the intro animation

        // TODO what about hit_left and hit_right?

        // TODO use summon vfx

        // TODO anger and charge are technically a loop section

        // TODO this is how we can tweak attack behaviours, or in the custom BroController
        //foreach (AttackBehaviorGroup item in actor.behaviorSpeculator.AttackBehaviors)
        //{
        //    foreach (var item2 in item.AttackBehaviors)
        //    {
        //        if (item2.NickName == "Spawn Bros")
        //        {
        //            item2.Probability = 0f;
        //        }
        //    }
        //}

        // understanding the 'The Good, the Bad and the Ugly' reference:
        // Angel is 'Angel Eyes': The Bad, a ruthless, confident, borderline-sadistic mercenary, takes a pleasure in killing and always finishes a job for which he is paid
        // Nome is 'Blondie' (the Man with No Name): the Good, a taciturn, confident bounty hunter, teams up with Tuco, and Angel Eyes temporarily, to find the buried gold
        // Tuc is 'Tuco Benedicto Pacífico Juan María Ramírez': the Ugly, a fast-talking, comically oafish yet also cunning, cagey, resilient and resourceful Mexican bandit

        private static void BuildWestBrosBossPrefab(AssetBundle assetBundle, out GameObject outObject, WestBros whichBro, bool isSmiley, tk2dSpriteCollectionData sourceSpriteCollection, tk2dSpriteAnimation sourceAnimations, bool keepIntroDoer)
        {
            GameObject prefab = ExpandCustomEnemyDatabase.GetOrLoadByGuid_Orig(isSmiley
                ? "ea40fcc863d34b0088f490f4e57f8913"  // Smiley
                : "c00390483f394a849c36143eb878998f").gameObject; // Shades

            outObject = UnityEngine.Object.Instantiate(prefab, null, false);

            try
            {
                string name = $"West Bros {whichBro}";

                outObject.SetActive(false);
                outObject.name = name;

                AIActor actor = outObject.GetComponent<AIActor>();

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

                // TODO replace with custom variants that work with multiples bros

                //   BroController (enraging twice currently does nothing)
                // x BulletBroDeathController
                // x BulletBrosIntroDoer
                //   BulletBroRepositionBehavior
                //   BulletBroSeekTargetBehavior

                // BulletBrosJumpBurst1 / BulletBrosJumpBurst2
                // BulletBrosAngryAttack1
                // BulletBrosSweepAttack1
                // BulletBrosTridentAttack1

                // Destroy(outObject.GetComponent<BroController>());
                UnityEngine.Object.Destroy(outObject.GetComponent<BulletBroDeathController>());
                outObject.AddComponent<ExpandWesternBroDeathController>();

                // only smiley has a bossIntroDoer, so the stuff after this would null reference if done with shade
                if (isSmiley)
                {
                    if (!keepIntroDoer)
                    {
                        UnityEngine.Object.Destroy(outObject.GetComponent<BulletBrosIntroDoer>());
                        UnityEngine.Object.Destroy(outObject.GetComponent<GenericIntroDoer>());
                    }
                    else
                    {
                        // BulletBrosIntroDoer is a SpecificIntroDoer; it does not inherent from GenericIntroDoer, it requires it to be present
                        BulletBrosIntroDoer bulletBrosIntroDoer = outObject.GetComponent<BulletBrosIntroDoer>();

                        // destroy it so we can add our own
                        UnityEngine.Object.Destroy(bulletBrosIntroDoer);

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

                    if (clip.name == "death_right")
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
                    }
                    else if (clip.name == "summon")
                    {
                        clip.name = "whistle";
                        clip.wrapMode = tk2dSpriteAnimationClip.WrapMode.Once;
                    }
                    else if (clip.name == "pound")
                    {
                        clip.name = "jump_attack";
                    }
                    else if (clip.name == "intro")
                    {
                        clip.wrapMode = tk2dSpriteAnimationClip.WrapMode.Once;

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

                animationClips.AddRange(clipsToAdd);

                tk2dSpriteAnimation spriteAnimation = animationsAsset.AddComponent<tk2dSpriteAnimation>();

                spriteAnimation.clips = animationClips.ToArray();

                tk2dSpriteAnimator spriteAnimator = outObject.GetComponent<tk2dSpriteAnimator>();
                spriteAnimator.Library = spriteAnimation;

                tk2dSprite sprite = outObject.GetComponent<tk2dSprite>();
                sprite.Collection = sourceSpriteCollection;
                sprite.SetSprite($"BB_{whichBro.ToString().ToLower()}_idle_front_001");

                AIAnimator animator = outObject.GetComponent<AIAnimator>();
                // removes the 'appear' animation
                animator.OtherAnimations.RemoveAt(0);

                // TODO remove shadow for now, because it needs special treatment, maybe even ignore it forever, not that important in the desert
                actor.HasShadow = false;
                actor.ShadowPrefab = null;
                UnityEngine.Object.Destroy(outObject.transform.Find("shadow").gameObject);

                AIShooter shooter = outObject.GetComponent<AIShooter>();

                // I don't know why they were on to begin with
                shooter.ToggleHandRenderers(false, "BroController");

                int gunID = -1;
                DebrisObject hatPrefab = null;

                switch (whichBro)
                {
                    case WestBros.Angel:
                        gunID = WestBrosAngelGunID;
                        WestBrosAngelGUID = actor.EnemyGuid;
                        hatPrefab = WestBrosAngelHatPrefab.GetComponent<DebrisObject>();
                        break;

                    case WestBros.Nome:
                        gunID = WestBrosNomeGunID;
                        WestBrosNomeGUID = actor.EnemyGuid;
                        hatPrefab = WestBrosNomeHatPrefab.GetComponent<DebrisObject>();
                        break;

                    case WestBros.Tuc:
                        gunID = WestBrosTucGunID;
                        WestBrosTucGUID = actor.EnemyGuid;
                        hatPrefab = WestBrosTucHatPrefab.GetComponent<DebrisObject>();
                        break;
                }

                if (gunID != -1)
                {
                    shooter.equippedGunId = gunID;
                }
                else
                {
                    ETGModConsole.Log("The West Bros Gun ID should have been set at this point already. Assigning fallback gun.");
                    shooter.equippedGunId = isSmiley ? 35 : 22;
                }

                shooter.RegenerateCache();

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

                // move the guns and recenter them, this can be tweaked later if needed or wanted
                sprite.PlaceAtPositionByAnchor(new Vector3(0f, 0f), tk2dBaseSprite.Anchor.LowerCenter);
                shooter.gunAttachPoint.position = new Vector3(-1f, 0.8f);

                // TODO balance
                //actor.healthHaver.ForceSetCurrentHealth(1000);
                //actor.healthHaver.SetHealthMaximum(1000);

                actor.RegenerateCache();

                ExpandCustomEnemyDatabase.AddEnemyToDatabase(outObject, actor.EnemyGuid, true);
                FakePrefab.MarkAsFakePrefab(outObject);
                UnityEngine.Object.DontDestroyOnLoad(outObject);
            }
            catch (Exception e)
            {
                ETGModConsole.Log($"Error setting up the western bro {whichBro}: " + e.ToString());
            }
        }
    }
}