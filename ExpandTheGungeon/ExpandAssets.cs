using System.IO;
using Ionic.Zip;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ExpandTheGungeon {
	
	public class ExpandAssets {
        
        public enum AssetSource { BraveResources, SharedAuto1, SharedAuto2, EnemiesBase, FlowBase }

        public static T LoadAsset<T>(string assetPath) where T : UnityEngine.Object {
            return ResourceManager.LoadAssetBundle(ExpandTheGungeon.ModAssetBundleName).LoadAsset<T>(assetPath);
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
        
        public static void InitCustomAssetBundle() {
            
            FieldInfo m_AssetBundlesField = typeof(ResourceManager).GetField("LoadedBundles", BindingFlags.Static | BindingFlags.NonPublic);
            Dictionary<string, AssetBundle> m_AssetBundles = (Dictionary<string, AssetBundle>)m_AssetBundlesField.GetValue(typeof(ResourceManager));
            
            AssetBundle m_ExpandSharedAssets1 = null;
            
            try {
                m_ExpandSharedAssets1 = LoadFromModZIPOrModFolder();
                if (m_ExpandSharedAssets1 != null) {
                    m_AssetBundles.Add("ExpandSharedAuto", m_ExpandSharedAssets1);
                } else {
                    string ErrorMessage = "[ExpandTheGungeon] ERROR: ExpandSharedAuto asset bundle not found!";
                    Debug.Log(ErrorMessage);
                    ExpandTheGungeon.ExceptionText = ErrorMessage;
                    return;
                }
            } catch (Exception ex) {
                string ErrorMessage = "[ExpandTheGungeon] ERROR: Exception while loading ExpandSharedAuto asset bundle! Possible GUID conflict with other custom AssetBundles?";
                Debug.Log(ErrorMessage);
                Debug.LogException(ex);
                ExpandTheGungeon.ExceptionText = ErrorMessage;
                return;
            }
        }

        public static AssetBundle LoadFromModZIPOrModFolder(string AssetBundleName = "expandsharedauto") {
            AssetBundle m_CachedBundle = null;
            if (File.Exists(ExpandTheGungeon.ZipFilePath)) {
                if (ExpandStats.debugMode) { Debug.Log("Zip Found"); }
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
        
        public static void InitAudio (AssetBundle expandSharedAssets1, string assetPath) {
            TextAsset SoundBankBinary = expandSharedAssets1.LoadAsset<TextAsset>(assetPath);
            if (SoundBankBinary) {
                byte[] array = SoundBankBinary.bytes;
			    IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			    try {
			    	Marshal.Copy(array, 0, intPtr, array.Length);
			    	uint num;
			    	AKRESULT akresult = AkSoundEngine.LoadAndDecodeBankFromMemory(intPtr, (uint)array.Length, false, SoundBankBinary.name, false, out num);
                    if (ExpandStats.debugMode) {
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
        
        public static void DumpSpriteCollection(tk2dSpriteCollectionData sprites) {
            string text = "DUMPsprites/" + sprites.spriteCollectionName;
            string path = Path.Combine(ETGMod.ResourcesDirectory, text.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");
            bool flag = File.Exists(path);
            bool staticFlag = false;
            if (!flag) {
                Texture2D texture2D = null;
                Texture2D texture2D2 = null;
                Color[] array = null;
                for (int i = 0; i < sprites.spriteDefinitions.Length; i++) {
                    tk2dSpriteDefinition tk2dSpriteDefinition = sprites.spriteDefinitions[i];
                    Texture2D texture2D3 = tk2dSpriteDefinition.material.mainTexture as Texture2D;
                    bool flag2 = texture2D3 == null || !tk2dSpriteDefinition.Valid || (tk2dSpriteDefinition.materialInst != null && staticFlag);
                    if (!flag2) {
                        string text2 = text + "/" + tk2dSpriteDefinition.name;
                        bool flag3 = texture2D != texture2D3;
                        if (flag3) {
                            texture2D = texture2D3;
                            texture2D2 = ETGMod.GetRW(texture2D3);
                            array = texture2D2.GetPixels();
                            path = Path.Combine(ETGMod.ResourcesDirectory, text.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");
                            Directory.GetParent(path).Create();
                            File.WriteAllBytes(path, ImageConversion.EncodeToPNG(texture2D2));
                        }
                        double num = 1.0;
                        double num2 = 1.0;
                        double num3 = 0.0;
                        double num4 = 0.0;
                        for (int j = 0; j < tk2dSpriteDefinition.uvs.Length; j++) {
                            bool flag4 = tk2dSpriteDefinition.uvs[j].x < num;
                            if (flag4) { num = tk2dSpriteDefinition.uvs[j].x; }
                            bool flag5 = tk2dSpriteDefinition.uvs[j].y < num2;
                            if (flag5) { num2 = tk2dSpriteDefinition.uvs[j].y; }
                            bool flag6 = num3 < tk2dSpriteDefinition.uvs[j].x;
                            if (flag6) { num3 = tk2dSpriteDefinition.uvs[j].x; }
                            bool flag7 = num4 < tk2dSpriteDefinition.uvs[j].y;
                            if (flag7) { num4 = tk2dSpriteDefinition.uvs[j].y; }
                        }
                        int num5 = (int)Math.Floor(num * texture2D3.width);
                        int num6 = (int)Math.Floor(num2 * texture2D3.height);
                        int num7 = (int)Math.Ceiling(num3 * texture2D3.width);
                        int num8 = (int)Math.Ceiling(num4 * texture2D3.height);
                        int num9 = num7 - num5;
                        int num10 = num8 - num6;
                        bool flag8 = tk2dSpriteDefinition.uvs[0].x == num && tk2dSpriteDefinition.uvs[0].y == num2 && tk2dSpriteDefinition.uvs[1].x == num3 && tk2dSpriteDefinition.uvs[1].y == num2 && tk2dSpriteDefinition.uvs[2].x == num && tk2dSpriteDefinition.uvs[2].y == num4 && tk2dSpriteDefinition.uvs[3].x == num3 && tk2dSpriteDefinition.uvs[3].y == num4;
                        Texture2D texture2D4;
                        if (flag8) {
                            texture2D4 = new Texture2D(num9, num10);
                            texture2D4.SetPixels(texture2D2.GetPixels(num5, num6, num9, num10));
                        } else {
                            bool flag9 = tk2dSpriteDefinition.uvs[0].x == tk2dSpriteDefinition.uvs[1].x;
                            if (flag9) {
                                int num11 = num10;
                                num10 = num9;
                                num9 = num11;
                            }
                            texture2D4 = new Texture2D(num9, num10);
                            double num12 = tk2dSpriteDefinition.uvs[1].x - tk2dSpriteDefinition.uvs[0].x;
                            double num13 = tk2dSpriteDefinition.uvs[2].x - tk2dSpriteDefinition.uvs[0].x;
                            double num14 = tk2dSpriteDefinition.uvs[1].y - tk2dSpriteDefinition.uvs[0].y;
                            double num15 = tk2dSpriteDefinition.uvs[2].y - tk2dSpriteDefinition.uvs[0].y;
                            double num16 = texture2D3.width * (tk2dSpriteDefinition.uvs[3].x - tk2dSpriteDefinition.uvs[0].x);
                            double num17 = texture2D3.height * (tk2dSpriteDefinition.uvs[3].y - tk2dSpriteDefinition.uvs[0].y);
                            double num18 = 0.001;
                            double num19 = (num12 < num18) ? 0.0 : num16;
                            double num20 = (num13 < num18) ? 0.0 : num16;
                            double num21 = (num14 < num18) ? 0.0 : num17;
                            double num22 = (num15 < num18) ? 0.0 : num17;
                            for (int k = 0; k < num10; k++) {
                                double num23 = k / (double)num10;
                                for (int l = 0; l < num9; l++) {
                                    double num24 = l / (double)num9;
                                    double num25 = num24 * num19 + num23 * num20;
                                    double num26 = num24 * num21 + num23 * num22;
                                    double num27 = Math.Round(tk2dSpriteDefinition.uvs[0].y * (float)texture2D3.height + num26) * texture2D3.width + Math.Round(tk2dSpriteDefinition.uvs[0].x * (float)texture2D3.width + num25);
                                    texture2D4.SetPixel(l, k, array[(int)num27]);
                                }
                            }
                        }
                        path = Path.Combine(ETGMod.ResourcesDirectory, text2.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");
                        bool flag10 = !File.Exists(path);
                        if (flag10) {
                            Directory.GetParent(path).Create();
                            File.WriteAllBytes(path, ImageConversion.EncodeToPNG(texture2D4));
                        }
                    }
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
	}
}

