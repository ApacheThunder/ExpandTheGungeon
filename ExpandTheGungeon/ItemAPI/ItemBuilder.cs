using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Gungeon;
using UnityEngine;
using HutongGames.Utility;

namespace ExpandTheGungeon.ItemAPI {

	public static class ItemBuilder {
        
        public enum CooldownType {
            Timed,
            Damage,
            PerRoom,
            None
        }

        private static Assembly baseAssembly;

        public static void SetAssembly(Type t) { baseAssembly = t.Assembly; }
        
        public static void Init() {
			FakePrefabHooks.Init();
			try {
				MethodBase method = new StackFrame(1, false).GetMethod();
				Type declaringType = method.DeclaringType;
				SetAssembly(declaringType);
			} catch (Exception ex) {
				ETGModConsole.Log(ex.Message, false);
				ETGModConsole.Log(ex.StackTrace, false);
			}
		}
        
        public static void AddSpriteToObject(GameObject targetObject, Texture2D sourceTexture) {
            SpriteBuilder.SpriteFromTexture(sourceTexture, targetObject);
        }

        public static void SetupItem(PickupObject item, string shortDesc, string longDesc, string AmmonomiconSpriteName, string idPool = "expandItems") {
			try {
				item.encounterTrackable = null;
				ETGMod.Databases.Items.SetupItem(item, item.name);
				SpriteBuilder.AddToAmmonomicon(item.sprite.Collection.GetSpriteDefinition(AmmonomiconSpriteName), item.sprite.Collection.GetSpriteDefinition(AmmonomiconSpriteName).material);
                item.encounterTrackable.journalData.AmmonomiconSprite = AmmonomiconSpriteName;
				GunExt.SetName(item, item.name);
				GunExt.SetShortDescription(item, shortDesc);
				GunExt.SetLongDescription(item, longDesc);
				bool flag = item is PlayerItem;
				if (flag) { (item as PlayerItem).consumable = false; }
				Game.Items.Add(idPool + ":" + item.name.ToLower().Replace(" ", "_"), item);
				ETGMod.Databases.Items.Add(item, false, "ANY");
			} catch (Exception ex) {
                UnityEngine.Debug.LogException(ex);
				ETGModConsole.Log(ex.Message, false);
				ETGModConsole.Log(ex.StackTrace, false);
			}
		}
        
        public static void SetupItem(PickupObject item, string shortDesc, string longDesc, string idPool = "expandItems") {
			try {
				item.encounterTrackable = null;
				ETGMod.Databases.Items.SetupItem(item, item.name);
				SpriteBuilder.AddToAmmonomicon(item.sprite.GetCurrentSpriteDef());
				item.encounterTrackable.journalData.AmmonomiconSprite = item.sprite.GetCurrentSpriteDef().name;
				GunExt.SetName(item, item.name);
				GunExt.SetShortDescription(item, shortDesc);
				GunExt.SetLongDescription(item, longDesc);
				bool flag = item is PlayerItem;
				if (flag) { (item as PlayerItem).consumable = false; }
				Game.Items.Add(idPool + ":" + item.name.ToLower().Replace(" ", "_"), item);
				ETGMod.Databases.Items.Add(item, false, "ANY");
			} catch (Exception ex) {
                UnityEngine.Debug.LogException(ex);
				ETGModConsole.Log(ex.Message, false);
				ETGModConsole.Log(ex.StackTrace, false);
			}
		}

        public static void SetupEXItem(PickupObject item, string name, string shortDesc, string longDesc, string idPool = "ex", bool createEncounterTrackable = true) {
			try {
                item.encounterTrackable = null;
                ETGMod.Databases.Items.SetupItem(item, item.name);
                SpriteBuilder.AddToAmmonomicon(item.sprite.GetCurrentSpriteDef());
                item.encounterTrackable.journalData.AmmonomiconSprite = item.sprite.GetCurrentSpriteDef().name;
                GunExt.SetName(item, name);
                GunExt.SetShortDescription(item, shortDesc);
                GunExt.SetLongDescription(item, longDesc);
                bool isPlayerItem = item is PlayerItem;
				if (isPlayerItem) { (item as PlayerItem).consumable = false; }
				Game.Items.Add(idPool + ":" + name.ToLower().Replace(" ", "_"), item);
				ETGMod.Databases.Items.Add(item, false, "ANY");
                if (!createEncounterTrackable) { UnityEngine.Object.Destroy(item.encounterTrackable); }
			} catch (Exception ex) {
                UnityEngine.Debug.LogException(ex);
				ETGModConsole.Log(ex.Message, false);
				ETGModConsole.Log(ex.StackTrace, false);
			}
		}

        public static void SetCooldownType(PlayerItem item, CooldownType cooldownType, float value) {
			item.damageCooldown = -1f;
			item.roomCooldown = -1;
			item.timeCooldown = -1f;
			switch (cooldownType) {
			    case CooldownType.Timed:
				    item.timeCooldown = value;
				    break;
			    case CooldownType.Damage:
				    item.damageCooldown = value;
				    break;
			    case CooldownType.PerRoom:
				    item.roomCooldown = (int)value;
				break;
			}
		}

		public static void AddPassiveStatModifier(PickupObject po, PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod method = 0) {
			StatModifier statModifier = new StatModifier();
			statModifier.amount = amount;
			statModifier.statToBoost = statType;
			statModifier.modifyType = method;
			bool flag = po is PlayerItem;
			if (flag) {
				PlayerItem playerItem = po as PlayerItem;
				bool flag2 = playerItem.passiveStatModifiers == null;
				if (flag2) {
					playerItem.passiveStatModifiers = new StatModifier[] { statModifier };
				} else {
					playerItem.passiveStatModifiers = playerItem.passiveStatModifiers.Concat(new StatModifier[] { statModifier }).ToArray();
				}
			} else {
				bool flag3 = po is PassiveItem;
				if (!flag3) {
					throw new NotSupportedException("Object must be of type PlayerItem or PassiveItem");
				}
				PassiveItem passiveItem = po as PassiveItem;
				bool flag4 = passiveItem.passiveStatModifiers == null;
				if (flag4) {
					passiveItem.passiveStatModifiers = new StatModifier[] { statModifier };
				} else {
					passiveItem.passiveStatModifiers = passiveItem.passiveStatModifiers.Concat(new StatModifier[] { statModifier }).ToArray();
				}
			}
		}

		public static IEnumerator HandleDuration(PlayerItem item, float duration, PlayerController user, Action<PlayerController> OnFinish) {
			bool isCurrentlyActive = item.IsCurrentlyActive;
			if (isCurrentlyActive) { yield break; }
            SetPrivateType(item, "m_isCurrentlyActive", true);
            SetPrivateType(item, "m_activeElapsed", 0f);
            SetPrivateType(item, "m_activeDuration", duration);
            item.OnActivationStatusChanged?.Invoke(item);
            float elapsed = GetPrivateType<PlayerItem, float>(item, "m_activeElapsed");
			float dur = GetPrivateType<PlayerItem, float>(item, "m_activeDuration");
			while (GetPrivateType<PlayerItem, float>(item, "m_activeElapsed") < ItemBuilder.GetPrivateType<PlayerItem, float>(item, "m_activeDuration") && item.IsCurrentlyActive) {
				yield return null;
			}
            SetPrivateType(item, "m_isCurrentlyActive", false);
            item.OnActivationStatusChanged?.Invoke(item);
            OnFinish?.Invoke(user);
            yield break;
		}

		private static void SetPrivateType<T>(T obj, string field, bool value) {
			FieldInfo field2 = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
			field2.SetValue(obj, value);
		}

		private static void SetPrivateType<T>(T obj, string field, float value) {
			FieldInfo field2 = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
			field2.SetValue(obj, value);
		}

		private static T2 GetPrivateType<T, T2>(T obj, string field) {
			FieldInfo field2 = typeof(T).GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
			return (T2)field2.GetValue(obj);
		}
	}
}
