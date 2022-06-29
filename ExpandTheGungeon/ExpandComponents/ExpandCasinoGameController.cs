using Dungeonator;
using MonoMod.RuntimeDetour;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandCasinoGameController : DungeonPlaceableBehaviour, IPlayerInteractable, IPlaceConfigurable {

        public ExpandCasinoGameController() {
            GameRewardOnAnimationEvent = false;
            GameRewardAnimationEventName = "itempop";
            MaxUses = 1;
            Cost = 100;
            mode = Mode.PunchoutArcade;
            textBoxOffset = new Vector3(1, 3.8f);
            displayTextKey = "It's a punchout arcade game featuring the Resourceful Rat.";
            disabledDisplayTextKey = "It's a punchout arcade game featuring the Resourceful Rat.\nIt appears to be out of order however.";
            lockedDisplayTextKey = "There is something under the covers.\nIt doesn't appear to be ready yet...";
            acceptOptionKey = "Spend 100 Hegemony Credits and play the game.";
            declineOptionKey = "You got better things to spend money on and decide to leave.";
            insufficientFundsDeclineOptionKey = "Too broke to play this game so leaving is the only option.";
            insufficientFundsKey = "It's a punchout arcade game featuring the Resourceful Rat.\nIt costs 100 Hegemony to play but you lack the funds!";
            basicLeaveKey = "Leave...";

            IdleAnimation = "idle";
            InteractAnimation = "interact";
            ActivateGameAnimation = "fight";
            SleepAnimation = "sleep";
            SleepFrame = string.Empty;
            WonAnimation = string.Empty;
            LostAnimation = string.Empty;
            LockedSpriteFrame = "cabinet_covered_001";
            LockedAnimation = string.Empty;

            animationState = AnimationState.Idle;

            DoingResults = false;
            ResultsGiven = false;
            Finished = false;

            m_Interacted = false;
            m_Uses = 0;
        }

        public bool GameRewardOnAnimationEvent;
        public string GameRewardAnimationEventName;

        public string displayTextKey;
        public string disabledDisplayTextKey;
        public string lockedDisplayTextKey;
        public string acceptOptionKey;
        public string declineOptionKey;
        public string insufficientFundsDeclineOptionKey;
        public string insufficientFundsKey;
        public string basicLeaveKey;

        public string IdleAnimation;
        public string InteractAnimation;
        public string ActivateGameAnimation;
        public string LockedAnimation;
        public string SleepAnimation;
        public string SleepFrame;
        public string WonAnimation;
        public string LostAnimation;
        public string LockedSpriteFrame;

        public Vector3 textBoxOffset;

        public int MaxUses;
        public int Cost;


        public enum Mode { LockedGame, PunchoutArcade, SlotMachine, ItemGunBallMachine };
        public Mode mode;
        
        [NonSerialized]
        public bool DoingResults;
        [NonSerialized]
        public bool ResultsGiven;
        [NonSerialized]
        public bool Finished;

        private enum AnimationState { SwitchingAnimation, Idle, Sleep, Locked };
        [NonSerialized]
        private AnimationState animationState;

        [NonSerialized]
        private bool m_Interacted;
        
        [NonSerialized]
        private int m_Uses;

        [NonSerialized]
        private string m_NewAnimation;
        [NonSerialized]
        private string m_CurrentAnimation;
        
        [NonSerialized]
        private RoomHandler m_ParentRoom;

        [NonSerialized]
        private ExpandPunchoutArcadeController m_PunchoutArcadeController;

        [NonSerialized]
        private PlayerController m_CurrentPlayer;

        private void Awake() {
            switch (mode) {
                default:
                    return;
                case Mode.ItemGunBallMachine:
                    spriteAnimator.AnimationEventTriggered = (Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>)Delegate.Combine(spriteAnimator.AnimationEventTriggered, new Action<tk2dSpriteAnimator, tk2dSpriteAnimationClip, int>(AnimationEventTriggered));
                    return;
            }
        }

        private void AnimationEventTriggered(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frame) {
            if (clip.GetFrame(frame).eventInfo == GameRewardAnimationEventName) {
               switch (mode) {
                    default:
                        return;
                    case Mode.ItemGunBallMachine:
                        DoGunBallRoll(m_CurrentPlayer);
                        return;
                }
            }
        }

        public void Interact(PlayerController interactor) {
            if (!m_Interacted) {
                m_CurrentPlayer = interactor;
                m_Interacted = true;
                switch (mode) {
                    case Mode.LockedGame:
                        StartCoroutine(HandleConversation(interactor));
                        break;
                    case Mode.PunchoutArcade:
                        if (!string.IsNullOrEmpty(InteractAnimation) && animationState != AnimationState.Sleep && animationState != AnimationState.Locked) {
                            m_NewAnimation = InteractAnimation;
                            animationState = AnimationState.SwitchingAnimation;
                        }
                        StartCoroutine(HandleConversation(interactor));
                        break;
                    case Mode.ItemGunBallMachine:
                        if (!string.IsNullOrEmpty(InteractAnimation) && animationState != AnimationState.Sleep && animationState != AnimationState.Locked) {
                            m_NewAnimation = InteractAnimation;
                            animationState = AnimationState.SwitchingAnimation;
                        }
                        StartCoroutine(HandleConversation(interactor));
                        break;
                    default:
                        StartCoroutine(HandleConversation(interactor, "MODE NOT IMPLEMENTED YET"));
                        break;
                }
            }
        }

        private IEnumerator HandleConversation(PlayerController interactor, string ResultsStringKey = null) {
            if (!interactor) { yield break; }

            string targetDisplayKey = displayTextKey;
            bool IsGameOverConversation = false;
            
            if (!string.IsNullOrEmpty(ResultsStringKey)) {
                IsGameOverConversation = true;
                targetDisplayKey = ResultsStringKey;
            }

            int selectedResponse = -1;

            switch (mode) {
                case Mode.LockedGame:
                    targetDisplayKey = lockedDisplayTextKey;
                    TextBoxManager.ShowTextBox(interactor.transform.position + textBoxOffset, interactor.transform, -1f, targetDisplayKey, instant: true, slideOrientation: TextBoxManager.BoxSlideOrientation.FORCE_RIGHT, showContinueText: false);
                    selectedResponse = -1;
                    interactor.SetInputOverride("gameConversation");
                    GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, basicLeaveKey, string.Empty);
                    yield return null;
                    while (!GameUIRoot.Instance.GetPlayerConversationResponse(out selectedResponse)) { yield return null; }
                    TextBoxManager.ClearTextBox(interactor.transform);
                    m_Interacted = false;
                    yield return null;
                    interactor.ClearInputOverride("gameConversation");
                    yield break;
                case Mode.PunchoutArcade:
                    bool IsBroke = false;
                    if (GameStatsManager.Instance.GetPlayerStatValue(TrackedStats.META_CURRENCY) < Cost) {
                        targetDisplayKey = insufficientFundsKey;
                        IsBroke = true;
                    }
                    if (m_Uses >= MaxUses && !IsGameOverConversation) { targetDisplayKey = disabledDisplayTextKey; }
                    TextBoxManager.ShowTextBox(interactor.transform.position + textBoxOffset, interactor.transform, -1f, targetDisplayKey, instant: true, slideOrientation: TextBoxManager.BoxSlideOrientation.FORCE_RIGHT, showContinueText: false);
                    selectedResponse = -1;
                    interactor.SetInputOverride("gameConversation");
                    if (m_Uses < MaxUses && GameStatsManager.Instance.GetPlayerStatValue(TrackedStats.META_CURRENCY) >= Cost) {
                        GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, acceptOptionKey, declineOptionKey);
                    } else {
                        if (m_Uses >= MaxUses && !IsGameOverConversation) {
                            GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, basicLeaveKey, string.Empty);
                        }
                        if (IsBroke | IsGameOverConversation) {
                            string TextKey = insufficientFundsDeclineOptionKey;
                            if (IsGameOverConversation) {
                                TextKey = basicLeaveKey;
                                if (ExpandPunchoutArcadeController.WonRatGame) {
                                    if (!string.IsNullOrEmpty(WonAnimation)) {
                                        m_NewAnimation = WonAnimation;
                                        animationState = AnimationState.SwitchingAnimation;
                                    }
                                } else {
                                    if (!string.IsNullOrEmpty(LostAnimation)) {
                                        m_NewAnimation = LostAnimation;
                                        animationState = AnimationState.SwitchingAnimation;
                                    }
                                }
                            }
                            GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, TextKey, string.Empty);
                        } else if (IsBroke) {
                            GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, declineOptionKey, string.Empty);
                        }
                    }
                    yield return null;
                    while (!GameUIRoot.Instance.GetPlayerConversationResponse(out selectedResponse)) { yield return null; }
                    TextBoxManager.ClearTextBox(interactor.transform);
                    if (m_Uses < MaxUses && selectedResponse == 0 && !IsBroke && !IsGameOverConversation) {
                        if (!string.IsNullOrEmpty(ActivateGameAnimation)) {
                            m_NewAnimation = ActivateGameAnimation;
                            animationState = AnimationState.SwitchingAnimation;
                        }
                        while (animationState != AnimationState.Idle) { yield return null; }
                        GameStatsManager.Instance.RegisterStatChange(TrackedStats.META_CURRENCY, -Cost);
                        Pixelator.Instance.FadeToColor(1f, Color.black, false, 0f);
                        yield return new WaitForSeconds(1.5f);
                        DoPunchoutGame(interactor);
                        m_Uses++;
                    } else {
                        if (IsGameOverConversation && targetDisplayKey != "MODE NOT IMPLEMENTED YET") {
                            if (ExpandPunchoutArcadeController.WonRatGame && m_PunchoutArcadeController) {
                                m_PunchoutArcadeController.MaybeGiveRewards(0.5f);
                            } else if (!ExpandPunchoutArcadeController.WonRatGame) {
                                AkSoundEngine.PostEvent("Play_OBJ_metronome_fail_01", gameObject);
                            }
                            yield return new WaitForSeconds(0.2f);
                            ResultsGiven = true;
                            GameManager.Instance.DungeonMusicController.EndVictoryMusic();
                            animationState = AnimationState.Sleep;
                        } else {
                            interactor.ClearInputOverride("gameConversation");
                        }
                        ExpandSettings.PlayingPunchoutArcade = false;
                        m_Interacted = false;
                    }
                    yield break;
                case Mode.SlotMachine:
                    yield break;
                case Mode.ItemGunBallMachine:
                    bool IsBroke_Gumball = false;
                    if (GameStatsManager.Instance.GetPlayerStatValue(TrackedStats.META_CURRENCY) < Cost) {
                        targetDisplayKey = insufficientFundsKey;
                        IsBroke_Gumball = true;
                    }
                    if (m_Uses >= MaxUses) { targetDisplayKey = disabledDisplayTextKey; }
                    TextBoxManager.ShowTextBox(interactor.transform.position + textBoxOffset, interactor.transform, -1f, targetDisplayKey, instant: true, slideOrientation: TextBoxManager.BoxSlideOrientation.FORCE_RIGHT, showContinueText: false);
                    selectedResponse = -1;
                    interactor.SetInputOverride("gameConversation");
                    if (m_Uses < MaxUses && !IsBroke_Gumball) {
                        GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, acceptOptionKey, declineOptionKey);
                    } else if (m_Uses >= MaxUses | IsBroke_Gumball) {
                        GameUIRoot.Instance.DisplayPlayerConversationOptions(interactor, null, basicLeaveKey, string.Empty);
                    }
                    yield return null;
                    while (!GameUIRoot.Instance.GetPlayerConversationResponse(out selectedResponse)) { yield return null; }
                    TextBoxManager.ClearTextBox(interactor.transform);
                    if (m_Uses < MaxUses && selectedResponse == 0 && !IsBroke_Gumball) {
                        GameStatsManager.Instance.RegisterStatChange(TrackedStats.META_CURRENCY, -Cost);
                        if (!string.IsNullOrEmpty(ActivateGameAnimation)) {
                            m_NewAnimation = ActivateGameAnimation;
                            animationState = AnimationState.SwitchingAnimation;
                        }
                        AkSoundEngine.PostEvent("Play_NPC_gunball_dispense_01", gameObject);
                        while (animationState != AnimationState.Idle) { yield return null; }
                        m_Uses++;
                        yield return null;
                        if (m_Uses >= MaxUses) { animationState = AnimationState.Sleep; }
                    }
                    interactor.ClearInputOverride("gameConversation");
                    m_Interacted = false;
                    yield break;
                default:
                    yield break;
            }
        }

        private void DoPunchoutGame(PlayerController player) {
            if (m_PunchoutArcadeController) {
                Destroy(m_PunchoutArcadeController.gameObject);
                m_PunchoutArcadeController = null;
            }
            GameObject arcadeObject = new GameObject("EX Punchout Arcade Game", new Type[] { typeof(ExpandPunchoutArcadeController) });
            arcadeObject.transform.position = player.transform.position;
            m_PunchoutArcadeController = arcadeObject.GetComponent<ExpandPunchoutArcadeController>();
            m_PunchoutArcadeController.ParentGameController = this;
            m_PunchoutArcadeController.StartPunchout(player);
        }

        private void DoGunBallRoll(PlayerController player) {
            if (UnityEngine.Random.value < 0.6f) {
                PickupObject.ItemQuality targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.C : PickupObject.ItemQuality.D) : PickupObject.ItemQuality.B;
                if (UnityEngine.Random.value < 0.2f) {
                    targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.B : PickupObject.ItemQuality.C) : PickupObject.ItemQuality.A;
                } else if (UnityEngine.Random.value < 0.01f) {
                    targetQuality = PickupObject.ItemQuality.S;
                }
                GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                PickupObject ItemOrGun = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                LootEngine.GivePrefabToPlayer(ItemOrGun.gameObject, player);
                if (ItemOrGun is Gun){
                    AkSoundEngine.PostEvent("Play_OBJ_weapon_pickup_01", gameObject);
                } else {
                    AkSoundEngine.PostEvent("Play_OBJ_item_pickup_01", gameObject);
                }
                GameObject original = (GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Pickup");
                GameObject gameOBJ = Instantiate(original);
                tk2dSprite component = gameOBJ.GetComponent<tk2dSprite>();
                component.PlaceAtPositionByAnchor(player.sprite.WorldCenter, tk2dBaseSprite.Anchor.MiddleCenter);
                component.HeightOffGround = -1.5f;
                component.UpdateZDepth();
            } else {
                KeyBulletPickup keyBullet = (KeyBulletPickup)PickupObjectDatabase.GetById(67);
                player.BloopItemAboveHead(keyBullet.sprite, keyBullet.overrideBloopSpriteName);
                GameObject gameOBJ2 = Instantiate((GameObject)ResourceCache.Acquire("Global VFX/VFX_Item_Pickup"));
                tk2dSprite component2 = gameOBJ2.GetComponent<tk2dSprite>();
                component2.PlaceAtPositionByAnchor(player.sprite.WorldCenter, tk2dBaseSprite.Anchor.MiddleCenter);
                component2.UpdateZDepth();
                AkSoundEngine.PostEvent("Play_OBJ_key_pickup_01", gameObject);
                player.carriedConsumables.KeyBullets += 1;
                player.carriedConsumables.ForceUpdateUI();
            }
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            switch (mode) {
                case Mode.PunchoutArcade:
                    if (!GameStatsManager.Instance.GetFlag(GungeonFlags.ITEMSPECIFIC_BOXING_GLOVE)) {
                        mode = Mode.LockedGame;
                        animationState = AnimationState.Locked;
                    }
                    break;
                case Mode.SlotMachine:
                    break;
                case Mode.ItemGunBallMachine:
                    break;
                case Mode.LockedGame:
                    break;
            }
            m_ParentRoom = room;
        }

        private void LateUpdate() {
            switch (animationState) {
                case AnimationState.SwitchingAnimation:
                    if (!string.IsNullOrEmpty(m_NewAnimation)) {
                        m_CurrentAnimation = m_NewAnimation;
                        spriteAnimator.Play(m_CurrentAnimation);
                        m_NewAnimation = string.Empty;
                        return;
                    }
                    if (!string.IsNullOrEmpty(m_CurrentAnimation) && !spriteAnimator.IsPlaying(m_CurrentAnimation)) {
                        if (mode == Mode.PunchoutArcade && m_CurrentAnimation == ActivateGameAnimation) { IdleAnimation = (IdleAnimation + "2"); }
                        m_CurrentAnimation = string.Empty;
                        animationState = AnimationState.Idle;
                    }
                    return;
                case AnimationState.Idle:
                    if (!spriteAnimator.IsPlaying(IdleAnimation)) { spriteAnimator.Play(IdleAnimation); }
                    return;
                case AnimationState.Sleep:
                    if (!string.IsNullOrEmpty(SleepFrame)) {
                        spriteAnimator.Stop();
                        if (sprite.CurrentSprite.name != SleepFrame) { sprite.SetSprite(SleepFrame); }
                    } else if (!string.IsNullOrEmpty(SleepAnimation) && !spriteAnimator.IsPlaying(SleepAnimation)) {
                        spriteAnimator.Play(SleepAnimation);
                    }
                    return;
                case AnimationState.Locked:
                    if (!string.IsNullOrEmpty(LockedAnimation)) {
                        if (!spriteAnimator.IsPlaying(LockedAnimation)) { spriteAnimator.Play(LockedAnimation); }
                    } else if (!string.IsNullOrEmpty(LockedSpriteFrame) && sprite.CurrentSprite.name != LockedSpriteFrame) {
                        spriteAnimator.Stop();
                        sprite.SetSprite(LockedSpriteFrame);
                    }
                    return;
                default:
                    return;
            }
        }

        private void Update() {
            switch (mode) {
                default:
                    return;
                case Mode.LockedGame:
                    return;
                case Mode.PunchoutArcade:
                    if (!Finished) { return; }
                    m_PunchoutArcadeController.DestroyOverlayUI();
                    GameUIRoot.Instance.PauseMenuPanel.GetComponent<PauseMenuController>().metaCurrencyPanel.IsVisible = true;
                    if (!DoingResults) {
                        string Text = "You didn't beat the rat so you won nothing!";
                        if (ExpandPunchoutArcadeController.WonRatGame) {
                            int Casings = 0;
                            int Hegemony = 0;

                            if (ExpandPunchoutArcadeController.RatKeyCount != -1) {
                                for (int K = 0; K < ExpandPunchoutArcadeController.RatKeyCount; K++) {
                                    foreach (int ItemID in ExpandPunchoutArcadeController.RewardIDs) {
                                        switch (ItemID) {
                                            case 68:
                                                Casings += 1;
                                                break;
                                            case 70:
                                                Casings += 5;
                                                break;
                                            case 74:
                                                Casings += 50;
                                                break;
                                            case 297:
                                                Hegemony += 5;
                                                break;
                                        }
                                    }
                                }
                            } else {
                                foreach (int ItemID in ExpandPunchoutArcadeController.RewardIDs) {
                                    switch (ItemID) {
                                        case 68:
                                            Casings += 1;
                                            break;
                                        case 70:
                                            Casings += 5;
                                            break;
                                        case 74:
                                            Casings += 50;
                                            break;
                                        case 297:
                                            Hegemony += 1;
                                            break;
                                    }
                                }
                            }
                            Text = "Congratulations!\nYou won " + Casings.ToString() + " casings" + " and " + Hegemony.ToString() + " Hegemony credits!";
                        }
                        StartCoroutine(HandleConversation(m_CurrentPlayer, Text));
                        DoingResults = true;
                    }

                    if (DoingResults && !ResultsGiven) { return; }

                    foreach (PlayerController player in GameManager.Instance.AllPlayers) {
                        player.ClearInputOverride("starting punchout");
                    }
                    foreach (PlayerController player in GameManager.Instance.AllPlayers) {
                        player.ClearInputOverride("gameConversation");
                    }
                    ResultsGiven = false;
                    DoingResults = false;
                    Finished = false;
                    m_CurrentPlayer = null;
                    break;
                case Mode.ItemGunBallMachine:
                    break;
                case Mode.SlotMachine:
                    break;
            }
        }
        
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }
        

        public float GetDistanceToPoint(Vector2 point) {
            if (m_Interacted | !sprite) { return 100f; }
            Vector3 v = BraveMathCollege.ClosestPointOnRectangle(point, specRigidbody.UnitBottomLeft, specRigidbody.UnitDimensions);
            return Vector2.Distance(point, v) / 1.5f;
        }

        public float GetOverrideMaxDistance() { return -1f; }
        
        public void OnEnteredRange(PlayerController interactor) {
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white);
        }

        public void OnExitRange(PlayerController interactor) {
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
        }

        protected override void OnDestroy() {
            ExpandSettings.PlayingPunchoutArcade = false;
            base.OnDestroy();
        }
    }
}

