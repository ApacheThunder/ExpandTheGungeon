using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

	public class FakePrefab : Component {

        internal static HashSet<GameObject> ExistingFakePrefabs = new HashSet<GameObject>();

        public static bool IsFakePrefab(UnityEngine.Object o) {
			bool flag = o is GameObject;
			bool result;
			if (flag) {
				result = ExistingFakePrefabs.Contains((GameObject)o);
			} else {
				bool flag2 = o is Component;
				result = (flag2 && ExistingFakePrefabs.Contains(((Component)o).gameObject));
			}
			return result;
		}

		public static void MarkAsFakePrefab(GameObject obj) { ExistingFakePrefabs.Add(obj); }

		public static GameObject Clone(GameObject obj) {
			bool flag = IsFakePrefab(obj);
			bool activeSelf = obj.activeSelf;
			bool flag2 = activeSelf;
			if (flag2) { obj.SetActive(false); }
			GameObject gameObject = Instantiate(obj);
			bool flag3 = activeSelf;
			if (flag3) { obj.SetActive(true); }
            ExistingFakePrefabs.Add(gameObject);
			bool flag4 = flag;
			if (flag4) { }
			return gameObject;
		}

		public static UnityEngine.Object InstantiateFakePrefab(UnityEngine.Object o, UnityEngine.Object new_o) {
            try {
                bool flag = o is GameObject && ExistingFakePrefabs.Contains((GameObject)o);
                if (flag) {
                    if (ExpandSettings.debugMode) { Tools.Print("Activating fake prefab: " + o.name, "FFFFFF", false); }
			    	((GameObject)new_o).SetActive(true);
			    } else {
			    	bool flag2 = o is Component && ExistingFakePrefabs.Contains(((Component)o).gameObject);
			    	if (flag2) { ((Component)new_o).gameObject.SetActive(true); }
			    }
            } catch (Exception ex) {
                if (ExpandSettings.debugMode) { Debug.Log(ex); }
            }
			return new_o;
		}
	}
}

