using Dungeonator;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandNPCController : DungeonPlaceableBehaviour, IPlayerInteractable, IPlaceConfigurable {

        public ExpandNPCController() {
            mode = Mode.CultistNPC;

            textBoxOffset = new Vector3(-0.9375f, 1.375f);

            DefaultDialog = new List<string>() {
                "Hello fellow follower of the gun!\nPlease behold our ultimate treasure! You are finally worthy to obtain it!",
                "It's not often we see anyone down here!\nIn that chest is a very special gun. We couldn't decide who to use it on to escape. Maybe you can have it?",
                "Hello there!\nPlease open that chest! It's totally safe! We promise!",
                "I'll be straight with you. We don't know what it does either!"
            };

            OtherDialog = new List<string>() {
                "Yes yes! I'm sure you are familiar with this gun! Please use it!",
                "If you have any friends with you, you are free to use it on them instead!",
                "We've lost all desire to leave this place. Please use it. We don't need it anymore!",
                "It's harmless! We promise!",
                "Be careful where you point that!\nWhy? ... No reason! It's safe to use! We tested it to make sure!"
            };

            OtherDialog2 = new List<string>() {
                "Oh I see you've chosen to use it on your friend! Don't worry. He's in a better place now!",
                "No he's not dead! You just killed his past! That's just a ... time corpse ... yeah that's what it is!",
                "Don't look at me! We didn't know how that gun works It's why we chose not to use it!",
                "You just killed his past! Oh how envious we are!. Just leave his ... time corpse ... behind. We'll dispose of it for you!"
            };

            OtherDialog3 = new List<string>() {
                "You survived? Well that gun was just a pale imitation of the real thing anyways.",
                "You should be d... It didn't work?\nWell maybe you didn't aim it correctly?",
                "Oh you're still alive?\nNo we didn't intend for it to kill you! We promise!",
                "Don't look at me. I think Jeff over there might have put the wrong ammunition in it! It's his fault!"
            };

            OtherDialog4 = new List<string>() {
                "Haha you fool! You shall be the sacrifice to Kaliber we need to finally escape!",
                "Your corpse wont be wasted! We will finally escape this accursed place!",
                "Oh your dead? That's too bad! Anyways, time to start the ritual!"
            };

            DeathDialog = new List<string>() {
                "Why!",
                "How did you know?",
                "It was meant to be you! Not me!",
                "You weren't supposed to use that on me!",
                "You shot me! We will have our revenge!"
            };

            TalkPointChild = "talkPoint";
            ShadowSprite = "DefaultShadowSprite";

            IdleAnimation = "idle";
            InteractAnimation = "interact";
            DeathAnimation = "cardboard_die";

            cultistDialogState = CultistDialogState.Default;

            animationState = AnimationState.Idle;
            
            KillNPC = false;
            
            m_Interacted = false;
            m_IsDead = false;
            m_Configured = false;
        }

        
        public List<string> DefaultDialog;
        public List<string> OtherDialog;
        public List<string> OtherDialog2;
        public List<string> OtherDialog3;
        public List<string> OtherDialog4;
        public List<string> DeathDialog;

        public string TalkPointChild;
        public string ShadowSprite;
        
        public string IdleAnimation;
        public string InteractAnimation;
        public string DeathAnimation;

        public Vector3 textBoxOffset;
        
        public enum Mode { CultistNPC, Other };
        public Mode mode;

        [NonSerialized]
        public bool KillNPC;
        
        public enum CultistDialogState { Default, OpenedChest, PlayerShot, PlayerSurvived, ShotCompanion, CultistShot };

        public CultistDialogState cultistDialogState;

        private enum AnimationState { SwitchingAnimation, Interacting, Idle, Dead };
        [NonSerialized]
        private AnimationState animationState;

        [NonSerialized]
        private bool m_Interacted;
        
        [NonSerialized]
        private RoomHandler m_ParentRoom;

        /*[NonSerialized]
        private PlayerController m_CurrentPlayer;*/

        [NonSerialized]
        private string m_NewAnimation;

        [NonSerialized]
        private string m_CurrentAnimation;

        [NonSerialized]
        private bool m_IsDead;

        [NonSerialized]
        private float m_DeathTimer;

        [NonSerialized]
        private Transform m_TalkPoint;

        [NonSerialized]
        private tk2dSprite m_ShadowSprite;
                
        [NonSerialized]
        private bool m_Configured;



        /*private void Awake() {
            switch (mode) {
                default:
                    return;
                case Mode.CultistNPC:
                    return;
            }

        }*/

        /*private void AnimationEventTriggered(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frame) {
            if (clip.GetFrame(frame).eventInfo == "EventExample") {
               switch (mode) {
                    default:
                        return;
                    case Mode.CultistNPC:
                        
                        return;
                }
            }
        }*/

        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;
            switch (mode) {
                case Mode.CultistNPC:
                    IdleAnimation = spriteAnimator?.CurrentClip?.name;
                    animationState = AnimationState.Idle;
                    if (gameObject.GetComponent<TalkDoerLite>()) {
                        m_ParentRoom = room;
                        if (room != null) {
                            room.DeregisterInteractable(gameObject.GetComponent<TalkDoerLite>());
                            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
                        }
                        Destroy(gameObject.GetComponent<TalkDoerLite>());
                        if (gameObject.GetComponent<PlayMakerFSM>()) Destroy(gameObject.GetComponent<PlayMakerFSM>());
                        if (room != null) {
                            room.RegisterInteractable(this);
                            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.black);
                        }
                        if (gameObject.transform.Find(TalkPointChild)) m_TalkPoint = gameObject.transform.Find(TalkPointChild);
                        if (gameObject.transform.Find(ShadowSprite) && gameObject.transform.Find(ShadowSprite).GetComponent<tk2dSprite>()) {
                            m_ShadowSprite = gameObject.transform.Find(ShadowSprite).GetComponent<tk2dSprite>();
                            m_ShadowSprite.gameObject.SetLayerRecursively(19);
                            Vector3 m_NewPosition = new Vector3(m_ShadowSprite.gameObject.transform.localPosition.x, m_ShadowSprite.gameObject.transform.localPosition.y, 0.273578f);
                            m_ShadowSprite.gameObject.transform.localPosition = m_NewPosition;
                            m_ShadowSprite.gameObject.transform.localPosition -= new Vector3(0.24f, 0.2f, 0);
                            m_ShadowSprite.enabled = true;
                            m_ShadowSprite.renderer.enabled = true;
                            /*m_ShadowSprite.HeightOffGround = -1.6f;
                            m_ShadowSprite.UpdateZDepth();*/
                        }
                    }
                    break;
                case Mode.Other:
                    break;
            }
            m_Configured = true;
        }

        public void Kill() {
            TextBoxManager.ClearTextBox(transform);
            DeathDialog = DeathDialog.Shuffle();
            cultistDialogState = CultistDialogState.CultistShot;
            m_DeathTimer = 3;
            if (m_ParentRoom != null)m_ParentRoom.DeregisterInteractable(this);
            if (sprite) SpriteOutlineManager.RemoveOutlineFromSprite(sprite);
            if (m_ShadowSprite) m_ShadowSprite.renderer.enabled = false;
            if (specRigidbody) Destroy(specRigidbody);
            if (aiAnimator) aiAnimator.enabled = false;
            StartCoroutine(HandleDialogPlayerless());
        }
        
        public void InteractPlayerless(CultistDialogState DialogState = CultistDialogState.Default) {
            if (DialogState == CultistDialogState.Default | DialogState == CultistDialogState.PlayerSurvived |
                DialogState == CultistDialogState.ShotCompanion)
            {
                m_Interacted = true;
            }
            TextBoxManager.ClearTextBox(transform);
            DefaultDialog = DefaultDialog.Shuffle();
            OtherDialog = OtherDialog.Shuffle();
            OtherDialog2 = OtherDialog2.Shuffle();
            OtherDialog3 = OtherDialog3.Shuffle();
            OtherDialog4 = OtherDialog4.Shuffle();
            DeathDialog = DeathDialog.Shuffle();
            switch (mode) {
                case Mode.CultistNPC:
                    cultistDialogState = DialogState;
                    StartCoroutine(HandleDialogPlayerless());
                    break;
                case Mode.Other:
                    m_Interacted = false;
                    break;
                default:
                    m_Interacted = false;
                    break;
            }
        }

        public void Interact(PlayerController interactor) {
            DefaultDialog = DefaultDialog.Shuffle();
            OtherDialog = OtherDialog.Shuffle();
            OtherDialog2 = OtherDialog2.Shuffle();
            OtherDialog3 = OtherDialog3.Shuffle();
            OtherDialog4 = OtherDialog4.Shuffle();
            DeathDialog = DeathDialog.Shuffle();
            TextBoxManager.ClearTextBox(transform);
            if (!m_Interacted) {
                // m_CurrentPlayer = interactor;
                m_Interacted = true;
                switch (mode) {
                    case Mode.CultistNPC:
                        StartCoroutine(HandleDialog(interactor));
                        break;
                    case Mode.Other:
                        /*if (!string.IsNullOrEmpty(InteractAnimation) && animationState != AnimationState.Dead) {
                            m_NewAnimation = InteractAnimation;
                            animationState = AnimationState.SwitchingAnimation;
                        }*/
                        StartCoroutine(HandleDialog(interactor));
                        break;
                    default:
                        StartCoroutine(HandleDialog(interactor));
                        break;
                }
            }
        }

        private IEnumerator HandleDialog(PlayerController interactor) {
            if (!interactor)yield break;
            switch (mode) {
                case Mode.CultistNPC:
                    string m_ChosenText = BraveUtility.RandomElement(DefaultDialog);
                    if (cultistDialogState == CultistDialogState.OpenedChest) {
                        m_ChosenText = BraveUtility.RandomElement(OtherDialog);
                    } else if (cultistDialogState == CultistDialogState.PlayerSurvived) {
                        m_ChosenText = BraveUtility.RandomElement(OtherDialog3);
                    } else if (cultistDialogState == CultistDialogState.PlayerShot) {
                        m_ChosenText = BraveUtility.RandomElement(OtherDialog4);
                    } else if (cultistDialogState == CultistDialogState.ShotCompanion) {
                        m_ChosenText = BraveUtility.RandomElement(OtherDialog2);
                    } else if (cultistDialogState == CultistDialogState.CultistShot) {
                        m_ChosenText = BraveUtility.RandomElement(DeathDialog);
                    }
                    if (m_TalkPoint) {
                        TextBoxManager.ShowTextBox(m_TalkPoint.position, transform, 5f, m_ChosenText, instant: false, slideOrientation: TextBoxManager.BoxSlideOrientation.FORCE_RIGHT, showContinueText: false);
                    } else {
                        TextBoxManager.ShowTextBox(transform.position + textBoxOffset, transform, 5f, m_ChosenText, instant: false, slideOrientation: TextBoxManager.BoxSlideOrientation.FORCE_RIGHT, showContinueText: false);
                    }
                    yield return new WaitForSeconds(0.5f);
                    m_Interacted = false;
                    yield break;                
                case Mode.Other:
                    yield break;
                default:
                    yield break;
            }
        }

        private IEnumerator HandleDialogPlayerless() {
            TextBoxManager.ClearTextBox(transform);
            if (cultistDialogState == CultistDialogState.CultistShot) {
                animationState = AnimationState.Dead;
                while (!spriteAnimator.IsPlaying(DeathAnimation))yield return null;
                while (spriteAnimator.IsPlaying(DeathAnimation))yield return null;
            }
            switch (mode) {
                case Mode.CultistNPC:
                    string m_ChosenText = BraveUtility.RandomElement(DefaultDialog);
                    if (cultistDialogState == CultistDialogState.OpenedChest) {
                        m_ChosenText = BraveUtility.RandomElement(OtherDialog);
                    } else if (cultistDialogState == CultistDialogState.PlayerSurvived) {
                        m_ChosenText = BraveUtility.RandomElement(OtherDialog3);
                    } else if (cultistDialogState == CultistDialogState.PlayerShot) {
                        m_ChosenText = BraveUtility.RandomElement(OtherDialog4);
                    } else if (cultistDialogState == CultistDialogState.ShotCompanion) {
                        m_ChosenText = BraveUtility.RandomElement(OtherDialog2);
                    } else if (cultistDialogState == CultistDialogState.CultistShot) {
                        m_ChosenText = BraveUtility.RandomElement(DeathDialog);
                    }
                    if (m_TalkPoint) {
                        TextBoxManager.ShowTextBox(m_TalkPoint.position, transform, 5, m_ChosenText, instant: false, slideOrientation: TextBoxManager.BoxSlideOrientation.FORCE_RIGHT, showContinueText: false);
                    } else {
                        TextBoxManager.ShowTextBox(transform.position + textBoxOffset, transform, 5, m_ChosenText, instant: false, slideOrientation: TextBoxManager.BoxSlideOrientation.FORCE_RIGHT, showContinueText: false);
                    }
                    if (cultistDialogState == CultistDialogState.Default | cultistDialogState == CultistDialogState.PlayerSurvived |
                        cultistDialogState == CultistDialogState.ShotCompanion) {
                        m_Interacted = false;
                    }
                    yield break;
                case Mode.Other:
                    yield break;
                default:
                    yield break;
            }
        }


        private void LateUpdate() {
            if (!m_Configured) return;
            switch (animationState) {
                case AnimationState.SwitchingAnimation:
                    if (!string.IsNullOrEmpty(m_NewAnimation)) {
                        m_CurrentAnimation = m_NewAnimation;
                        spriteAnimator.Play(m_CurrentAnimation);
                        m_NewAnimation = string.Empty;
                        return;
                    }
                    if (!string.IsNullOrEmpty(m_CurrentAnimation) && !spriteAnimator.IsPlaying(m_CurrentAnimation)) {
                        m_CurrentAnimation = string.Empty;
                        animationState = AnimationState.Idle;
                    }
                    return;
                case AnimationState.Idle:
                    if (!spriteAnimator.IsPlaying(IdleAnimation))spriteAnimator.Play(IdleAnimation);
                    return;
                case AnimationState.Dead:
                    if (!m_IsDead && !string.IsNullOrEmpty(DeathAnimation) && !spriteAnimator.IsPlaying(DeathAnimation)) {
                        spriteAnimator.Play(DeathAnimation);
                        m_IsDead = true;
                    }
                    return;
                default:
                    return;
            }
        }

        private void Update() {
            if (!m_Configured) return;
            switch (mode) {
                default:
                    return;
                case Mode.CultistNPC:
                    if (m_IsDead && !m_Interacted && animationState == AnimationState.Dead) {
                        if (spriteAnimator.IsPlaying(DeathAnimation)) return;
                        m_DeathTimer -= BraveTime.DeltaTime;
                        if (m_DeathTimer <= 0) {
                            Gun m_CachedMutantArmGun = (PickupObjectDatabase.GetById(333) as Gun);
                            if (m_CachedMutantArmGun?.singleModule?.projectiles[0]?.gameObject.GetComponent<GoopModifier>()?.goopDefinition) {
                                GameObject m_GoopInstance = new GameObject("Cultist Bloodstain", typeof(tk2dSprite)) { layer = 0 };
                                m_GoopInstance.transform.position = transform.position;
                                m_GoopInstance.transform.SetParent(gameObject.transform);
                                m_GoopInstance.transform.localPosition = new Vector3(-1.4f, -0.2f);
                                tk2dSprite m_Sprite = m_GoopInstance.GetComponent<tk2dSprite>();
                                if (m_Sprite) {
                                    m_Sprite.Collection = m_CachedMutantArmGun.sprite.Collection;
                                    m_Sprite.SetSprite(0);
                                    m_Sprite.renderer.enabled = false;
                                    GoopDoer m_Goop = m_GoopInstance.AddComponent<GoopDoer>();
                                    m_Goop.goopDefinition = m_CachedMutantArmGun.singleModule.projectiles[0].gameObject.GetComponent<GoopModifier>().goopDefinition;
                                    m_Goop.positionSource = GoopDoer.PositionSource.SpriteCenter;
                                    m_Goop.updateTiming = GoopDoer.UpdateTiming.Always;
                                    m_Goop.updateFrequency = 0.05f;
                                    m_Goop.isTimed = false;
                                    m_Goop.goopTime = 1;
                                    m_Goop.updateOnPreDeath = true;
                                    m_Goop.updateOnDeath = false;
                                    m_Goop.updateOnAnimFrames = true;
                                    m_Goop.updateOnCollision = false;
                                    m_Goop.updateOnGrounded = false;
                                    m_Goop.updateOnDestroy = false;
                                    m_Goop.defaultGoopRadius = 1;
                                    m_Goop.suppressSplashes = false;
                                    m_Goop.goopSizeVaries = true;
                                    m_Goop.varyCycleTime = 0.9f;
                                    m_Goop.radiusMin = UnityEngine.Random.Range(0.4f, 0.8f);
                                    m_Goop.radiusMax = UnityEngine.Random.Range(0.8f, 1);
                                    m_Goop.goopSizeRandom = true;
                                    m_Goop.UsesDispersalParticles = false;
                                    m_Goop.DispersalDensity = 3;
                                    m_Goop.DispersalMinCoherency = 0.2f;
                                    m_Goop.DispersalMaxCoherency = 1;
                                }
                            }
                            TextBoxManager.ClearTextBox(transform);
                            PickupObject.ItemQuality targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.A : PickupObject.ItemQuality.B) : PickupObject.ItemQuality.S;
                            GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                            PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                            if (item) {
                                List<int> m_AllowedItemsInRainbowMode = new List<int>() {
                                    GlobalItemIds.SmallHeart,
                                    GlobalItemIds.FullHeart,
                                    GlobalItemIds.AmmoPickup,
                                    GlobalItemIds.SpreadAmmoPickup,
                                    GlobalItemIds.Spice,
                                    GlobalItemIds.Junk,
                                    GlobalItemIds.GoldJunk,
                                    GlobalItemIds.Key,
                                    GlobalItemIds.GlassGuonStone,
                                    GlobalItemIds.Junk,
                                    GlobalItemIds.GoldJunk,
                                    GlobalItemIds.SackKnightBoon,
                                    GlobalItemIds.Blank,
                                    GlobalItemIds.Map,
                                    120 // armor
                                };
                                if (!GameStatsManager.Instance.IsRainbowRun | m_AllowedItemsInRainbowMode.Contains(item.PickupObjectId)) {
                                    LootEngine.SpawnItem(item.gameObject, sprite.WorldCenter, Vector2.zero, 0f, true, true, false);
                                } else {
                                    if (m_ParentRoom != null && GameManager.Instance.RewardManager.BowlerNoteOtherSource) {
                                        string CustomText = "Corpses aren't {wb}Rainbow Chests{w}!\n\nNo RAAAAAIIIINBOW, no item!\n\n{wb}-Bowler{w}";
                                        ExpandUtility.SpawnCustomBowlerNote(GameManager.Instance.RewardManager.BowlerNoteOtherSource, sprite.WorldCenter, m_ParentRoom, CustomText, false);
                                    }
                                }
                            }
                            m_Interacted = true;
                            Destroy(this);
                            return;
                        }
                    }
                    return;
                case Mode.Other:
                    return;
            }
        }
        
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }
        

        public float GetDistanceToPoint(Vector2 point) {
            if (!sprite)return 1000f;
            Vector3 v = BraveMathCollege.ClosestPointOnRectangle(point, specRigidbody.UnitBottomLeft, specRigidbody.UnitDimensions);
            return Vector2.Distance(point, v) / 1.5f;
        }

        public float GetOverrideMaxDistance() { return -1f; }
        
        public void OnEnteredRange(PlayerController interactor) {
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite);
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white);
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.black);
            sprite.UpdateZDepth();
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

