using System;
using System.Collections;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ItemAPI;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandChamberGunProcessor : MonoBehaviour, ILevelLoadedListener {

        public ExpandChamberGunProcessor() {
            RefillsOnFloorChange = true;
            JustActiveReloaded = false;
            // Mimic gun used as placeholder for unsupported/unused tilesets
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
            PhobosGunID = 734; // mimic_gun
            OldWestGunID = 734; // mimic_gun
            SpaceGunID = 734; // mimic_gun
            FinalGeonID = 734; // mimic_gun
        }

        public bool RefillsOnFloorChange;

        [NonSerialized]
        public bool JustActiveReloaded;
        

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
        public int PhobosGunID;
        [PickupIdentifier]
        public int OldWestGunID;
        [PickupIdentifier]
        public int SpaceGunID;
        [PickupIdentifier]
        public int FinalGeonID;

        
        private GlobalDungeonData.ValidTilesets m_currentTileset;
        private Gun m_gun;

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
                // Getting this master round will unlock all forms.
                if (playerController.HasPassiveItem(CustomMasterRounds.GtlichFloorMasterRoundID)) { return true; }
                switch (t) {
                    case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                        return playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Castle);
                    case GlobalDungeonData.ValidTilesets.GUNGEON:
                        return playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Gungeon);
                    case GlobalDungeonData.ValidTilesets.MINEGEON:
                        return playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Mines);
                    case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                        return playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Catacombs);
                    case GlobalDungeonData.ValidTilesets.FORGEGEON:
                        return playerController.HasPassiveItem(GlobalItemIds.MasteryToken_Forge);
                    default:
                        return false;
                }
            } else {
                return false;
            }
        }

        private void ChangeToTileset(GlobalDungeonData.ValidTilesets tileSet) {
            switch (tileSet) {
                case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                    ChangeForme(CastleGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.CASTLEGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.SEWERGEON:
                    ChangeForme(OublietteGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.SEWERGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.JUNGLEGEON:
                    ChangeForme(JungleGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.JUNGLEGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.GUNGEON:
                    ChangeForme(GungeonGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.GUNGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                    ChangeForme(AbbeyGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.CATHEDRALGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.BELLYGEON:
                    ChangeForme(BellyGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.BELLYGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.MINEGEON:
                    ChangeForme(MinesGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.MINEGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.RATGEON:
                    ChangeForme(RatgeonGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.RATGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                    ChangeForme(HollowGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.CATACOMBGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                    ChangeForme(OfficeGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.OFFICEGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.WESTGEON:
                    ChangeForme(OldWestGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.WESTGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.PHOBOSGEON:
                    ChangeForme(PhobosGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.PHOBOSGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.FORGEGEON:
                    ChangeForme(ForgeGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.FORGEGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.SPACEGEON:
                    ChangeForme(SpaceGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.SPACEGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.HELLGEON:
                    ChangeForme(HellGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.HELLGEON;
                    return;
                case GlobalDungeonData.ValidTilesets.FINALGEON:
                    ChangeForme(FinalGeonID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.FINALGEON;
                    return;
                default:
                    ChangeForme(CastleGunID);
                    m_currentTileset = GlobalDungeonData.ValidTilesets.CASTLEGEON;
                    return;
            }
        }

        private void ChangeForme(int targetID) {
            Gun targetGun = (PickupObjectDatabase.GetById(targetID) as Gun);
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
            switch (inTileset) {
                case GlobalDungeonData.ValidTilesets.CASTLEGEON:
                    return GlobalDungeonData.ValidTilesets.SEWERGEON;
                case GlobalDungeonData.ValidTilesets.SEWERGEON:
                    return GlobalDungeonData.ValidTilesets.JUNGLEGEON;
                case GlobalDungeonData.ValidTilesets.JUNGLEGEON:
                    return GlobalDungeonData.ValidTilesets.GUNGEON;
                case GlobalDungeonData.ValidTilesets.GUNGEON:
                    return GlobalDungeonData.ValidTilesets.CATHEDRALGEON;
                case GlobalDungeonData.ValidTilesets.CATHEDRALGEON:
                    return GlobalDungeonData.ValidTilesets.BELLYGEON;
                case GlobalDungeonData.ValidTilesets.BELLYGEON:
                    return GlobalDungeonData.ValidTilesets.MINEGEON;
                case GlobalDungeonData.ValidTilesets.MINEGEON:
                    return GlobalDungeonData.ValidTilesets.RATGEON;
                case GlobalDungeonData.ValidTilesets.RATGEON:
                    return GlobalDungeonData.ValidTilesets.CATACOMBGEON;
                case GlobalDungeonData.ValidTilesets.CATACOMBGEON:
                    return GlobalDungeonData.ValidTilesets.OFFICEGEON;
                case GlobalDungeonData.ValidTilesets.OFFICEGEON:
                    return GlobalDungeonData.ValidTilesets.WESTGEON;
                case GlobalDungeonData.ValidTilesets.WESTGEON:
                    return GlobalDungeonData.ValidTilesets.PHOBOSGEON;
                case GlobalDungeonData.ValidTilesets.PHOBOSGEON:
                    return GlobalDungeonData.ValidTilesets.FORGEGEON;
                case GlobalDungeonData.ValidTilesets.FORGEGEON:
                    return GlobalDungeonData.ValidTilesets.SPACEGEON;
                case GlobalDungeonData.ValidTilesets.SPACEGEON:
                    return GlobalDungeonData.ValidTilesets.HELLGEON;
                case GlobalDungeonData.ValidTilesets.HELLGEON:
                    return GlobalDungeonData.ValidTilesets.FINALGEON;
                case GlobalDungeonData.ValidTilesets.FINALGEON:
                    return GlobalDungeonData.ValidTilesets.CASTLEGEON;
                default:
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

