using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using static ExpandTheGungeon.ExpandUtilities.ReflectionHelpers;
using ExpandTheGungeon.ExpandPrefab;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEntityManager : BraveBehaviour {
        
        public ExpandEntityManager() {
            Configured = false;
            IsOnBackRoomsFloor = false;
            MaxPlayerAwayTime = 20;
            EntitySize = new IntVector2(2, 2);
            AIAnimatorSpawnClip = "spawn";
            AIAnimatorDeSpawnClip = "despawn";
            EntityPlayScreemEvent = "Play_EX_EntityScreams_01";
            EntityStopScreemEvent = "Stop_EX_EntityScreams_01";

            m_SettingsApplied = false;
            m_IsTeleporting = false;
            m_ScreemStarted = false;
            m_PlayerEaten = false;
            m_PlayerAwayTime = 0;
        }

        [NonSerialized]
        public bool Configured;
        [NonSerialized]
        public bool IsOnBackRoomsFloor;
        [SerializeField]
        public float MaxPlayerAwayTime;
        [SerializeField]
        public IntVector2 EntitySize;
        [SerializeField]
        public string AIAnimatorSpawnClip;
        [SerializeField]
        public string AIAnimatorDeSpawnClip;
        [SerializeField]
        public string EntityPlayScreemEvent;
        [SerializeField]
        public string EntityStopScreemEvent;

        [NonSerialized]
        private bool m_SettingsApplied;
        [NonSerialized]
        private bool m_IsTeleporting;
        [NonSerialized]
        private bool m_ScreemStarted;
        [NonSerialized]
        private bool m_PlayerEaten;
        [NonSerialized]
        private float m_PlayerAwayTime;

        [NonSerialized]
        private PlayerController m_Player;
        [NonSerialized]
        private RoomHandler m_CurrentRoom;
        [NonSerialized]
        private RoomHandler m_TargetTeleportRoom;

        public void Awake() { }

        public void Start() {
            specRigidbody.Reinitialize();
        }

        public void Update() {
            if (!m_ScreemStarted) {
                StartCoroutine(DoScream());
                m_ScreemStarted = true;
            }
            if (!Configured) return;
            if (m_PlayerEaten) {
                specRigidbody.Reinitialize();
                if (m_Player) {
                    m_Player.CurrentPoisonMeterValue = 0f;
                    if (GameManager.Instance.SecondaryPlayer)GameManager.Instance.SecondaryPlayer.CurrentPoisonMeterValue = 0;
                }
                return;
            }
            if (aiActor.IsBlackPhantom && IsOnBackRoomsFloor)aiActor.UnbecomeBlackPhantom();
            if (aiActor.ParentRoom != null)aiActor.ParentRoom = null;
            if (gameObject.transform.parent != null)gameObject.transform.parent = null;
            if (m_PlayerEaten)return;
            
            if (m_SettingsApplied) {
                if (!IsOnBackRoomsFloor)return;
                if (m_IsTeleporting) return;
                if (!GameManager.Instance) return;
                if (!m_Player) m_Player = GameManager.Instance.PrimaryPlayer;
                if (!m_Player) return;
                m_CurrentRoom = transform.position.GetAbsoluteRoom();
                // if (m_CurrentRoom != null && m_Player.CurrentRoom != null && m_CurrentRoom != m_Player.CurrentRoom) {
                if (m_CurrentRoom != null && m_Player.CurrentRoom != null && !GameManager.Instance.IsAnyPlayerInRoom(m_CurrentRoom)) {
                    m_PlayerAwayTime += BraveTime.DeltaTime;
                    if (m_PlayerAwayTime > MaxPlayerAwayTime) {
                        if (m_Player.CurrentRoom.area != null && m_Player.CurrentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.EXIT) {
                            m_PlayerAwayTime = (MaxPlayerAwayTime - 5);
                            if (m_PlayerAwayTime < 0) m_PlayerAwayTime = 5;
                            return;
                        }
                        m_PlayerAwayTime = 0;
                        if (aiActor && specRigidbody && aiAnimator && spriteAnimator && behaviorSpeculator) {
                            if (m_Player.CurrentRoom != null) {
                                if (m_Player.CurrentRoom.connectedRooms != null && m_Player.CurrentRoom.connectedRooms.Count > 0) {
                                    m_TargetTeleportRoom = BraveUtility.RandomElement(m_Player.CurrentRoom.connectedRooms);
                                } else {
                                    m_TargetTeleportRoom = m_CurrentRoom;
                                }
                                if (m_TargetTeleportRoom != null) {
                                    // IntVector2? newPosition = ExpandUtility.GetRandomAvailableCellSmart(m_TargetTeleportRoom, EntitySize, false);
                                    Vector2 newPosition = FindSafeSpawnInRoom(m_TargetTeleportRoom, EntitySize, sprite, new Vector2(-0.15f, 0.5f));
                                    m_IsTeleporting = true;
                                    AkSoundEngine.PostEvent(EntityStopScreemEvent, gameObject);
                                    StartCoroutine(DoTeleport(newPosition));
                                    /*if (newPosition.HasValue) {
                                        m_IsTeleporting = true;
                                        AkSoundEngine.PostEvent(EntityStopScreemEvent, gameObject);
                                        StartCoroutine(DoTeleport(newPosition.Value));
                                    } else{
                                        m_PlayerAwayTime = (MaxPlayerAwayTime - 2);
                                        if (m_PlayerAwayTime < 0) m_PlayerAwayTime = 2;
                                        return;
                                    }*/
                                } else {
                                    m_PlayerAwayTime = (MaxPlayerAwayTime - 2);
                                    if (m_PlayerAwayTime < 0) m_PlayerAwayTime = 2;
                                    return;
                                }
                            } else {
                                m_PlayerAwayTime = (MaxPlayerAwayTime - 5);
                                if (m_PlayerAwayTime < 0) m_PlayerAwayTime = 10;
                                return;
                            }
                        }
                    }
                    return;
                } else if (m_CurrentRoom != null && m_Player.CurrentRoom != null && m_CurrentRoom == m_Player.CurrentRoom) {
                    m_PlayerAwayTime = 0;
                }
                return;
            }
            if (IsOnBackRoomsFloor) {
                if (visibilityManager) {
                    visibilityManager.ChangeToVisibility(RoomHandler.VisibilityStatus.VISITED, true);
                    Destroy(visibilityManager);
                }
                if (aiActor) {
                    aiActor.ImmuneToAllEffects = true;
                    aiActor.IgnoreForRoomClear = true;
                    aiActor.CollisionKnockbackStrength = 0;
                    aiActor.CollisionDamage = 0;
                    aiActor.knockbackDoer.weight = 100f;
                    aiActor.CorpseObject = null;
                }
                if (healthHaver) {
                    healthHaver.SetHealthMaximum(1000);
                    healthHaver.ForceSetCurrentHealth(1000);
                    healthHaver.PreventAllDamage = true;
                }
            } else {
                aiActor.ImmuneToAllEffects = true;
                aiActor.IgnoreForRoomClear = false;
                if (behaviorSpeculator) {
                    behaviorSpeculator.enabled = false;
                    behaviorSpeculator.InstantFirstTick = false;
                    behaviorSpeculator.PostAwakenDelay = 1;
                    behaviorSpeculator.enabled = true;
                }
            }
            m_SettingsApplied = true;
        }

        private IEnumerator DoTeleport(Vector2 targetPosition) {
            behaviorSpeculator.enabled = false;
            aiActor.ClearPath();
            if (specRigidbody) {
                specRigidbody.Velocity = Vector2.zero;
                specRigidbody.Reinitialize();
            }
            yield return null;
            if (aiAnimator && spriteAnimator) {
                aiAnimator.enabled = true;
                spriteAnimator.enabled = true;
                aiAnimator.PlayUntilFinished(AIAnimatorDeSpawnClip, true, "despawn");
                while (!aiAnimator.IsPlaying(AIAnimatorDeSpawnClip)) yield return null;
                while (aiAnimator.IsPlaying(AIAnimatorDeSpawnClip)) yield return null;

            }
            transform.position = targetPosition;
            if (specRigidbody)specRigidbody.Reinitialize();
            if (aiAnimator && spriteAnimator) {
                aiAnimator.enabled = true;
                spriteAnimator.enabled = true;
                aiAnimator.PlayUntilFinished(AIAnimatorSpawnClip, true, "Respawn");
                while (!aiAnimator.IsPlaying(AIAnimatorSpawnClip))yield return null;
                while (aiAnimator.IsPlaying(AIAnimatorSpawnClip))yield return null;
                
            }
            if (behaviorSpeculator)behaviorSpeculator.enabled = true;
            AkSoundEngine.PostEvent(EntityPlayScreemEvent, gameObject);
            m_IsTeleporting = false;
            yield break;
        }

        private IEnumerator DoScream() {
            yield return null;
            float delay = 3f;
            float timer = 0f;
            while (timer < delay) {
                yield return null;
                if (!aiAnimator.IsPlaying("spawn"))break;
                timer += BraveTime.DeltaTime;
                specRigidbody.Reinitialize();
            }
            foreach (PixelCollider collider in specRigidbody.PixelColliders) { collider.Enabled = true; }
            if (specRigidbody && IsOnBackRoomsFloor) specRigidbody.OnPreRigidbodyCollision += OnPreRigidBodyCollision;
            AkSoundEngine.PostEvent(EntityPlayScreemEvent, gameObject);
            yield break;
        }

        public void OnPreRigidBodyCollision(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider) {
            if (!this | !gameObject | !aiActor | !otherRigidbody) return;
            if (m_PlayerEaten) { PhysicsEngine.SkipCollision = true; return; }
            try {
                PlayerController m_PlayerController = otherRigidbody.GetComponent<PlayerController>();
                Chest m_Chest = otherRigidbody.GetComponent<Chest>();
                MajorBreakable m_MajorBreakable = otherRigidbody.GetComponent<MajorBreakable>();
                MinorBreakable m_MinorBreakable = otherRigidbody.GetComponent<MinorBreakable>();
                ExpandHatMindController m_HatMindController = otherRigidbody.GetComponent<ExpandHatMindController>();
                Projectile m_Projectile = otherRigidbody.GetComponent<Projectile>();
                BeamController m_BeamController = otherRigidbody.GetComponent<BeamController>();
                BasicBeamController m_BasicBeamController = otherRigidbody.GetComponent<BasicBeamController>();
                ProjectileAndBeamMotionModule m_ProjectileAndBeamMotionModule = otherRigidbody.GetComponent<ProjectileAndBeamMotionModule>();
                PhysicsEngine.SkipCollision = (m_PlayerController | m_MajorBreakable | m_MinorBreakable | m_Projectile | m_BeamController | m_BasicBeamController | (m_ProjectileAndBeamMotionModule != null));
                if (m_HatMindController && m_HatMindController.owner)m_HatMindController.Detach();
                if (m_PlayerController) {
                    if (!m_PlayerController.healthHaver.IsVulnerable) return;
                    m_PlayerEaten = true;
                    m_PlayerController.SetInputOverride("got eaten");
                    behaviorSpeculator.enabled = false;
                    aiAnimator.enabled = false;
                    aiActor.BehaviorOverridesVelocity = false;
                    aiActor.ClearPath();
                    myRigidbody.Velocity = Vector2.zero;
                    myRigidbody.Reinitialize();
                    spriteAnimator.Stop();
                    StartCoroutine(HandleExitFloor(m_PlayerController));
                    return;
                } else if (m_MajorBreakable) {
                    if (m_Chest && m_Chest.IsMimic && !m_Chest.IsOpen) {
                        PhysicsEngine.SkipCollision = true;
                        m_MajorBreakable.ApplyDamage(1, myRigidbody.Velocity, false);
                        return;
                    } else if (m_Chest && m_MajorBreakable.TemporarilyInvulnerable && !m_Chest.IsOpen && !m_Chest.IsMimic) {
                        PhysicsEngine.SkipCollision = true;
                        if (otherRigidbody.sprite) {
                            SpriteOutlineManager.RemoveOutlineFromSprite(otherRigidbody.sprite, false);
                            otherRigidbody.renderer.enabled = false;
                        }
                        InvokeMethod(typeof(Chest), "ExplodeInSadness", m_Chest);
                        return;
                    }
                    m_MajorBreakable.Break(new Vector2(1, 0));
                } else if (m_MinorBreakable) {
                    m_MinorBreakable.Break(myRigidbody.Velocity);
                }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) {
                    ETGModConsole.Log("[ExpandTheGungeon] Warning: Exception caught at ExpandEntityController.OnPreRigidBodyCollision!");
                    Debug.LogException(ex);
                }
                return;
            }
        }
        


        private IEnumerator HandleExitFloor(PlayerController player) {
            m_Player = player;
            m_Player.IsVisible = false;
            yield return null;
            float elapsed = 0f;
            float duration = 0.5f;
            Vector3 startPos = m_Player.specRigidbody.GetUnitCenter(ColliderType.Ground);
            Vector3 finalOffset = (transform.position + new Vector3(0.5f, 0.2f));
            GameObject dummySpriteObject = new GameObject("PlayerSpriteDupe", new Type[] { typeof(tk2dSprite) }) { layer = 22 };
            dummySpriteObject.transform.position = startPos;
            tk2dSprite targetSprite = dummySpriteObject.GetComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(targetSprite, (m_Player.sprite as tk2dSprite));
            targetSprite.SetSprite(m_Player.sprite.spriteId);
            AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
            AkSoundEngine.PostEvent(EntityStopScreemEvent, gameObject);
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                if (!targetSprite || !targetSprite.transform) { break; }
                targetSprite.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.01f, 0.01f, 1f), elapsed / duration);
                targetSprite.transform.position = Vector3.Lerp(startPos, finalOffset, elapsed / duration);
                yield return null;
            }
            AkSoundEngine.PostEvent("Play_VO_lichA_cackle_01", gameObject);
            Vector2 BottomOffset = dummySpriteObject.transform.position;
            Vector2 TopOffset = dummySpriteObject.transform.position + new Vector3(1, 1);
            Color TargetColor = new Color(0.5f, 0.1f, 0.1f);
            GlobalSparksDoer.DoRandomParticleBurst(5, BottomOffset, TopOffset, new Vector3(-1, 1), 70f, 0.5f, null, new float?(0.75f), new Color?(TargetColor), GlobalSparksDoer.SparksType.BLOODY_BLOOD);
            GlobalSparksDoer.DoRandomParticleBurst(5, BottomOffset, TopOffset, Vector3.left, 70f, 0.5f, null, new float?(1.5f), new Color?(TargetColor), GlobalSparksDoer.SparksType.BLOODY_BLOOD);
            GlobalSparksDoer.DoRandomParticleBurst(5, BottomOffset, TopOffset, Vector3.left, 70f, 0.5f, null, new float?(2.25f), new Color?(TargetColor), GlobalSparksDoer.SparksType.BLOODY_BLOOD);
            GlobalSparksDoer.DoRandomParticleBurst(5, BottomOffset, TopOffset, new Vector3(-1, -1), 70f, 0.5f, null, new float?(3), new Color?(TargetColor), GlobalSparksDoer.SparksType.BLOODY_BLOOD);
            yield return new WaitForSeconds(1);
            if (ExpandDebugCamera.DebugCameraEnabled) {
                ExpandDebugCamera.SetInitialCameraPosition(Pixelator.Instance, GameManager.Instance.MainCameraController);
            } else {
                Pixelator.Instance.FadeToBlack(0.15f, false, 0f);
            }
            yield return new WaitForSeconds(0.3f);
            specRigidbody.OnPreRigidbodyCollision -= OnPreRigidBodyCollision;
            Destroy(dummySpriteObject);
            AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
            AkSoundEngine.PostEvent("Stop_EX_MUS_All", gameObject);
            yield return null;
            m_Player.ClearAllInputOverrides();
            yield return null;
            m_Player.PrepareForSceneTransition();
            GameManager.Instance.LoadNextLevel();
            yield break;
        }

        private Vector2 FindSafeSpawnInRoom(RoomHandler targetRoom, IntVector2 clearance, tk2dBaseSprite targetSprite, Vector2? offset = null) {
            Vector2 m_result;
            IntVector2? randomAvailableCell = targetRoom.GetRandomAvailableCell(new IntVector2?(clearance), new CellTypes?(CellTypes.FLOOR), false, null);
            m_result = ((randomAvailableCell == null) ? targetRoom.GetCenterCell().ToVector2() : randomAvailableCell.Value.ToVector2());
            m_result += ((sprite.GetUntrimmedBounds().size).XY().WithY(0f) / 2f);
            if (offset.HasValue) m_result += offset.Value;
            return m_result;
        }

        
        protected override void OnDestroy() {
            AkSoundEngine.PostEvent(EntityStopScreemEvent, gameObject);
            if (specRigidbody && IsOnBackRoomsFloor)specRigidbody.OnPreRigidbodyCollision -= OnPreRigidBodyCollision;
            base.OnDestroy();
        }
    }
}

