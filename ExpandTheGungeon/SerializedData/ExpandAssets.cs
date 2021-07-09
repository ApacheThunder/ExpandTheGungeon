using System.IO;
using Ionic.Zip;
using UnityEngine;
using System;

namespace ExpandTheGungeon.SerializedData {
	
	public static class ExpandAssets {
        
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
	}
}

