using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;


namespace ExpandTheGungeon.ItemAPI {

    public class CustomMasterRounds : MonoBehaviour {

        public static int CanyonMasterRoundID = -1;

        public static GameObject CanyonMasterRound;

        public static void Init(AssetBundle expandSharedAssets1) {

            // Master round for Custom Secret Floor via Hollow
            CanyonMasterRound = expandSharedAssets1.LoadAsset<GameObject>("Corrupted Master Round");
            BasicStatPickup CanyonMasterRoundItem = CanyonMasterRound.AddComponent<BasicStatPickup>();
            ItemBuilder.AddSpriteToObject(CanyonMasterRound, expandSharedAssets1.LoadAsset<Texture2D>("glitchround"), false, false);
            string shortDesc = "Corrupted Chamber";
            string longDesc = "This weird artifact indicates mastery of... somewhere";
            ItemBuilder.SetupItem(CanyonMasterRoundItem, shortDesc, longDesc, "ex");
            // CanyonMasterRoundItem.quality = PickupObject.ItemQuality.SPECIAL;
            CanyonMasterRoundItem.quality = PickupObject.ItemQuality.EXCLUDED;
            CanyonMasterRoundItem.ItemSpansBaseQualityTiers = false;
            CanyonMasterRoundItem.additionalMagnificenceModifier = 0;
            CanyonMasterRoundItem.ItemRespectsHeartMagnificence = true;
            CanyonMasterRoundItem.associatedItemChanceMods = new LootModData[0];
            CanyonMasterRoundItem.contentSource = ContentSource.BASE;
            CanyonMasterRoundItem.ShouldBeExcludedFromShops = false;
            CanyonMasterRoundItem.CanBeDropped = true;
            CanyonMasterRoundItem.PreventStartingOwnerFromDropping = false;
            CanyonMasterRoundItem.PersistsOnDeath = false;
            CanyonMasterRoundItem.RespawnsIfPitfall = false;
            CanyonMasterRoundItem.PreventSaveSerialization = false;
            CanyonMasterRoundItem.IgnoredByRat = false;
            CanyonMasterRoundItem.SaveFlagToSetOnAcquisition = 0;
            // CanyonMasterRoundItem.ForcedPositionInAmmonomicon = 5;
            CanyonMasterRoundItem.UsesCustomCost = true;
            CanyonMasterRoundItem.CustomCost = 90;
            CanyonMasterRoundItem.CanBeSold = true;
            CanyonMasterRoundItem.passiveStatModifiers = new StatModifier[0];
            CanyonMasterRoundItem.ArmorToGainOnInitialPickup = 0;
            CanyonMasterRoundItem.modifiers = new List<StatModifier>() {
                new StatModifier() {
                    statToBoost = PlayerStats.StatType.Health,
                    modifyType = StatModifier.ModifyMethod.ADDITIVE,
                    amount = 1,
                    isMeatBunBuff = false
                }
            };
            CanyonMasterRoundItem.ArmorToGive = 0;
            CanyonMasterRoundItem.ModifiesDodgeRoll = false;
            CanyonMasterRoundItem.DodgeRollTimeMultiplier = 0.9f;
            CanyonMasterRoundItem.DodgeRollDistanceMultiplier = 1.25f;
            CanyonMasterRoundItem.AdditionalInvulnerabilityFrames = 0;
            CanyonMasterRoundItem.IsJunk = false;
            CanyonMasterRoundItem.GivesCurrency = false;
            CanyonMasterRoundItem.CurrencyToGive = 0;
            CanyonMasterRoundItem.IsMasteryToken = true;
            CanyonMasterRoundID = CanyonMasterRoundItem.PickupObjectId;
        }
    }
}

