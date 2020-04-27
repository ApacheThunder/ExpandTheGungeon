using System.Collections.Generic;
using ExpandTheGungeon.ItemAPI;
using UnityEngine;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandSynergies : MonoBehaviour {

        public static void Init() {
            List<AdvancedSynergyEntry> m_TempSynergyList = new List<AdvancedSynergyEntry>();

            foreach (AdvancedSynergyEntry entry in GameManager.Instance.SynergyManager.synergies) { m_TempSynergyList.Add(entry); }

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "Master of Unlocking",
                    MandatoryItemIDs = new List<int>() { TheLeadKey.TheLeadKeyPickupID, 356 }, // The Lead Key + Trusty Lockpicks
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0) { 140, 356 }, // Master of Unlocking + Trusty Lockpicks. The original synergy left unused in the game.
                    // NumberObjectsRequired = 2,
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>() { CustomSynergyType.MASTER_OF_UNLOCKING }
                }
            );

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "Master of Unlocking",
                    MandatoryItemIDs = new List<int>() { 140, 356 }, // Master of Unlocking + Trusty Lockpicks. The original synergy left unused in the game.
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>() { CustomSynergyType.MASTER_OF_UNLOCKING }
                }
            );

            GameManager.Instance.SynergyManager.synergies = m_TempSynergyList.ToArray();
            return;
        }
    }
}

