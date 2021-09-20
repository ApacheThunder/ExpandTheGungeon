using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandPrefab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents
{
    public class ExpandWesternBroController : BraveBehaviour
    {
        public WestBros whichBro;

        public string enrageAnim;

        public float enrageAnimTime;

        public GameObject overheadVfx;

        public float postEnrageMoveSpeed;

        public float postSecondEnrageMoveSpeed;

        public float enrageHealToPercent;

        public static List<ExpandWesternBroController> GetOtherWesternBros(AIActor me)
        {
            return GetOtherWesternBros(me.gameObject);
        }

        public static List<ExpandWesternBroController> GetOtherWesternBros(GameObject me)
        {
            bool flag = false;
            List<ExpandWesternBroController> allBros = new List<ExpandWesternBroController>(ExpandStaticReferenceManager.AllWesternBros);

            for (int i = allBros.Count - 1; i >= 0; i--)
            {
                if (allBros[i] && allBros[i].healthHaver && allBros[i].healthHaver.IsAlive)
                {
                    if (me == allBros[i].gameObject)
                    {
                        flag = true;
                        allBros.Remove(allBros[i]);
                    }
                }
                else
                {
                    allBros.Remove(allBros[i]);
                }
            }

            if (!flag)
            {
                Debug.LogWarning("Searched for a western bro, but didn't even find myself (" + me.name + ")", me);
            }

            return allBros;
        }

        public void Awake()
        {
            ExpandStaticReferenceManager.AllWesternBros.Add(this);
        }

        public void Update()
        {
            if (!base.healthHaver.IsDead && this.m_shouldEnrage && base.behaviorSpeculator.IsInterruptable)
            {
                this.m_shouldEnrage = false;
                base.behaviorSpeculator.InterruptAndDisable();
                base.aiActor.ClearPath();
                base.StartCoroutine(this.EnrageCR());
            }

            if (this.m_isEnraged)
            {
                this.m_overheadVfxTimer += BraveTime.DeltaTime;

                if (this.m_overheadVfxInstance && this.m_overheadVfxTimer > 1.5f)
                {
                    this.m_overheadVfxInstance.GetComponent<tk2dSpriteAnimator>().PlayAndDestroyObject("rage_face_vfx_out", null);
                    this.m_overheadVfxInstance = null;
                }

                if (GameManager.Options.ShaderQuality != GameOptions.GenericHighMedLowOption.LOW && GameManager.Options.ShaderQuality != GameOptions.GenericHighMedLowOption.VERY_LOW && base.aiActor && !base.aiActor.IsGone)
                {
                    this.m_particleCounter += BraveTime.DeltaTime * 40f;

                    if (this.m_particleCounter > 1f)
                    {
                        int num = Mathf.FloorToInt(this.m_particleCounter);
                        this.m_particleCounter %= 1f;
                        GlobalSparksDoer.DoRandomParticleBurst(num, base.aiActor.sprite.WorldBottomLeft.ToVector3ZisY(0f), base.aiActor.sprite.WorldTopRight.ToVector3ZisY(0f), Vector3.up, 90f, 0.5f, null, null, null, GlobalSparksDoer.SparksType.BLACK_PHANTOM_SMOKE);
                    }
                }
            }
        }

        protected override void OnDestroy()
        {
            ExpandStaticReferenceManager.AllWesternBros.Remove(this);
            base.OnDestroy();
        }

        public void Enrage()
        {
            this.m_shouldEnrage = true;
        }

        private IEnumerator EnrageCR()
        {
            if (this.healthHaver.GetCurrentHealthPercentage() < this.enrageHealToPercent)
            {
                this.healthHaver.ForceSetCurrentHealth(this.enrageHealToPercent * this.healthHaver.GetMaxHealth());
            }

            bool isSecondEnrage = m_isEnraged;

            this.ProcessAttackGroup(this.behaviorSpeculator.AttackBehaviors, isSecondEnrage);

            this.aiShooter.ToggleGunAndHandRenderers(false, "BroController");

            this.aiAnimator.PlayUntilFinished(this.enrageAnim, true, null, -1f, false);

            float timer = 0f;

            this.m_isEnraged = false;

            while (timer < this.enrageAnimTime)
            {
                yield return null;

                timer += BraveTime.DeltaTime;

                if (!this.m_isEnraged && timer / this.enrageAnimTime >= 0.25f)
                {
                    if (this.overheadVfx)
                    {
                        this.m_overheadVfxInstance = this.aiActor.PlayEffectOnActor(this.overheadVfx, new Vector3(0f, 1.375f, 0f), true, true, false);
                        this.m_overheadVfxTimer = 0f;
                    }

                    this.m_isEnraged = true;
                }
            }

            this.aiAnimator.EndAnimationIf(this.enrageAnim);
            this.aiShooter.ToggleGunAndHandRenderers(true, "BroController");

            var moveSpeedToUse = isSecondEnrage ? postSecondEnrageMoveSpeed : postEnrageMoveSpeed;

            if (moveSpeedToUse >= 0f)
            {
                this.aiActor.MovementSpeed = TurboModeController.MaybeModifyEnemyMovementSpeed(moveSpeedToUse);
            }

            this.behaviorSpeculator.enabled = true;

            yield break;
        }

        // TODO a lot to do here, second enrage, probabilities etc
        private void ProcessAttackGroup(List<AttackBehaviorBase> attackBehaviors, bool isSecondEnrage)
        {
            AttackBehaviorGroup group = attackBehaviors[0] as AttackBehaviorGroup;

            foreach (var attackBehaviour in group.AttackBehaviors)
            {
                if (attackBehaviour.Behavior is ShootGunBehavior)
                {
                    (attackBehaviour.Behavior as ShootGunBehavior).StopDuringAttack = false;
                }

                switch (attackBehaviour.NickName)
                {
                    // non enraged basic shooting
                    case "Basic Shooting":
                        attackBehaviour.Probability = 0f;
                        break;
                    // enraged basic shooting
                    case "Basic Shooting (Angry)":
                        attackBehaviour.Probability = 1f;
                        break;
                    // as far as I can see this is never used, but it's still set to 0 on enrage
                    case "Leading Shot":
                        attackBehaviour.Probability = 0f;
                        break;
                    // non enraged jump attack
                    case "Jump Attack":
                        attackBehaviour.Probability = 0f;
                        break;
                    // enraged jump attack
                    case "Jump Attack (Angry)":
                        attackBehaviour.Probability = 1f;
                        break;
                    // spawn adds attack
                    case "Spawn Bros":
                        //attackBehaviour.Probability = 0f; // previously enraged bros couldn't summon, but I want that, maybe even increase it
                        break;

                    default:
                        break;
                }

                // unchanged attacks:
                // Charge Player
                // Trident Shot
                // Sweep Shot
                // smiley's sweep and shades' trident attack are never set to something other than probability of 1, so I'm ignorig them here
            }
        }

        private bool m_shouldEnrage;

        private bool m_isEnraged;

        private GameObject m_overheadVfxInstance;

        private float m_overheadVfxTimer;

        private float m_particleCounter;
    }
}