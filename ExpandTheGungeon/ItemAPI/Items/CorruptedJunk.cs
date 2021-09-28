using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using ExpandTheGungeon.ExpandPrefab;
using ExpandTheGungeon.ExpandMain;
using ExpandTheGungeon.SpriteAPI;

namespace ExpandTheGungeon.ItemAPI {
    
    public class CorruptedJunk : PassiveItem {
                
        public static GameObject CorruptedJunkObject;

        public static int CorruptedJunkID;

        private static List<string> m_SpriteNames;

        public static void Init(AssetBundle expandSharedAssets1) {
            
            CorruptedJunkObject = expandSharedAssets1.LoadAsset<GameObject>("Corrupted Junk");

            CorruptedJunk poopSack = CorruptedJunkObject.AddComponent<CorruptedJunk>();
            SpriteSerializer.AddSpriteToObject(CorruptedJunkObject, ExpandPrefabs.EXItemCollection, "corrupted_poopsack_09");
            
            string shortDesc = "Next Time... What even is this!?";
            string longDesc = "Just some corrupted junk.\n\nCarrying this around makes you question your sanity...";

            ItemBuilder.SetupItem(poopSack, shortDesc, longDesc, "ex");
            poopSack.quality = ItemQuality.A;
            if (!ExpandSettings.EnableEXItems) { poopSack.quality = ItemQuality.EXCLUDED; }
            poopSack.CanBeDropped = false;
            CorruptedJunkID = poopSack.PickupObjectId;

            m_SpriteNames = new List<string> {
                "corrupted_poopsack_01",
                "corrupted_poopsack_02",
                "corrupted_poopsack_03",
                "corrupted_poopsack_04",
                "corrupted_poopsack_05",
                "corrupted_poopsack_06",
                "corrupted_poopsack_07",
                "corrupted_poopsack_08",
                "corrupted_poopsack_09",
                "corrupted_poopsack_10"
            };
            
            ExpandUtility.GenerateSpriteAnimator(CorruptedJunkObject, playAutomatically: true);
            ExpandUtility.AddAnimation(CorruptedJunkObject.GetComponent<tk2dSpriteAnimator>(), ExpandPrefabs.EXItemCollection.GetComponent<tk2dSpriteCollectionData>(), m_SpriteNames, "idle", tk2dSpriteAnimationClip.WrapMode.RandomLoop, 20);
        }
        

        public CorruptedJunk() {
            m_PickedUp = false;

            AllowedEffects = new List<ItemEffectType>() {
                ItemEffectType.BigCash,
                ItemEffectType.ArmorUp,
                ItemEffectType.ManyKeys,
                ItemEffectType.BlanksRUS,
                ItemEffectType.HealthPak,
                ItemEffectType.DrStone,
                ItemEffectType.MuchAmmo
            };
        }

        private bool m_PickedUp;

        public enum ItemEffectType { BigCash, ArmorUp, ManyKeys, BlanksRUS, HealthPak, DrStone, MuchAmmo }

        public List<ItemEffectType> AllowedEffects;

        private ItemEffectType m_SelectedEffect;

        public override void Pickup(PlayerController player) {                        
            base.Pickup(player);
            ExpandPlaceWallMimic.PlayerHasCorruptedJunk = true;
            HandleUIAnimation();
            HandleRandomEffect(player);
            m_PickedUp = true;
        }

        private void HandleUIAnimation() {

            // if (m_PickedUp) { return; }

            MinimapUIController minimapDock = null;

            if (Minimap.Instance) {
                minimapDock = Minimap.Instance.UIMinimap;
            } else {
                minimapDock = FindObjectOfType<MinimapUIController>();
            }
            
            if (minimapDock) {
                List<Tuple<tk2dSprite, PassiveItem>> m_DockItems = ReflectionHelpers.ReflectGetField<List<Tuple<tk2dSprite, PassiveItem>>>(typeof(MinimapUIController), "dockItems", minimapDock);
                
                if (m_DockItems != null && m_DockItems.Count > 0) {
                    for (int i = 0; i < m_DockItems.Count; i++) {
                        if (m_DockItems[i].Second is CorruptedJunk) {
                            if (!m_DockItems[i].First.gameObject.GetComponent<tk2dSpriteAnimator>()) {
                                ExpandUtility.GenerateSpriteAnimator(m_DockItems[i].First.gameObject, playAutomatically: true);
                                ExpandUtility.AddAnimation(m_DockItems[i].First.gameObject.GetComponent<tk2dSpriteAnimator>(), m_DockItems[i].First.Collection, m_SpriteNames, "idle", tk2dSpriteAnimationClip.WrapMode.RandomLoop, 20);
                            }
                            if (m_DockItems[i].First.spriteAnimator) {
                                if (!m_DockItems[i].First.spriteAnimator.IsPlaying("idle")) {
                                    m_DockItems[i].First.spriteAnimator.Play("idle");
                                }
                            }
                        } else {
                            if (!m_PickedUp && UnityEngine.Random.value <= 0.6f) { ExpandShaders.Instance.ApplyGlitchShader(m_DockItems[i].First); }
                        }
                    }
                }

                List<Tuple<tk2dSprite, PassiveItem>> m_secondaryDockItems = ReflectionHelpers.ReflectGetField<List<Tuple<tk2dSprite, PassiveItem>>>(typeof(MinimapUIController), "secondaryDockItems", minimapDock);

                if (m_secondaryDockItems != null && m_secondaryDockItems.Count > 0) {
                    for (int i = 0; i < m_secondaryDockItems.Count; i++) {
                        if (m_secondaryDockItems[i].Second is CorruptedJunk) {
                            if (!m_secondaryDockItems[i].First.gameObject.GetComponent<tk2dSpriteAnimator>()) {
                                ExpandUtility.GenerateSpriteAnimator(m_secondaryDockItems[i].First.gameObject, playAutomatically: true);
                                ExpandUtility.AddAnimation(m_secondaryDockItems[i].First.gameObject.GetComponent<tk2dSpriteAnimator>(), m_secondaryDockItems[i].First.Collection, m_SpriteNames, "idle", tk2dSpriteAnimationClip.WrapMode.RandomLoop, 20);
                            }
                            if (m_secondaryDockItems[i].First.spriteAnimator) {
                                if (!m_secondaryDockItems[i].First.spriteAnimator.IsPlaying("idle")) {
                                    m_secondaryDockItems[i].First.spriteAnimator.Play("idle");
                                }
                            }
                        } else {
                            if (!m_PickedUp && UnityEngine.Random.value <= 0.6f) { ExpandShaders.Instance.ApplyGlitchShader(m_secondaryDockItems[i].First); }
                        }
                    }
                }
            }
        }


        private void HandleRandomEffect(PlayerController player) {

            if (m_PickedUp | AllowedEffects == null | AllowedEffects.Count <=0 ) { return; }

            ExpandShaders.Instance.GlitchScreenForDuration();
            
            AllowedEffects = AllowedEffects.Shuffle();

            m_SelectedEffect = BraveUtility.RandomElement(AllowedEffects);

            if (m_SelectedEffect == ItemEffectType.ArmorUp) {
                player.healthHaver.Armor += 10f;
            } else if (m_SelectedEffect == ItemEffectType.BigCash) {
                player.carriedConsumables.Currency = 999;
            } else if (m_SelectedEffect == ItemEffectType.ManyKeys) {
                player.carriedConsumables.KeyBullets = 999;
            } else if (m_SelectedEffect == ItemEffectType.BlanksRUS) {
                PickupObject blankPickup = PickupObjectDatabase.GetById(224);
                for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++) {
                    if (blankPickup) { LootEngine.GivePrefabToPlayer(blankPickup.gameObject, player); }
                }
            } else if (m_SelectedEffect == ItemEffectType.HealthPak) {
                if (player.characterIdentity != PlayableCharacters.Robot) {
                    StatModifier healthUp = new StatModifier() {
                        statToBoost = PlayerStats.StatType.Health,
                        amount = UnityEngine.Random.Range(4, 6),
                        modifyType = StatModifier.ModifyMethod.ADDITIVE,
                        isMeatBunBuff = false,
                    };
                    player.ownerlessStatModifiers.Add(healthUp);
                    player.stats.RecalculateStats(player, false, false);
                } else {
                    player.healthHaver.Armor += UnityEngine.Random.Range(4, 6);
                }
            } else if (m_SelectedEffect == ItemEffectType.DrStone) {
                PickupObject glassStone = PickupObjectDatabase.GetById(565);
                for (int i = 0; i < UnityEngine.Random.Range(8, 12); i++) {
                    if (glassStone) { LootEngine.GivePrefabToPlayer(glassStone.gameObject, player); }
                }
            } else if (m_SelectedEffect == ItemEffectType.MuchAmmo) {
                StatModifier ManyBullets = new StatModifier() {
                    statToBoost = PlayerStats.StatType.AmmoCapacityMultiplier,
                    amount = UnityEngine.Random.Range(1.5f, 3),
                    modifyType = StatModifier.ModifyMethod.MULTIPLICATIVE,
                    isMeatBunBuff = false,
                };
                player.ownerlessStatModifiers.Add(ManyBullets);
                player.stats.RecalculateStats(player, false, false);
            }
            return;
        }
        
        public override DebrisObject Drop(PlayerController player) {
            DebrisObject drop = base.Drop(player);
            GetComponent<CorruptedJunk>().m_pickedUpThisRun = true;
            GetComponent<CorruptedJunk>().m_PickedUp = true;
            ExpandPlaceWallMimic.PlayerHasCorruptedJunk = false;
            return drop;
        }


        public override void MidGameSerialize(List<object> data) {
            base.MidGameSerialize(data);
            data.Add(m_PickedUp);
        }

        public override void MidGameDeserialize(List<object> data) {
            base.MidGameDeserialize(data);
            if (data.Count == 1) { m_PickedUp = (bool)data[0]; }
        }


        protected override void Update() {
            base.Update();
        }

        protected override void OnDestroy() {
            ExpandPlaceWallMimic.PlayerHasCorruptedJunk = false;
            base.OnDestroy();
        }
    }
}

