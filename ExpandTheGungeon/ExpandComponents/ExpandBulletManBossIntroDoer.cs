using UnityEngine;
using System.Collections.Generic;
using Dungeonator;
using System.Collections;

namespace ExpandTheGungeon.ExpandComponents {

    [RequireComponent(typeof(GenericIntroDoer))]
    public class ExpandBulletManBossIntroDoer : SpecificIntroDoer {

        public ExpandBulletManBossIntroDoer() {
            BossFlipAnimation = "cover_leap_left";
            TargetTables = new List<FlippableCover>();
            DoTableFlips = true;
            FlipAllTables = false;
            FlipDirectionIsRandom = false;
            FlipDirections = new List<DungeonData.Direction>() {
                DungeonData.Direction.WEST,
                DungeonData.Direction.EAST,
                DungeonData.Direction.SOUTH,
                DungeonData.Direction.NORTH
            };
            m_FlipDirection = DungeonData.Direction.SOUTH;
            m_IsFinished = false;
            PreFlipDelay = 1;
            PostFlipDelay = 0.5f;
        }

        public string BossFlipAnimation;

        public List<FlippableCover> TargetTables;

        public bool DoTableFlips;
        public bool FlipAllTables;
        public bool FlipDirectionIsRandom;

        public float PreFlipDelay;
        public float PostFlipDelay;
        
        public List<DungeonData.Direction> FlipDirections;

        private DungeonData.Direction m_FlipDirection;

        private AIActor m_AIActor;

        private RoomHandler m_room;

        private bool m_IsFinished;

        public override bool IsIntroFinished { get { return m_IsFinished; } }

        public void Start() {
            m_AIActor = aiActor;
            m_room = aiActor.ParentRoom;

            if (GameManager.Instance?.Dungeon?.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                aiActor.AdditionalSafeItemDrops = new List<PickupObject>() { PickupObjectDatabase.GetById(727) };
            }

            if (m_room == null | !m_room.hierarchyParent | m_room.hierarchyParent.childCount == 0) { DoTableFlips = false; return; }

            int m_ChildCount = m_room.hierarchyParent.childCount;

            for (int i = 0; i < m_ChildCount; i++) {
                Transform m_CurrentTransform = m_room.hierarchyParent.GetChild(i);
                if (m_CurrentTransform && m_CurrentTransform.gameObject?.GetComponent<FlippableCover>()) {
                    FlippableCover m_CurrentTable = m_CurrentTransform.gameObject.GetComponent<FlippableCover>();
                    if (!m_CurrentTable.IsBroken && !m_CurrentTable.IsFlipped &&
                        m_CurrentTable.flipStyle != FlippableCover.FlipStyle.NO_FLIPS) {
                        TargetTables.Add(m_CurrentTransform.gameObject.GetComponent<FlippableCover>());
                    }
                    if (!FlipAllTables) break;
                }
            }

            if (TargetTables.Count <= 0) { DoTableFlips = false; return; }
            
        }

        public override void PlayerWalkedIn(PlayerController player, List<tk2dSpriteAnimator> animators) {
            m_AIActor.aiShooter.AimAtPoint(m_AIActor.CenterPosition + new Vector2(-2, 0.35f));
        }


        public override void StartIntro(List<tk2dSpriteAnimator> animators) {
            if (DoTableFlips) { StartCoroutine(FlipTables()); } else { m_IsFinished = true; }
        }

        private IEnumerator FlipTables() {
            if (PreFlipDelay > 0) yield return StartCoroutine(TimeInvariantWait(PreFlipDelay));
            DungeonData.Direction m_ChosenDirection = m_FlipDirection;
            foreach (FlippableCover table in TargetTables) {
                if (!table.IsBroken && !table.IsFlipped && table.flipStyle != FlippableCover.FlipStyle.NO_FLIPS) {
                    if (FlipDirectionIsRandom) {
                        FlipDirections = FlipDirections.Shuffle();
                        m_ChosenDirection = BraveUtility.RandomElement(FlipDirections);
                    } else {
                        m_ChosenDirection = table.GetFlipDirection(specRigidbody);
                    }
                    if (!string.IsNullOrEmpty(BossFlipAnimation))m_AIActor.spriteAnimator.Play(BossFlipAnimation);
                    if (FlipDirectionIsRandom) { table.Flip(m_ChosenDirection); } else { table.Flip(specRigidbody); }
                    string m_FlipAnimation = GetTableFlipAnimName(table.flipAnimation, m_ChosenDirection);
                    while (!table.spriteAnimator.IsPlaying(m_FlipAnimation)) yield return null;
                    for (float elapsed = 0f; elapsed < 2; elapsed += GameManager.INVARIANT_DELTA_TIME) {
                        table.spriteAnimator.UpdateAnimation(GameManager.INVARIANT_DELTA_TIME);
                        if (!table.spriteAnimator.IsPlaying(m_FlipAnimation)) break;
                        yield return null;
                    }
                    while (!table.IsFlipped)yield return null;
                    if (!FlipAllTables)break;
                }
            }
            if (PostFlipDelay > 0) yield return StartCoroutine(TimeInvariantWait(PostFlipDelay));
            m_IsFinished = true;
            yield break;
        }

        private string GetTableFlipAnimName(string name, DungeonData.Direction dir) {
            if (name.Contains("{0}"))return string.Format(name, dir.ToString().ToLower());
            return name;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

