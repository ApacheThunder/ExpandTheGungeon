using UnityEngine;


namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandRemoveGunOnAmmoDepletion : MonoBehaviour {

        public ExpandRemoveGunOnAmmoDepletion() {
            AmmoCutOff = 0;
            ExplodesOnDepletion = true;
        }

        public int AmmoCutOff;

        public bool ExplodesOnDepletion;

        protected Gun m_gun;

        private void Start() { m_gun = GetComponent<Gun>(); }

        private void Update() {
            if (enabled && m_gun && m_gun.ammo <= AmmoCutOff && m_gun.CurrentOwner is PlayerController) {
                PlayerController playerController = m_gun.CurrentOwner as PlayerController;
                if (playerController) {
                    playerController.inventory.RemoveGunFromInventory(m_gun);
                    if (ExplodesOnDepletion) {
                        Exploder.DoDefaultExplosion(m_gun.sprite.WorldCenter, Vector2.zero, null, true, CoreDamageTypes.None, false);
                    }
                    Destroy(m_gun.gameObject);
                }
            }
        }
    }
}

 