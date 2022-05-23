using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandGungeoneerMimicDeathController : BraveBehaviour {

        public ExpandGungeoneerMimicDeathController() {
            DeathClipName = "death_shot";
            DeathClipName_Armorless = "death_shot_armorless";
        }

        private ExpandGungeoneerMimicBossController m_GungeoneerMimicBossController;

        public string DeathClipName;
        public string DeathClipName_Armorless;

        public void Start() {
            healthHaver.ManualDeathHandling = true;
            healthHaver.OnPreDeath += OnBossDeath;
            healthHaver.OverrideKillCamTime = new float?(5);
            m_GungeoneerMimicBossController = gameObject.GetComponent<ExpandGungeoneerMimicBossController>();
        }

        protected override void OnDestroy() { base.OnDestroy(); }

        private void OnBossDeath(Vector2 dir) {
            if (m_GungeoneerMimicBossController) {
                m_GungeoneerMimicBossController.IntroDone = false;
                m_GungeoneerMimicBossController.Disconnect();
            }
            GameManager.Instance.PrimaryPlayer.CurrentInputState = PlayerInputState.NoInput;
            if (GameManager.Instance.SecondaryPlayer) { GameManager.Instance.SecondaryPlayer.CurrentInputState = PlayerInputState.NoInput; }
            StartCoroutine(HandleDeathSequence());
        }

        private IEnumerator HandleDeathSequence() {
            GameManager.Instance.PauseRaw(true);
            Pixelator.Instance.DoFinalNonFadedLayer = true;
            aiShooter.CurrentGun.gameObject.SetActive(false);
            aiShooter.gunAttachPoint.gameObject.SetActive(false);
            aiShooter.ToggleHandRenderers(false, "Death Sequance");
            int PreviousLayer = gameObject.layer;
            gameObject.SetLayerRecursively(LayerMask.NameToLayer("Unfaded"));
            float elapsed = 0f;
            float duration = 0.8f;
            tk2dBaseSprite spotlightSprite = (Instantiate(BraveResources.Load<GameObject>("DeathShadow", ".prefab"), specRigidbody.UnitCenter, Quaternion.identity)).GetComponent<tk2dBaseSprite>();
            spotlightSprite.spriteAnimator.ignoreTimeScale = true;
            spotlightSprite.spriteAnimator.Play();
            tk2dSpriteAnimator whooshAnimator = spotlightSprite.transform.GetChild(0).GetComponent<tk2dSpriteAnimator>();
            whooshAnimator.ignoreTimeScale = true;
            whooshAnimator.Play();
            Pixelator.Instance.CustomFade(0.6f, 0f, Color.white, Color.black, 0.1f, 0.5f);
            Pixelator.Instance.LerpToLetterbox(0.35f, 0.8f);
            spotlightSprite.color = Color.white;
            yield return StartCoroutine(InvariantWait(0.4f));
            GameObject clockhairObject = Instantiate(BraveResources.Load<GameObject>("Clockhair", ".prefab"));
            ClockhairController clockhair = clockhairObject.GetComponent<ClockhairController>();
            elapsed = 0f;
            duration = clockhair.ClockhairInDuration;
            Vector3 clockhairTargetPosition = sprite.WorldCenter;
            Vector3 clockhairStartPosition = clockhairTargetPosition + new Vector3(-20f, 5f, 0f);
            clockhair.renderer.enabled = false;
            clockhair.spriteAnimator.Play("clockhair_intro");
            clockhair.hourAnimator.Play("hour_hand_intro");
            clockhair.minuteAnimator.Play("minute_hand_intro");
            clockhair.secondAnimator.Play("second_hand_intro");
            bool hasWobbled = false;
            while (elapsed < duration) {
                if (GameManager.INVARIANT_DELTA_TIME == 0f) { elapsed += 0.05f; }
                elapsed += GameManager.INVARIANT_DELTA_TIME;
                float t2 = elapsed / duration;
                float smoothT = Mathf.SmoothStep(0f, 1f, t2);
                Vector3 currentPosition = Vector3.Slerp(clockhairStartPosition, clockhairTargetPosition, smoothT);
                clockhairObject.transform.position = currentPosition.WithZ(0f);
                if (t2 > 0.5f) {
                    clockhair.renderer.enabled = true;
                    clockhair.spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                }
                if (t2 > 0.75f) {
                    clockhair.hourAnimator.GetComponent<Renderer>().enabled = true;
                    clockhair.minuteAnimator.GetComponent<Renderer>().enabled = true;
                    clockhair.secondAnimator.GetComponent<Renderer>().enabled = true;
                    clockhair.hourAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                    clockhair.minuteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                    clockhair.secondAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                }
                if (!hasWobbled && clockhair.spriteAnimator.CurrentFrame == clockhair.spriteAnimator.CurrentClip.frames.Length - 1) {
                    clockhair.spriteAnimator.Play("clockhair_wobble");
                    hasWobbled = true;
                }
                clockhair.sprite.UpdateZDepth();
                spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                yield return null;
            }
            if (!hasWobbled) { clockhair.spriteAnimator.Play("clockhair_wobble"); }
            clockhair.SpinToSessionStart(clockhair.ClockhairSpinDuration);
            elapsed = 0f;
            duration = clockhair.ClockhairSpinDuration + clockhair.ClockhairPauseBeforeShot;
            while (elapsed < duration) {
                if (GameManager.INVARIANT_DELTA_TIME == 0f) { elapsed += 0.05f; }
                elapsed += GameManager.INVARIANT_DELTA_TIME;
                clockhair.spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                yield return null;
            }
            elapsed = 0f;
            duration = 0.1f;
            clockhairStartPosition = clockhairObject.transform.position;
            clockhairTargetPosition = clockhairStartPosition + new Vector3(0f, 12f, 0f);
            clockhair.spriteAnimator.Play("clockhair_fire");
            clockhair.hourAnimator.GetComponent<Renderer>().enabled = false;
            clockhair.minuteAnimator.GetComponent<Renderer>().enabled = false;
            clockhair.secondAnimator.GetComponent<Renderer>().enabled = false;
            //Setup Daeth Animation Here;
            if (m_GungeoneerMimicBossController && m_GungeoneerMimicBossController.m_Player) {
                // aiActor.aiAnimator.enabled = false;
                if (m_GungeoneerMimicBossController.m_Player.UseArmorlessAnim) {
                    spriteAnimator.Play(DeathClipName_Armorless);
                } else {
                    spriteAnimator.Play(DeathClipName);
                }
            }
            while (elapsed < duration) {
                if (GameManager.INVARIANT_DELTA_TIME == 0f) { elapsed += 0.05f; }
                elapsed += GameManager.INVARIANT_DELTA_TIME;
                clockhair.spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);                
                spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                yield return null;
            }
            elapsed = 0f;
            duration = 1f;
            while (elapsed < duration) {
                if (GameManager.INVARIANT_DELTA_TIME == 0f) { elapsed += 0.05f; }
                elapsed += GameManager.INVARIANT_DELTA_TIME;
                if (clockhair.spriteAnimator.CurrentFrame == clockhair.spriteAnimator.CurrentClip.frames.Length - 1) {
                    clockhair.renderer.enabled = false;
                } else {
                    clockhair.spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                }
                spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);                
                yield return null;
            }
            yield return StartCoroutine(InvariantWait(1f));
            Pixelator.Instance.FadeToColor(0.25f, Pixelator.Instance.FadeColor, true);
            Pixelator.Instance.LerpToLetterbox(1f, 0.25f);
            Destroy(spotlightSprite.gameObject);
            Pixelator.Instance.DoFinalNonFadedLayer = false;
            gameObject.SetLayerRecursively(PreviousLayer);
            GameManager.Instance.ForceUnpause();
            GameManager.Instance.PreventPausing = false;
            BraveTime.ClearMultiplier(GameManager.Instance.gameObject);
            yield return new WaitForSeconds(1f);
            GameManager.Instance.PrimaryPlayer.CurrentInputState = PlayerInputState.AllInput;
            if (GameManager.Instance.SecondaryPlayer) { GameManager.Instance.SecondaryPlayer.CurrentInputState = PlayerInputState.AllInput; }
            GameManager.Instance.MainCameraController.SetManualControl(false, true);
            Destroy(clockhairObject);
            m_GungeoneerMimicBossController.ModifyCamera(false);
            healthHaver.DeathAnimationComplete(null, null);
            if (gameObject.GetComponent<ExpandGungeoneerMimicIntroDoer>()) {
                Destroy(gameObject.GetComponent<ExpandGungeoneerMimicIntroDoer>().MirrorBase);
                Destroy(gameObject.GetComponent<ExpandGungeoneerMimicIntroDoer>().MirrorShatterFX);
            }

            yield break;
        }
        
        private IEnumerator InvariantWait(float delay, bool forceAnimationUpdate = true) {
            float elapsed = 0f;
            while (elapsed < delay) {
                if (GameManager.INVARIANT_DELTA_TIME == 0f) { elapsed += 0.05f; }
                elapsed += GameManager.INVARIANT_DELTA_TIME;
                if (forceAnimationUpdate) { spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME); }
                yield return null;
            }
            yield break;
        }
    }
}

