using Dungeonator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ItemAPI { 

    public class ExpandRedScarf : PassiveItem {

        public static BlinkPassiveItem m_BlinkPassive;

        public static GameObject EXRedScarfObject;
        
        public static void Init(AssetBundle expandSharedAssets1) {
            
            m_BlinkPassive = PickupObjectDatabase.GetById(436).GetComponent<BlinkPassiveItem>();
            
            if (!m_BlinkPassive) { return; }

            EXRedScarfObject = expandSharedAssets1.LoadAsset<GameObject>("Bloodied Scarf");
            EXRedScarfObject.AddComponent<ExpandRedScarf>();
            EXRedScarfObject.AddComponent<tk2dSprite>();
            ExpandUtility.DuplicateSprite(EXRedScarfObject.GetComponent<tk2dSprite>(), m_BlinkPassive.GetComponent<tk2dSprite>());

            ExpandRedScarf redScarf = EXRedScarfObject.GetComponent<ExpandRedScarf>();
            ItemBuilder.SetupItem(redScarf, "Blink Away", "Dodge roll is augmented with a blink\n\nThis simple scarf was once worn by a skilled assassin. Betrayed by his brothers and assumed dead...", "ex");
            ItemBuilder.AddPassiveStatModifier(redScarf, PlayerStats.StatType.ReloadSpeed, 1.3f, StatModifier.ModifyMethod.MULTIPLICATIVE);
            // redScarf.PickupObjectId = m_BlinkPassive.PickupObjectId;
            redScarf.itemName = "Red Bandana";
            redScarf.PickupObjectId = 436;
            redScarf.quality = m_BlinkPassive.quality;
            if (!ExpandSettings.EnableEXItems) { redScarf.quality = ItemQuality.EXCLUDED; }
            redScarf.additionalMagnificenceModifier = m_BlinkPassive.additionalMagnificenceModifier;
            redScarf.ItemSpansBaseQualityTiers = m_BlinkPassive.ItemSpansBaseQualityTiers;
            redScarf.ItemRespectsHeartMagnificence = m_BlinkPassive.ItemRespectsHeartMagnificence;
            redScarf.associatedItemChanceMods = m_BlinkPassive.associatedItemChanceMods;
            redScarf.contentSource = m_BlinkPassive.contentSource;
            redScarf.ShouldBeExcludedFromShops = m_BlinkPassive.ShouldBeExcludedFromShops;
            redScarf.CanBeDropped = m_BlinkPassive.CanBeDropped;
            redScarf.PreventStartingOwnerFromDropping = m_BlinkPassive.PreventStartingOwnerFromDropping;
            redScarf.PersistsOnDeath = m_BlinkPassive.PersistsOnDeath;
            redScarf.RespawnsIfPitfall = m_BlinkPassive.RespawnsIfPitfall;
            redScarf.PreventStartingOwnerFromDropping = m_BlinkPassive.PreventStartingOwnerFromDropping;
            redScarf.IgnoredByRat = m_BlinkPassive.IgnoredByRat;
            redScarf.SaveFlagToSetOnAcquisition = m_BlinkPassive.SaveFlagToSetOnAcquisition;
            redScarf.ForcedPositionInAmmonomicon = m_BlinkPassive.ForcedPositionInAmmonomicon;
            redScarf.UsesCustomCost = m_BlinkPassive.UsesCustomCost;
            redScarf.CustomCost = m_BlinkPassive.CustomCost;
            redScarf.PersistsOnPurchase = m_BlinkPassive.PersistsOnPurchase;
            redScarf.CanBeSold = m_BlinkPassive.CanBeSold;
            redScarf.passiveStatModifiers = m_BlinkPassive.passiveStatModifiers;
            redScarf.ArmorToGainOnInitialPickup = m_BlinkPassive.ArmorToGainOnInitialPickup;
            redScarf.minimapIcon = m_BlinkPassive.minimapIcon;
            redScarf.DodgeRollTimeMultiplier = m_BlinkPassive.DodgeRollTimeMultiplier;
            redScarf.DodgeRollDistanceMultiplier = m_BlinkPassive.DodgeRollDistanceMultiplier;
            redScarf.AdditionalInvulnerabilityFrames = m_BlinkPassive.AdditionalInvulnerabilityFrames;
            redScarf.ScarfPrefab = m_BlinkPassive.ScarfPrefab;
            redScarf.BlinkpoofVfx = m_BlinkPassive.BlinkpoofVfx;

            // Try to prevent original item from showing up. (can still be accessed via MTG console's give command however)
            if (ExpandSettings.EnableEXItems) { m_BlinkPassive.quality = ItemQuality.EXCLUDED; }
        }
        
        public float DodgeRollTimeMultiplier;        
        public float DodgeRollDistanceMultiplier;
        
        public int AdditionalInvulnerabilityFrames;

        public ScarfAttachmentDoer ScarfPrefab;
        public GameObject BlinkpoofVfx;

        private bool m_CurrentlyBlinking;

        private float m_timeHeldBlinkButton;
        private Vector2 m_cachedBlinkPosition;
        private Vector2 lockedDodgeRollDirection;
        private tk2dSprite m_extantBlinkShadow;

        private ScarfAttachmentDoer m_scarf;
        private AfterImageTrailController afterimage;


        public override void Pickup(PlayerController player) {
            if (m_pickedUp) { return; }
            m_CurrentlyBlinking = false;
            m_cachedBlinkPosition = player.transform.position;
            // if (player.IsDodgeRolling) { player.ForceStopDodgeRoll(); }
            if (ScarfPrefab) {
                m_scarf = Instantiate(ScarfPrefab.gameObject).GetComponent<ScarfAttachmentDoer>();
                m_scarf.Initialize(player);
            }
            if (!ActiveFlagItems.ContainsKey(player)) {
                ActiveFlagItems.Add(player, new Dictionary<Type, int>());
            }
            if (!ActiveFlagItems[player].ContainsKey(GetType())) {
                ActiveFlagItems[player].Add(GetType(), 1);
            } else {
                ActiveFlagItems[player][GetType()] = ActiveFlagItems[player][GetType()] + 1;
            }
            afterimage = player.gameObject.AddComponent<AfterImageTrailController>();
            afterimage.spawnShadows = false;
            afterimage.shadowTimeDelay = 0.05f;
            afterimage.shadowLifetime = 0.3f;
            afterimage.minTranslation = 0.05f;
            afterimage.dashColor = Color.black;
            afterimage.maxEmission = 0f;
            afterimage.minEmission = 0f;
            afterimage.OverrideImageShader = ShaderCache.Acquire("Brave/Internal/DownwellAfterImage");
            base.Pickup(player);
        }
        
        protected override void Update() {
            if (!GameManager.HasInstance || GameManager.Instance.IsLoadingLevel || Dungeon.IsGenerating || GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.END_TIMES) {
                 return;
            }
            if (Owner && m_pickedUp) { HandleBlink(Owner); }
            base.Update();
        }

        public void HandleBlink(PlayerController Owner) {
            if (GameManager.Instance.Dungeon && GameManager.Instance.Dungeon.IsEndTimes) {
                ClearBlinkShadow();
                return;
            }
            if (Owner.WasPausedThisFrame) {
                ClearBlinkShadow();
                return;
            }

            // if (!CheckDodgeRollDepth(Owner)) { return; }

            /*PlayerController.DodgeRollState? m_dodgeRollState = ReflectionHelpers.ReflectGetField<PlayerController.DodgeRollState>(typeof(PlayerController), "m_dodgeRollState", Owner);
            if (m_dodgeRollState.HasValue) {
                if (m_dodgeRollState == PlayerController.DodgeRollState.AdditionalDelay) { return; }
            }*/           
            // if (Owner.IsFlying && !CanDodgeRollWhileFlying(Owner)) { return; }

            BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(Owner.PlayerIDX);

            GungeonActions m_activeActions = ReflectionHelpers.ReflectGetField<GungeonActions>(typeof(PlayerController), "m_activeActions", Owner);
            Vector2 currentDirection = AdjustInputVector(m_activeActions.Move.Vector, BraveInput.MagnetAngles.movementCardinal, BraveInput.MagnetAngles.movementOrdinal);

            bool WillBlink = false;
            bool flag2 = false;            
            if (instanceForPlayer.GetButtonDown(GungeonActions.GungeonActionType.DodgeRoll)) {
                flag2 = true;
            }
            if (instanceForPlayer.ActiveActions.DodgeRollAction.IsPressed) {
                m_timeHeldBlinkButton += BraveTime.DeltaTime;
                if (m_timeHeldBlinkButton < 0.3f) {
                    m_cachedBlinkPosition = Owner.specRigidbody.UnitCenter;
                } else {
                    Vector2 cachedBlinkPosition = m_cachedBlinkPosition;
                    bool IsKeyboardAndMouse = BraveInput.GetInstanceForPlayer(Owner.PlayerIDX).IsKeyboardAndMouse(false);
                    if (IsKeyboardAndMouse) {
                        m_cachedBlinkPosition = Owner.unadjustedAimPoint.XY() - (Owner.CenterPosition - Owner.specRigidbody.UnitCenter);
                    } else {                        
                        if (m_activeActions != null) { m_cachedBlinkPosition += m_activeActions.Aim.Vector.normalized * BraveTime.DeltaTime * 15f; }
                    }
                    m_cachedBlinkPosition = BraveMathCollege.ClampToBounds(m_cachedBlinkPosition, GameManager.Instance.MainCameraController.MinVisiblePoint, GameManager.Instance.MainCameraController.MaxVisiblePoint);
                    
                    UpdateBlinkShadow(Owner, m_cachedBlinkPosition - cachedBlinkPosition, CanBlinkToPoint(Owner, m_cachedBlinkPosition, Owner.transform.position.XY() - Owner.specRigidbody.UnitCenter));                                              
                }
            } else if (instanceForPlayer.ActiveActions.DodgeRollAction.WasReleased || flag2) {
                if (m_timeHeldBlinkButton >= 0.3f) { WillBlink = true; }
            } else {
                m_timeHeldBlinkButton = 0f;
            }
            if (WillBlink) {                
                /*if (m_timeHeldBlinkButton < 0.3f) {
                    m_cachedBlinkPosition = Owner.specRigidbody.UnitCenter + currentDirection.normalized * Owner.rollStats.GetModifiedDistance(Owner);
                }*/
                Owner.healthHaver.TriggerInvulnerabilityPeriod(0.001f);
                // Owner.rollStats.blinkDistanceMultiplier = 1f;
                Owner.DidUnstealthyAction();
                BlinkToPoint(Owner, m_cachedBlinkPosition);
                m_timeHeldBlinkButton = 0;
                WillBlink = false;
            }
        }

        public void BlinkToPoint(PlayerController Owner, Vector2 targetPoint) {
            m_cachedBlinkPosition = targetPoint;
            lockedDodgeRollDirection = (m_cachedBlinkPosition - Owner.specRigidbody.UnitCenter).normalized;
            Vector2 centerOffset = Owner.transform.position.XY() - Owner.specRigidbody.UnitCenter;
            bool flag = CanBlinkToPoint(Owner, m_cachedBlinkPosition, centerOffset);
            ClearBlinkShadow();
            if (flag) {
                m_CurrentlyBlinking = true;
                StartCoroutine(HandleBlinkTeleport(Owner, m_cachedBlinkPosition, lockedDodgeRollDirection));
            } else {
                Vector2 a = Owner.specRigidbody.UnitCenter - m_cachedBlinkPosition;
                float num = a.magnitude;
                Vector2? vector = null;
                float num2 = 0f;
                a = a.normalized;
                while (num > 0f) {
                    num2 += 1f;
                    num -= 1f;
                    Vector2 vector2 = m_cachedBlinkPosition + a * num2;
                    if (CanBlinkToPoint(Owner, vector2 + new Vector2(1, 0), centerOffset)) {
                        vector = new Vector2?(vector2);
                        break;
                    }
                }
                if (vector != null) {
                    Vector2 normalized = (vector.Value - Owner.specRigidbody.UnitCenter).normalized;
                    float num3 = Vector2.Dot(normalized, lockedDodgeRollDirection);
                    if (num3 > 0f) {
                        m_cachedBlinkPosition = vector.Value;
                        m_CurrentlyBlinking = true;
                        StartCoroutine(HandleBlinkTeleport(Owner, m_cachedBlinkPosition, lockedDodgeRollDirection));
                    }
                }
            }
        }

        private IEnumerator HandleBlinkTeleport(PlayerController Owner, Vector2 targetPoint, Vector2 targetDirection) {

            targetPoint = (targetPoint - new Vector2(0.75f, 0.125f));

            OnBlinkStarted(Owner, targetDirection);
            List<AIActor> m_rollDamagedEnemies = ReflectionHelpers.ReflectGetField<List<AIActor>>(typeof(PlayerController), "m_rollDamagedEnemies", Owner);
            if (m_rollDamagedEnemies != null) {
                m_rollDamagedEnemies.Clear();
                FieldInfo m_rollDamagedEnemiesClear = typeof(PlayerController).GetField("m_rollDamagedEnemies", BindingFlags.Instance | BindingFlags.NonPublic);
                m_rollDamagedEnemiesClear.SetValue(Owner, m_rollDamagedEnemies);
            }

            if (Owner.knockbackDoer) { Owner.knockbackDoer.ClearContinuousKnockbacks(); }
            Owner.IsEthereal = true;
            Owner.IsVisible = false;
            float RecoverySpeed = GameManager.Instance.MainCameraController.OverrideRecoverySpeed;
            bool IsLerping = GameManager.Instance.MainCameraController.IsLerping;
            yield return new WaitForSeconds(0.1f);
            GameManager.Instance.MainCameraController.OverrideRecoverySpeed = 80f;
            GameManager.Instance.MainCameraController.IsLerping = true;
            if (Owner.IsPrimaryPlayer) {
                GameManager.Instance.MainCameraController.UseOverridePlayerOnePosition = true;
                GameManager.Instance.MainCameraController.OverridePlayerOnePosition = targetPoint;                
                yield return new WaitForSeconds(0.12f);
                Owner.specRigidbody.Velocity = Vector2.zero;
                Owner.specRigidbody.Position = new Position(targetPoint);
                GameManager.Instance.MainCameraController.UseOverridePlayerOnePosition = false;
            } else {
                GameManager.Instance.MainCameraController.UseOverridePlayerTwoPosition = true;
                GameManager.Instance.MainCameraController.OverridePlayerTwoPosition = targetPoint;                
                yield return new WaitForSeconds(0.12f);
                Owner.specRigidbody.Velocity = Vector2.zero;
                Owner.specRigidbody.Position = new Position(targetPoint);
                GameManager.Instance.MainCameraController.UseOverridePlayerTwoPosition = false;
            }
            GameManager.Instance.MainCameraController.OverrideRecoverySpeed = RecoverySpeed;
            GameManager.Instance.MainCameraController.IsLerping = IsLerping;
            Owner.IsEthereal = false;
            Owner.IsVisible = true;
            m_CurrentlyBlinking = false;
            if (Owner.CurrentFireMeterValue <= 0f) { yield break; }
            Owner.CurrentFireMeterValue = Mathf.Max(0f, Owner.CurrentFireMeterValue -= 0.5f);
            if (Owner.CurrentFireMeterValue == 0f) {
                Owner.IsOnFire = false;
                yield break;
            }
            yield break;
        }

        protected void ClearBlinkShadow() {
            if (m_extantBlinkShadow) {
                Destroy(m_extantBlinkShadow.gameObject);
                m_extantBlinkShadow = null;
            }
        }

        protected void UpdateBlinkShadow(PlayerController Owner, Vector2 delta, bool canWarpDirectly) {
            int? m_overrideFlatColorID = ReflectionHelpers.ReflectGetField<int>(typeof(PlayerController), "m_overrideFlatColorID", Owner);
            if (m_extantBlinkShadow == null) {
                GameObject go = new GameObject("blinkshadow");
                m_extantBlinkShadow = tk2dSprite.AddComponent(go, Owner.sprite.Collection, Owner.sprite.spriteId);
                m_extantBlinkShadow.transform.position = m_cachedBlinkPosition + (Owner.sprite.transform.position.XY() - Owner.specRigidbody.UnitCenter);
                tk2dSpriteAnimator tk2dSpriteAnimator = m_extantBlinkShadow.gameObject.AddComponent<tk2dSpriteAnimator>();
                tk2dSpriteAnimator.Library = Owner.spriteAnimator.Library;     
                if (m_overrideFlatColorID.HasValue) { m_extantBlinkShadow.renderer.material.SetColor(m_overrideFlatColorID.Value, (!canWarpDirectly) ? new Color(0.4f, 0f, 0f, 1f) : new Color(0.25f, 0.25f, 0.25f, 1f)); }
                m_extantBlinkShadow.usesOverrideMaterial = true;
                m_extantBlinkShadow.FlipX = Owner.sprite.FlipX;
                m_extantBlinkShadow.FlipY = Owner.sprite.FlipY;
                Owner.OnBlinkShadowCreated?.Invoke(m_extantBlinkShadow);
            } else {
                if (delta == Vector2.zero) {
                    m_extantBlinkShadow.spriteAnimator.Stop();
                    m_extantBlinkShadow.SetSprite(Owner.sprite.Collection, Owner.sprite.spriteId);
                } else {
                    float? m_currentGunAngle = null;
                    try { m_currentGunAngle = ReflectionHelpers.ReflectGetField<float>(typeof(PlayerController), "m_currentGunAngle", Owner); } catch (Exception) { }
                    string baseAnimationName = string.Empty;
                    if (m_currentGunAngle.HasValue) {
                        baseAnimationName =  GetBaseAnimationName(Owner, delta, m_currentGunAngle.Value, false, true);
                    }
                    if (!string.IsNullOrEmpty(baseAnimationName) && !m_extantBlinkShadow.spriteAnimator.IsPlaying(baseAnimationName)) {
                        m_extantBlinkShadow.spriteAnimator.Play(baseAnimationName);
                    }
                }
                if (m_overrideFlatColorID.HasValue) { m_extantBlinkShadow.renderer.material.SetColor(m_overrideFlatColorID.Value, (!canWarpDirectly) ? new Color(0.4f, 0f, 0f, 1f) : new Color(0.25f, 0.25f, 0.25f, 1f)); }
                m_extantBlinkShadow.transform.position = m_cachedBlinkPosition + (Owner.sprite.transform.position.XY() - Owner.specRigidbody.UnitCenter);
            }
            m_extantBlinkShadow.FlipX = Owner.sprite.FlipX;
            m_extantBlinkShadow.FlipY = Owner.sprite.FlipY;
        }

        protected virtual string GetBaseAnimationName(PlayerController Owner, Vector2 v, float gunAngle, bool invertThresholds = false, bool forceTwoHands = false) {            
            bool RenderBodyHand = m_RenderBodyHand(Owner);

            string text = string.Empty;
            bool flag = Owner.CurrentGun != null;
            if (flag && Owner.CurrentGun.Handedness == GunHandedness.NoHanded) { forceTwoHands = true; }
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.END_TIMES) { flag = false; }
            float num = 155f;
            float num2 = 25f;
            if (invertThresholds) { num = -155f; num2 -= 50f; }
            float num3 = 120f;
            float num4 = 60f;
            float num5 = -60f;
            float num6 = -120f;
            bool flag2 = gunAngle <= num && gunAngle >= num2;
            if (invertThresholds) { flag2 = (gunAngle <= num || gunAngle >= num2); }
            if (Owner.IsGhost) {
                if (flag2) {
                    if (gunAngle < num3 && gunAngle >= num4) {
                        text = "ghost_idle_back";
                    } else {
                        float num7 = 105f;
                        if (Mathf.Abs(gunAngle) > num7) {
                            text = "ghost_idle_back_left";
                        } else {
                            text = "ghost_idle_back_right";
                        }
                    }
                } else if (gunAngle <= num5 && gunAngle >= num6) {
                    text = "ghost_idle_front";
                } else {
                    float num8 = 105f;
                    if (Mathf.Abs(gunAngle) > num8) {
                        text = "ghost_idle_left";
                    } else {
                        text = "ghost_idle_right";
                    }
                }
            } else if (Owner.IsFlying) {
                if (flag2) {
                    if (gunAngle < num3 && gunAngle >= num4) {
                        text = "jetpack_up";
                    } else {
                        text = "jetpack_right_bw";
                    }
                } else if (gunAngle <= num5 && gunAngle >= num6) {
                    text = ((!RenderBodyHand) ? "jetpack_down" : "jetpack_down_hand");
                } else {
                    text = ((!RenderBodyHand) ? "jetpack_right" : "jetpack_right_hand");
                }
            } else if (v == Vector2.zero || Owner.IsStationary) {
                if (Owner.IsPetting) {
                    text = "pet";
                } else if (flag2) {
                    if (gunAngle < num3 && gunAngle >= num4) {
                        string text2 = ((!forceTwoHands && flag) || Owner.ForceHandless) ? ((!RenderBodyHand) ? "idle_backward" : "idle_backward_hand") : "idle_backward_twohands";
                        text = text2;
                    } else {
                        string text3 = ((!forceTwoHands && flag) || Owner.ForceHandless) ? "idle_bw" : "idle_bw_twohands";
                        text = text3;
                    }
                } else if (gunAngle <= num5 && gunAngle >= num6) {
                    string text4 = ((!forceTwoHands && flag) || Owner.ForceHandless) ? ((!RenderBodyHand) ? "idle_forward" : "idle_forward_hand") : "idle_forward_twohands";
                    text = text4;
                } else {
                    string text5 = ((!forceTwoHands && flag) || Owner.ForceHandless) ? ((!RenderBodyHand) ? "idle" : "idle_hand") : "idle_twohands";
                    text = text5;
                }
            } else if (flag2) {
                string text6 = ((!forceTwoHands && flag) || Owner.ForceHandless) ? "run_right_bw" : "run_right_bw_twohands";
                if (gunAngle < num3 && gunAngle >= num4) {
                    text6 = (((!forceTwoHands && flag) || Owner.ForceHandless) ? ((!RenderBodyHand) ? "run_up" : "run_up_hand") : "run_up_twohands");
                }
                text = text6;
            } else {
                string text7 = "run_right";
                if (gunAngle <= num5 && gunAngle >= num6) { text7 = "run_down"; }
                if ((forceTwoHands || !flag) && !Owner.ForceHandless) {
                    text7 += "_twohands";
                } else if (RenderBodyHand) {
                    text7 += "_hand";
                }
                text = text7;
            }
            if (Owner.UseArmorlessAnim && !Owner.IsGhost) { text += "_armorless"; }
            return text;
        }

        protected bool CanBlinkToPoint(PlayerController Owner, Vector2 point, Vector2 centerOffset) {
            bool flag = Owner.IsValidPlayerPosition(point + centerOffset);
            if (flag && Owner.CurrentRoom != null) {
                CellData cellData = GameManager.Instance.Dungeon.data[point.ToIntVector2(VectorConversions.Floor)];
                if (cellData == null) { return false; }
                RoomHandler nearestRoom = cellData.nearestRoom;
                if (cellData.type != CellType.FLOOR) { flag = false; }
                if (Owner.CurrentRoom.IsSealed && nearestRoom != Owner.CurrentRoom) { flag = false; }
                if (Owner.CurrentRoom.IsSealed && cellData.isExitCell) { flag = false; }
                if (nearestRoom.visibility == RoomHandler.VisibilityStatus.OBSCURED || nearestRoom.visibility == RoomHandler.VisibilityStatus.REOBSCURED) { flag = false; }
            }
            if (Owner.CurrentRoom == null) { flag = false; }
            if (Owner.IsDodgeRolling | Owner.IsFalling | Owner.IsCurrentlyCoopReviving | Owner.IsInMinecart | Owner.IsInputOverridden) { return false; }
            return flag;
        }

        private bool m_RenderBodyHand(PlayerController Onwer) {
                return !Onwer.ForceHandless && Onwer.CurrentSecondaryGun == null && (Onwer.CurrentGun == null || Onwer.CurrentGun.Handedness != GunHandedness.TwoHanded);
        }
        
        private bool CheckDodgeRollDepth(PlayerController Owner) {
            if (Owner.IsSlidingOverSurface && !Owner.DodgeRollIsBlink) {
                return !Owner.CurrentRoom.IsShop && GameManager.Instance.CurrentLevelOverrideState != GameManager.LevelOverrideState.TUTORIAL;
            }
            bool flag = IsFlagSetForCharacter(Owner, typeof(PegasusBootsItem));
            int num = (!flag) ? 1 : 2;
            if (flag && Owner.HasActiveBonusSynergy(CustomSynergyType.TRIPLE_JUMP, false)) { num++; }
            num = 1;
            int? m_currentDodgeRollDepth = ReflectionHelpers.ReflectGetField<int>(typeof(PlayerController), "m_currentDodgeRollDepth", Owner);
            if (!m_currentDodgeRollDepth.HasValue) { m_currentDodgeRollDepth = 0; }
            return !Owner.IsDodgeRolling || m_currentDodgeRollDepth.Value < num;
        }

        protected virtual bool CanDodgeRollWhileFlying(PlayerController Owner) {
            return Owner.AdditionalCanDodgeRollWhileFlying.Value || (ActiveFlagItems.ContainsKey(Owner) && ActiveFlagItems[Owner].ContainsKey(typeof(WingsItem)));
        }
        
        protected Vector2 AdjustInputVector(Vector2 rawInput, float cardinalMagnetAngle, float ordinalMagnetAngle) {
            float num = BraveMathCollege.ClampAngle360(BraveMathCollege.Atan2Degrees(rawInput));
            float num2 = num % 90f;
            float num3 = (num + 45f) % 90f;
            float num4 = 0f;
            if (cardinalMagnetAngle > 0f) {
                if (num2 < cardinalMagnetAngle) {
                    num4 = -num2;
                } else if (num2 > 90f - cardinalMagnetAngle) {
                    num4 = 90f - num2;
                }
            }
            if (ordinalMagnetAngle > 0f) {
                if (num3 < ordinalMagnetAngle) {
                    num4 = -num3;
                } else if (num3 > 90f - ordinalMagnetAngle) {
                    num4 = 90f - num3;
                }
            }
            num += num4;
            return (Quaternion.Euler(0f, 0f, num) * Vector3.right).XY() * rawInput.magnitude;
        }
        
        private void OnBlinkStarted(PlayerController obj, Vector2 dirVec) {
            if (GameManager.Instance.Dungeon && GameManager.Instance.Dungeon.IsEndTimes) { return; }
            obj.StartCoroutine(HandleAfterImageStop(obj));
        }

        private IEnumerator HandleAfterImageStop(PlayerController player) {
            player.PlayEffectOnActor(BlinkpoofVfx, Vector3.zero, false, true, false);
            AkSoundEngine.PostEvent("Play_CHR_ninja_dash_01", gameObject);
            afterimage.spawnShadows = true;
            while (m_CurrentlyBlinking) { yield return null; }
            if (afterimage) { afterimage.spawnShadows = false; }
            player.PlayEffectOnActor(BlinkpoofVfx, Vector3.zero, false, true, false);
            AkSoundEngine.PostEvent("Play_CHR_ninja_dash_01", gameObject);
            yield break;
        }

        public override DebrisObject Drop(PlayerController player) {
            DebrisObject debrisObject = base.Drop(player);
            ClearBlinkShadow();
            m_CurrentlyBlinking = false;
            m_cachedBlinkPosition = player.transform.position;
            if (ActiveFlagItems[player].ContainsKey(GetType())) {
                ActiveFlagItems[player][GetType()] = Mathf.Max(0, ActiveFlagItems[player][GetType()] - 1);
                if (ActiveFlagItems[player][GetType()] == 0) { ActiveFlagItems[player].Remove(GetType()); }
            }
            if (m_scarf) {
                Destroy(m_scarf.gameObject);
                m_scarf = null;
            }
            if (afterimage) { Destroy(afterimage); }
            afterimage = null;
            debrisObject.GetComponent<ExpandRedScarf>().m_pickedUpThisRun = true;
            return debrisObject;
        }

        protected override void OnDestroy() {
            ClearBlinkShadow();
            m_CurrentlyBlinking = false;
            if (m_scarf) {
                Destroy(m_scarf.gameObject);
                m_scarf = null;
            }
            if (m_pickedUp && m_owner && ActiveFlagItems != null && ActiveFlagItems.ContainsKey(m_owner) && ActiveFlagItems[m_owner].ContainsKey(GetType())) {
                ActiveFlagItems[m_owner][GetType()] = Mathf.Max(0, ActiveFlagItems[m_owner][GetType()] - 1);
                if (ActiveFlagItems[m_owner][GetType()] == 0) {
                    ActiveFlagItems[m_owner].Remove(GetType());
                }
            }
            if (m_owner != null) {
                PlayerController owner = m_owner;
                if (afterimage) { Destroy(afterimage); }
                afterimage = null;
            }
            base.OnDestroy();
        }
    }
}

