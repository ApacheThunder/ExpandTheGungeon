using System.IO;
using Ionic.Zip;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ExpandTheGungeon {
	
	public static class ExpandAssets {
        
        public static T LoadAsset<T>(string assetPath) where T : UnityEngine.Object {
            return ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<T>(assetPath);
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


        public static void InitAudio(string zippath, string filepath) {
            int FilesLoaded = 0;
            if (File.Exists(zippath)) {
                Debug.Log("Zip Found");
                using (ZipFile ModZIP = ZipFile.Read(zippath)) {
                    if (ModZIP != null && ModZIP.Entries.Count > 0) {
                        foreach (ZipEntry entry in ModZIP.Entries) {
                            if (entry.FileName.EndsWith(".bnk")) {
                                using (MemoryStream ms = new MemoryStream()) {
                                    entry.Extract(ms);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    LoadSoundbankFromStream(ms, entry.FileName.ToLower().Replace(".bnk", string.Empty));
                                    FilesLoaded++;
                                }
                            }
                        }
                        if (FilesLoaded > 0) { return; }
                    }
                }
            }
            // Zip file wasn't found. Try to load from Mod folder instead.
            AutoloadFromPath(filepath, "ExpandTheGungeon");
        }

        public static void AutoloadFromModPath(string path, string prefix) {
			if (string.IsNullOrEmpty(path)) { throw new ArgumentNullException("path", "Path cannot be null."); }			
			if (string.IsNullOrEmpty(prefix)) { throw new ArgumentNullException("prefix", "Prefix name cannot be null."); }
			prefix = prefix.Trim();
			if (string.IsNullOrEmpty(prefix)) { throw new ArgumentException("Prefix name cannot be an empty (or whitespace only) string.", "prefix"); }
			path = path.Replace('/', Path.DirectorySeparatorChar);
			path = path.Replace('\\', Path.DirectorySeparatorChar);
			if (!Directory.Exists(path)) {
                if (ExpandStats.debugMode) {
                    Console.WriteLine(string.Format("{0}: No autoload directory in path, not autoloading anything. Path='{1}'.", typeof(ExpandAssets), path));
                }
			} else {
				List<string> list = new List<string>(Directory.GetFiles(path, "*.bnk", SearchOption.AllDirectories));
				for (int i = 0; i < list.Count; i++) {
					string text = list[i];
					string text2 = text;
					text2 = text2.Replace('/', Path.DirectorySeparatorChar);
					text2 = text2.Replace('\\', Path.DirectorySeparatorChar);
					text2 = text2.Substring(text2.IndexOf(path) + path.Length);
					text2 = text2.Substring(0, text2.Length - ".bnk".Length);
					bool flag5 = text2.IndexOf(Path.DirectorySeparatorChar) == 0;
					if (flag5) { text2 = text2.Substring(1); }
					text2 = prefix + ":" + text2;
                    if (ExpandStats.debugMode) {
                        Console.WriteLine(string.Format("{0}: Soundbank found, attempting to autoload: name='{1}' file='{2}'", typeof(ExpandAssets), text2, text));
                    }
					using (FileStream fileStream = File.OpenRead(text)) { LoadSoundbankFromStream(fileStream, text2); }
				}
			}
		}
        
		public static void AutoloadFromPath(string path, string prefix) {
			if (string.IsNullOrEmpty(path)) { throw new ArgumentNullException("path", "Path cannot be null."); }			
			if (string.IsNullOrEmpty(prefix)) { throw new ArgumentNullException("prefix", "Prefix name cannot be null."); }
			prefix = prefix.Trim();
			if (string.IsNullOrEmpty(prefix)) { throw new ArgumentException("Prefix name cannot be an empty (or whitespace only) string.", "prefix"); }
			path = path.Replace('/', Path.DirectorySeparatorChar);
			path = path.Replace('\\', Path.DirectorySeparatorChar);
			if (!Directory.Exists(path)) {
                if (ExpandStats.debugMode) {
                    Console.WriteLine(string.Format("{0}: No autoload directory in path, not autoloading anything. Path='{1}'.", typeof(ExpandAssets), path));
                }
			} else {
				List<string> list = new List<string>(Directory.GetFiles(path, "*.bnk", SearchOption.AllDirectories));
				for (int i = 0; i < list.Count; i++) {
					string text = list[i];
					string text2 = text;
					text2 = text2.Replace('/', Path.DirectorySeparatorChar);
					text2 = text2.Replace('\\', Path.DirectorySeparatorChar);
					text2 = text2.Substring(text2.IndexOf(path) + path.Length);
					text2 = text2.Substring(0, text2.Length - ".bnk".Length);
					bool flag5 = text2.IndexOf(Path.DirectorySeparatorChar) == 0;
					if (flag5) { text2 = text2.Substring(1); }
					text2 = prefix + ":" + text2;
                    if (ExpandStats.debugMode) {
                        Console.WriteLine(string.Format("{0}: Soundbank found, attempting to autoload: name='{1}' file='{2}'", typeof(ExpandAssets), text2, text));
                    }
					using (FileStream fileStream = File.OpenRead(text)) { LoadSoundbankFromStream(fileStream, text2); }
				}
			}
		}
        
		private static void LoadSoundbankFromStream(Stream stream, string name) {
			byte[] array = StreamToByteArray(stream);
			IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
			try {
				Marshal.Copy(array, 0, intPtr, array.Length);
				uint num;
				AKRESULT akresult = AkSoundEngine.LoadAndDecodeBankFromMemory(intPtr, (uint)array.Length, false, name, false, out num);
                if (ExpandStats.debugMode) {
                    Console.WriteLine(string.Format("Result of soundbank load: {0}.", akresult));
                }
			} finally {
                Marshal.FreeHGlobal(intPtr);
            }
		}

        public static byte[] StreamToByteArray(Stream input) {
			byte[] array = new byte[16384];
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream()) {
				int count;
				while ((count = input.Read(array, 0, array.Length)) > 0) { memoryStream.Write(array, 0, count); }
				result = memoryStream.ToArray();
			}
			return result;
		}

	}
}

