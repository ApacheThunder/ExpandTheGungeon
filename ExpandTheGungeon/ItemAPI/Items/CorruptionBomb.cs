using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ExpandComponents;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ItemAPI {

    public class CorruptionBomb : PlayerItem {
                        
        public static GameObject corruptionbomb;

        public static int CorruptionBombPickupID;

        private static GameObject glitchBombSpawnObject;
        private static GameObject glitchBombMinimapObject;
        
        private List<int> CommonItemDrops = new List<int>() { 73, 127, 224, 565 };
        private List<int> RareItemDrops = new List<int>() { 74, 276, 137, 104, 63, 78, 67, 120, 600 };
        
        public static void Init(AssetBundle expandSharedAssets1) {

            string name = "Corruption Bomb";
            corruptionbomb = expandSharedAssets1.LoadAsset<GameObject>("EXCorruptionBomb");
            corruptionbomb.name = name;
            SpriteSerializer.AddSpriteToObject(corruptionbomb, ExpandPrefabs.EXItemCollection, "corruptionbomb");
            
            CorruptionBomb corruptionBombComponent = corruptionbomb.AddComponent<CorruptionBomb>();
            
            string shortDesc = "Causes widespread corruption.";
            string longDesc = "It is said that a mysterious gungeoneer from a far away\nuniverse brought this item into the Gungeon.\n\nHis excessive use of this item may be responsible for the corrupted floors some gungeoneers may stumble upon.";
            ItemBuilder.SetupItem(corruptionBombComponent, shortDesc, longDesc, "ex");
            ItemBuilder.SetCooldownType(corruptionBombComponent, ItemBuilder.CooldownType.Damage, 750f);
            corruptionBombComponent.IgnoredByRat = true;
            corruptionBombComponent.consumable = false;
            corruptionBombComponent.canStack = true;
            corruptionBombComponent.numberOfUses = 3;
            corruptionBombComponent.m_cachedNumberOfUses = 3;
            corruptionBombComponent.UsesNumberOfUsesBeforeCooldown = true;
            corruptionBombComponent.quality = ItemQuality.S;
            if (!ExpandSettings.EnableEXItems) { corruptionBombComponent.quality = ItemQuality.EXCLUDED; }
            CorruptionBombPickupID = corruptionBombComponent.PickupObjectId;


            // Bomb Minimap Icon Object
            glitchBombMinimapObject = expandSharedAssets1.LoadAsset<GameObject>("EXCorruptionBomb_MinimapIcon");
            SpriteSerializer.AddSpriteToObject(glitchBombMinimapObject, ExpandPrefabs.EXItemCollection, "corruptionbomb_minimapicon");
            
            corruptionBombComponent.minimapIcon = glitchBombMinimapObject;

            List<string> spritePaths = new List<string>() {
                "corruptionbomb_spin_01",
                "corruptionbomb_spin_02",
                "corruptionbomb_spin_03",
                "corruptionbomb_spin_04",
                "corruptionbomb_spin_05",
                "corruptionbomb_spin_06",
                "corruptionbomb_spin_07",
                "corruptionbomb_spin_08",
                "corruptionbomb_spin_09",
                "corruptionbomb_spin_10"
            };

            // Bomb Spawn FX Object
            glitchBombSpawnObject = expandSharedAssets1.LoadAsset<GameObject>("EXCorruptionBomb_Projectile");
            SpriteSerializer.AddSpriteToObject(glitchBombSpawnObject, ExpandPrefabs.EXItemCollection, "corruptionbomb_spin_01");

            ExpandUtility.GenerateSpriteAnimator(glitchBombSpawnObject, AlwaysIgnoreTimeScale: true);
            ExpandUtility.AddAnimation(glitchBombSpawnObject.GetComponent<tk2dSpriteAnimator>(), ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), spritePaths, "CorruptionSpawn", frameRate: 7);
        }
        

        public override void Pickup(PlayerController player) {
            base.Pickup(player);
            player.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Combine(player.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(AnyDamageDealt));
            IgnoredByRat = false;
            if (!ExpandSharedHooks.IsHooksInstalled) { ExpandSharedHooks.InstallPrimaryHooks(); }
            AkSoundEngine.PostEvent("Play_EX_CorruptionBombPickup_01", player.gameObject);
        }

        protected override void OnPreDrop(PlayerController user) {
            base.OnPreDrop(user);
            if (user) {
                user.OnAnyEnemyReceivedDamage = (Action<float, bool, HealthHaver>)Delegate.Remove(user.OnAnyEnemyReceivedDamage, new Action<float, bool, HealthHaver>(AnyDamageDealt));
            }
        }
        
        protected override void AfterCooldownApplied(PlayerController user) {
            base.AfterCooldownApplied(user);
            numberOfUses = 3;
        }

        public override bool CanBeUsed(PlayerController user) {
            if (!user || user.InExitCell || user.CurrentRoom == null || CurrentlyInUse) { return false; }
            return base.CanBeUsed(user);
        }
        
        /*public bool NotCurrentlyUsable(PlayerController user) {
            if (CurrentlyInUse) { return true; }
            return false;
        }*/

        public bool CurrentlyInUse = false;

        public bool CorruptionAlreadyExists(PlayerController user) {
            if (ExpandStaticReferenceManager.AllCorruptionSoundObjects.Count > 0) {
                foreach (GameObject soundObject in ExpandStaticReferenceManager.AllCorruptionSoundObjects) {
                    if (soundObject && user.CurrentRoom != null && soundObject.transform.position.GetAbsoluteRoom() != null) {
                        if (soundObject.transform.position.GetAbsoluteRoom() == user.CurrentRoom) { return true; }
                    }
                }
            }
            return false;
        }

        protected override void DoEffect(PlayerController user) {
            AkSoundEngine.PostEvent("Play_EX_CorruptionBombFire_01", user.gameObject);
            CurrentlyInUse = true;
            CleanUpRoom(user);
            GameManager.Instance.StartCoroutine(HandleSpawnAnimation(user));
        }

        private void CleanUpRoom(PlayerController user) {
            if (user.GetAbsoluteParentRoom() == null) { return; }

            RoomHandler m_CurrentRoom = user.GetAbsoluteParentRoom();
            
            if (StaticReferenceManager.AllDebris.Count > 0) {
                for (int i = 0; i < StaticReferenceManager.AllDebris.Count; i++) {
                    if (StaticReferenceManager.AllDebris[i] && StaticReferenceManager.AllDebris[i].gameObject && StaticReferenceManager.AllDebris[i].gameObject.transform) {
                        if (StaticReferenceManager.AllDebris[i].transform.position.GetAbsoluteRoom() != null && StaticReferenceManager.AllDebris[i].gameObject.transform.position.GetAbsoluteRoom() == m_CurrentRoom) {
                            if (!string.IsNullOrEmpty(StaticReferenceManager.AllDebris[i].gameObject.name) && 
                                (StaticReferenceManager.AllDebris[i].gameObject.name.StartsWith("EX_RoomCorruption_") | StaticReferenceManager.AllDebris[i].gameObject.name.StartsWith("GlitchTile"))
                            ) {
                                StaticReferenceManager.AllDebris[i].ForceDestroyAndMaybeRespawn();
                            }
                        }  
                    }
                }
            }
                
            if (ExpandStaticReferenceManager.AllGlitchTiles.Count > 0) {
                for (int i = 0; i < ExpandStaticReferenceManager.AllGlitchTiles.Count; i++) {      
                    if (ExpandStaticReferenceManager.AllGlitchTiles[i] && ExpandStaticReferenceManager.AllGlitchTiles[i].gameObject && ExpandStaticReferenceManager.AllGlitchTiles[i].gameObject.transform) {
                        if (ExpandStaticReferenceManager.AllGlitchTiles[i].gameObject.transform.position.GetAbsoluteRoom() != null && ExpandStaticReferenceManager.AllGlitchTiles[i].gameObject.transform.position.GetAbsoluteRoom() == m_CurrentRoom) {
                            Destroy(ExpandStaticReferenceManager.AllGlitchTiles[i].gameObject);
                        }
                    }
                }
            }
            
            if (ExpandStaticReferenceManager.AllCorruptionSoundObjects.Count > 0) {
                for (int i = 0; i < ExpandStaticReferenceManager.AllCorruptionSoundObjects.Count; i++) {
                    GameObject SoundObject = ExpandStaticReferenceManager.AllCorruptionSoundObjects[i];
                    if (SoundObject && SoundObject.transform) {
                        if (SoundObject.transform.position.GetAbsoluteRoom() != null && SoundObject.transform.position.GetAbsoluteRoom() == m_CurrentRoom) {
                            AkSoundEngine.PostEvent("Stop_EX_CorruptionAmbience_01", SoundObject);
                            AkSoundEngine.PostEvent("Stop_EX_CorruptionAmbience_02", SoundObject);
                            ExpandStaticReferenceManager.AllCorruptionSoundObjects.Remove(SoundObject);
                            Destroy(SoundObject);
                        }
                    }  
                }
            }
        }


        private IEnumerator HandleSpawnAnimation(PlayerController user) {
            GameObject targetObject = Instantiate(glitchBombSpawnObject, user.CenterPosition, Quaternion.identity);
            yield return null;
            tk2dSpriteAnimator bombAnimator = targetObject.GetComponent<tk2dSpriteAnimator>();
            if (bombAnimator != null && user.CurrentRoom != null) {
                Vector2 roomBottomLeft = user.CurrentRoom.area.UnitBottomLeft;
                Vector2 roomTopRight = user.CurrentRoom.area.UnitTopRight;
                Vector3 movementDirection = new Vector3(0, 3f);

                BraveInput instanceForPlayer = BraveInput.GetInstanceForPlayer(user.PlayerIDX);
                if (instanceForPlayer) {
                    if (instanceForPlayer.IsKeyboardAndMouse()) {
                        movementDirection = Vector3Extensions.XY(user.unadjustedAimPoint) - targetObject.transform.PositionVector2();
                    } else if (instanceForPlayer.ActiveActions != null) {
                        movementDirection = instanceForPlayer.ActiveActions.Aim.Vector;
                    }
                    movementDirection.Normalize();
                }
                
                float speedDivider = 20;

                movementDirection = new Vector3((movementDirection.x / speedDivider), (movementDirection.y / speedDivider));

                bombAnimator.Play("CorruptionSpawn");
                float DarkFadeTime = 0;
                float DarkhHoldTime = 1.4f;
                float FlashFadeTime = 0.3f;
                float FlashHoldTime = 0.4f;

                bool CmaeraOverridden = (GameManager.Instance.MainCameraController.UseOverridePlayerTwoPosition | GameManager.Instance.MainCameraController.UseOverridePlayerOnePosition | GameManager.Instance.MainCameraController.ManualControl);

                Pixelator.Instance.DoFinalNonFadedLayer = true;
                Pixelator.Instance.FadeToColor(DarkFadeTime, new Color(0, 0, 0, 0.6f), true, DarkhHoldTime);
                BraveTime.SetTimeScaleMultiplier(0.15f, glitchBombSpawnObject);
                if (!CmaeraOverridden) {
                    GameManager.Instance.MainCameraController.SetManualControl(true, false);
                    Pixelator.Instance.LerpToLetterbox(0.35f, 0.6f);
                }
                while (bombAnimator.Playing) {
                    targetObject.transform.position = BraveMathCollege.ClampToBounds((targetObject.transform.position += movementDirection), roomBottomLeft + Vector2.one, roomTopRight - Vector2.one);
                    GameManager.Instance.MainCameraController.OverridePosition = targetObject.transform.position;
                    yield return null;
                }
                Pixelator.Instance.DoFinalNonFadedLayer = false;
                BraveTime.ClearMultiplier(glitchBombSpawnObject);
                if (!CmaeraOverridden) {
                    GameManager.Instance.MainCameraController.SetManualControl(false, true);
                    Pixelator.Instance.LerpToLetterbox(0.5f, 0f);
                }
                AkSoundEngine.PostEvent("Play_EX_CorruptionBombExplode_01", targetObject);
                Pixelator.Instance.FadeToColor(FlashFadeTime, new Color(1, 1, 1, 0.7f), true, FlashHoldTime);
                Vector2 lastPosition = targetObject.transform.PositionVector2();
                LootEngine.DoDefaultSynergyPoof(lastPosition);
                Destroy(targetObject);
                RoomHandler m_targetRoom = lastPosition.GetAbsoluteRoom();

                if (m_targetRoom == null) {
                    GameManager.Instance.StartCoroutine(DelayedUsableTimer(1.5f));
                    yield break;
                }
                
                GameObject m_NewSoundObject = new GameObject("EX_RoomCorruption_" + UnityEngine.Random.Range(10000, 99999).ToString());
                m_NewSoundObject.transform.position = lastPosition;
                // m_NewSoundObject.transform.parent = m_targetRoom.hierarchyParent;
                m_NewSoundObject.AddComponent<ExpandCorruptedRoomAmbiencePlacable>();
                
                ExpandCorruptedRoomAmbiencePlacable m_SoundPlacable = m_NewSoundObject.GetComponent<ExpandCorruptedRoomAmbiencePlacable>();
                m_SoundPlacable.CameFromCorruptionBomb = true;
                m_SoundPlacable.GoesAwayEventually = true;
                m_SoundPlacable.ConfigureOnPlacement(m_targetRoom);

                yield return null;

                float m_prevWaveDist = 0;
                float distortionMaxRadius = 40f;
                float distortionDuration = 1.5f;
                float distortionIntensity = 1.25f;
                float distortionThickness = 0.08f;
                Exploder.DoDistortionWave(lastPosition, distortionIntensity, distortionThickness, distortionMaxRadius, distortionDuration);
                TriggerBlank(lastPosition, silent: true, breaksObjects: false, overrideForce: 0, disableDamage: true);

                List<IntVector2> m_CachedRoomCellList = new List<IntVector2>();
                
                IntVector2 roomGridBottomLeft = (m_targetRoom.area.UnitBottomLeft.ToIntVector2() - new IntVector2(2, 2));
                IntVector2 roomGrid = (m_targetRoom.area.dimensions + new IntVector2(4, 4));

                for (int X = 0; X <= roomGrid.x; X++) {
                    for (int Y = 0; Y <= roomGrid.y; Y++) {
                        IntVector2 targetPosition = (new IntVector2(X, Y) + new IntVector2(roomGridBottomLeft.x, roomGridBottomLeft.y));
                        m_CachedRoomCellList.Add(targetPosition);
                    }
                }
                

                if (m_NewSoundObject) { ExpandStaticReferenceManager.AllCorruptionSoundObjects.Add(m_NewSoundObject); }

                GameManager.Instance.StartCoroutine(DelayedUsableTimer(1.5f));

                float waveRemaining = distortionDuration - BraveTime.DeltaTime;

                List<GameObject> shrinesToProcess = new List<GameObject>();
                List<GameObject> npcsToProcess = new List<GameObject>();
                List<MinorBreakable> breakablesToProcess = new List<MinorBreakable>();
                List<GameObject> tablesToProcess = new List<GameObject>();
                List<GameObject> movableTrapsToProcess = new List<GameObject>();
                List<GameObject> hammersToProcess = new List<GameObject>();

                FlippableCover[] AllTables = FindObjectsOfType<FlippableCover>();
                ChallengeShrineController[] AllChallengeShrines = FindObjectsOfType<ChallengeShrineController>();

                while (waveRemaining > 0f) {
                    waveRemaining -= BraveTime.DeltaTime;
                    float waveDist = BraveMathCollege.LinearToSmoothStepInterpolate(0f, distortionMaxRadius, 1f - waveRemaining / distortionDuration);

                    if (m_CachedRoomCellList.Count > 0) {
                        for (int i = 0; i < m_CachedRoomCellList.Count; i++) {
                            Vector2 unitCenter = m_CachedRoomCellList[i].ToVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            bool tileSpawned = false;
                            if (unitCenter.GetAbsoluteRoom() != null && (unitCenter - new Vector2(0, 1)).GetAbsoluteRoom() == m_targetRoom) {
                                if (!tileSpawned && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                    tileSpawned = true;
                                    GenerateCorruptedTileAtPositionMaybe(GameManager.Instance.Dungeon, m_targetRoom, unitCenter.ToIntVector2(), 0.2f, true);
                                }
                            }
                        }
                    }
                    if (StaticReferenceManager.AllMinorBreakables.Count > 0) {
                        for (int i = 0; i < StaticReferenceManager.AllMinorBreakables.Count; i++) {
                            Vector2 unitCenter = StaticReferenceManager.AllMinorBreakables[i].transform.PositionVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            bool modified = false;
                            string ObjectName = StaticReferenceManager.AllMinorBreakables[i].gameObject.name;
                            if (unitCenter.GetAbsoluteRoom() != null && unitCenter.GetAbsoluteRoom() == m_targetRoom) {
                                if (!modified && (string.IsNullOrEmpty(ObjectName) | !ObjectName.StartsWith("Corrupted")) && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                    modified = true;
                                    if (UnityEngine.Random.value <= 0.4f) {
                                        tk2dBaseSprite targetSprite = StaticReferenceManager.AllMinorBreakables[i].gameObject.GetComponent<tk2dBaseSprite>();
                                        if (targetSprite) { ExpandShaders.Instance.ApplyGlitchShader(targetSprite); }
                                        if (!breakablesToProcess.Contains(StaticReferenceManager.AllMinorBreakables[i])) {
                                            breakablesToProcess.Add(StaticReferenceManager.AllMinorBreakables[i]);
                                        }
                                    } else if (UnityEngine.Random.value <= 0.6f) {
                                        StaticReferenceManager.AllMinorBreakables[i].Break();
                                    }
                                }
                            }   
                        }
                    }
                    if (StaticReferenceManager.AllLocks != null) {
                        if (StaticReferenceManager.AllLocks.Count > 0) {
                            for (int i = 0; i < StaticReferenceManager.AllLocks.Count; i++) {
                                Vector2 unitCenter = StaticReferenceManager.AllLocks[i].transform.PositionVector2();
                                float num = Vector2.Distance(unitCenter, lastPosition);
                                bool modified = false;
                                if (StaticReferenceManager.AllLocks[i] != null && unitCenter.GetAbsoluteRoom() != null && ((unitCenter - new Vector2(2, 2)).GetAbsoluteRoom() == m_targetRoom | (unitCenter + new Vector2(2, 2)).GetAbsoluteRoom() == m_targetRoom)) {
                                    if (!modified && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                        modified = true;
                                        if (StaticReferenceManager.AllLocks[i].lockMode == InteractableLock.InteractableLockMode.NORMAL && StaticReferenceManager.AllLocks[i].IsLocked) {
                                            if (StaticReferenceManager.AllLocks[i].gameObject) {
                                                StaticReferenceManager.AllLocks[i].ForceUnlock();
                                                LootEngine.DoDefaultItemPoof(unitCenter, false, true);
                                                AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", StaticReferenceManager.AllLocks[i].gameObject);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (StaticReferenceManager.AllChests.Count > 0) {
                        for (int i = 0; i < StaticReferenceManager.AllChests.Count; i++) {
                            Vector2 unitCenter = StaticReferenceManager.AllChests[i].transform.PositionVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            bool modified = false;
                            if (StaticReferenceManager.AllChests[i] != null && unitCenter.GetAbsoluteRoom() != null && unitCenter.GetAbsoluteRoom() == m_targetRoom) {
                                if (!modified && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                    modified = true;
                                    if (!StaticReferenceManager.AllChests[i].pickedUp && StaticReferenceManager.AllChests[i].IsLocked && !StaticReferenceManager.AllChests[i].IsRainbowChest && StaticReferenceManager.AllChests[i].ChestIdentifier != Chest.SpecialChestIdentifier.RAT)
                                    {
                                        LootEngine.DoDefaultItemPoof(unitCenter, false, true);
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", StaticReferenceManager.AllChests[i].gameObject);
                                        StaticReferenceManager.AllChests[i].ForceUnlock();
                                    }
                                }
                            }
                        }
                    }
                    if (StaticReferenceManager.AllNpcs.Count > 0) {
                         for (int i = 0; i < StaticReferenceManager.AllNpcs.Count; i++) {
                            Vector2 unitCenter = StaticReferenceManager.AllNpcs[i].transform.PositionVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            bool modified = false;
                            string ObjectName = StaticReferenceManager.AllNpcs[i].gameObject.name;
                            if (StaticReferenceManager.AllNpcs[i].gameObject.GetComponent<UltraFortunesFavor>()) {
                                if (!StaticReferenceManager.AllNpcs[i].gameObject.GetComponent<UltraFortunesFavor>().enabled) { modified = true; }
                            } else {
                                modified = true;
                            }
                            if (!modified && (string.IsNullOrEmpty(ObjectName) | !ObjectName.StartsWith("Corrupted") | !StaticReferenceManager.AllNpcs[i].gameObject.GetComponent<AIActor>()) && !StaticReferenceManager.AllNpcs[i].gameObject.GetComponent<AIActor>() && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                modified = true;
                                if (StaticReferenceManager.AllNpcs[i] != null && unitCenter.GetAbsoluteRoom() != null && unitCenter.GetAbsoluteRoom() == m_targetRoom) {
                                    tk2dBaseSprite targetBaseSprite = StaticReferenceManager.AllNpcs[i].gameObject.GetComponent<tk2dBaseSprite>();
                                    if (targetBaseSprite) { ExpandShaders.Instance.ApplyGlitchShader(targetBaseSprite); }
                                    LootEngine.DoDefaultItemPoof(unitCenter, false, true);
                                    if (npcsToProcess != null && !npcsToProcess.Contains(StaticReferenceManager.AllNpcs[i].gameObject)) {
                                        npcsToProcess.Add(StaticReferenceManager.AllNpcs[i].gameObject);
                                    }
                                    AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", StaticReferenceManager.AllNpcs[i].gameObject);
                                }
                            }
                        }
                    }
                    if (AllChallengeShrines != null && AllChallengeShrines.Length > 0) {
                        for (int i = 0; i < AllChallengeShrines.Length; i++) {
                            Vector2 unitCenter = AllChallengeShrines[i].gameObject.transform.PositionVector2();
                            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                            float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                            float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                            float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);
                            bool modified = false;
                            string ObjectName = AllChallengeShrines[i].gameObject.name;
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            if (AllChallengeShrines[i] != null && AllChallengeShrines[i].GetAbsoluteParentRoom() != null && AllChallengeShrines[i].GetAbsoluteParentRoom() == m_targetRoom) {
                                if (!modified && (string.IsNullOrEmpty(ObjectName) | !ObjectName.StartsWith("Corrupted")) && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                    modified = true;
                                    tk2dBaseSprite[] shrineSprites = AllChallengeShrines[i].gameObject.GetComponentsInChildren<tk2dBaseSprite>(true);
                                    if (shrineSprites != null && !shrinesToProcess.Contains(AllChallengeShrines[i].gameObject)) {
                                        shrinesToProcess.Add(AllChallengeShrines[i].gameObject);
                                        foreach (tk2dBaseSprite baseSprite in shrineSprites) { ExpandShaders.Instance.ApplyGlitchShader(baseSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat); }
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", AllChallengeShrines[i].gameObject);
                                    } else if (AllChallengeShrines[i].gameObject.GetComponent<tk2dBaseSprite>() && !shrinesToProcess.Contains(AllChallengeShrines[i].gameObject)) {
                                        shrinesToProcess.Add(AllChallengeShrines[i].gameObject);
                                        ExpandShaders.Instance.ApplyGlitchShader(AllChallengeShrines[i].gameObject.GetComponent<tk2dBaseSprite>(), true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", AllChallengeShrines[i].gameObject);
                                    }
                                }
                            }
                        }
                    }

                    if (StaticReferenceManager.AllAdvancedShrineControllers != null && StaticReferenceManager.AllAdvancedShrineControllers.Count > 0) {
                        List<AdvancedShrineController> allAdvancedShrineControllers = StaticReferenceManager.AllAdvancedShrineControllers;
                        for (int i = 0; i < allAdvancedShrineControllers.Count; i++) {
                            Vector2 unitCenter = allAdvancedShrineControllers[i].gameObject.transform.PositionVector2();
                            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                            float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                            float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                            float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);
                            bool modified = false;
                            string ObjectName = allAdvancedShrineControllers[i].gameObject.name;
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            if (allAdvancedShrineControllers[i] != null && allAdvancedShrineControllers[i].GetAbsoluteParentRoom() != null && allAdvancedShrineControllers[i].GetAbsoluteParentRoom() == m_targetRoom) {
                                if (!modified && (string.IsNullOrEmpty(ObjectName) | !ObjectName.StartsWith("Corrupted")) && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                    modified = true;
                                    tk2dBaseSprite[] shrineSprites = allAdvancedShrineControllers[i].gameObject.GetComponentsInChildren<tk2dBaseSprite>(true);
                                    if (shrineSprites != null && !shrinesToProcess.Contains(allAdvancedShrineControllers[i].gameObject)) {
                                        shrinesToProcess.Add(allAdvancedShrineControllers[i].gameObject);
                                        foreach (tk2dBaseSprite baseSprite in shrineSprites) { ExpandShaders.Instance.ApplyGlitchShader(baseSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat); }
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", allAdvancedShrineControllers[i].gameObject);
                                    } else if (allAdvancedShrineControllers[i].gameObject.GetComponent<tk2dBaseSprite>() && !shrinesToProcess.Contains(allAdvancedShrineControllers[i].gameObject)) {
                                        shrinesToProcess.Add(allAdvancedShrineControllers[i].gameObject);
                                        ExpandShaders.Instance.ApplyGlitchShader(allAdvancedShrineControllers[i].gameObject.GetComponent<tk2dBaseSprite>(), true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", allAdvancedShrineControllers[i].gameObject);
                                    }
                                }
                            }
                        }
                    }
                    if (AllTables != null && AllTables.Length > 0) {
                         for (int i = 0; i < AllTables.Length; i++) {
                            Vector2 unitCenter = AllTables[i].transform.PositionVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            bool modified = false;
                            string ObjectName = AllTables[i].gameObject.name;
                            if (AllTables[i] != null && AllTables[i].GetComponent<FlippableCover>() && !AllTables[i].name.StartsWith("Corrupted") && unitCenter.GetAbsoluteRoom() != null && unitCenter.GetAbsoluteRoom() == m_targetRoom) {
                                if (!modified && (string.IsNullOrEmpty(ObjectName) | !ObjectName.StartsWith("Corrupted")) && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                    modified = true;
                                    if (UnityEngine.Random.value <= 0.6f) {
                                        if (!tablesToProcess.Contains(AllTables[i].gameObject)) { tablesToProcess.Add(AllTables[i].gameObject); }
                                        if (AllTables[i].gameObject.GetComponentsInChildren<tk2dBaseSprite>(true) != null) {
                                            tk2dBaseSprite[] tableSprites = AllTables[i].gameObject.GetComponentsInChildren<tk2dBaseSprite>(true);
                                            if (tableSprites.Length > 0) {
                                                foreach (tk2dBaseSprite tableSprite in tableSprites) { ExpandShaders.Instance.ApplyGlitchShader(tableSprite); }
                                            }
                                        } else {
                                            tk2dBaseSprite targetSprite = AllTables[i].gameObject.GetComponent<tk2dBaseSprite>();
                                            if (targetSprite) { ExpandShaders.Instance.ApplyGlitchShader(targetSprite); }
                                        }
                                        LootEngine.DoDefaultItemPoof(unitCenter, false, true);
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", AllTables[i].gameObject);
                                    }
                                }
                            }
                        }
                    }
                    if (ExpandStaticReferenceManager.AllBeholsterShrines != null && ExpandStaticReferenceManager.AllBeholsterShrines.Count > 0) {
                        List<BeholsterShrineController> allBeholsterShrines = ExpandStaticReferenceManager.AllBeholsterShrines;
                        for (int i = 0; i < allBeholsterShrines.Count; i++) {
                            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                            float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                            float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                            float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);
                            bool modified = false;
                            string ObjectName = ExpandStaticReferenceManager.AllBeholsterShrines[i].gameObject.name;
                            Vector2 unitCenter = allBeholsterShrines[i].transform.PositionVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);                            
                            if (!modified && (string.IsNullOrEmpty(ObjectName) | !ObjectName.StartsWith("Corrupted")) && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                modified = true;
                                if (allBeholsterShrines[i] != null && allBeholsterShrines[i].GetAbsoluteParentRoom() != null && allBeholsterShrines[i].GetAbsoluteParentRoom() == m_targetRoom) {
                                    tk2dBaseSprite[] beholsterShrineSprites = allBeholsterShrines[i].gameObject.GetComponentsInChildren<tk2dBaseSprite>(true);
                                    if (beholsterShrineSprites != null && !shrinesToProcess.Contains(allBeholsterShrines[i].gameObject)) {
                                        foreach (tk2dBaseSprite baseSprite in beholsterShrineSprites) { ExpandShaders.Instance.ApplyGlitchShader(baseSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat); }
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", allBeholsterShrines[i].gameObject);
                                        shrinesToProcess.Add(allBeholsterShrines[i].gameObject);
                                    } else if (allBeholsterShrines[i].gameObject.GetComponent<tk2dBaseSprite>() && !shrinesToProcess.Contains(allBeholsterShrines[i].gameObject)) {
                                        ExpandShaders.Instance.ApplyGlitchShader(allBeholsterShrines[i].gameObject.GetComponent<tk2dBaseSprite>(), true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", allBeholsterShrines[i].gameObject);
                                        shrinesToProcess.Add(allBeholsterShrines[i].gameObject);
                                    }
                                }
                            }
                        }
                    }
                    if (ExpandStaticReferenceManager.AllMovingTraps.Count > 0) {
                         for (int i = 0; i < ExpandStaticReferenceManager.AllMovingTraps.Count; i++) {
                            PathingTrapController MovingTrap = ExpandStaticReferenceManager.AllMovingTraps[i];
                            Vector2 unitCenter = MovingTrap.transform.PositionVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            bool modified = false;
                            if (!modified && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                modified = true;
                                if (MovingTrap != null && unitCenter.GetAbsoluteRoom() != null && unitCenter.GetAbsoluteRoom() == m_targetRoom) {
                                    tk2dSlicedSprite[] targetSlicedSprites = MovingTrap.gameObject.GetComponents<tk2dSlicedSprite>();
                                    if (targetSlicedSprites != null) {
                                        foreach (tk2dSlicedSprite slicedSprite in targetSlicedSprites) {
                                            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                                            float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                                            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                                            float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                                            float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);
                                            ExpandShaders.Instance.ApplyGlitchShader(slicedSprite, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                        }
                                    }
                                    LootEngine.DoDefaultItemPoof(unitCenter, false, true);
                                    AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", MovingTrap.gameObject);
                                    if (!movableTrapsToProcess.Contains(MovingTrap.gameObject)) { movableTrapsToProcess.Add(MovingTrap.gameObject); }
                                    if (ExpandStaticReferenceManager.AllMovingTraps.Contains(MovingTrap)) { ExpandStaticReferenceManager.AllMovingTraps.Remove(MovingTrap); }
                                }
                            }
                        }
                    }
                    if (ExpandStaticReferenceManager.AllConveyorBelts.Count > 0) {
                         for (int i = 0; i < ExpandStaticReferenceManager.AllConveyorBelts.Count; i++) {
                            Vector2 unitCenter = ExpandStaticReferenceManager.AllConveyorBelts[i].transform.PositionVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            bool modified = false;
                            if (!modified && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                modified = true;
                                if (ExpandStaticReferenceManager.AllConveyorBelts[i] != null) {
                                    if (unitCenter.GetAbsoluteRoom() != null && unitCenter.GetAbsoluteRoom() == m_targetRoom) {                                        
                                        ConveyorBelt conveyorBeltObject = ExpandStaticReferenceManager.AllConveyorBelts[i];
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectDestroyed_01", conveyorBeltObject.gameObject);
                                        if (ExpandStaticReferenceManager.AllConveyorBelts.Contains(conveyorBeltObject)) { ExpandStaticReferenceManager.AllConveyorBelts.Remove(conveyorBeltObject); }
                                        Destroy(conveyorBeltObject.gameObject);
                                    }
                                }
                            }
                        }
                    }
                    if (StaticReferenceManager.AllForgeHammers.Count > 0) {
                         for (int i = 0; i < StaticReferenceManager.AllForgeHammers.Count; i++) {
                            Vector2 unitCenter = StaticReferenceManager.AllForgeHammers[i].gameObject.transform.PositionVector2();
                            float num = Vector2.Distance(unitCenter, lastPosition);
                            bool modified = false;
                            if (!modified && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                modified = true;
                                if (StaticReferenceManager.AllForgeHammers[i] != null) {
                                    if (unitCenter.GetAbsoluteRoom() != null && unitCenter.GetAbsoluteRoom() == m_targetRoom) {
                                        ForgeHammerController forgeHammer = StaticReferenceManager.AllForgeHammers[i];
                                        GameObject forgeHammerObject = StaticReferenceManager.AllForgeHammers[i].gameObject;
                                        if (forgeHammer.sprite.renderer.enabled) {
                                            if (!hammersToProcess.Contains(forgeHammer.gameObject)) { hammersToProcess.Add(forgeHammer.gameObject); }
                                            float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                                            float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                                            float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                                            float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                                            float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);
                                            ExpandShaders.Instance.ApplyGlitchShader(forgeHammer.sprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                            AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", forgeHammerObject);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (m_targetRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All) != null && m_targetRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All).Count != 0) {
                        for (int i = 0; i < m_targetRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All).Count; i++) {
                            AIActor enemy = m_targetRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All)[i];
                            bool enemyModified = false;
                            if (enemy != null && (string.IsNullOrEmpty(enemy.OverrideDisplayName) | !enemy.OverrideDisplayName.StartsWith("Corrupted")) && !enemy.healthHaver.IsDead && !enemy.healthHaver.IsBoss && enemy.healthHaver.IsVulnerable && !enemy.IgnoreForRoomClear && !enemy.IsMimicEnemy && enemy.HasBeenEngaged && enemy.specRigidbody && enemy.specRigidbody.CollideWithOthers) {
                                Vector2 unitCenter = enemy.specRigidbody.GetUnitCenter(ColliderType.HitBox);
                                if (unitCenter == null) { unitCenter = enemy.sprite.WorldCenter; }
                                float num = Vector2.Distance(unitCenter, lastPosition);                                
                                if (!enemyModified && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                    enemyModified = true;
                                    if (enemy != null &&  !string.IsNullOrEmpty(enemy.EnemyGuid)) {
                                        if (enemy.behaviorSpeculator) {
                                            enemy.behaviorSpeculator.InterruptAndDisable();
                                            if (enemy.EnemyGuid == "c0260c286c8d4538a697c5bf24976ccf" | enemy.EnemyGuid == "5f15093e6f684f4fb09d3e7e697216b4") {
                                                enemy.behaviorSpeculator.enabled = false;
                                            } else {
                                                Destroy(enemy.behaviorSpeculator);
                                            }
                                            if (UnityEngine.Random.value <= 0.4f) { enemy.ClearPath(); }
                                        }

                                        if (enemy.aiShooter) { enemy.aiShooter.ToggleGunAndHandRenderers(false, "CorruptionFX"); }
                                        
                                        if (enemy.gameObject.GetComponent<ExplodeOnDeath>() == null && enemy.gameObject.GetComponent<ExpandExplodeOnDeath>() == null) {
                                            enemy.gameObject.AddComponent<ExpandExplodeOnDeath>();
                                            ExpandExplodeOnDeath m_Exploder = enemy.gameObject.GetComponent<ExpandExplodeOnDeath>();
                                            if (enemy.EnemyGuid != "98ea2fe181ab4323ab6e9981955a9bca" && enemy.EnemyGuid != "21dd14e5ca2a4a388adab5b11b69a1e1" && enemy.EnemyGuid != "ccf6d241dad64d989cbcaca2a8477f01") { m_Exploder.ExplosionNotGuranteed = true; }
                                        }

                                        if (enemy.gameObject.GetComponent<BulletKingToadieController>()) {
                                            Destroy(enemy.gameObject.GetComponent<BulletKingToadieController>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(15);
                                        }

                                        if (enemy.gameObject.GetComponent<BloodbulonController>()){
                                            Destroy(enemy.gameObject.GetComponent<BloodbulonController>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                        }
                                        if (enemy.gameObject.GetComponent<KaliberController>()) {
                                            Destroy(enemy.gameObject.GetComponent<KaliberController>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(25);
                                        }
                                        if (enemy.gameObject.GetComponent<TBulonController>()) {
                                            Destroy(enemy.gameObject.GetComponent<TBulonController>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(18);
                                        }
                                        if (enemy.gameObject.GetComponent<MakeVulnerableOnRoomClear>()) {
                                            Destroy(enemy.gameObject.GetComponent<MakeVulnerableOnRoomClear>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(30);
                                        }
                                        if (enemy.gameObject.GetComponent<KillOnRoomUnseal>()) {
                                            Destroy(enemy.gameObject.GetComponent<MakeVulnerableOnRoomClear>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(30);
                                        }

                                        if (enemy.gameObject.GetComponent<FloatingEyeController>()) {
                                            Destroy(enemy.gameObject.GetComponent<FloatingEyeController>());
                                        }

                                        if (enemy.gameObject.GetComponent<CrazedController>()) { Destroy(enemy.gameObject.GetComponent<CrazedController>()); }


                                        if (enemy.EnemyGuid == "21dd14e5ca2a4a388adab5b11b69a1e1") {
                                            if (enemy.gameObject.GetComponentsInChildren<BodyPartController>(true) != null) {
                                                for (int C = 0; C < enemy.gameObject.GetComponentsInChildren<BodyPartController>(true).Length; C++) {
                                                    Destroy(enemy.gameObject.GetComponentsInChildren<BodyPartController>(true)[C]);
                                                }
                                            }
                                            if (enemy.gameObject.GetComponent<ShelletonRespawnController>()) { Destroy(enemy.gameObject.GetComponent<ShelletonRespawnController>()); }
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(35);
                                        }


                                        List<string> ExcludedEnemies = new List<string>() {
                                            "383175a55879441d90933b5c4e60cf6f", // spectre_gun_nut
                                            "022d7c822bc146b58fe3b0287568aaa2", // blizzbulon
                                            "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                                            "5f15093e6f684f4fb09d3e7e697216b4", // dynamite_kin_office
                                            "7b0b1b6d9ce7405b86b75ce648025dd6", // beadie
                                            "475c20c1fd474dfbad54954e7cba29c1", // tarnisher
                                        };
                                        List<string> IgnoreForRoomClear = new List<string>() {
                                            "9b2cf2949a894599917d4d391a0b7394", // high_gunjurer
                                            "3cadf10c489b461f9fb8814abc1a09c1", // minelet
                                            "9b4fb8a2a60a457f90dcf285d34143ac", // gat
                                            "cd4a4b7f612a4ba9a720b9f97c52f38c", // lead_maiden
                                            "9215d1a221904c7386b481a171e52859", // lead_maiden_fridge
                                            "4db03291a12144d69fe940d5a01de376" // hollowpoint
                                        };
                                        
                                        if (!ExcludedEnemies.Contains(enemy.EnemyGuid)) {
                                            if (enemy.aiAnimator) { Destroy(enemy.aiAnimator); }
                                            if (enemy.spriteAnimator) { Destroy(enemy.spriteAnimator); }

                                            if (enemy.gameObject.GetComponent<BulletKingToadieController>()) {
                                                Destroy(enemy.gameObject.GetComponent<BulletKingToadieController>());
                                            }

                                            enemy.gameObject.AddComponent<tk2dSpriteAnimator>();
                                            tk2dSpriteAnimator dummyAnimator = enemy.gameObject.GetComponent<tk2dSpriteAnimator>();
                                            dummyAnimator.Library = null;
                                            dummyAnimator.DefaultClipId = 0;
                                            dummyAnimator.AdditionalCameraVisibilityRadius = 0;
                                            dummyAnimator.AnimateDuringBossIntros = false;
                                            dummyAnimator.AlwaysIgnoreTimeScale = true;
                                            dummyAnimator.ForceSetEveryFrame = false;
                                            dummyAnimator.playAutomatically = false;
                                            dummyAnimator.IsFrameBlendedAnimation = false;
                                            dummyAnimator.clipTime = 0;
                                            dummyAnimator.deferNextStartClip = false;
                                            SpriteBuilder.AddAnimation(dummyAnimator, enemy.sprite.Collection, new List<int>() { enemy.sprite.spriteId }, "DummyFrame", tk2dSpriteAnimationClip.WrapMode.Once);
                                        }

                                        if (enemy.gameObject.GetComponent<KillOnRoomUnseal>()) {
                                            Destroy(enemy.gameObject.GetComponent<KillOnRoomUnseal>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(30);
                                        }

                                        if (enemy.gameObject.GetComponent<CrazedController>()) { Destroy(enemy.gameObject.GetComponent<CrazedController>()); }

                                        enemy.HitByEnemyBullets = true;
                                        enemy.CollisionDamage = 0;
                                        enemy.AlwaysShowOffscreenArrow = true;
                                        enemy.healthHaver.IsVulnerable = true;
                                        enemy.healthHaver.PreventAllDamage = false;
                                        enemy.healthHaver.incorporealityOnDamage = false;

                                        if (IgnoreForRoomClear.Contains(enemy.EnemyGuid)) {
                                            m_targetRoom.DeregisterEnemy(enemy);
                                            enemy.AlwaysShowOffscreenArrow = false;
                                            enemy.IgnoreForRoomClear = true;
                                        }
                                        // enemy.healthHaver.RegenerateCache();
                                        if (!string.IsNullOrEmpty(enemy.GetActorName())) {
                                            enemy.OverrideDisplayName = ("Corrupted " + enemy.GetActorName());
                                        } else {
                                            enemy.OverrideDisplayName = ("Corrupted Enemy");
                                        }

                                        if (UnityEngine.Random.value <= 0.05f) {
                                            if (enemy.AdditionalSafeItemDrops == null) { enemy.AdditionalSafeItemDrops = new List<PickupObject>(); }
                                            if (UnityEngine.Random.value <= 0.1f) {
                                                enemy.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(BraveUtility.RandomElement(RareItemDrops)));
                                            } else {
                                                enemy.AdditionalSafeItemDrops.Add(PickupObjectDatabase.GetById(BraveUtility.RandomElement(CommonItemDrops)));
                                            }
                                        }
                                        
                                        if (enemy.EnemyGuid == "0239c0680f9f467dbe5c4aab7dd1eca6" | enemy.EnemyGuid == "e61cab252cfb435db9172adc96ded75f" | enemy.EnemyGuid == "1a4872dafdb34fd29fe8ac90bd2cea67") {
                                            ExpandGlitchedEnemies.GlitchExistingEnemy(enemy);
                                        } else {
                                            if (enemy.gameObject.GetComponent<SpawnEnemyOnDeath>() != null) { Destroy(enemy.gameObject.GetComponent<SpawnEnemyOnDeath>()); }

                                            if (enemy.GetComponent<ExpandSpawnGlitchObjectOnDeath>() == null && enemy.GetComponent<ExpandExplodeOnDeath>() == null) {
                                                if (UnityEngine.Random.value <= 0.25f) { enemy.gameObject.AddComponent<ExpandSpawnGlitchObjectOnDeath>(); }
                                            }
                                        }

                                        if (enemy.sprite) { ExpandShaders.Instance.ApplyGlitchShader(enemy.sprite); }
                                        
                                        // GameManager.Instance.StartCoroutine(DelayedEnemyDamage(enemy, 0.5f, 3, ignoreInvulnerablity: true));
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", enemy.gameObject);
                                        LootEngine.DoDefaultItemPoof(unitCenter, false, true);                                        
                                    }
                                }
                            } else if (enemy != null && !string.IsNullOrEmpty(enemy.EnemyGuid) && (string.IsNullOrEmpty(enemy.OverrideDisplayName) | !enemy.OverrideDisplayName.StartsWith("Corrupted")) && !enemy.healthHaver.IsDead && enemy.IgnoreForRoomClear) {
                                List<string> ValidEnemies = new List<string> {
                                    "b5e699a0abb94666bda567ab23bd91c4", // bullet_kings_toadie
                                    "d4dd2b2bbda64cc9bcec534b4e920518", // bullet_kings_toadie_revenge
                                    "02a14dec58ab45fb8aacde7aacd25b01", // old_kings_toadie
                                    "699cd24270af4cd183d671090d8323a1", // key_bullet_kin
                                    "a446c626b56d4166915a4e29869737fd", // chance_bullet_kin
                                    "ba928393c8ed47819c2c5f593100a5bc", // metal_cube_guy (trap version)
                                    "88f037c3f93b4362a040a87b30770407", // gun_reaper
                                    "33b212b856b74ff09252bf4f2e8b8c57", // lead_cube
                                    "3f2026dc3712490289c4658a2ba4a24b", // flesh_cube
                                };
                                if ((ValidEnemies.Contains(enemy.EnemyGuid) | enemy.IsMimicEnemy) && enemy.EnemyGuid != "479556d05c7c44f3b6abb3b2067fc778") {
                                    Vector2 unitCenter = enemy.specRigidbody.GetUnitCenter(ColliderType.HitBox);
                                    float num = Vector2.Distance(unitCenter, lastPosition);
                                    if (!enemyModified && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                        enemyModified = true;
                                        if (!string.IsNullOrEmpty(enemy.GetActorName())) {
                                            enemy.OverrideDisplayName = ("Corrupted " + enemy.GetActorName());
                                        } else {
                                            enemy.OverrideDisplayName = ("Corrupted Enemy");
                                        }

                                        if (enemy.gameObject.GetComponent<BulletKingToadieController>()) {
                                            Destroy(enemy.gameObject.GetComponent<BulletKingToadieController>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(15);
                                        }

                                        if (enemy.gameObject.GetComponent<UnearthController>()) { Destroy(enemy.GetComponent<UnearthController>()); }

                                        if (enemy.behaviorSpeculator) {
                                            enemy.behaviorSpeculator.InterruptAndDisable();
                                            Destroy(enemy.behaviorSpeculator);
                                            // enemy.behaviorSpeculator.enabled = false;
                                            if (UnityEngine.Random.value <= 0.4f) { enemy.ClearPath(); }
                                        }
                                        if (enemy.aiShooter) { enemy.aiShooter.ToggleGunAndHandRenderers(false, "CorruptionFX"); }

                                        if (enemy.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc") {
                                            if (enemy.gameObject.GetComponent<ExpandThwompManager>()) {
                                                Destroy(enemy.gameObject.GetComponent<ExpandThwompManager>());
                                            }
                                        }

                                        if (!enemy.GetComponent<ExplodeOnDeath>()) {
                                            enemy.gameObject.AddComponent<ExpandExplodeOnDeath>();
                                            ExpandExplodeOnDeath m_Exploder = enemy.gameObject.GetComponent<ExpandExplodeOnDeath>();
                                            if (!enemy.gameObject.GetComponent<MakeVulnerableOnRoomClear>() && enemy.EnemyGuid != "ba928393c8ed47819c2c5f593100a5bc") {
                                                m_Exploder.ExplosionNotGuranteed = true;
                                                m_Exploder.ExplosionOdds = 0;
                                            }
                                        }

                                        if (enemy.sprite) {
                                            /*if (enemy.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc" && GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.WESTGEON) {
                                                ExpandShaders.Instance.ApplyGlitchShader(ExpandPrefabs.StoneCubeWestTexture, enemy.sprite);
                                            } else {
                                                ExpandShaders.Instance.ApplyGlitchShader(enemy.sprite);
                                            }*/
                                            ExpandShaders.Instance.ApplyGlitchShader(enemy.sprite);
                                        }
                                        
                                        if (enemy.gameObject.GetComponent<FloatingEyeController>()) {
                                            Destroy(enemy.gameObject.GetComponent<FloatingEyeController>());
                                        }

                                        List<string> ExcludedEnemies = new List<string>() {
                                            "383175a55879441d90933b5c4e60cf6f", // spectre_gun_nut
                                            "022d7c822bc146b58fe3b0287568aaa2", // blizzbulon
                                            "c0260c286c8d4538a697c5bf24976ccf", // dynamite_kin
                                            "5f15093e6f684f4fb09d3e7e697216b4" // dynamite_kin_office
                                        };

                                        if (!ExcludedEnemies.Contains(enemy.EnemyGuid)) {
                                            if (enemy.aiAnimator) { Destroy(enemy.aiAnimator); }
                                            if (enemy.spriteAnimator) { Destroy(enemy.spriteAnimator); }
                                            
                                            enemy.gameObject.AddComponent<tk2dSpriteAnimator>();
                                            tk2dSpriteAnimator dummyAnimator = enemy.gameObject.GetComponent<tk2dSpriteAnimator>();
                                            dummyAnimator.Library = null;
                                            dummyAnimator.DefaultClipId = 0;
                                            dummyAnimator.AdditionalCameraVisibilityRadius = 0;
                                            dummyAnimator.AnimateDuringBossIntros = false;
                                            dummyAnimator.AlwaysIgnoreTimeScale = true;
                                            dummyAnimator.ForceSetEveryFrame = false;
                                            dummyAnimator.playAutomatically = false;
                                            dummyAnimator.IsFrameBlendedAnimation = false;
                                            dummyAnimator.clipTime = 0;
                                            dummyAnimator.deferNextStartClip = false;
                                            SpriteBuilder.AddAnimation(dummyAnimator, enemy.sprite.Collection, new List<int>() { enemy.sprite.spriteId }, "DummyFrame", tk2dSpriteAnimationClip.WrapMode.Once);
                                        }

                                        if (enemy.gameObject.GetComponent<MakeVulnerableOnRoomClear>()) {
                                            Destroy(enemy.gameObject.GetComponent<MakeVulnerableOnRoomClear>());
                                            enemy.healthHaver.minimumHealth = 0f;
                                            enemy.healthHaver.ForceSetCurrentHealth(30);
                                        }

                                        if (enemy.gameActor.GetComponent<KillOnRoomUnseal>()) { Destroy(enemy.gameObject.GetComponent<KillOnRoomUnseal>()); }

                                        if (enemy.EnemyGuid == "33b212b856b74ff09252bf4f2e8b8c57" | enemy.EnemyGuid == "3f2026dc3712490289c4658a2ba4a24b" | enemy.EnemyGuid == "ba928393c8ed47819c2c5f593100a5bc") {
                                            if (user.IsInCombat) { enemy.healthHaver.SetHealthMaximum(25); }
                                        } else if (enemy.EnemyGuid == "88f037c3f93b4362a040a87b30770407") {
                                            enemy.healthHaver.SetHealthMaximum(25);
                                        }
                                        
                                        enemy.HitByEnemyBullets = true;
                                        enemy.CollisionDamage = 0;
                                        enemy.healthHaver.IsVulnerable = true;
                                        enemy.healthHaver.PreventAllDamage = false;
                                        enemy.healthHaver.incorporealityOnDamage = false;

                                        // GameManager.Instance.StartCoroutine(DelayedEnemyDamage(enemy, 0.5f, 3f));
                                        LootEngine.DoDefaultItemPoof(unitCenter, false, true);
                                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectTransform_01", enemy.gameObject);
                                    }
                                }
                            } else if (!enemyModified && enemy != null && !string.IsNullOrEmpty(enemy.EnemyGuid) && !enemy.healthHaver.IsDead && enemy.healthHaver.IsBoss && enemy.HasBeenEngaged) {
                                Vector2 unitCenter = enemy.specRigidbody.GetUnitCenter(ColliderType.HitBox);
                                float num = Vector2.Distance(unitCenter, lastPosition);
                                if (!enemyModified && num >= m_prevWaveDist - 0.25f && num <= waveDist + 0.25f) {
                                    enemyModified = true;
                                    GameManager.Instance.StartCoroutine(DelayedEnemyDamage(enemy, 0.5f, 145));
                                }
                            }
                        }
                    }
                    m_prevWaveDist = waveDist;
                    yield return null;
                }
                if (breakablesToProcess.Count > 0) {
                    foreach (MinorBreakable breakable in breakablesToProcess) {
                        if (breakable) {
                            if (!string.IsNullOrEmpty(breakable.gameObject.name)) {
                                breakable.gameObject.name = ("Corrupted " + breakable.gameObject.name);
                            } else {
                                breakable.gameObject.name = ("Corrupted Object");
                            }
                            breakable.OnBreakContext = (Action<MinorBreakable>)Delegate.Combine(breakable.OnBreakContext, new Action<MinorBreakable>(MaybeSpawnSomethingOnBreak));
                        }
                    }
                }
                if (shrinesToProcess.Count > 0) {
                    foreach (GameObject shrineObject in shrinesToProcess) {
                        if (shrineObject) {
                            AdvancedShrineController shrine = shrineObject.GetComponent<AdvancedShrineController>();
                            ShrineController shrine2 = shrineObject.GetComponent<ShrineController>();
                            ChallengeShrineController shrine3 = shrineObject.GetComponent<ChallengeShrineController>();
                            BeholsterShrineController beholsterShrine = shrineObject.GetComponent<BeholsterShrineController>();
                            RoomHandler m_shrineParentRoom = shrineObject.transform.position.GetAbsoluteRoom();
                            if (!string.IsNullOrEmpty(shrineObject.gameObject.name)) {
                                shrineObject.gameObject.name = ("Corrupted " + shrineObject.gameObject.name);
                            } else {
                                shrineObject.gameObject.name = ("Corrupted Shrine");
                            }
                            if (shrine) {                                
                                if (m_shrineParentRoom != null) {
                                    m_shrineParentRoom.DeregisterInteractable(shrine);
                                    shrine.GetRidOfMinimapIcon();
                                    Destroy(shrineObject.GetComponent<AdvancedShrineController>());
                                }
                            }
                            if (shrine2) {
                                m_shrineParentRoom.DeregisterInteractable(shrine2);
                                shrine2.GetRidOfMinimapIcon();
                                Destroy(shrineObject.GetComponent<ShrineController>());
                            }
                            if (shrine3) {                                
                                if (m_shrineParentRoom != null) {
                                    m_shrineParentRoom.DeregisterInteractable(shrine);
                                    shrine3.GetRidOfMinimapIcon();
                                    Destroy(shrineObject.GetComponent<ChallengeShrineController>());
                                }
                            }
                            if (beholsterShrine) {
                                ExpandStaticReferenceManager.AllBeholsterShrines.Remove(beholsterShrine);
                                m_shrineParentRoom.DeregisterInteractable(beholsterShrine);                                
                                Destroy(shrineObject.GetComponent<BeholsterShrineController>());
                            }
                            if (shrineObject.GetComponent<TalkDoerLite>()) { Destroy(shrineObject.GetComponent<TalkDoerLite>()); }
                            ExpandUtility.GenerateHealthHaver(shrineObject, 25, exploderSpawnsItem: true);
                        }
                        yield return null;
                    }
                }
                if (npcsToProcess.Count > 0) {
                    foreach (GameObject npcObject in npcsToProcess) {
                        RoomHandler m_parentroom = npcObject.transform.position.GetAbsoluteRoom();
                        if (m_parentroom != null) {
                            TalkDoerLite npc = npcObject.GetComponent<TalkDoerLite>();
                            if (npc) {
                                if (npc.sprite && npc.specRigidbody) {
                                    if (m_parentroom != null) { m_parentroom.DeregisterInteractable(npc); }
                                    if (npc.gameObject.GetComponent<UltraFortunesFavor>() != null && npc.gameObject.GetComponent<UltraFortunesFavor>().enabled) {
                                        npc.gameObject.GetComponent<UltraFortunesFavor>().enabled = false;
                                    }
                                    npc.renderer.enabled = false;
                                    if (npc.specRigidbody) { npc.specRigidbody.CollideWithOthers = false; }
                                    if (SpriteOutlineManager.HasOutline(npc.sprite)) {
                                        SpriteOutlineManager.RemoveOutlineFromSprite(npc.sprite, false);
                                    }
                                    npc.sprite.RegenerateCache();
                                    string NPCName = string.Empty;
                                    if (!string.IsNullOrEmpty(npc.gameObject.name)) {
                                        NPCName = "Corrupted NPC " + npc.gameObject.name;
                                    } else {
                                        NPCName = "Corrupted NPC";
                                    }
                                    GameObject dummyNPC = new GameObject(NPCName);
                                    dummyNPC.transform.position = npc.gameObject.transform.position;
                                    dummyNPC.transform.parent = m_targetRoom.hierarchyParent;
                                    dummyNPC.AddComponent<tk2dSprite>();
                                    tk2dSprite dummySprite = dummyNPC.GetComponent<tk2dSprite>();
                                    ExpandUtility.DuplicateSprite(dummySprite, npc.sprite as tk2dSprite);
                                    dummySprite.renderer.enabled = true;
                                    dummySprite.HeightOffGround = 0.5f;
                                    ExpandShaders.Instance.ApplyGlitchShader(dummySprite);
                                    dummyNPC.AddComponent<SpeculativeRigidbody>();
                                    SpeculativeRigidbody rigidBody = dummyNPC.GetComponent<SpeculativeRigidbody>();
                                    if (npc.specRigidbody) {
                                        ExpandUtility.DuplicateRigidBody(rigidBody, npc.specRigidbody);
                                        rigidBody.CollideWithOthers = true;
                                        rigidBody.Velocity = Vector2.zero;
                                    }
                                    dummyNPC.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
                                    dummySprite.UpdateZDepth();
                                    ExpandUtility.GenerateHealthHaver(dummyNPC, 20, exploderSpawnsItem: BraveUtility.RandomBool());
                                    HealthHaver dummyNPCHealthhaver = dummyNPC.GetComponent<HealthHaver>();
                                    ExpandExplodeOnDeath npcExploder = dummyNPC.GetComponent<ExpandExplodeOnDeath>();
                                    if (npcExploder) { npcExploder.isCorruptedNPC = true; }
                                    if (StaticReferenceManager.AllShops != null && StaticReferenceManager.AllShops.Count > 0) {
                                        foreach (BaseShopController shop in StaticReferenceManager.AllShops) {
                                            if (shop.shopkeepFSM.GetComponent<TalkDoerLite>()) {
                                                if (shop.GetAbsoluteParentRoom() != null && shop.GetAbsoluteParentRoom() == m_parentroom) {
                                                    if (shop.shopkeepFSM.GetComponent<TalkDoerLite>() && shop.shopkeepFSM.GetComponent<TalkDoerLite>() == npc) {
                                                        if (npcExploder) {
                                                            npcExploder.NPCShop = shop;
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                } else {
                                    npc.SpeaksGleepGlorpenese = true;
                                }
                            }
                        }
                        yield return null;
                    }
                }
                if (movableTrapsToProcess.Count > 0) {
                    for (int i = 0; i < movableTrapsToProcess.Count; i++) {
                        if (movableTrapsToProcess[i] && movableTrapsToProcess[i].GetComponent<SpeculativeRigidbody>()) {
                            movableTrapsToProcess[i].GetComponent<SpeculativeRigidbody>().Velocity = Vector2.zero;
                            GameObject dummyTrap = new GameObject("Corrupted Trap" + UnityEngine.Random.Range(100, 999));
                            dummyTrap.transform.position = movableTrapsToProcess[i].transform.position;
                            dummyTrap.transform.parent = m_targetRoom.hierarchyParent;
                            tk2dSlicedSprite[] slicedSprites = movableTrapsToProcess[i].GetComponents<tk2dSlicedSprite>();
                            if (slicedSprites != null) {
                                foreach (tk2dSlicedSprite slicedSprite in slicedSprites) {
                                    dummyTrap.AddComponent<tk2dSlicedSprite>();
                                    tk2dSlicedSprite newSlicedSprite = dummyTrap.GetComponent<tk2dSlicedSprite>();
                                    ExpandUtility.DuplicateSlicedSprite(newSlicedSprite, slicedSprite);
                                    float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                                    float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                                    float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                                    float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                                    float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);
                                    ExpandShaders.Instance.ApplyGlitchShader(newSlicedSprite, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
                                }
                            }
                            dummyTrap.AddComponent<SpeculativeRigidbody>();
                            SpeculativeRigidbody dummyRegidBody = dummyTrap.GetComponent<SpeculativeRigidbody>();
                            ExpandUtility.DuplicateRigidBody(dummyRegidBody, movableTrapsToProcess[i].GetComponent<SpeculativeRigidbody>());

                            ExpandUtility.GenerateHealthHaver(dummyTrap, 35);
                            Destroy(movableTrapsToProcess[i].gameObject);
                        }
                        yield return null;
                    }
                }
                if (hammersToProcess.Count > 0) {
                    foreach (GameObject hammer in hammersToProcess) {
                        ForgeHammerController hammerController = hammer.GetComponent<ForgeHammerController>();
                        if (hammerController && hammerController.sprite && hammerController.sprite.renderer.enabled) {
                            tk2dSprite baseSprite = (hammerController.sprite as tk2dSprite);
                            if (baseSprite && hammerController.specRigidbody) {
                                float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                                float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                                float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                                float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                                float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);

                                hammerController.renderer.enabled = false;

                                GameObject dummyHammer = new GameObject("Corrupted Dead Blow " + UnityEngine.Random.Range(100, 999)) { layer = hammer.layer };
                                dummyHammer.transform.position = hammer.transform.position;
                                dummyHammer.transform.parent = m_targetRoom.hierarchyParent;

                                dummyHammer.AddComponent<tk2dSprite>();
                                dummyHammer.AddComponent<SpeculativeRigidbody>();

                                tk2dSprite dummySprite = dummyHammer.GetComponent<tk2dSprite>();
                                SpeculativeRigidbody dummyRigidBody = dummyHammer.GetComponent<SpeculativeRigidbody>();

                                ExpandUtility.DuplicateSprite(dummySprite, baseSprite);
                                ExpandUtility.DuplicateRigidBody(dummyRigidBody, hammerController.specRigidbody);

                                dummyRigidBody.enabled = true;
                                dummySprite.renderer.enabled = true;

                                ExpandShaders.Instance.ApplyGlitchShader(dummySprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);

                                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(dummyRigidBody, null, false);

                                ExpandUtility.GenerateHealthHaver(dummyHammer, 30);
                                hammerController.Deactivate();
                            }             
                        } else if (hammerController) {
                            hammerController.Deactivate();
                        }
                    }
                }
                if(tablesToProcess.Count > 0) {
                    foreach (GameObject tableObject in tablesToProcess) {
                        if (tableObject) {
                            if (!string.IsNullOrEmpty(tableObject.gameObject.name)) {
                                tableObject.gameObject.name = ("Corrupted " + tableObject.gameObject.name);
                            } else {
                                tableObject.gameObject.name = "Corrupted Table";
                            }
                            
                            if (tableObject.GetComponent<FlippableCover>() && UnityEngine.Random.value <= 0.3f) {
                                m_targetRoom.DeregisterInteractable(tableObject.GetComponent<FlippableCover>());
                            }
                            tableObject.AddComponent<ExpandKickableObject>();
                            m_targetRoom.RegisterInteractable(tableObject.GetComponent<ExpandKickableObject>());
                        }
                    }
                }
            }
            yield break;
        }

        private void TriggerBlank(Vector2 center, PlayerController Owner = null, float overrideRadius = 40f, float overrideTimeAtMaxRadius = 0.5f, bool silent = false, bool breaksWalls = true, bool breaksObjects = true, float overrideForce = -1f, bool disableDamage = false) {
            GameObject silencerVFX = (!silent) ? ((GameObject)BraveResources.Load("Global VFX/BlankVFX", ".prefab")) : null;
            if (!silent) {
                AkSoundEngine.PostEvent("Play_OBJ_silenceblank_use_01", base.gameObject);
                AkSoundEngine.PostEvent("Stop_ENM_attack_cancel_01", base.gameObject);
            }
            GameObject gameObject = new GameObject("silencer");
            SilencerInstance silencerInstance = gameObject.AddComponent<SilencerInstance>();
            silencerInstance.ForceNoDamage = disableDamage;
            
            silencerInstance.TriggerSilencer(center, 50f, overrideRadius, silencerVFX, (!silent) ? 0.15f : 0f, (!silent) ? 0.2f : 0f, (!silent) ? 50 : 0, (!silent) ? 10 : 0, (!silent) ? ((overrideForce < 0f) ? 140f : overrideForce) : 0f, (!breaksObjects) ? 0 : ((!silent) ? 15 : 5), overrideTimeAtMaxRadius, Owner, breaksWalls, false);
            if (Owner) { Owner.DoVibration(Vibration.Time.Quick, Vibration.Strength.Medium); }
        }

        private IEnumerator DelayedUsableTimer(float delay) {
            yield return new WaitForSeconds(delay);
            CurrentlyInUse = false;
            yield break;
        }

        private IEnumerator DelayedEnemyDamage(AIActor targetEnemy, float delay, float DamageAmount, Vector2? DamageDirection = null, CoreDamageTypes DamageType = CoreDamageTypes.None, DamageCategory damageCategory = DamageCategory.Normal, bool ignoreInvulnerablity = false, bool ignoreDamageCap = false ) {
            yield return new WaitForSeconds(delay);
            Vector2 direction = Vector2.zero;
            if (DamageDirection.HasValue) { direction = DamageDirection.Value; }
            targetEnemy.healthHaver.ApplyDamage(DamageAmount, direction, "DelayedCorruptionBombDamage", DamageType, damageCategory, ignoreInvulnerablity, null, ignoreDamageCap);
            yield break;
        }

        private void AnyDamageDealt(float damageAmount, bool fatal, HealthHaver target) {
            Vector3 worldPosition = target.transform.position;
            AIActor targetActor = target.GetComponent<AIActor>();
            if (targetActor && damageAmount >= 3 && !fatal) {
                if (!string.IsNullOrEmpty(targetActor.OverrideDisplayName) && targetActor.OverrideDisplayName.StartsWith("Corrupted")) {
                    AkSoundEngine.PostEvent("Play_EX_CorruptedObjectDamage_01", target.gameObject);
                }
            } else if (targetActor && fatal) {
                if (!string.IsNullOrEmpty(targetActor.OverrideDisplayName) && targetActor.OverrideDisplayName.StartsWith("Corrupted")) {
                    AkSoundEngine.PostEvent("Play_EX_CorruptedObjectDestroyed_01", target.gameObject);
                }           
            } else {
                SpeculativeRigidbody rigidBody = target.GetComponent<SpeculativeRigidbody>();                
                if (rigidBody && !targetActor && !fatal) {
                    if (!string.IsNullOrEmpty(target.gameObject.name) && target.gameObject.name.StartsWith("Corrupted")) {
                        AkSoundEngine.PostEvent("Play_EX_CorruptedObjectDamage_01", target.gameObject);
                    }
                }
            }
        }

        private void MaybeSpawnSomethingOnBreak(MinorBreakable breakable) {
            if (breakable.CenterPoint.GetAbsoluteRoom() != null) {
                if (UnityEngine.Random.value <= 0.6f) {
                    if (UnityEngine.Random.value <= 0.65f) {
                        RoomHandler currentRoom = breakable.CenterPoint.GetAbsoluteRoom();
                        AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(BraveUtility.RandomElement(ExpandLists.PotCritterGUIDList)), breakable.CenterPoint, currentRoom, correctForWalls: true); ;
                    } else {
                        if (UnityEngine.Random.value <= 0.1f) {
                            List<int> SpawnableRareItems = new List<int>() { 74, 73, 127 };
                            LootEngine.SpawnItem(PickupObjectDatabase.GetById(BraveUtility.RandomElement(SpawnableRareItems)).gameObject, breakable.CenterPoint, Vector2.zero, 0f, true, false, false);
                        } else {
                            List<int> SpawnableItems = new List<int>() { 68, 70 };
                            LootEngine.SpawnItem(PickupObjectDatabase.GetById(BraveUtility.RandomElement(SpawnableItems)).gameObject, breakable.CenterPoint, Vector2.zero, 0f, true, false, false);
                        }
                    }
                }                
            }
        }

        private void GenerateCorruptedTileAtPositionMaybe(Dungeon dungeon, RoomHandler parentRoom, IntVector2 position, float CorruptionOdds = 0.5f, bool AllowGlitchShader = true, float GlitchShaderOdds = 0.2f) {

            if (dungeon == null | parentRoom == null | UnityEngine.Random.value > CorruptionOdds) { return; }
            
            tk2dSpriteCollectionData dungeonCollection = dungeon.tileIndices.dungeonCollection;
            
            List<int> CurrentFloorWallIDs = new List<int>();
            List<int> CurrentFloorFloorIDs = new List<int>();
            List<int> CurrentFloorMiscIDs = new List<int>();

            // Select Sprite ID lists based on tileset. (IDs corrispond to different sprites depending on tileset dungeonCollection)
            if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                CurrentFloorWallIDs = ExpandLists.CastleWallIDs;
                CurrentFloorFloorIDs = ExpandLists.CastleFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.CastleMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.GUNGEON) {
                CurrentFloorWallIDs = ExpandLists.GungeonWallIDs;
                CurrentFloorFloorIDs = ExpandLists.GungeonFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.GungeonMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.MINEGEON) {
                CurrentFloorWallIDs = ExpandLists.MinesWallIDs;
                CurrentFloorFloorIDs = ExpandLists.MinesFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.MinesMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                CurrentFloorWallIDs = ExpandLists.HollowsWallIDs;
                CurrentFloorFloorIDs = ExpandLists.HollowsFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.HollowsMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                CurrentFloorWallIDs = ExpandLists.ForgeWallIDs;
                CurrentFloorFloorIDs = ExpandLists.ForgeFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.ForgeMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.HELLGEON) {
                CurrentFloorWallIDs = ExpandLists.BulletHell_WallIDs;
                CurrentFloorFloorIDs = ExpandLists.BulletHell_FloorIDs;
                CurrentFloorMiscIDs = ExpandLists.BulletHell_MiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.SEWERGEON) {
                CurrentFloorWallIDs = ExpandLists.SewerWallIDs;
                CurrentFloorFloorIDs = ExpandLists.SewerFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.SewerMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) {
                CurrentFloorWallIDs = ExpandLists.AbbeyWallIDs;
                CurrentFloorFloorIDs = ExpandLists.AbbeyFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.AbbeyMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.RATGEON | dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                CurrentFloorWallIDs = ExpandLists.RatDenWallIDs;
                CurrentFloorFloorIDs = ExpandLists.RatDenFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.RatDenMiscIDs;
            } else if (dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                foreach (int id in ExpandLists.Nakatomi_OfficeWallIDs) { CurrentFloorWallIDs.Add(id); }
                foreach (int id in ExpandLists.Nakatomi_OfficeFloorIDs) { CurrentFloorFloorIDs.Add(id); }
                foreach (int id in ExpandLists.Nakatomi_OfficeMiscIDs) { CurrentFloorMiscIDs.Add(id); }
                foreach (int id in ExpandLists.Nakatomi_FutureWallIDs) { CurrentFloorWallIDs.Add(id + 704); }
                foreach (int id in ExpandLists.Nakatomi_FutureFloorIDs) { CurrentFloorFloorIDs.Add(id + 704); }
                foreach (int id in ExpandLists.Nakatomi_FutureMiscIDs) { CurrentFloorMiscIDs.Add(id + 704); }
            } else {
                Dungeon tempDungeonPrefab = DungeonDatabase.GetOrLoadByName("Base_Gungeon");
                dungeonCollection = tempDungeonPrefab.tileIndices.dungeonCollection;
                CurrentFloorWallIDs = ExpandLists.GungeonWallIDs;
                CurrentFloorFloorIDs = ExpandLists.GungeonFloorIDs;
                CurrentFloorMiscIDs = ExpandLists.GungeonMiscIDs;
                tempDungeonPrefab = null;
            }
            
            bool isWallCell = false;

            if (dungeon.data.isWall(position.x, position.y) | dungeon.data.isAnyFaceWall(position.x, position.y) | dungeon.data.isWall(position.x, position.y - 1)) {
                isWallCell = true;
            }
            
            GameObject m_GlitchTile = new GameObject("GlitchTile_" + UnityEngine.Random.Range(1000000, 9999999)) { layer = 22 };
            m_GlitchTile.transform.position = position.ToVector2();
            m_GlitchTile.transform.parent = parentRoom.hierarchyParent;
            
            List<int> spriteIDs = new List<int>();
            int TileType = UnityEngine.Random.Range(1, 3);
            if (TileType == 1) { spriteIDs = CurrentFloorWallIDs.Shuffle(); }
            if (TileType == 2) { spriteIDs = CurrentFloorFloorIDs.Shuffle(); }
            if (TileType == 3) { spriteIDs = CurrentFloorMiscIDs.Shuffle(); }
            
            m_GlitchTile.AddComponent<tk2dSprite>();
            
            tk2dSprite m_GlitchSprite = m_GlitchTile.GetComponent<tk2dSprite>();
            m_GlitchSprite.Collection = dungeonCollection;
            m_GlitchSprite.SetSprite(m_GlitchSprite.Collection, BraveUtility.RandomElement(spriteIDs));
            m_GlitchSprite.ignoresTiltworldDepth = false;
            m_GlitchSprite.depthUsesTrimmedBounds = false;
            m_GlitchSprite.allowDefaultLayer = false;
            m_GlitchSprite.OverrideMaterialMode = tk2dBaseSprite.SpriteMaterialOverrideMode.NONE;
            m_GlitchSprite.independentOrientation = false;
            m_GlitchSprite.hasOffScreenCachedUpdate = false;
            if (isWallCell) {
                m_GlitchSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.PERPENDICULAR;
            } else {
                m_GlitchSprite.CachedPerpState = tk2dBaseSprite.PerpendicularState.FLAT;
            }
            m_GlitchSprite.SortingOrder = 2;
            m_GlitchSprite.IsBraveOutlineSprite = false;
            m_GlitchSprite.IsZDepthDirty = false;
            m_GlitchSprite.ApplyEmissivePropertyBlock = false;
            m_GlitchSprite.GenerateUV2 = false;
            m_GlitchSprite.LockUV2OnFrameOne = false;
            m_GlitchSprite.StaticPositions = false;

            m_GlitchTile.AddComponent<DebrisObject>();
            DebrisObject m_GlitchDebris = m_GlitchTile.GetComponent<DebrisObject>();
            m_GlitchDebris.angularVelocity = 0;
            m_GlitchDebris.angularVelocityVariance = 0;
            m_GlitchDebris.animatePitFall = false;
            m_GlitchDebris.bounceCount = 0;
            m_GlitchDebris.breakOnFallChance = 0;
            m_GlitchDebris.breaksOnFall = false;
            m_GlitchDebris.canRotate = false;
            m_GlitchDebris.changesCollisionLayer = false;
            m_GlitchDebris.collisionStopsBullets = false;
            m_GlitchDebris.followupBehavior = DebrisObject.DebrisFollowupAction.None;
            m_GlitchDebris.IsAccurateDebris = true;
            m_GlitchDebris.IsCorpse = false;
            m_GlitchDebris.lifespanMax = 1200;
            m_GlitchDebris.lifespanMin = 1100;
            m_GlitchDebris.motionMultiplier = 0;
            m_GlitchDebris.pitFallSplash = false;
            m_GlitchDebris.playAnimationOnTrigger = false;
            m_GlitchDebris.PreventAbsorption = true;
            m_GlitchDebris.PreventFallingInPits = true;
            m_GlitchDebris.Priority = EphemeralObject.EphemeralPriority.Ephemeral;
            m_GlitchDebris.shouldUseSRBMotion = false;
            m_GlitchDebris.usesDirectionalFallAnimations = false;
            m_GlitchDebris.usesLifespan = true;

            if (isWallCell) {                
                if (dungeon.data.isFaceWallLower(position.x, position.y) && !dungeon.data.isWall(position.x, position.y - 1)) {
                    DepthLookupManager.ProcessRenderer(m_GlitchSprite.renderer, DepthLookupManager.GungeonSortingLayer.BACKGROUND);
                    m_GlitchSprite.IsPerpendicular = false;
                    m_GlitchSprite.HeightOffGround = 0;
                    m_GlitchSprite.UpdateZDepth();                    
                } else {
                    m_GlitchSprite.HeightOffGround = 3;
                    m_GlitchSprite.UpdateZDepth();
                }
                m_GlitchTile.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
            } else {
                DepthLookupManager.ProcessRenderer(m_GlitchSprite.renderer, DepthLookupManager.GungeonSortingLayer.BACKGROUND);
                m_GlitchTile.SetLayerRecursively(LayerMask.NameToLayer("BG_Critical"));
                m_GlitchSprite.IsPerpendicular = false;
                // m_GlitchSprite.HeightOffGround = -4f;
                m_GlitchSprite.HeightOffGround = -1.7f;
                m_GlitchSprite.SortingOrder = 2;
                m_GlitchSprite.UpdateZDepth();
            }
            if (AllowGlitchShader && UnityEngine.Random.value <= GlitchShaderOdds) {
                float RandomIntervalFloat = UnityEngine.Random.Range(0.02f, 0.06f);
                float RandomDispFloat = UnityEngine.Random.Range(0.07f, 0.09f);
                float RandomDispIntensityFloat = UnityEngine.Random.Range(0.085f, 0.2f);
                float RandomColorProbFloat = UnityEngine.Random.Range(0.04f, 0.15f);
                float RandomColorIntensityFloat = UnityEngine.Random.Range(0.08f, 0.14f);
            
                ExpandShaders.Instance.ApplyGlitchShader(m_GlitchSprite, true, RandomIntervalFloat, RandomDispFloat, RandomDispIntensityFloat, RandomColorProbFloat, RandomColorIntensityFloat);
            }
            return;
        }
    }
}

