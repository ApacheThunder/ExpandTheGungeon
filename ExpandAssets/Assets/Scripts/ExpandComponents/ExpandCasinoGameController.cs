using Dungeonator;
using System;
using System.Collections;
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

        public void ConfigureOnPlacement(RoomHandler room) {            
            m_ParentRoom = room;
        }
        private void Update() { }
        
        public void OnEnteredRange(PlayerController interactor) { }

        public void OnExitRange(PlayerController interactor) { }

        public void Interact(PlayerController interactor) { }

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


    }
}

