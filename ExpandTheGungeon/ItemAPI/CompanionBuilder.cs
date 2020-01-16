/*using System;
using System.Collections.Generic;
using System.Reflection;
using Gungeon;
using MonoMod.RuntimeDetour;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

	public class CompanionBuilder {
        
        public static Dictionary<string, GameObject> companionDictionary = new Dictionary<string, GameObject>();

        private static GameObject behaviorSpeculatorPrefab;

        public static void Init() {
			string companionGuid = Game.Items["dog"].GetComponent<CompanionItem>().CompanionGuid;
			AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(companionGuid);
            behaviorSpeculatorPrefab = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid.gameObject);
			foreach (object obj in behaviorSpeculatorPrefab.transform) {
				Transform transform = (Transform)obj;
				bool flag = transform != behaviorSpeculatorPrefab.transform;
				if (flag) {
					UnityEngine.Object.DestroyImmediate(transform);
				}
			}

			foreach (Component component in behaviorSpeculatorPrefab.GetComponents<Component>()) {
				bool flag2 = component.GetType() != typeof(BehaviorSpeculator);
				if (flag2) {
					UnityEngine.Object.DestroyImmediate(component);
				}
			}
			UnityEngine.Object.DontDestroyOnLoad(behaviorSpeculatorPrefab);
			FakePrefab.MarkAsFakePrefab(behaviorSpeculatorPrefab);
            behaviorSpeculatorPrefab.SetActive(false);
			Hook hook = new Hook(
                typeof(EnemyDatabase).GetMethod("GetOrLoadByGuid", BindingFlags.Static | BindingFlags.Public),
                typeof(CompanionBuilder).GetMethod("GetOrLoadByGuid", BindingFlags.Static | BindingFlags.Public)
            );
		}

		public static AIActor GetOrLoadByGuid(Func<string, AIActor> orig, string guid) {
			foreach (string text in companionDictionary.Keys) {
				bool flag = text == guid;
				if (flag) { return companionDictionary[text].GetComponent<AIActor>(); }
			}
			return orig(guid);
		}

		public static GameObject BuildPrefab(string name, string guid, string defaultSpritePath, IntVector2 hitboxOffset, IntVector2 hitBoxSize) {
			bool flag = companionDictionary.ContainsKey(guid);
			GameObject result;
			if (flag) {
				Tools.PrintError("CompanionBuilder: Tried to create two companion prefabs with the same GUID!", "FF0000");
				result = null;
			} else {
				GameObject gameObject = UnityEngine.Object.Instantiate(behaviorSpeculatorPrefab);
				gameObject.name = name;
				tk2dSprite component = SpriteBuilder.SpriteFromResource(defaultSpritePath, gameObject, false).GetComponent<tk2dSprite>();
				component.SetUpSpeculativeRigidbody(hitboxOffset, hitBoxSize).CollideWithOthers = false;
				gameObject.AddComponent<tk2dSpriteAnimator>();
				gameObject.AddComponent<AIAnimator>();
				HealthHaver healthHaver = gameObject.AddComponent<HealthHaver>();
				healthHaver.RegisterBodySprite(component, false, 0);
				healthHaver.PreventAllDamage = true;
				healthHaver.SetHealthMaximum(15000f, null, false);
				healthHaver.FullHeal();
				AIActor aiactor = gameObject.AddComponent<AIActor>();
				aiactor.State = AIActor.ActorState.Normal;
				aiactor.EnemyGuid = guid;
				BehaviorSpeculator component2 = gameObject.GetComponent<BehaviorSpeculator>();
				component2.MovementBehaviors = new List<MovementBehaviorBase>();
				component2.AttackBehaviors = new List<AttackBehaviorBase>();
				component2.TargetBehaviors = new List<TargetBehaviorBase>();
				component2.OverrideBehaviors = new List<OverrideBehaviorBase>();
				component2.OtherBehaviors = new List<BehaviorBase>();
				EnemyDatabaseEntry item = new EnemyDatabaseEntry {
					myGuid = guid,
					placeableWidth = 2,
					placeableHeight = 2,
					isNormalEnemy = false
				};
				EnemyDatabase.Instance.Entries.Add(item);
                companionDictionary.Add(guid, gameObject);
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				FakePrefab.MarkAsFakePrefab(gameObject);
				gameObject.SetActive(false);
				result = gameObject;
			}
			return result;
		}
	}
}*/

