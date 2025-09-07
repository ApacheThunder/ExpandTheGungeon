using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class BuildAssetBundles {
	
	public static Dictionary<BuildTarget, string> Builds = new Dictionary<BuildTarget, string> {
		{ BuildTarget.StandaloneWindows, "Windows" },
		{ BuildTarget.StandaloneLinux, "Linux" },
		{ BuildTarget.StandaloneOSX, "MacOS" }
	};

    public static string AssetBundlesPath = "Assets/AssetBundles/";
    public static BuildAssetBundleOptions BundleOptions = BuildAssetBundleOptions.None;

    [MenuItem("Asset Bundles/Build AssetBundles")]
    public static void BuildAllAssetBundles() {
        if(!Directory.Exists(AssetBundlesPath))Directory.CreateDirectory(AssetBundlesPath);
		foreach (var kvp in Builds) {
			Debug.Log(kvp.Value);
            if (!Directory.Exists(AssetBundlesPath + kvp.Value))Directory.CreateDirectory((AssetBundlesPath + kvp.Value));
            BuildPipeline.BuildAssetBundles((AssetBundlesPath + kvp.Value), BundleOptions, kvp.Key);
		}
    }
}

