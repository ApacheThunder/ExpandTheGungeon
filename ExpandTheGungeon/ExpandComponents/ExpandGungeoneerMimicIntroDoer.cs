using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    [RequireComponent(typeof(GenericIntroDoer))]
    public class ExpandGungeoneerMimicIntroDoer : SpecificIntroDoer {
        
        public GameObject MirrorBase;
        public GameObject MirrorShatterFX;
        public GameObject ShatterSystem;

        public bool m_finished;

        private bool m_initialized;        
        private bool m_MirrorHasShattered;
        private bool m_MirrorDepthUpdated;

        private ExpandGungeoneerMimicBossController m_GungeoneerMimicController;

        private AIActor m_AIActor;

        public void Update() {
            if (!m_initialized) {
                m_AIActor = aiActor;                
                m_GungeoneerMimicController = m_AIActor.gameObject.GetComponent<ExpandGungeoneerMimicBossController>();
                if (MirrorBase && MirrorBase.GetComponent<tk2dSprite>()) {
                    MirrorBase.GetComponent<tk2dSprite>().HeightOffGround += 2f;
                    MirrorBase.GetComponent<tk2dSprite>().UpdateZDepth();
                }
                m_MirrorDepthUpdated = false;
                m_MirrorHasShattered = false;
                m_initialized = true;
            }
        }

        public override void PlayerWalkedIn(PlayerController player, List<tk2dSpriteAnimator> animators) {
            if (m_AIActor) {
                m_AIActor.GetAbsoluteParentRoom().CompletelyPreventLeaving = true;
                m_AIActor.ToggleRenderers(false);
                m_AIActor.aiShooter.handObject.gameObject.SetActive(false);
                m_AIActor.IsGone = true;
                m_AIActor.State = AIActor.ActorState.Inactive;
            }
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
                AkSoundEngine.PostEvent("Play_OBJ_crystal_shatter_01", gameObject);
                AkSoundEngine.PostEvent("Play_OBJ_pot_shatter_01", gameObject);
                AkSoundEngine.PostEvent("Play_OBJ_glass_shatter_01", gameObject);
                
                mirrorAnimation.Play("MirrorGlassCrack");
                while (mirrorAnimation.IsPlaying("MirrorGlassCrack")) { yield return null; }
                if (MirrorBase.GetComponent<tk2dSprite>()) { MirrorBase.GetComponent<tk2dSprite>().SetSprite("PlayerMimicMirror_Broken"); }
                if (ShatterSystem) { ShatterSystem.SetActive(true); }
                if (MirrorShatterFX) {
                    MirrorShatterFX.SetActive(true);
                    MirrorShatterFX.GetComponent<tk2dSpriteAnimator>().PlayAndDestroyObject("PlayerMimicShatter");
                }
                if (MirrorBase) { AkSoundEngine.PostEvent("Play_OBJ_mirror_shatter_01", gameObject); }
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

            List<int> RandomStrings = new List<int>() { 1, 2, 3, 4, 5 };

            int RandomString = BraveUtility.RandomElement(RandomStrings);

            string DialogOption1 = "I...MUST...BECOME...YOU!";

            string DialogOption2 = "My fellow mimics have had enough of you!";
            string DialogOption2_Line2 = "Time for me to to take your place so they may finally be free!";

            string DialogOption3 = "The mimic army shall rise!";
            string DialogOption3_Line2 = "Your corpse shall be the proof that I need to lead them!";

            string DialogOption4 = "I will kill you twice!";
            string DialogOption4_Line2 = "First I will kill you here.";
            string DialogOption4_Line3 = "Then I will get the bullet and kill your past!";

            string DialogOption5 = "I shall become the ultimate mimic!";
            string DialogOption5_Line2 = "I will kill you here so that I can take your place.";
            string DialogOption5_Line3 = "Your fellow Gungeoneers will suspect nothing!";
            
            if (RandomString == 1) {
                yield return StartCoroutine(TalkRaw(DialogOption1, dialogBoxOffset));
            } else if (RandomString == 2) {
                yield return StartCoroutine(TalkRaw(DialogOption2, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption2_Line2, dialogBoxOffset));
            } else if (RandomString == 3) {
                yield return StartCoroutine(TalkRaw(DialogOption3, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption3_Line2, dialogBoxOffset));
            } else if (RandomString == 4) {
                yield return StartCoroutine(TalkRaw(DialogOption4, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption4_Line2, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption4_Line3, dialogBoxOffset));
            } else if (RandomString == 5) {
                yield return StartCoroutine(TalkRaw(DialogOption5, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption5_Line2, dialogBoxOffset));
                yield return StartCoroutine(TalkRaw(DialogOption5_Line3, dialogBoxOffset));
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
            m_AIActor.State = AIActor.ActorState.Normal;
            m_AIActor.specRigidbody.CollideWithOthers = true;
            m_AIActor.ToggleRenderers(true);
            m_AIActor.aiShooter.handObject.gameObject.SetActive(true);
            if (MirrorBase && MirrorBase.GetComponent<tk2dSprite>()) {
                MirrorBase.GetComponent<tk2dSprite>().HeightOffGround -= 2f;
                MirrorBase.GetComponent<tk2dSprite>().UpdateZDepth();
                m_MirrorDepthUpdated = true;
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
            m_AIActor.aiShooter.ToggleGunAndHandRenderers(true, "GungeoneerMimicIntroDoer");
            m_AIActor.specRigidbody.CollideWithOthers = true;
            m_AIActor.aiActor.IsGone = false;
            m_AIActor.aiActor.State = AIActor.ActorState.Normal;
            m_AIActor.aiShooter.AimAtPoint(m_AIActor.CenterPosition - new Vector2(0, -2));
            m_AIActor.aiShooter.gunAttachPoint.gameObject.SetActive(true);
            m_AIActor.aiShooter.handObject.gameObject.SetActive(true);
            if (!m_MirrorDepthUpdated && MirrorBase && MirrorBase.GetComponent<tk2dSprite>()) {
                MirrorBase.GetComponent<tk2dSprite>().HeightOffGround -= 2f;
                MirrorBase.GetComponent<tk2dSprite>().UpdateZDepth();
            }
            // if (m_GungeoneerMimicController) { m_GungeoneerMimicController.IntroDone = true; }
            if (!m_MirrorHasShattered) {
                if (MirrorBase.GetComponent<tk2dSprite>()) { MirrorBase.GetComponent<tk2dSprite>().SetSprite("PlayerMimicMirror_Broken"); }
                if (MirrorBase) { AkSoundEngine.PostEvent("Play_OBJ_mirror_shatter_01", gameObject); }
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

