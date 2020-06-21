using System.IO;
using Ionic.Zip;
using UnityEngine;
using System;

namespace ExpandTheGungeon.SerializedData {
	
	public static class ExpandAssets {

        public static readonly string FullPathGame = Path.Combine(Application.dataPath, "..");        
        public static readonly string ModFolderPath = Path.Combine(FullPathGame, "Mods/ExpandTheGungeon");

        public static AssetBundle LoadFromModZIPOrModFolder(string AssetBundleName = "expandsharedauto") {
            AssetBundle m_CachedBundle = null;
            if (File.Exists(ModFolderPath + ".zip")) {
                if (ExpandStats.debugMode) { Debug.Log("Zip Found"); }
                ZipFile ModZIP = ZipFile.Read(ModFolderPath + ".zip");
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
            } else if (File.Exists(ModFolderPath + "/" + AssetBundleName)) {
                try { m_CachedBundle = AssetBundle.LoadFromFile(ModFolderPath + "/" + AssetBundleName); } catch (Exception) { }
            }
            if (m_CachedBundle) { return m_CachedBundle; } else { return null; }
        }
	}
}

