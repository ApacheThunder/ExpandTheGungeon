using System.Collections.Generic;
using ExpandTheGungeon.ItemAPI;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandSynergies {

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

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "Master Chambers",
                    MandatoryItemIDs = new List<int>() { 647, CustomMasterRounds.GtlichFloorMasterRoundID }, // Synergy Notification for Chamber Gun and custom master round for Base_Canyon
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );


            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "It fires Grenade Kin now...",
                    MandatoryItemIDs = new List<int>() { 39, BulletKinGun.BulletKinGunID }, // Synergy for The Bullet Kin Gun + RPG
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );


             m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = string.Empty,
                    MandatoryItemIDs = new List<int>() { 815, BulletKinGun.BulletKinGunID }, // Lich Eyes Synergy for this gun.
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );
            
            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "It fires Shotgun Kin now...",
                    MandatoryItemIDs = new List<int>() { 51, BulletKinGun.BulletKinGunID }, // Synergy for The Bullet Kin Gun + Regular Shotgun
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "It fires jammed things now...",
                    MandatoryItemIDs = new List<int>() { 407, BulletKinGun.BulletKinGunID }, // Synergy for The Bullet Kin Gun + Sixth Chamber
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "It fires chickens now...",
                    MandatoryItemIDs = new List<int>() { 572, BulletKinGun.BulletKinGunID }, // Synergy for Chicken Flute + The Bullet Kin Gun
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "Twisted Bricks...",
                    MandatoryItemIDs = new List<int>() { 293, CursedBrick.CursedBrickID }, // Synergy for Mimic Tooth Neckless and Cursed Brick
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "Corrupted Bricks...",
                    MandatoryItemIDs = new List<int>() { CorruptedJunk.CorruptedJunkID, CursedBrick.CursedBrickID }, // Synergy for Corrupted Junk and Cursed Brick
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "Become Friend's Ship...",
                    MandatoryItemIDs = new List<int>() { PortableShip.PortableShipID, 326 }, // Synergy for Portable Ship and Number 2
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );

            m_TempSynergyList.Add(
                new AdvancedSynergyEntry() {
                    NameKey = "Clownin Around...",
                    MandatoryItemIDs = new List<int>() { ClownBullets.ClownBulletsID, ClownFriend.ClownFriendID}, // Synergy for Clown Bullets and Clown Friend
                    IgnoreLichEyeBullets = true,
                    SuppressVFX = false,
                    RequiresAtLeastOneGunAndOneItem = false,
                    MandatoryGunIDs = new List<int>(0),
                    OptionalGunIDs = new List<int>(0),
                    OptionalItemIDs = new List<int>(0),
                    ActivationStatus = SynergyEntry.SynergyActivation.ACTIVE,
                    ActiveWhenGunUnequipped = true,
                    statModifiers = new List<StatModifier>(0),
                    bonusSynergies = new List<CustomSynergyType>(0)
                }
            );

            GameManager.Instance.SynergyManager.synergies = m_TempSynergyList.ToArray();
            return;
        }
    }
}

