using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ExpandResources {
    
    public static class ExpandResourceManager {

        public static List<string> AssetBundles;
                
        public static void InitCustomAssetBundles() {
            AssetBundles = new List<string>() {
                "ExpandResources\\expandsharedauto"
            };

            FieldInfo m_AssetBundlesField = typeof(ResourceManager).GetField("LoadedBundles", BindingFlags.Static | BindingFlags.NonPublic);
            Dictionary<string, AssetBundle> m_AssetBundles = (Dictionary<string, AssetBundle>)m_AssetBundlesField.GetValue(typeof(ResourceManager));

            foreach (string bundlePath in AssetBundles) {
                m_AssetBundles.Add("ExpandSharedAuto", ResourceExtractor.GetAssetBundleFromResource(bundlePath));
            }
        }
    }
}

