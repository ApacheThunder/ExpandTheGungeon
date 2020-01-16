using System;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

    // More flexible version. Allows loading prefabs from different asset bundles. Must specify full path to prefab however. ;)
    public class ExpandLoadPrefab {
        private static AssetBundle m_assetBundle;

        public static UnityEngine.Object Load(string assetbundle, string path, string extension = ".prefab") {
            if (m_assetBundle == null) { EnsureLoaded(assetbundle); }
            return m_assetBundle.LoadAsset<UnityEngine.Object>(path + extension);
        }

        public static UnityEngine.Object Load(string assetbundle, string path, Type type, string extension = ".prefab") {
            if (m_assetBundle == null) { EnsureLoaded(assetbundle); }
            return m_assetBundle.LoadAsset(path + extension, type);
        }

        public static T Load<T>(string assetbundle, string path, string extension = ".prefab") where T : UnityEngine.Object {
            if (m_assetBundle == null) { EnsureLoaded(assetbundle); }
            return m_assetBundle.LoadAsset<T>(path + extension);
        }

        public static void EnsureLoaded(string assetbundle) {
            if (m_assetBundle == null) { m_assetBundle = ResourceManager.LoadAssetBundle(assetbundle); }
        }
    }
}

