using System.Collections.Generic;
using UnityEngine;
using Dungeonator;


namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandConfettiSpawner : BraveBehaviour {

        public ExpandConfettiSpawner() {
            RemoveOnSpawn = true;
            UseSpriteAsAnchorPoint = true;
            SpawnOffset = new Vector2(-0.75f, 0.25f);
            spawnStyle = SpawnStyle.OnChestOpen;
            ConfettiNames = new List<string> { "Global VFX/Confetti_Blue_001", "Global VFX/Confetti_Yellow_001", "Global VFX/Confetti_Green_001" };
            m_ConfettiSpawned = false;
            m_Configured = false;
        }

        public bool RemoveOnSpawn;
        public bool UseSpriteAsAnchorPoint;
        public Vector2 SpawnOffset;
        public enum SpawnStyle { OnChestOpen, Other }
        public SpawnStyle spawnStyle;
        public List<string> ConfettiNames;

        private bool m_ConfettiSpawned;
        private bool m_Configured;

        private Chest m_ParentChest;

        public void Start() {
            switch (spawnStyle) {
                case SpawnStyle.OnChestOpen:
                    m_ParentChest = gameObject.GetComponent<Chest>();
                    m_Configured = true;
                    return;
                default:
                    return;
            }
        }

        public void Update() {
            if (m_ConfettiSpawned | !m_Configured | !GameManager.HasInstance | GameManager.IsShuttingDown | 
                GameManager.Instance.IsLoadingLevel | !GameManager.Instance.Dungeon |
                Dungeon.IsGenerating
                )
            {
                return;
            }
            switch (spawnStyle) {
                case SpawnStyle.OnChestOpen:
                    if (!m_ParentChest | (UseSpriteAsAnchorPoint && !m_ParentChest.sprite)) return;
                    if (m_ParentChest.IsOpen) {
                        if (UseSpriteAsAnchorPoint) {
                            DoConfetti(sprite.WorldBottomCenter);
                        } else {
                            DoConfetti(transform.position);
                        }
                        m_ConfettiSpawned = true;
                        if (RemoveOnSpawn)Destroy(this);
                    }
                    return;
                default:
                    return;
            }
        }

        private void DoConfetti(Vector2 startPosition) {
            Vector2 vector = (startPosition + SpawnOffset);
            for (int i = 0; i < 16; i++) {
                ConfettiNames = ConfettiNames.Shuffle();
                GameObject ConfettiObject = (GameObject)ResourceCache.Acquire(BraveUtility.RandomElement(ConfettiNames));
                if (ConfettiObject) {
                    WaftingDebrisObject component = Instantiate(ConfettiObject).GetComponent<WaftingDebrisObject>();
                    if (component) {
                        component.sprite.PlaceAtPositionByAnchor(vector.ToVector3ZUp(0f) + new Vector3(0.5f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                        Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
                        insideUnitCircle.y = -Mathf.Abs(insideUnitCircle.y);
                        component.Trigger(insideUnitCircle.ToVector3ZUp(1.5f) * UnityEngine.Random.Range(0.5f, 2f), 0.5f, 0f);
                    }
                }
            }
            if (BraveUtility.RandomBool()) {
                AkSoundEngine.PostEvent("Play_EX_PartySFX_01", gameObject);
            } else {
                AkSoundEngine.PostEvent("Play_OBJ_prize_won_01", gameObject);
            }
        }
    }
}

