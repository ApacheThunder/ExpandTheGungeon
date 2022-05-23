using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.SpriteAPI;
using ExpandTheGungeon.ItemAPI;

namespace ExpandTheGungeon.ExpandComponents {

    [RequireComponent(typeof(GenericIntroDoer))]
    public class ExpandGungeoneerMimicIntroDoer : SpecificIntroDoer {
        
        public GameObject MirrorBase;
        public GameObject MirrorShatterFX;
        public GameObject ShatterSystem;

        public bool m_finished;

        // private bool m_initialized;        
        private bool m_MirrorHasShattered;

        private ExpandGungeoneerMimicBossController m_GungeoneerMimicController;

        private AIActor m_AIActor;

        public void Start() {
            m_AIActor = aiActor;
            m_AIActor.AdditionalSafeItemDrops = new List<PickupObject>() { Mimiclay.MimiclayObject.GetComponent<Mimiclay>() };
            m_GungeoneerMimicController = m_AIActor.gameObject.GetComponent<ExpandGungeoneerMimicBossController>();
        }

         private void DoInitialConfiguration(PlayerController player) {

            m_AIActor.sprite.SetSprite(player.sprite.Collection, player.sprite.GetCurrentSpriteDef().name);

            tk2dSprite m_HandSprite = m_AIActor.aiShooter.handObject.gameObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(m_HandSprite, player.primaryHand.gameObject.GetComponent<tk2dSprite>());
            m_AIActor.aiShooter.handObject.sprite.SetSprite(player.primaryHand.sprite.Collection, player.primaryHand.sprite.GetCurrentSpriteDef().name);
            
            // Generate BossCard based on current Player.
            Texture2D BossCardForeground = ExpandUtility.FlipTexture(Instantiate(player.BosscardSprites[0]));
            // Mirror thing will be used as static background. (will be the same for all possible boss cards)
            Texture2D BossCardBackground = ExpandAssets.LoadAsset<Texture2D>("MimicInMirror_BossCardBackground");
            // Combine foreground boss card generated from PlayerController onto the static background image loased in earlier. Resolutions must match!
            Texture2D BossCardTexture = ExpandUtility.CombineTextures(BossCardBackground, BossCardForeground);

            GenericIntroDoer miniBossIntroDoer = gameObject.GetComponent<GenericIntroDoer>();
            if (BossCardTexture) { miniBossIntroDoer.portraitSlideSettings.bossArtSprite = BossCardTexture; }

            if (player.characterIdentity == PlayableCharacters.Bullet) {
                m_AIActor.EnemySwitchState = "Metal_Bullet_Man";
            } else if (player.characterIdentity == PlayableCharacters.Convict) {
                m_AIActor.EnemySwitchState = "Convict";
            } else if (player.characterIdentity == PlayableCharacters.CoopCultist) {
                m_AIActor.EnemySwitchState = "Cultist";
            } else if (player.characterIdentity == PlayableCharacters.Cosmonaut) {
                m_AIActor.EnemySwitchState = "Cosmonaut";
            } else if (player.characterIdentity == PlayableCharacters.Guide) {
                m_AIActor.EnemySwitchState = "Guide";
            } else if (player.characterIdentity == PlayableCharacters.Gunslinger) {
                m_AIActor.EnemySwitchState = "Gunslinger";
            } else if (player.characterIdentity == PlayableCharacters.Ninja) {
                m_AIActor.EnemySwitchState = "Ninja";
            } else if (player.characterIdentity == PlayableCharacters.Pilot) {
                m_AIActor.EnemySwitchState = "Rogue";
            } else if (player.characterIdentity == PlayableCharacters.Robot) {
                m_AIActor.EnemySwitchState = "Robot";
            } else if (player.characterIdentity == PlayableCharacters.Soldier) {
                m_AIActor.EnemySwitchState = "Marine";
            } else if (player.characterIdentity == PlayableCharacters.Eevee) {
                m_AIActor.EnemySwitchState = "Convict";
                ExpandShaders.ApplyParadoxPlayerShader(m_AIActor.sprite);
            } else {
                m_AIActor.EnemySwitchState = "Gun Cultist";
            }
            List<tk2dSpriteAnimationClip> m_AnimationClips = new List<tk2dSpriteAnimationClip>();
            
            foreach (tk2dSpriteAnimationClip clip in player.spriteAnimator.Library.clips) {
                if (!string.IsNullOrEmpty(clip.name)) {
                    if (clip.name.ToLower() == "dodge") {
                        m_AnimationClips.Add(clip);
                        if (clip.frames != null && clip.frames.Length > 0) {
                            if (!player.UseArmorlessAnim) { m_AIActor.sprite.SetSprite(clip.frames[0].spriteId); }
                        }
                    } else if (clip.name.ToLower() == "dodge_armorless") {
                        m_AnimationClips.Add(clip);
                        if (clip.frames != null && clip.frames.Length > 0) {
                            if (player.UseArmorlessAnim) { m_AIActor.sprite.SetSprite(clip.frames[0].spriteId); }
                        }
                    } else if (clip.name.ToLower() == "run_down") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "run_down_armorless") {
                        m_AnimationClips.Add(clip);
                    } else if(clip.name.ToLower() == "death_shot") {
                        m_AnimationClips.Add(clip);
                    } else if (clip.name.ToLower() == "death_shot_armorless") {
                        m_AnimationClips.Add(clip);
                    }
                }
            }

            if (m_AnimationClips.Count > 0) {
                if (!m_AIActor.spriteAnimator.Library) { m_AIActor.spriteAnimator.Library = m_AIActor.gameObject.AddComponent<tk2dSpriteAnimation>(); }
                m_AIActor.spriteAnimator.Library.clips = m_AnimationClips.ToArray();
            }

            MirrorController mirror = ExpandPrefabs.CurrsedMirror.GetComponent<MirrorController>();
                                                
            MirrorBase = Instantiate(ExpandPrefabs.DoppelgunnerMirror, gameObject.transform.position - new Vector3(0.25f, 1), Quaternion.identity);
            ShatterSystem = Instantiate(mirror.ShatterSystem, MirrorBase.transform.position, Quaternion.identity);
            ShatterSystem.SetActive(false);
            ShatterSystem.transform.parent = MirrorBase.transform;

            MirrorShatterFX = Instantiate(ExpandPrefabs.DoppelgunnerMirrorFX, (MirrorBase.transform.position - Vector3.one), Quaternion.identity);
            MirrorShatterFX.SetActive(false);
        }

        public void Update() { }

        public override void PlayerWalkedIn(PlayerController player, List<tk2dSpriteAnimator> animators) {
            if (m_AIActor) {
                if (m_GungeoneerMimicController) {
                    m_GungeoneerMimicController.m_Player = player;
                    m_GungeoneerMimicController.Init();
                }
                m_AIActor.GetAbsoluteParentRoom().CompletelyPreventLeaving = true;
                m_AIActor.ToggleRenderers(false);
                m_AIActor.IsGone = true;
                m_AIActor.State = AIActor.ActorState.Inactive;
            }

            DoInitialConfiguration(player);


            if (MirrorBase && MirrorBase.GetComponent<tk2dSprite>()) {
                MirrorBase.GetComponent<tk2dSprite>().HeightOffGround += 2f;
                MirrorBase.GetComponent<tk2dSprite>().UpdateZDepth();
            }
            
            m_MirrorHasShattered = false;            
        }

        public override void StartIntro(List<tk2dSpriteAnimator> animators) { StartCoroutine(DoIntro()); }

        public override bool IsIntroFinished { get { return m_finished; } }

        public override void OnBossCard() { }
        
        private IEnumerator DoIntro() {

            yield return StartCoroutine(WaitForSecondsInvariant(1f));
            tk2dSpriteAnimator mirrorAnimation = null;
            if (MirrorBase) { mirrorAnimation = MirrorBase.GetComponent<tk2dSpriteAnimator>(); }
            if (mirrorAnimation) {
                mirrorAnimation.Play("PlayerMimicFadeIn");
                while (mirrorAnimation.IsPlaying("PlayerMimicFadeIn")) { yield return null; }
                yield return StartCoroutine(WaitForSecondsInvariant(1f));
            }
            yield return StartCoroutine(DoTalk(new Vector3(0.5f, 1.25f)));
            yield return StartCoroutine(WaitForSecondsInvariant(0.6f));
            if (mirrorAnimation) {                
                mirrorAnimation.Play("MirrorGlassCrack");
                AkSoundEngine.PostEvent("Play_OBJ_crystal_shatter_01", GameManager.Instance.MainCameraController.gameObject);
                AkSoundEngine.PostEvent("Play_OBJ_pot_shatter_01", GameManager.Instance.MainCameraController.gameObject);
                AkSoundEngine.PostEvent("Play_OBJ_glass_shatter_01", GameManager.Instance.MainCameraController.gameObject);
                while (mirrorAnimation.IsPlaying("MirrorGlassCrack")) { yield return null; }
                AkSoundEngine.PostEvent("Play_OBJ_mirror_shatter_01", GameManager.Instance.MainCameraController.gameObject);
                if (MirrorBase.GetComponent<tk2dSprite>()) { MirrorBase.GetComponent<tk2dSprite>().SetSprite("PlayerMimicMirror_Broken"); }
                if (ShatterSystem) { ShatterSystem.SetActive(true); }
                if (MirrorShatterFX) {
                    MirrorShatterFX.SetActive(true);
                    MirrorShatterFX.GetComponent<tk2dSpriteAnimator>().PlayAndDestroyObject("PlayerMimicShatter");
                }
                m_MirrorHasShattered = true;
                yield return StartCoroutine(WalkThroughMirror());
            } else {
                yield return StartCoroutine(WalkThroughMirror());
            }
            m_finished = true;
            yield break;
        }
        
        private IEnumerator DoTalk(Vector3 dialogBoxOffset) {
            GetComponent<GenericIntroDoer>().SuppressSkipping = true;
            TextBoxManager.TIME_INVARIANT = true;

            List<int> RandomStrings = new List<int>() { 1, 2, 3, 4 };

            int RandomString = BraveUtility.RandomElement(RandomStrings);

            string DialogOption1 = "This Gungeon ain't big enough for both of us!";

            string DialogOption2 = "I will kill you twice!";
            string DialogOption2_Line2 = "First I will kill you here.";
            string DialogOption2_Line3 = "Then I will get the bullet and kill your past!";

            string DialogOption3 = "I shall become the ultimate clone!";
            string DialogOption3_Line2 = "I will kill you here so that I can take your place.";
            string DialogOption3_Line3 = "Your fellow Gungeoneers will suspect nothing!";

            string DialogOption4 = "You can kill your past, but how about your present?";
            
            if (RandomString == 1) {
                yield return StartCoroutine(TalkRaw(DialogOption1, dialogBoxOffset));
            } else if (RandomString == 2) {
                yield return StartCoroutine(TalkRaw(DialogOption2, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption2_Line2, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption2_Line3, dialogBoxOffset));

            } else if (RandomString == 3) {
                yield return StartCoroutine(TalkRaw(DialogOption3, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption3_Line2, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption3_Line3, dialogBoxOffset));
            } else if (RandomString == 4) {
                yield return StartCoroutine(TalkRaw(DialogOption4, dialogBoxOffset));
            }

            yield return null;
            TextBoxManager.TIME_INVARIANT = false;
            GetComponent<GenericIntroDoer>().SuppressSkipping = false;
            yield break;
        }

        private IEnumerator TalkRaw(string plaintext, Vector3 DialogBoxOffset) {
            TextBoxManager.ShowTextBox(transform.position + DialogBoxOffset, transform, 5f, plaintext, "playermimicboss", false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
            bool advancedPressed = false;
            while (!advancedPressed) {
                advancedPressed = (BraveInput.GetInstanceForPlayer(0).WasAdvanceDialoguePressed() || BraveInput.GetInstanceForPlayer(1).WasAdvanceDialoguePressed());
                yield return null;
            }
            TextBoxManager.ClearTextBox(transform);
            yield break;
        }
        
        private IEnumerator WalkThroughMirror() {
            float time = 1.5f;
            m_AIActor.IsGone = false;
            m_AIActor.State = AIActor.ActorState.Awakening;
            // m_AIActor.State = AIActor.ActorState.Normal;
            m_AIActor.specRigidbody.CollideWithOthers = true;
            m_AIActor.ToggleRenderers(true);
            if (MirrorBase && MirrorBase.GetComponent<tk2dSprite>()) {
                MirrorBase.GetComponent<tk2dSprite>().HeightOffGround -= 2f;
                MirrorBase.GetComponent<tk2dSprite>().UpdateZDepth();
            }
            if (m_GungeoneerMimicController.m_Player.UseArmorlessAnim) {
                m_AIActor.spriteAnimator.Play("dodge_armorless");
                while (m_AIActor.spriteAnimator.IsPlaying("dodge_armorless")) {
                    float Y = 0.025f;
                    m_AIActor.gameObject.transform.position -= new Vector3(0, Y);
                    m_AIActor.specRigidbody.Reinitialize();
                    m_AIActor.spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                    yield return null;
                }
            } else {
                m_AIActor.spriteAnimator.Play("dodge");
                while (m_AIActor.spriteAnimator.IsPlaying("dodge")) {
                    float Y = 0.05f;
                    m_AIActor.gameObject.transform.position -= new Vector3(0, Y);
                    m_AIActor.specRigidbody.Reinitialize();
                    m_AIActor.spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                    yield return null;
                }
            }
            m_AIActor.spriteAnimator.Play("run_down");
            for (float elapsed = 0f; elapsed < time; elapsed += GameManager.INVARIANT_DELTA_TIME) {
                float Y = 0.025f;
                m_AIActor.spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                m_AIActor.gameObject.transform.position -= new Vector3(0, Y);
                m_AIActor.specRigidbody.Reinitialize();
                yield return null;
            }
            yield break;
        }

        private IEnumerator WaitForSecondsInvariant(float time) {
            for (float elapsed = 0f; elapsed < time; elapsed += GameManager.INVARIANT_DELTA_TIME) { yield return null; }
            yield break;
        }

        public override void EndIntro() {
            m_finished = true;
            StopAllCoroutines();
            m_AIActor.ToggleRenderers(true);
            m_AIActor.specRigidbody.CollideWithOthers = true;
            m_AIActor.IsGone = false;
            m_AIActor.State = AIActor.ActorState.Normal;
            // m_AIActor.State = AIActor.ActorState.Awakening;
            m_AIActor.aiShooter.AimAtPoint(m_AIActor.CenterPosition - new Vector2(0, -2));
            m_AIActor.aiShooter.gunAttachPoint.gameObject.SetActive(true);
            if (MirrorBase && MirrorBase.GetComponent<tk2dSprite>()) {
                MirrorBase.GetComponent<tk2dSprite>().HeightOffGround -= 2f;
                MirrorBase.GetComponent<tk2dSprite>().UpdateZDepth();
            }
            if (m_GungeoneerMimicController) {
                m_GungeoneerMimicController.ModifyCamera(true);
                m_GungeoneerMimicController.IntroDone = true;
            }
            if (!m_MirrorHasShattered) {
                if (MirrorBase.GetComponent<tk2dSprite>()) { MirrorBase.GetComponent<tk2dSprite>().SetSprite("PlayerMimicMirror_Broken"); }
                AkSoundEngine.PostEvent("Play_OBJ_mirror_shatter_01", GameManager.Instance.MainCameraController.gameObject);
                if (MirrorShatterFX) {
                    MirrorShatterFX.SetActive(true);
                    MirrorShatterFX.GetComponent<tk2dSpriteAnimator>().PlayAndDestroyObject("PlayerMimicShatter");
                }
                if (ShatterSystem) {
                    ShatterSystem.SetActive(true);
                    StartCoroutine(WaitForShatterParticles());
                }
            } else {
                StartCoroutine(WaitForShatterParticles());
            }
        }

        private IEnumerator WaitForShatterParticles() {
            yield return new WaitForSeconds(2.5f);
            ShatterSystem.GetComponent<ParticleSystem>().Pause(false);
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

