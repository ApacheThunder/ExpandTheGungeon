using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;


public class BuildAssetBundles {
	
	public static Dictionary<BuildTarget, string> Builds = new Dictionary<BuildTarget, string> {
		// { BuildTarget.StandaloneOSX, "MacOS" },
		{ BuildTarget.StandaloneLinux, "Linux" },
		{ BuildTarget.StandaloneWindows, "Windows" }
	};

    [MenuItem("Asset Bundles/Build AssetBundles")]
    public static void BuildAllAssetBundles() {
        // string assetBundleDirectory = "Assets/AssetBundles";
        string assetBundleDirectory = "Assets/AssetBundles/";
        if(!Directory.Exists(assetBundleDirectory)) { Directory.CreateDirectory(assetBundleDirectory); }
        if(!Directory.Exists((assetBundleDirectory + "Windows"))) { Directory.CreateDirectory((assetBundleDirectory + "Windows")); }
        if(!Directory.Exists((assetBundleDirectory + "Linux"))) { Directory.CreateDirectory((assetBundleDirectory + "Linux")); }
        // if(!Directory.Exists((assetBundleDirectory + "MacOS"))) { Directory.CreateDirectory((assetBundleDirectory + "MacOS")); }
		// BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
		foreach (var kvp in Builds) {
			Debug.Log(kvp.Value);
			BuildPipeline.BuildAssetBundles((assetBundleDirectory + kvp.Value), BuildAssetBundleOptions.None, kvp.Key);
		}
    }
	
}

