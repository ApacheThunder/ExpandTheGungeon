using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandEntityManager : BraveBehaviour {
        
        public ExpandEntityManager() {
            Configured = false;
            IsOnBackRoomsFloor = false;
            MaxPlayerAwayTime = 30;
            EntitySize = new IntVector2(1, 2);
            AIAnimatorSpawnClip = "spawn";
            EntityPlayScreemEvent = "Play_EX_EntityScreams_01";
            EntityStopScreemEvent = "Stop_EX_EntityScreams_01";

            m_SettingsApplied = false;
            m_IsTeleporting = false;
            m_ScreemStarted = false;
            m_PlayerEaten = false;
            m_PlayerAwayTime = 0;
        }

        public bool Configured;
        public bool IsOnBackRoomsFloor;

        public float MaxPlayerAwayTime;

        public IntVector2 EntitySize;
        public string AIAnimatorSpawnClip;

        public string EntityPlayScreemEvent;
        public string EntityStopScreemEvent;

        private bool m_SettingsApplied;
        private bool m_IsTeleporting;
        private bool m_ScreemStarted;
        private bool m_PlayerEaten;


        private float m_PlayerAwayTime;

        private PlayerController m_Player;

        private RoomHandler m_CurrentRoom;
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
            if (m_PlayerEaten)specRigidbody.Reinitialize();
            if (aiActor.IsBlackPhantom && IsOnBackRoomsFloor) {
                aiActor.UnbecomeBlackPhantom();
            }
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
                                behaviorSpeculator.enabled = false;
                                aiActor.ClearPath();
                                specRigidbody.Velocity = Vector2.zero;
                                aiAnimator.enabled = false;
                                spriteAnimator.Stop();
                                aiActor.ToggleRenderers(false);
                                if (m_Player.CurrentRoom.connectedRooms != null && m_Player.CurrentRoom.connectedRooms.Count > 0) {
                                    m_TargetTeleportRoom = BraveUtility.RandomElement(m_Player.CurrentRoom.connectedRooms);
                                } else {
                                    m_TargetTeleportRoom = m_CurrentRoom;
                                }
                                if (m_TargetTeleportRoom != null) {
                                        IntVector2? newPosition = ExpandUtility.GetRandomAvailableCellSmart(m_TargetTeleportRoom, EntitySize, false);
                                        if (newPosition.HasValue) {
                                            m_IsTeleporting = true;
                                            AkSoundEngine.PostEvent(EntityStopScreemEvent, gameObject);
                                            StartCoroutine(DoTeleport(newPosition.Value));
                                        } else{
                                            m_PlayerAwayTime = (MaxPlayerAwayTime - 2);
                                            if (m_PlayerAwayTime < 0) m_PlayerAwayTime = 2;
                                            return;
                                        }
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
                if (aiActor) {
                    aiActor.ImmuneToAllEffects = true;
                    aiActor.IgnoreForRoomClear = true;
                    aiActor.CollisionKnockbackStrength = 0;
                    aiActor.CollisionDamage = 0;
                    aiActor.knockbackDoer.weight = 100f;
                }
                if (healthHaver) {
                    healthHaver.SetHealthMaximum(1000);
                    healthHaver.ForceSetCurrentHealth(1000);
                    healthHaver.PreventAllDamage = true;
                }
            } else {
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

        private IEnumerator DoTeleport(IntVector2 targetPosition) {
            aiActor.ToggleRenderers(true);
            transform.position = targetPosition.ToVector3();
            if (specRigidbody)specRigidbody.Reinitialize();
            yield return null;
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
            if (!m_PlayerEaten && otherRigidbody.GetComponent<PlayerController>() && aiActor) {
                if (!otherRigidbody.GetComponent<PlayerController>().healthHaver.IsVulnerable)return;
                m_PlayerEaten = true;
                otherRigidbody.GetComponent<PlayerController>().SetInputOverride("got eaten");
                behaviorSpeculator.enabled = false;
                aiAnimator.enabled = false;
                aiActor.BehaviorOverridesVelocity = false;
                aiActor.ClearPath();
                specRigidbody.Velocity = Vector2.zero;
                specRigidbody.Reinitialize();
                spriteAnimator.Stop();
                StartCoroutine(HandleExitFloor(otherRigidbody.GetComponent<PlayerController>()));
                return;
            } else if (otherRigidbody.GetComponent<MajorBreakable>()) {
                otherRigidbody.GetComponent<MajorBreakable>().Break(new Vector2(1, 0));
            } else if (otherRigidbody.GetComponent<MinorBreakable>()) {
                otherRigidbody.GetComponent<MinorBreakable>().Break(new Vector2(1, 0));
            } else if (otherRigidbody.GetComponent<Projectile>()) {
                Destroy(otherRigidbody.gameObject);
            }
        }
        


        private IEnumerator HandleExitFloor(PlayerController player) {
            m_Player = player;
            m_Player.ToggleRenderer(false, "got eaten");
            m_Player.ToggleGunRenderers(false, "got eaten");
            m_Player.ToggleHandRenderers(false, "got eaten");
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
            yield return null;
            AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
            AkSoundEngine.PostEvent(EntityStopScreemEvent, gameObject);
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                if (!targetSprite || !targetSprite.transform) { break; }
                targetSprite.transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.01f, 0.01f, 1f), elapsed / duration);
                targetSprite.transform.position = Vector3.Lerp(startPos, finalOffset, elapsed / duration);
                yield return null;
            }
            // AkSoundEngine.PostEvent("Play_CHR_muncher_eat_01", gameObject);
            // yield return new WaitForSeconds(0.15f);
            AkSoundEngine.PostEvent("Play_VO_lichA_cackle_01", gameObject);
            Vector2 BottomOffset = dummySpriteObject.transform.position;
            Vector2 TopOffset = dummySpriteObject.transform.position + new Vector3(1, 1);
            Color TargetColor = new Color(0.5f, 0.1f, 0.1f);
            GlobalSparksDoer.DoRandomParticleBurst(5, BottomOffset, TopOffset, new Vector3(-1, 1), 70f, 0.5f, null, new float?(0.75f), new Color?(TargetColor), GlobalSparksDoer.SparksType.BLOODY_BLOOD);
            GlobalSparksDoer.DoRandomParticleBurst(5, BottomOffset, TopOffset, Vector3.left, 70f, 0.5f, null, new float?(1.5f), new Color?(TargetColor), GlobalSparksDoer.SparksType.BLOODY_BLOOD);
            GlobalSparksDoer.DoRandomParticleBurst(5, BottomOffset, TopOffset, Vector3.left, 70f, 0.5f, null, new float?(2.25f), new Color?(TargetColor), GlobalSparksDoer.SparksType.BLOODY_BLOOD);
            GlobalSparksDoer.DoRandomParticleBurst(5, BottomOffset, TopOffset, new Vector3(-1, -1), 70f, 0.5f, null, new float?(3), new Color?(TargetColor), GlobalSparksDoer.SparksType.BLOODY_BLOOD);
            yield return new WaitForSeconds(1);
            Pixelator.Instance.FadeToBlack(0.15f, false, 0f);
            yield return new WaitForSeconds(0.3f);
            // AkSoundEngine.PostEvent("Play_CHR_muncher_chew_01", gameObject);
            // yield return new WaitForSeconds(4);;
            Destroy(dummySpriteObject);
            m_Player.ToggleRenderer(true, "got eaten");
            m_Player.ToggleGunRenderers(true, "got eaten");
            m_Player.ToggleHandRenderers(true, "got eaten");
            m_Player.ClearAllInputOverrides();
            // GameManager.Instance.LoadCustomLevel("tt_belly");
            AkSoundEngine.PostEvent("Stop_MUS_All", gameObject);
            AkSoundEngine.PostEvent("Stop_EX_MUS_All", gameObject);
            GameManager.Instance.LoadNextLevel();
            yield break;
        }

        
        protected override void OnDestroy() {
            // Incase something kills the entity before it finishes the process of activating floor transition.
            // Not normally possible but who knows given all the modded items floating about. :P
            if (IsOnBackRoomsFloor && m_PlayerEaten && m_Player) {
                m_Player.ToggleRenderer(true, "got eaten");
                m_Player.ToggleGunRenderers(true, "got eaten");
                m_Player.ToggleHandRenderers(true, "got eaten");
                m_Player.ClearAllInputOverrides();
            }
            AkSoundEngine.PostEvent(EntityStopScreemEvent, gameObject);
            if (specRigidbody && IsOnBackRoomsFloor)specRigidbody.OnPreRigidbodyCollision -= OnPreRigidBodyCollision;
            base.OnDestroy();
        }
    }
}

