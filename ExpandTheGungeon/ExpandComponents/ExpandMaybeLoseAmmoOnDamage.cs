using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandMaybeLoseAmmoOnDamage : MonoBehaviour, IGunInheritable {

        public ExpandMaybeLoseAmmoOnDamage() {
            DepleteAmmoOnDamage = true;
            DepleteAmmoOnDamageOdds = 0.2f;
            HasBootlegTransmorgify = true;
            IsBootlegShotgun = true;
            TransfmorgifyTargetGUIDs = new List<string>();
        }
        
        public bool DepleteAmmoOnDamage;
        public bool HasBootlegTransmorgify;
        public bool IsBootlegShotgun;        
        public float DepleteAmmoOnDamageOdds;

        public List<string> TransfmorgifyTargetGUIDs;

        private bool m_hasAwoken;
        private bool m_gunBroken;
        private Gun m_gun;
        private PlayerController m_playerOwner;

        public bool Broken {
            get { return m_gunBroken; }
            set { m_gunBroken = value; }
        }

        private void Awake() {
            m_hasAwoken = true;
            if (!m_gun) { m_gun = GetComponent<Gun>(); }
            if (m_gun) {
                m_gun.OnInitializedWithOwner = (Action<GameActor>)Delegate.Combine(m_gun.OnInitializedWithOwner, new Action<GameActor>(OnGunInitialized));
                m_gun.OnDropped = (Action)Delegate.Combine(m_gun.OnDropped, new Action(OnGunDroppedOrDestroyed));
                if (m_gun.CurrentOwner != null) { OnGunInitialized(m_gun.CurrentOwner); }

                if (TransfmorgifyTargetGUIDs != null && IsBootlegShotgun) {
                    List<string> m_GUIDlist = new List<string>() {
                        ExpandCustomEnemyDatabase.BootlegShotgunManBlueGUID,
                        ExpandCustomEnemyDatabase.BootlegShotgunManRedGUID
                    };
                    m_GUIDlist = m_GUIDlist.Shuffle();
                    TransfmorgifyTargetGUIDs = new List<string>() { BraveUtility.RandomElement(m_GUIDlist) };
                }
            }
        }

        private void Start() { }

        private void Update() { }
        
        private void OnGunInitialized(GameActor actor) {
            if (m_playerOwner != null) { OnGunDroppedOrDestroyed(); }
            if (actor == null) { return; }

            if (actor is PlayerController) {
                m_playerOwner = (actor as PlayerController);
                m_playerOwner.OnReceivedDamage += OnReceivedDamage;
                m_gun.PostProcessProjectile += TransmorgifyPostProcess;
            }
        }

        public void TransmorgifyPostProcess(Projectile projectile) {
            projectile.CanTransmogrify = true;
            projectile.ChanceToTransmogrify = 0.15f;
            projectile.TransmogrifyTargetGuids = TransfmorgifyTargetGUIDs.ToArray();
        }

        private void OnReceivedDamage(PlayerController player) {
            if (player && player.CurrentGun == m_gun) {
                if (!m_gunBroken) { m_gunBroken = true; }
                if (DepleteAmmoOnDamage && UnityEngine.Random.value <= 0.5) { m_gun.ammo = 0; }
            }
        }
        
        private void OnGunDroppedOrDestroyed() {
            if (m_playerOwner != null) {
                m_playerOwner.OnReceivedDamage -= OnReceivedDamage;
                m_playerOwner = null;
            }
        }

        public void InheritData(Gun sourceGun) {
            if (sourceGun) {
                if (!m_hasAwoken) { m_gun = GetComponent<Gun>(); }
                ExpandMaybeLoseAmmoOnDamage component = sourceGun.GetComponent<ExpandMaybeLoseAmmoOnDamage>();
                if (component) { m_gunBroken = component.m_gunBroken; }
            }
        }

        public void MidGameSerialize(List<object> data, int dataIndex) { data.Add(Broken); }

        public void MidGameDeserialize(List<object> data, ref int dataIndex) {
            Broken = (bool)data[dataIndex];
            dataIndex++;
        }

        private void OnDestroy() { OnGunDroppedOrDestroyed(); }
    }
}

