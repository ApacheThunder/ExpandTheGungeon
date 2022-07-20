using System.IO;
using Ionic.Zip;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon {
	
	public class ExpandAssets {
        
        public enum AssetSource { BraveResources, SharedAuto1, SharedAuto2, EnemiesBase, FlowBase }

        public static T LoadAsset<T>(string assetPath) where T : UnityEngine.Object {
            return ResourceManager.LoadAssetBundle(ExpandTheGungeon.ModAssetBundleName).LoadAsset<T>(assetPath);
        }

        public static T LoadSpriteAsset<T>(string assetPath) where T : UnityEngine.Object {
            return ResourceManager.LoadAssetBundle(ExpandTheGungeon.ModSpriteAssetBundleName).LoadAsset<T>(assetPath);
        }
        
        public static T LoadOfficialAsset<T>(string assetPath, AssetSource assetType) where T : UnityEngine.Object {
            switch (assetType) {
                case AssetSource.BraveResources:
                    return ResourceManager.LoadAssetBundle("brave_resources_001").LoadAsset<T>(assetPath);
                case AssetSource.SharedAuto1:
                    return ResourceManager.LoadAssetBundle("shared_auto_001").LoadAsset<T>(assetPath);
                case AssetSource.SharedAuto2:
                    return ResourceManager.LoadAssetBundle("shared_auto_002").LoadAsset<T>(assetPath);
                case AssetSource.EnemiesBase:
                    return ResourceManager.LoadAssetBundle("enemies_base_001").LoadAsset<T>(assetPath);
                case AssetSource.FlowBase:
                    return ResourceManager.LoadAssetBundle("flows_base_001").LoadAsset<T>(assetPath);
                default:
                    return ResourceManager.LoadAssetBundle("brave_resources_001").LoadAsset<T>(assetPath);
            }
        }

        public static void InitSpritesAssetBundle() {
            FieldInfo m_AssetBundlesField = typeof(ResourceManager).GetField("LoadedBundles", BindingFlags.Static | BindingFlags.NonPublic);
            Dictionary<string, AssetBundle> m_AssetBundles = (Dictionary<string, AssetBundle>)m_AssetBundlesField.GetValue(typeof(ResourceManager));
            
            AssetBundle m_ExpandSpritesBase = null;
            m_ExpandSpritesBase = LoadFromModZIPOrModFolder(ExpandTheGungeon.ModSpriteAssetBundleName.ToLower());
            if (m_ExpandSpritesBase != null) {
                m_AssetBundles.Add(ExpandTheGungeon.ModSpriteAssetBundleName, m_ExpandSpritesBase);
                ExpandSettings.spritesBundlePresent = true;
            }
        }
                
        public static void InitCustomAssetBundles(string nameSpace = null) {
            Dictionary<string, AssetBundle> m_AssetBundles = ReflectionHelpers.ReflectGetField<Dictionary<string, AssetBundle>>(typeof(ResourceManager), "LoadedBundles");
            AssetBundle m_ExpandSharedAssets1 = null;
            
            try {
                if (string.IsNullOrEmpty(nameSpace)) {
                    m_ExpandSharedAssets1 = LoadFromModZIPOrModFolder(ExpandTheGungeon.ModAssetBundleName.ToLower());
                } else {                    
                    m_ExpandSharedAssets1 = LoadAssetBundleFromResource(ExpandTheGungeon.ModAssetBundleName, nameSpace);
                }
                
                if (m_ExpandSharedAssets1 != null) {
                    m_AssetBundles.Add(ExpandTheGungeon.ModAssetBundleName, m_ExpandSharedAssets1);
                } else {
                    string ErrorMessage = "[ExpandTheGungeon] ERROR: ExpandSharedAuto asset bundle not found!";
                    Debug.Log(ErrorMessage);
                    ExpandTheGungeon.ExceptionText = ErrorMessage;
                    return;
                }
            } catch (Exception ex) {
                string ErrorMessage = "[ExpandTheGungeon] ERROR: Exception while loading custom asset bundles! Possible GUID conflict with other custom AssetBundles?";
                Debug.Log(ErrorMessage);
                Debug.LogException(ex);
                ExpandTheGungeon.ExceptionText = ErrorMessage;
                return;
            }
        }

        public static AssetBundle LoadFromModZIPOrModFolder(string AssetBundleName) {
            AssetBundle m_CachedBundle = null;
            if (File.Exists(ExpandTheGungeon.ZipFilePath)) {
                if (ExpandSettings.debugMode) { Debug.Log("Zip Found"); }
                ZipFile ModZIP = ZipFile.Read(ExpandTheGungeon.ZipFilePath);
                if (ModZIP != null && ModZIP.Entries.Count > 0) {                    
                    foreach (ZipEntry entry in ModZIP.Entries) {
                        if (entry.FileName == AssetBundleName) {
                            using (MemoryStream ms = new MemoryStream()) {
                                entry.Extract(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                m_CachedBundle = AssetBundle.LoadFromStream(ms);
                                break;
                            }
                        }
                    }
                }
            } else if (File.Exists(ExpandTheGungeon.FilePath + "/" + AssetBundleName)) {
                try { m_CachedBundle = AssetBundle.LoadFromFile(ExpandTheGungeon.FilePath + "/" + AssetBundleName); } catch (Exception) { }
            }
            if (m_CachedBundle) { return m_CachedBundle; } else { return null; }
        }
        
        public static AssetBundle LoadAssetBundleFromResource(string AssetBundleName, string nameSpace) {
            using (Stream manifestResourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream($"{nameSpace}." + AssetBundleName)) {
                if (manifestResourceStream != null) {
                    byte[] array = new byte[manifestResourceStream.Length];
                    manifestResourceStream.Read(array, 0, array.Length);
                    return AssetBundle.LoadFromMemory(array);
                }
            }
            ETGModConsole.Log("No bytes found in " + AssetBundleName, false);
            return null;
        }


        public static void InitAudio (AssetBundle expandSharedAssets1, string assetPath) {
            TextAsset SoundBankBinary = expandSharedAssets1.LoadAsset<TextAsset>(assetPath);
            if (SoundBankBinary) {
                byte[] array = SoundBankBinary.bytes;
			    IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			    try {
			    	Marshal.Copy(array, 0, intPtr, array.Length);
			    	uint num;
			    	AKRESULT akresult = AkSoundEngine.LoadAndDecodeBankFromMemory(intPtr, (uint)array.Length, false, SoundBankBinary.name, false, out num);
                    if (ExpandSettings.debugMode) {
                        Console.WriteLine(string.Format("Result of soundbank load: {0}.", akresult));
                    }
			    } finally {
                    Marshal.FreeHGlobal(intPtr);
                }
            }
        }
        
        public static void DumpTexture2DToFile(Texture2D target, bool useRandomFilenames = false) {
            if (target == null) { return; }
            string text = "DUMPsprites/" + "DUMP" + target.name;
            string text2 = text + "/" + "DUMP" + target.name;
            if (useRandomFilenames) {
                text += ("_" + Guid.NewGuid().ToString());
                text2 += ("_" + Guid.NewGuid().ToString());
            }
            string path = Path.Combine(ETGMod.ResourcesDirectory, text.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");
            bool fileExists = File.Exists(path);
            if (!fileExists) {
                path = Path.Combine(ETGMod.ResourcesDirectory, text2.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");
                bool folderPath = !File.Exists(path);
                if (folderPath) {
                    Directory.GetParent(path).Create();
                    File.WriteAllBytes(path, ImageConversion.EncodeToPNG(target));
                }
            }
        }
        
        public static string RetrieveStringFromAssetBundle(AssetBundle bundle, string AssetPath) {
            return bundle.LoadAsset<TextAsset>(AssetPath).text;
        }

        public static List<string> BuildStringListFromAssetBundle(string assetPath) {
            if(string.IsNullOrEmpty(assetPath)) { return new List<string>(); }

            TextAsset m_Asset = LoadAsset<TextAsset>(assetPath);

            if (!m_Asset) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: TextAsset: " + assetPath +" was not found!");
                return new List<string>(0);
            }

            string text = BytesToString(LoadAsset<TextAsset>(assetPath).bytes);
            
            if (string.IsNullOrEmpty(text)) {
                ETGModConsole.Log("[ExpandTheGungeon] ERROR: TextAsset: " + m_Asset.name + " contains no strings!");
                return new List<string>(0);
            }

            List<string> m_CachedList = new List<string>();

            string[] m_CachedStringArray = text.Split(new char[] { '\n' });

            if (m_CachedStringArray == null | m_CachedStringArray.Length <= 0) { return m_CachedList; }

            foreach (string textString in m_CachedStringArray) { m_CachedList.Add(textString); }

            return m_CachedList;
        }

        public static string BytesToString(byte[] bytes) { return Encoding.UTF8.GetString(bytes, 0, bytes.Length); }
        
        public static void SaveStringToFile(string text, string filePath, string fileName) {
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(filePath, fileName), true)) { streamWriter.WriteLine(text); }
        }

        public static string DeserializeJSONDataFromAssetBundle(AssetBundle bundle, string AssetPath, string basePath = "Assets/ExpandSerializedData/", string fileExtension = ".txt") {
            string m_ResultAsset = string.Empty;
            try { m_ResultAsset = bundle.LoadAsset<TextAsset>((basePath + AssetPath + fileExtension)).text; } catch (Exception) { }
            if (!string.IsNullOrEmpty(m_ResultAsset)) {
                return m_ResultAsset;
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] Error! Requested Text asset: " + AssetPath + " returned null! Ensure asset exists in asset bundle!", true);
                return string.Empty;
            }
        }

        public static string[] GetLinesFromAssetBundle(AssetBundle bundle, string AssetPath, string basePath = "Assets/", string fileExtension = ".txt") {
            string m_ResultAsset = string.Empty;
            try { m_ResultAsset = bundle.LoadAsset<TextAsset>((basePath + AssetPath + fileExtension)).text; } catch (Exception) { }
            if (!string.IsNullOrEmpty(m_ResultAsset)) {
                return m_ResultAsset.Split(new char[] { '\n' });
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] Error! Requested Text asset: " + AssetPath + " returned null! Ensure asset exists in asset bundle!", true);
                return new string[0];
            }
        }

        public static TileIndexGrid DeserializeTileIndexGridFromAssetBundle(AssetBundle bundle, string AssetPath, string basePath = "Assets/ExpandSerializedData/TilesetData/", string fileExtension = ".txt") {
            string serializedData = string.Empty;
            try { serializedData = bundle.LoadAsset<TextAsset>((basePath + AssetPath + fileExtension)).text; } catch (Exception) { }
            if (!string.IsNullOrEmpty(serializedData)) {
                TileIndexGrid m_TileIndexGridData = ScriptableObject.CreateInstance<TileIndexGrid>();
                JsonUtility.FromJsonOverwrite(serializedData, m_TileIndexGridData);
                return m_TileIndexGridData;
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] Error! Requested Text asset: " + AssetPath + " returned null! Ensure asset exists in asset bundle!", true);
                return null;
            }
        }

        public static FacewallIndexGridDefinition DeserializeFacewallGridDefinitionFromAssetBundle(AssetBundle bundle, string AssetPath, string basePath = "Assets/ExpandSerializedData/TilesetData/", string fileExtension = ".txt") {
            string serializedData = string.Empty;
            try { serializedData = bundle.LoadAsset<TextAsset>((basePath + AssetPath + fileExtension)).text; } catch (Exception) { }
            if (!string.IsNullOrEmpty(serializedData)) {
                FacewallIndexGridDefinition m_FaceWallIndexGridDefinition = new FacewallIndexGridDefinition();
                JsonUtility.FromJsonOverwrite(serializedData, m_FaceWallIndexGridDefinition);
                return m_FaceWallIndexGridDefinition;
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] Error! Requested Text asset: " + AssetPath + " returned null! Ensure asset exists in asset bundle!", true);
                return null;
            }
        }
	}
}

