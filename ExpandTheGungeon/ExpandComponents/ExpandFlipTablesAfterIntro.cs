
using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandFlipTablesAfterIntro : BraveBehaviour {

        public ExpandFlipTablesAfterIntro() {
            TargetTables = new List<FlippableCover>();
            FlipDelay = 0;
            FlipAllTables = false;
            FlipDirectionIsRandom = false;
            FlipDirection = DungeonData.Direction.SOUTH;
            FlipDirections = new List<DungeonData.Direction>() {
                DungeonData.Direction.WEST,
                DungeonData.Direction.EAST,
                DungeonData.Direction.SOUTH,
                DungeonData.Direction.NORTH
            };
        }

        [Header("Table Info")]
        public List<FlippableCover> TargetTables;
        
        public float FlipDelay;

        public bool FlipAllTables;
        public bool FlipDirectionIsRandom;

        public DungeonData.Direction FlipDirection;

        public List<DungeonData.Direction> FlipDirections;

        private RoomHandler m_room;
        
        public void Start() {
            m_room = aiActor.ParentRoom;
            
            if (m_room == null | !m_room.hierarchyParent | m_room.hierarchyParent.childCount == 0) { Destroy(this); return; }

            int m_ChildCount = m_room.hierarchyParent.childCount;

            for (int i = 0; i < m_ChildCount; i++) {
                Transform m_CurrentTransform = m_room.hierarchyParent.GetChild(i);
                if (m_CurrentTransform && m_CurrentTransform.gameObject?.GetComponent<FlippableCover>()) {
                    FlippableCover m_CurrentTable = m_CurrentTransform.gameObject.GetComponent<FlippableCover>();
                    if (!m_CurrentTable.IsBroken && !m_CurrentTable.IsFlipped && 
                        m_CurrentTable.flipStyle != FlippableCover.FlipStyle.NO_FLIPS) {
                        TargetTables.Add(m_CurrentTransform.gameObject.GetComponent<FlippableCover>());
                    }
                    if (!FlipAllTables)break;
                }
            }

            if (TargetTables.Count <= 0) { Destroy(this); return; }
            
            GenericIntroDoer introDoer = gameObject.GetComponent<GenericIntroDoer>();

            if (!introDoer) { Destroy(this); return; }

            introDoer.OnIntroFinished = (Action)Delegate.Combine(introDoer.OnIntroFinished, new Action(OnIntroFinished));
        }


        private void OnIntroFinished() {
            if (TargetTables.Count <= 0) return;
            StartCoroutine(FlipTables(FlipDelay));
        }
        
        private IEnumerator FlipTables(float delay) {
            if (delay > 0)yield return new WaitForSeconds(delay);
            foreach (FlippableCover table in TargetTables) {
                if (!table.IsBroken && !table.IsFlipped && table.flipStyle != FlippableCover.FlipStyle.NO_FLIPS) {
                    if (FlipDirectionIsRandom) {
                        FlipDirections = FlipDirections.Shuffle();
                        table.Flip(BraveUtility.RandomElement(FlipDirections));
                    } else {
                        table.Flip(FlipDirection);
                    }
                }
            }
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

