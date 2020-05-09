using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;


namespace ExpandTheGungeon.ItemAPI {

    public class CustomMasterRounds : MonoBehaviour {

        public static int CanyonMasterRoundID = -1;

        public static void Init() {

            // Master round for Custom Secret Floor via Hollow
            string name = "Corrupted Master Round";
            string resourcePath = "ExpandTheGungeon/Textures/Items/glitchround";
            GameObject canyonMasterObject = new GameObject();
            BasicStatPickup canyonMasterRound = canyonMasterObject.AddComponent<BasicStatPickup>();
            ItemBuilder.AddSpriteToObject(name, resourcePath, canyonMasterObject, true);
            string shortDesc = "Corrupted Chamber";
            string longDesc = "This weird artefact indicates mastery of... somewhere";
            ItemBuilder.SetupItem(canyonMasterRound, shortDesc, longDesc, "ex");
            canyonMasterRound.quality = PickupObject.ItemQuality.SPECIAL;
            canyonMasterRound.ItemSpansBaseQualityTiers = false;
            canyonMasterRound.additionalMagnificenceModifier = 0;
            canyonMasterRound.ItemRespectsHeartMagnificence = true;
            canyonMasterRound.associatedItemChanceMods = new LootModData[0];
            canyonMasterRound.contentSource = ContentSource.BASE;
            canyonMasterRound.ShouldBeExcludedFromShops = false;
            canyonMasterRound.CanBeDropped = true;
            canyonMasterRound.PreventStartingOwnerFromDropping = false;
            canyonMasterRound.PersistsOnDeath = false;
            canyonMasterRound.RespawnsIfPitfall = false;
            canyonMasterRound.PreventSaveSerialization = false;
            canyonMasterRound.IgnoredByRat = false;
            canyonMasterRound.SaveFlagToSetOnAcquisition = 0;
            // canyonMasterRound.ForcedPositionInAmmonomicon = 5;
            canyonMasterRound.UsesCustomCost = true;
            canyonMasterRound.CustomCost = 90;
            canyonMasterRound.CanBeSold = true;
            canyonMasterRound.passiveStatModifiers = new StatModifier[0];
            canyonMasterRound.ArmorToGainOnInitialPickup = 0;
            canyonMasterRound.modifiers = new List<StatModifier>() {
                new StatModifier() {
                    statToBoost = PlayerStats.StatType.Health,
                    modifyType = StatModifier.ModifyMethod.ADDITIVE,
                    amount = 1,
                    isMeatBunBuff = false
                }
            };
            canyonMasterRound.ArmorToGive = 0;
            canyonMasterRound.ModifiesDodgeRoll = false;
            canyonMasterRound.DodgeRollTimeMultiplier = 0.9f;
            canyonMasterRound.DodgeRollDistanceMultiplier = 1.25f;
            canyonMasterRound.AdditionalInvulnerabilityFrames = 0;
            canyonMasterRound.IsJunk = false;
            canyonMasterRound.GivesCurrency = false;
            canyonMasterRound.CurrencyToGive = 0;
            canyonMasterRound.IsMasteryToken = true;
            CanyonMasterRoundID = canyonMasterRound.PickupObjectId;
        }
    }
}

