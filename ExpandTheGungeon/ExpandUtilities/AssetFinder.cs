using System.IO;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

    public class AssetFinder : BraveBehaviour {

        public static GameObject GetOrLoadByName(string name) {
            AssetBundle assetBundle = ResourceManager.LoadAssetBundle("dungeons/" + name.ToLower());
            DebugTime.RecordStartTime();
            GameObject component = assetBundle.LoadAsset<GameObject>(name);
            DebugTime.Log("AssetBundle.LoadAsset<Dungeon>({0})", new object[] { name });
            return component;
        }


        public static void ListAssets(string[] args) {
			FieldInfo field = typeof(BraveResources).GetField("m_assetBundle", BindingFlags.Static | BindingFlags.NonPublic);
			AssetBundle assetBundle = field.GetValue(null) as AssetBundle;
			string[] allAssetNames = assetBundle.GetAllAssetNames();
			ETGModConsole.Log("Assets: " + allAssetNames.Length, false);
			foreach (string text in allAssetNames) { ETGModConsole.Log(text, false); }
		}

        public static void ListAssetsInBundle(AssetBundle assetBundle) {
            string[] allAssetNames = assetBundle.GetAllAssetNames();
            ETGModConsole.Log("Assets: " + allAssetNames.Length, false);
            foreach (string text in allAssetNames) { ETGModConsole.Log(text, false); }
        }

        public static void CheckSubAssetsInBundle(AssetBundle assetBundle, string assetName) {
            GameObject[] BaseObject = assetBundle.LoadAssetWithSubAssets<GameObject>(assetName);
            if (BaseObject == null) {
                ETGModConsole.Log("ERROR: Selected Asset does not exist or is null!", false);
                return;
            }
            ETGModConsole.Log("Asset Count: " + BaseObject.Length, false);
            for (int i = 0; i < BaseObject.Length; i++) { ETGModConsole.Log(BaseObject[i].ToString(), false); }
        }

        public static void ListSharedAssets() {
			string path = Path.Combine(ETGMod.ResourcesDirectory, "assets.txt");
			using (StreamWriter streamWriter = new StreamWriter(path, true)) {
				string[] array = new string[] {
					"shared_base_001",
					"shared_auto_002",
					"shared_auto_001"
				};

                foreach (string text in array) {
					streamWriter.WriteLine(string.Format("=={0}==", text));
					AssetBundle assetBundle = ResourceManager.LoadAssetBundle(text);
					string[] allAssetNames = assetBundle.GetAllAssetNames();
					ETGModConsole.Log("Assets: " + allAssetNames.Length, false);
					foreach (string str in allAssetNames) { streamWriter.WriteLine("\t" + str); }
				}
			}
		}

		public static string[] assetBundles = new string[] {
			"dungeon_scene_001",
			"shared_base_001",
			"shared_auto_002",
			"foyer_003",
			"foyer_001",
			"shared_auto_001",
			"enemies_base_001",
			"foyer_002",
			"dungeons",
			"dungeons/base_tutorial",
			"dungeons/finalscenario_soldier",
			"dungeons/base_foyer",
			"dungeons/finalscenario_convict",
			"dungeons/base_castle",
			"dungeons/base_cathedral",
			"dungeons/base_mines",
			"dungeons/finalscenario_coop",
			"dungeons/base_catacombs",
			"dungeons/base_bullethell",
			"dungeons/finalscenario_pilot",
			"dungeons/finalscenario_guide",
			"dungeons/finalscenario_robot",
			"dungeons/base_forge",
			"dungeons/finalscenario_bullet",
			"dungeons/base_gungeon",
			"dungeons/base_resourcefulrat",
			"dungeons/base_sewer",
            "dungeons/base_nakatomi",
			"flows_base_001",
			"brave_resources_001",
			"encounters_base_001"
		};
	}
}
