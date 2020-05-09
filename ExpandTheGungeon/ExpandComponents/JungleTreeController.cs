using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ItemAPI;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class JungleTreeController : BraveBehaviour, IPlaceConfigurable {

        public JungleTreeController() {
            targetLevelName = "tt_jungle";
            PitOffset = new IntVector2(5, 3);
            m_Triggered = false;
        }

        public string targetLevelName;
        public IntVector2 PitOffset;

        private bool m_Triggered;

        private RoomHandler m_ParentRoom;

        private IEnumerator Start() {
            yield return null;
            while (Dungeon.IsGenerating) { yield return null; }
            yield return null;
            
            IntVector2 baseCellPosition = (transform.position.IntXY(VectorConversions.Floor) + new IntVector2(4, 2));
            
            for(int X = -1; X < 2; X++) {
                for (int Y = -1; Y < 2; Y++) {
                    IntVector2 PositionOffset = baseCellPosition + new IntVector2(X, Y);
                    CellData cellData = GameManager.Instance.Dungeon.data[PositionOffset];
                    cellData.OnCellGooped = (Action<CellData>)Delegate.Combine(cellData.OnCellGooped, new Action<CellData>(HandleGooped));
                }
            }
                        
            specRigidbody.OnRigidbodyCollision = (SpeculativeRigidbody.OnRigidbodyCollisionDelegate)Delegate.Combine(specRigidbody.OnRigidbodyCollision, new SpeculativeRigidbody.OnRigidbodyCollisionDelegate(HandleCollision));
            // specRigidbody.OnHitByBeam = (Action<BasicBeamController>)Delegate.Combine(specRigidbody.OnHitByBeam, new Action<BasicBeamController>(HandleBeamCollision));
            yield break;
        }

        private void HandleGooped(CellData obj) {
            if (obj != null) { StartCoroutine(HandleGoopWait(obj)); }
        }

        private IEnumerator HandleGoopWait(CellData obj) {
            IntVector2 goopPosition = (obj.position.ToCenterVector2() / DeadlyDeadlyGoopManager.GOOP_GRID_SIZE).ToIntVector2(VectorConversions.Floor);
            if (DeadlyDeadlyGoopManager.allGoopPositionMap.ContainsKey(goopPosition)) {
                DeadlyDeadlyGoopManager deadlyDeadlyGoopManager = DeadlyDeadlyGoopManager.allGoopPositionMap[goopPosition];
                if (deadlyDeadlyGoopManager) {
                    while (obj != null && deadlyDeadlyGoopManager != null && !deadlyDeadlyGoopManager.IsPositionOnFire(obj.position.ToCenterVector2()) && !m_Triggered) {
                        yield return null;
                    }
                    if (deadlyDeadlyGoopManager.IsPositionOnFire(obj.position.ToCenterVector2()) && !m_Triggered) {
                        m_Triggered = true;
                        OnFireStarted();
                        yield break;
                    }
                }
            }
            yield break;
        }


        /*private void HandleBeamCollision(BasicBeamController obj) {
            GoopModifier component = obj.GetComponent<GoopModifier>();
            if (component && component.goopDefinition != null && component.goopDefinition.CanBeIgnited && component.goopDefinition.fireEffect != null) { OnFireStarted(); }
        }*/

        private void HandleCollision(CollisionData rigidbodyCollision) {
            Projectile projectile = rigidbodyCollision.OtherRigidbody.projectile;
            if (projectile && projectile.AppliesFire) { OnFireStarted(); }
        }

        private void OnFireStarted() {
            IntVector2 baseCellPosition = (transform.position.IntXY(VectorConversions.Floor) + new IntVector2(4, 2));

            for (int X = -1; X < 2; X++) {
                for (int Y = -1; Y < 2; Y++) {
                    IntVector2 PositionOffset = baseCellPosition + new IntVector2(X, Y);
                    CellData cellData = GameManager.Instance.Dungeon.data[PositionOffset];
                    cellData.OnCellGooped = (Action<CellData>)Delegate.Remove(cellData.OnCellGooped, new Action<CellData>(HandleGooped));
                }
            }

            specRigidbody.OnRigidbodyCollision = (SpeculativeRigidbody.OnRigidbodyCollisionDelegate)Delegate.Remove(specRigidbody.OnRigidbodyCollision, new SpeculativeRigidbody.OnRigidbodyCollisionDelegate(HandleCollision));
            // specRigidbody.OnHitByBeam = (Action<BasicBeamController>)Delegate.Remove(specRigidbody.OnHitByBeam, new Action<BasicBeamController>(HandleBeamCollision));

            GameObject JungleTreeFrame = new GameObject("Jungle Tree Frame") { layer = 0 };
            JungleTreeFrame.transform.position = transform.position;
            ItemBuilder.AddSpriteToObject(JungleTreeFrame, "ExpandTheGungeon/Textures/JungleAssets/Jungle_Tree_Large_Frame", false, false);
            tk2dSprite JungleTreeFrameSprite = JungleTreeFrame.GetComponent<tk2dSprite>();
            JungleTreeFrameSprite.HeightOffGround = 3;
            JungleTreeFrameSprite.UpdateZDepth();

            GameObject PitManager = new GameObject("Jungle Pit Manager") { layer = 0 };
            PitManager.transform.position = (transform.position + new Vector3(5, 2));
            ItemBuilder.AddSpriteToObject(PitManager, "ExpandTheGungeon/Textures/Items/babygoodhammer", false, false);

            tk2dSprite pitSprite = PitManager.GetComponent<tk2dSprite>();
            pitSprite.renderer.enabled = false;

            ExpandUtility.GenerateOrAddToRigidBody(PitManager, CollisionLayer.Trap, PixelCollider.PixelColliderGeneration.Manual, IsTrigger: true, dimensions: new IntVector2(2, 2));

            JungleTreePitController junglePitManager = PitManager.AddComponent<JungleTreePitController>();
            junglePitManager.targetLevelName = targetLevelName;

            StartCoroutine(HandleDelayedFireDamage());
        }


        private IEnumerator HandleDelayedFireDamage() {
            Vector2 BottomOffset1 = new Vector3(5, 2);
            Vector2 TopOffset1 = new Vector3(9, 6);
            Vector2 BottomOffset2 = new Vector3(4, 1);
            Vector2 TopOffset2 = new Vector3(8, 6);
            GlobalSparksDoer.DoRandomParticleBurst(25, (sprite.WorldBottomLeft + BottomOffset2), (sprite.WorldBottomLeft + TopOffset2), Vector3.up, 70f, 0.5f, null, new float?(0.75f), new Color?(new Color(4f, 0.3f, 0f)), GlobalSparksDoer.SparksType.SOLID_SPARKLES);
            GlobalSparksDoer.DoRandomParticleBurst(25, (sprite.WorldBottomLeft + BottomOffset2), (sprite.WorldBottomLeft + TopOffset2), Vector3.up, 70f, 0.5f, null, new float?(1.5f), new Color?(new Color(4f, 0.3f, 0f)), GlobalSparksDoer.SparksType.SOLID_SPARKLES);
            GlobalSparksDoer.DoRandomParticleBurst(25, (sprite.WorldBottomLeft + BottomOffset2), (sprite.WorldBottomLeft + TopOffset2), Vector3.up, 70f, 0.5f, null, new float?(2.25f), new Color?(new Color(4f, 0.3f, 0f)), GlobalSparksDoer.SparksType.SOLID_SPARKLES);
            GlobalSparksDoer.DoRandomParticleBurst(25, (sprite.WorldBottomLeft + BottomOffset2), (sprite.WorldBottomLeft + TopOffset2), Vector3.up, 70f, 0.5f, null, new float?(3), new Color?(new Color(4f, 0.3f, 0f)), GlobalSparksDoer.SparksType.SOLID_SPARKLES);
            yield return new WaitForSeconds(2f);
            GlobalSparksDoer.DoRandomParticleBurst(25, (sprite.WorldBottomLeft + BottomOffset2), (sprite.WorldBottomLeft + TopOffset2), Vector3.up, 70f, 0.5f, null, new float?(1.75f), new Color?(new Color(4f, 0.3f, 0f)), GlobalSparksDoer.SparksType.STRAIGHT_UP_FIRE);
            GlobalSparksDoer.DoRandomParticleBurst(25, (sprite.WorldBottomLeft + BottomOffset2), (sprite.WorldBottomLeft + TopOffset2), Vector3.up, 70f, 0.5f, null, new float?(2.5f), new Color?(new Color(4f, 0.3f, 0f)), GlobalSparksDoer.SparksType.STRAIGHT_UP_FIRE);
            GlobalSparksDoer.DoRandomParticleBurst(25, (sprite.WorldBottomLeft + BottomOffset2), (sprite.WorldBottomLeft + TopOffset2), Vector3.up, 70f, 0.5f, null, new float?(3.25f), new Color?(new Color(4f, 0.3f, 0f)), GlobalSparksDoer.SparksType.STRAIGHT_UP_FIRE);
            GlobalSparksDoer.DoRandomParticleBurst(25, (sprite.WorldBottomLeft + BottomOffset2), (sprite.WorldBottomLeft + TopOffset2), Vector3.up, 70f, 0.5f, null, new float?(4), new Color?(new Color(4f, 0.3f, 0f)), GlobalSparksDoer.SparksType.STRAIGHT_UP_FIRE);
            Exploder.DoDefaultExplosion((transform.position + PitOffset.ToVector3() + new Vector3(1,0)), Vector2.zero, ignoreQueues: true);
            specRigidbody.PixelColliders[0].Enabled = false;
            specRigidbody.RecheckTriggers = true;
            specRigidbody.RegenerateColliders = true;

            IntVector2 basePosition = (transform.position.IntXY(VectorConversions.Floor) + PitOffset);
            IntVector2 cellPos = basePosition;
            IntVector2 cellPos2 = (basePosition + new IntVector2(1, 0));
            DeadlyDeadlyGoopManager.ForceClearGoopsInCell(cellPos);            
            DeadlyDeadlyGoopManager.ForceClearGoopsInCell(cellPos2);
            sprite.SetSprite("Jungle_Tree_Large_Open");
            yield return new WaitForSeconds(0.4f);
            m_ParentRoom.ForcePitfallForFliers = true;
            CellData cellData = GameManager.Instance.Dungeon.data[cellPos];
            CellData cellData2 = GameManager.Instance.Dungeon.data[cellPos2];
            cellData.fallingPrevented = false;
            cellData2.fallingPrevented = false;
            yield break;
        }        

        public void ConfigureOnPlacement(RoomHandler room) {
            m_ParentRoom = room;

            IntVector2 basePosition = (transform.position.IntXY(VectorConversions.Floor) + PitOffset);
            IntVector2 cellPos = basePosition;
            IntVector2 cellPos2 = (basePosition + new IntVector2(1, 0));            
            CellData cellData = GameManager.Instance.Dungeon.data[cellPos];
            CellData cellData2 = GameManager.Instance.Dungeon.data[cellPos2];

            cellData.type = CellType.PIT;
            cellData2.type = CellType.PIT;
            cellData.forceAllowGoop = true;
            cellData2.forceAllowGoop = true;
            cellData.fallingPrevented = true;
            cellData2.fallingPrevented = true;
        }

        private void Update() { }
        private void LateUpdate() { }
        
        protected override void OnDestroy() { base.OnDestroy(); }
    }
    
    public class JungleTreePitController : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public JungleTreePitController() { targetLevelName = "tt_jungle"; }

        public string targetLevelName;

        private void Start() {
            SpeculativeRigidbody Rigidbody = specRigidbody;
            Rigidbody.OnEnterTrigger = (SpeculativeRigidbody.OnTriggerDelegate)Delegate.Combine(Rigidbody.OnEnterTrigger, new SpeculativeRigidbody.OnTriggerDelegate(HandleTriggerEntered));
            SpeculativeRigidbody Rigidbody2 = specRigidbody;
            Rigidbody2.OnExitTrigger = (SpeculativeRigidbody.OnTriggerExitDelegate)Delegate.Combine(Rigidbody2.OnExitTrigger, new SpeculativeRigidbody.OnTriggerExitDelegate(HandleTriggerExited));
        }
        
        private void HandleTriggerEntered(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody, CollisionData collisionData) {
            PlayerController component = specRigidbody.GetComponent<PlayerController>();
            if (component) { component.LevelToLoadOnPitfall = targetLevelName; }
        }

        private void HandleTriggerExited(SpeculativeRigidbody specRigidbody, SpeculativeRigidbody sourceSpecRigidbody) {
            PlayerController component = specRigidbody.GetComponent<PlayerController>();
            if (component) { component.LevelToLoadOnPitfall = string.Empty; }
        }

        public void ConfigureOnPlacement(RoomHandler room) { }

        private void Update() { }
        
        /*public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            // SpriteOutlineManager.AddOutlineToSprite(base.sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            // base.sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            // SpriteOutlineManager.RemoveOutlineFromSprite(base.sprite, false);
            // sprite.UpdateZDepth();
        }

        public float GetDistanceToPoint(Vector2 point) {
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }

        public float GetOverrideMaxDistance() { return -1f; }

        public void Interact(PlayerController player) {
            GameManager.Instance.LoadCustomLevel(targetLevelName);
        }

        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }*/

        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

