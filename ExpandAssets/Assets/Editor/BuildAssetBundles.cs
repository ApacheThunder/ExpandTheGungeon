using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class BuildAssetBundles {

    public static string AssetBundlesPath = "Assets/AssetBundles/";
	public static string WindowsPath = "Windows";
	
	public static BuildTarget targetPlatform = BuildTarget.StandaloneWindows;
    public static BuildAssetBundleOptions BundleOptions = BuildAssetBundleOptions.None;	


    [MenuItem("Asset Bundles/Build AssetBundles")]
    public static void BuildAllAssetBundles() {
		
        if (!Directory.Exists(AssetBundlesPath))Directory.CreateDirectory(AssetBundlesPath);
		if (!Directory.Exists(AssetBundlesPath + WindowsPath))Directory.CreateDirectory((AssetBundlesPath + WindowsPath));
		
		Debug.Log(WindowsPath); 
		BuildPipeline.BuildAssetBundles((AssetBundlesPath + WindowsPath), BundleOptions, targetPlatform);
    }
}

