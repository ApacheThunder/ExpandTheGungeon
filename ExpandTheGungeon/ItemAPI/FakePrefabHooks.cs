using System;
using MonoMod.RuntimeDetour;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

	public static class FakePrefabHooks {

        public delegate TResult Func<T1, T2, T3, T4, T5, out TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

        public static void Init() {
			Hook hook = new Hook(
                typeof(PlayerController).GetMethod("AcquirePassiveItemPrefabDirectly"),
                typeof(FakePrefabHooks).GetMethod("AcquirePassiveItemPrefabDirectly")
            );
			Hook hook2 = new Hook(
                typeof(PlayerItem).GetMethod("Pickup"), 
                typeof(FakePrefabHooks).GetMethod("ActivePickup")
            );
			Hook hook3 = new Hook(
                typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[] { typeof(UnityEngine.Object), typeof(Transform), typeof(bool) }),
                typeof(FakePrefabHooks).GetMethod("InstantiateOPI")
            );

			Hook hook4 = new Hook(
                typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[] { typeof(UnityEngine.Object), typeof(Transform) }),
                typeof(FakePrefabHooks).GetMethod("InstantiateOP")
            );

			Hook hook5 = new Hook(
                typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[] { typeof(UnityEngine.Object) }),
                typeof(FakePrefabHooks).GetMethod("InstantiateO")
            );
			Hook hook6 = new Hook(
                typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[] { typeof(UnityEngine.Object), typeof(Vector3), typeof(Quaternion) }),
                typeof(FakePrefabHooks).GetMethod("InstantiateOPR")
            );
			Hook hook7 = new Hook(
                typeof(UnityEngine.Object).GetMethod("Instantiate", new Type[] { typeof(UnityEngine.Object), typeof(Vector3), typeof(Quaternion), typeof(Transform) }),
                typeof(FakePrefabHooks).GetMethod("InstantiateOPRP")
            );
		}

		public static void AcquirePassiveItemPrefabDirectly(Action<PlayerController, PassiveItem> orig, PlayerController self, PassiveItem item) {
            try {
                bool flag = FakePrefab.IsFakePrefab(item.gameObject);
                bool flag2 = flag;
                if (flag2) { item.gameObject.SetActive(true); }
                orig(self, item);
                bool flag3 = flag;
                if (flag3) { item.gameObject.SetActive(false); }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { Debug.Log(ex); }
            }
		}

		public static void ActivePickup(Action<PlayerItem, PlayerController> orig, PlayerItem self, PlayerController player) {
            try {
                bool flag = FakePrefab.IsFakePrefab(self.gameObject);
                bool flag2 = flag;
                if (flag2) { self.gameObject.SetActive(true); }
                orig(self, player);
                bool flag3 = flag;
                if (flag3) { self.gameObject.SetActive(false); }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { Debug.Log(ex); }
            }
		}

		public static UnityEngine.Object InstantiateOPI(Func<UnityEngine.Object, Transform, bool, UnityEngine.Object> orig, UnityEngine.Object original, Transform parent, bool instantiateInWorldSpace) {
			return FakePrefab.InstantiateFakePrefab(original, orig(original, parent, instantiateInWorldSpace));
		}

		public static UnityEngine.Object InstantiateOP(Func<UnityEngine.Object, Transform, UnityEngine.Object> orig, UnityEngine.Object original, Transform parent) {
			return FakePrefab.InstantiateFakePrefab(original, orig(original, parent));
		}

		public static UnityEngine.Object InstantiateO(Func<UnityEngine.Object, UnityEngine.Object> orig, UnityEngine.Object original) {
			return FakePrefab.InstantiateFakePrefab(original, orig(original));
        }

		public static UnityEngine.Object InstantiateOPR(Func<UnityEngine.Object, Vector3, Quaternion, UnityEngine.Object> orig, UnityEngine.Object original, Vector3 position, Quaternion rotation) {
			return FakePrefab.InstantiateFakePrefab(original, orig(original, position, rotation));
		}

		public static UnityEngine.Object InstantiateOPRP(Func<UnityEngine.Object, Vector3, Quaternion, Transform, UnityEngine.Object> orig, UnityEngine.Object original, Vector3 position, Quaternion rotation, Transform parent) {
			return FakePrefab.InstantiateFakePrefab(original, orig(original, position, rotation, parent));
		}
	}
}

