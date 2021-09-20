using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI
{
    public class CustomHoveringGunSynergyProcessor : MonoBehaviour
    {
        public CustomSynergyType RequiredSynergy;

        public bool RequiresSynergy;

        public int TargetGunID;

        public bool UsesMultipleGuns;

        public int[] TargetGunIDs;

        public HoveringGunController.HoverPosition PositionType;

        public HoveringGunController.AimType AimType;

        public HoveringGunController.FireType FireType;

        public float FireCooldown;

        public float FireDuration;

        public bool OnlyOnEmptyReload;

        public string ShootAudioEvent;

        public string OnEveryShotAudioEvent;

        public string FinishedShootingAudioEvent;

        public HoveringGunSynergyProcessor.TriggerStyle Trigger;

        public int NumToTrigger;

        public float TriggerDuration;

        public bool ConsumesTargetGunAmmo;

        public float ChanceToConsumeTargetGunAmmo;

        public CustomHoveringGunSynergyProcessor() : base()
        {
            this.FireCooldown = 1f;
            this.FireDuration = 2f;
            this.NumToTrigger = 1;
            this.TriggerDuration = -1f;
            this.ChanceToConsumeTargetGunAmmo = 0.5f;
            this.m_hovers = new List<HoveringGunController>();
            this.m_initialized = new List<bool>();
        }

        public void Awake()
        {
            this.m_gun = base.GetComponent<Gun>();
            this.m_item = base.GetComponent<PassiveItem>();
        }

        private bool IsInitialized(int index)
        {
            return this.m_initialized.Count > index && this.m_initialized[index];
        }

        public void Update()
        {
            if (this.Trigger == HoveringGunSynergyProcessor.TriggerStyle.CONSTANT)
            {
                if (this.m_gun)
                {
                    if (this.m_gun && this.m_gun.isActiveAndEnabled && this.m_gun.CurrentOwner && (!this.RequiresSynergy || this.m_gun.OwnerHasSynergy(this.RequiredSynergy)))
                    {
                        for (int i = 0; i < this.NumToTrigger; i++)
                        {
                            if (!this.IsInitialized(i))
                            {
                                this.Enable(i);
                            }
                        }
                    }
                    else
                    {
                        this.DisableAll();
                    }
                }
                else if (this.m_item)
                {
                    if (this.m_item && this.m_item.Owner && (!this.RequiresSynergy || this.m_item.Owner.HasActiveBonusSynergy(this.RequiredSynergy, false)))
                    {
                        for (int j = 0; j < this.NumToTrigger; j++)
                        {
                            if (!this.IsInitialized(j))
                            {
                                this.Enable(j);
                            }
                        }
                    }
                    else
                    {
                        this.DisableAll();
                    }
                }
            }
            else if (this.Trigger == HoveringGunSynergyProcessor.TriggerStyle.ON_DAMAGE)
            {
                if (!this.m_actionsLinked && this.m_gun && this.m_gun.CurrentOwner)
                {
                    PlayerController playerController = this.m_gun.CurrentOwner as PlayerController;
                    this.m_cachedLinkedPlayer = playerController;
                    playerController.OnReceivedDamage += this.HandleOwnerDamaged;
                    this.m_actionsLinked = true;
                }
                else if (this.m_actionsLinked && this.m_gun && !this.m_gun.CurrentOwner && this.m_cachedLinkedPlayer)
                {
                    this.m_cachedLinkedPlayer.OnReceivedDamage -= this.HandleOwnerDamaged;
                    this.m_cachedLinkedPlayer = null;
                    this.m_actionsLinked = false;
                }
            }
            else if (this.Trigger == HoveringGunSynergyProcessor.TriggerStyle.ON_ACTIVE_ITEM)
            {
                if (!this.m_actionsLinked && this.m_gun && this.m_gun.CurrentOwner)
                {
                    PlayerController playerController2 = this.m_gun.CurrentOwner as PlayerController;
                    this.m_cachedLinkedPlayer = playerController2;
                    playerController2.OnUsedPlayerItem += this.HandleOwnerItemUsed;
                    this.m_actionsLinked = true;
                }
                else if (this.m_actionsLinked && this.m_gun && !this.m_gun.CurrentOwner && this.m_cachedLinkedPlayer)
                {
                    this.m_cachedLinkedPlayer.OnUsedPlayerItem -= this.HandleOwnerItemUsed;
                    this.m_cachedLinkedPlayer = null;
                    this.m_actionsLinked = false;
                }
            }
        }

        private void HandleOwnerItemUsed(PlayerController sourcePlayer, PlayerItem sourceItem)
        {
            if ((!this.RequiresSynergy || sourcePlayer.HasActiveBonusSynergy(this.RequiredSynergy, false)) && this.GetOwner())
            {
                for (int i = 0; i < this.NumToTrigger; i++)
                {
                    int num = 0;
                    while (this.IsInitialized(num))
                    {
                        num++;
                    }
                    this.Enable(num);
                    base.StartCoroutine(this.ActiveItemDisable(num, sourcePlayer));
                }
            }
        }

        private void HandleOwnerDamaged(PlayerController sourcePlayer)
        {
            if ((!this.RequiresSynergy || sourcePlayer.HasActiveBonusSynergy(this.RequiredSynergy, false)))
            {
                for (int i = 0; i < this.NumToTrigger; i++)
                {
                    int num = 0;
                    while (this.IsInitialized(num))
                    {
                        num++;
                    }
                    this.Enable(num);
                    base.StartCoroutine(this.TimedDisable(num, this.TriggerDuration));
                }
            }
        }

        private IEnumerator ActiveItemDisable(int index, PlayerController player)
        {
            yield return null;
            while (player && player.CurrentItem && player.CurrentItem.IsActive)
            {
                yield return null;
            }
            this.Disable(index);
            yield break;
        }

        private IEnumerator TimedDisable(int index, float duration)
        {
            yield return new WaitForSeconds(duration);
            this.Disable(index);
            yield break;
        }

        private void OnDisable()
        {
            this.DisableAll();
        }

        private PlayerController GetOwner()
        {
            if (this.m_gun)
            {
                return this.m_gun.CurrentOwner as PlayerController;
            }
            if (this.m_item)
            {
                return this.m_item.Owner;
            }
            return null;
        }

        private void Enable(int index)
        {
            if (this.m_initialized.Count > index && this.m_initialized[index])
            {
                return;
            }
            PlayerController owner = this.GetOwner();
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(ResourceCache.Acquire("Global Prefabs/HoveringGun") as GameObject, owner.CenterPosition.ToVector3ZisY(0f), Quaternion.identity);
            gameObject.transform.parent = owner.transform;
            while (this.m_hovers.Count < index + 1)
            {
                this.m_hovers.Add(null);
                this.m_initialized.Add(false);
            }
            this.m_hovers[index] = gameObject.GetComponent<HoveringGunController>();
            this.m_hovers[index].ShootAudioEvent = this.ShootAudioEvent;
            this.m_hovers[index].OnEveryShotAudioEvent = this.OnEveryShotAudioEvent;
            this.m_hovers[index].FinishedShootingAudioEvent = this.FinishedShootingAudioEvent;
            this.m_hovers[index].ConsumesTargetGunAmmo = this.ConsumesTargetGunAmmo;
            this.m_hovers[index].ChanceToConsumeTargetGunAmmo = this.ChanceToConsumeTargetGunAmmo;
            this.m_hovers[index].Position = this.PositionType;
            this.m_hovers[index].Aim = this.AimType;
            this.m_hovers[index].Trigger = this.FireType;
            this.m_hovers[index].CooldownTime = this.FireCooldown;
            this.m_hovers[index].ShootDuration = this.FireDuration;
            this.m_hovers[index].OnlyOnEmptyReload = this.OnlyOnEmptyReload;
            Gun gun = null;
            int num = this.TargetGunID;
            if (this.UsesMultipleGuns)
            {
                num = this.TargetGunIDs[index];
            }
            for (int i = 0; i < owner.inventory.AllGuns.Count; i++)
            {
                if (owner.inventory.AllGuns[i].PickupObjectId == num)
                {
                    gun = owner.inventory.AllGuns[i];
                }
            }
            if (!gun)
            {
                gun = (PickupObjectDatabase.Instance.InternalGetById(num) as Gun);
            }
            this.m_hovers[index].Initialize(gun, owner);
            this.m_initialized[index] = true;
        }

        private void Disable(int index)
        {
            if (this.m_hovers[index])
            {
                UnityEngine.Object.Destroy(this.m_hovers[index].gameObject);
            }
        }

        private void DisableAll()
        {
            for (int i = 0; i < this.m_hovers.Count; i++)
            {
                if (this.m_hovers[i])
                {
                    UnityEngine.Object.Destroy(this.m_hovers[i].gameObject);
                }
            }
            this.m_hovers.Clear();
            this.m_initialized.Clear();
        }

        public void OnDestroy()
        {
            if (this.m_actionsLinked && this.m_cachedLinkedPlayer)
            {
                this.m_cachedLinkedPlayer.OnReceivedDamage -= this.HandleOwnerDamaged;
                this.m_cachedLinkedPlayer = null;
                this.m_actionsLinked = false;
            }
        }

        private bool m_actionsLinked;

        private PlayerController m_cachedLinkedPlayer;

        private Gun m_gun;

        private PassiveItem m_item;

        private List<HoveringGunController> m_hovers;

        private List<bool> m_initialized;
    }
}