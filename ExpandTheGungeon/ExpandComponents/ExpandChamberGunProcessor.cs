using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ItemAPI;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandChamberGunProcessor : MonoBehaviour, ILevelLoadedListener {

        public ExpandChamberGunProcessor() {
            CastleGunID = 647;
            GungeonGunID = 660;
            MinesGunID = 807;
            HollowGunID = 659;
            ForgeGunID = 658;
            HellGunID = 763;
            OublietteGunID = 657;
            JungleGunID = 368; // el_tigre
            BellyGunID = 734; // mimic_gun
            AbbeyGunID = 806;
            RatgeonGunID = 808;
            OfficeGunID = 823;
            CanyonGunID = 734; // mimic_gun
            OldWestGunID = 734; // mimic_gun
            RefillsOnFloorChange = true;
        }
        
        [PickupIdentifier]
        public int CastleGunID;

        [PickupIdentifier]
        public int GungeonGunID;

        [PickupIdentifier]
        public int MinesGunID;

        [PickupIdentifier]
        public int HollowGunID;

        [PickupIdentifier]
        public int ForgeGunID;

        [PickupIdentifier]
        public int HellGunID;

        [PickupIdentifier]
        public int OublietteGunID;

        [PickupIdentifier]
        public int JungleGunID;

        [PickupIdentifier]
        public int BellyGunID;

        [PickupIdentifier]
        public int AbbeyGunID;

        [PickupIdentifier]
        public int RatgeonGunID;

        [PickupIdentifier]
        public int OfficeGunID;

        [PickupIdentifier]
        public int CanyonGunID; // Using Phobos tileset ID

        [PickupIdentifier]
        public int OldWestGunID;

        public bool RefillsOnFloorChange;

        private GlobalDungeonData.ValidTilesets m_currentTileset;
        private Gun m_gun;

        [NonSerialized]
        public bool JustActiveReloaded;

        private void Awake() {
            m_currentTileset = GlobalDungeonData.ValidTilesets.CASTLEGEON;
            m_gun = GetComponent<Gun>();
            Gun gun = m_gun;
            gun.OnReloadPressed = (Action<PlayerController, Gun, bool>)Delegate.Combine(gun.OnReloadPressed, new Action<PlayerController, Gun, bool>(HandleReloadPressed));
        }

        private GlobalDungeonData.ValidTilesets GetFloorTileset() {
            if (GameManager.Instance.IsLoadingLevel || !GameManager.Instance.Dungeon) { return GlobalDungeonData.ValidTilesets.CASTLEGEON; }
            return GameManager.Instance.Dungeon.tileIndices.tilesetId;
        }

        private bool IsValidTileset(GlobalDungeonData.ValidTilesets t) {
            if (t == GetFloorTileset()) { return true; }
            PlayerController playerController = m_gun.CurrentOwner as PlayerController;
            if (playerController) {
                if (t == GlobalDungeonData.ValidTilesets.CASTLEGEON && playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Castle)) { return true; }
                if (t == GlobalDungeonData.ValidTilesets.GUNGEON && playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Gungeon)) { return true; }
                if (t == GlobalDungeonData.ValidTilesets.MINEGEON && playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Mines)) { return true; }
                if (t == GlobalDungeonData.ValidTilesets.CATACOMBGEON && playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Catacombs)) { return true; }
                if (t == GlobalDungeonData.ValidTilesets.PHOBOSGEON && playerController.HasPassiveItem(CustomMasterRounds.CanyonMasterRoundID)) { return true; }
                if (t == GlobalDungeonData.ValidTilesets.FORGEGEON && playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Forge)) { return true; }
            }
            return false;
        }

        private void ChangeToTileset(GlobalDungeonData.ValidTilesets t) {
            if (t == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                ChangeForme(CastleGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.CASTLEGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.SEWERGEON) {
                ChangeForme(OublietteGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.SEWERGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                ChangeForme(JungleGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.JUNGLEGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.GUNGEON) {
                ChangeForme(GungeonGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.GUNGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) {
                ChangeForme(AbbeyGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.CATHEDRALGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.BELLYGEON) {
                ChangeForme(BellyGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.BELLYGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.MINEGEON) {
                ChangeForme(MinesGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.MINEGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.RATGEON) {
                ChangeForme(RatgeonGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.RATGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                ChangeForme(HollowGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.CATACOMBGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                ChangeForme(OfficeGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.OFFICEGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                ChangeForme(CanyonGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.PHOBOSGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                ChangeForme(CanyonGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.PHOBOSGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.WESTGEON) {
                ChangeForme(OldWestGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.WESTGEON;
            } else if (t == GlobalDungeonData.ValidTilesets.HELLGEON) {
                ChangeForme(HellGunID);
                m_currentTileset = GlobalDungeonData.ValidTilesets.HELLGEON;
            }
        }

        private void ChangeForme(int targetID) {
            Gun targetGun = PickupObjectDatabase.GetById(targetID) as Gun;
            m_gun.TransformToTargetGun(targetGun);
        }

        private void Update() {
            if (Dungeon.IsGenerating || GameManager.Instance.IsLoadingLevel) { return; }
            if (m_gun && (!m_gun.CurrentOwner || !IsValidTileset(m_currentTileset))) {
                GlobalDungeonData.ValidTilesets validTilesets = GetFloorTileset();
                if (!m_gun.CurrentOwner) { validTilesets = GlobalDungeonData.ValidTilesets.CASTLEGEON; }
                if (m_currentTileset != validTilesets) { ChangeToTileset(validTilesets); }
            }
            JustActiveReloaded = false;
        }

        private GlobalDungeonData.ValidTilesets GetNextTileset(GlobalDungeonData.ValidTilesets inTileset) {

            if (inTileset == GlobalDungeonData.ValidTilesets.CASTLEGEON) {
                return GlobalDungeonData.ValidTilesets.SEWERGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.SEWERGEON) {
                return GlobalDungeonData.ValidTilesets.JUNGLEGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.JUNGLEGEON) {
                return GlobalDungeonData.ValidTilesets.GUNGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.GUNGEON) {
                return GlobalDungeonData.ValidTilesets.CATHEDRALGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.CATHEDRALGEON) {
                return GlobalDungeonData.ValidTilesets.BELLYGEON;
            } if (inTileset == GlobalDungeonData.ValidTilesets.BELLYGEON) {
                return GlobalDungeonData.ValidTilesets.MINEGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.MINEGEON) {
                return GlobalDungeonData.ValidTilesets.RATGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.RATGEON) {
                return GlobalDungeonData.ValidTilesets.CATACOMBGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.CATACOMBGEON) {
                return GlobalDungeonData.ValidTilesets.PHOBOSGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                return GlobalDungeonData.ValidTilesets.OFFICEGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.OFFICEGEON) {
                return GlobalDungeonData.ValidTilesets.WESTGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.WESTGEON) {
                return GlobalDungeonData.ValidTilesets.FORGEGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.FORGEGEON) {
                return GlobalDungeonData.ValidTilesets.HELLGEON;
            } else if (inTileset == GlobalDungeonData.ValidTilesets.HELLGEON) {
                return GlobalDungeonData.ValidTilesets.CASTLEGEON;
            } else {
                return GlobalDungeonData.ValidTilesets.CASTLEGEON;
            }
        }

        private GlobalDungeonData.ValidTilesets GetNextValidTileset() {
            GlobalDungeonData.ValidTilesets nextTileset = GetNextTileset(m_currentTileset);
            while (!IsValidTileset(nextTileset)) { nextTileset = GetNextTileset(nextTileset); }
            return nextTileset;
        }

        private void HandleReloadPressed(PlayerController ownerPlayer, Gun sourceGun, bool manual) {
            if (JustActiveReloaded) { return; }
            if (manual && !sourceGun.IsReloading) {
                GlobalDungeonData.ValidTilesets nextValidTileset = GetNextValidTileset();
                if (m_currentTileset != nextValidTileset) { ChangeToTileset(nextValidTileset); }
            }
        }

        public void BraveOnLevelWasLoaded() {
            if (RefillsOnFloorChange && m_gun && m_gun.CurrentOwner) { m_gun.StartCoroutine(DelayedRegainAmmo()); }
        }

        private IEnumerator DelayedRegainAmmo() {
            yield return null;
            while (Dungeon.IsGenerating) { yield return null; }
            if (RefillsOnFloorChange && m_gun && m_gun.CurrentOwner) { m_gun.GainAmmo(m_gun.AdjustedMaxAmmo); }
            yield break;
        }
    }
}

