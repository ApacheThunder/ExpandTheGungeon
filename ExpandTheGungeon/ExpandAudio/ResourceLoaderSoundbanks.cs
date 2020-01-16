using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ExpandTheGungeon.ExpandAudio {
	
	public class ResourceLoaderSoundbanks {
        
		public void AutoloadFromAssembly(Assembly assembly, string prefix) {
			bool flag = assembly == null;
			if (flag) { throw new ArgumentNullException("assembly", "Assembly cannot be null."); }
			bool flag2 = prefix == null;
			if (flag2) { throw new ArgumentNullException("prefix", "Prefix name cannot be null."); }
			prefix = prefix.Trim();
			bool flag3 = prefix == "";
			if (flag3) { throw new ArgumentException("Prefix name cannot be an empty (or whitespace only) string.", "prefix"); }
			List<string> list = new List<string>(assembly.GetManifestResourceNames());
			for (int i = 0; i < list.Count; i++) {
				string text = list[i];
				string text2 = text;
				text2 = text2.Replace('/', Path.DirectorySeparatorChar);
				text2 = text2.Replace('\\', Path.DirectorySeparatorChar);
				bool flag4 = text2.IndexOf(AudioResourceLoader.ResourcesDirectoryName) != 0;
				if (!flag4) {
					text2 = text2.Substring(text2.IndexOf(AudioResourceLoader.ResourcesDirectoryName) + AudioResourceLoader.ResourcesDirectoryName.Length);
					bool flag5 = text2.LastIndexOf(".bnk") != text2.Length - ".bnk".Length;
					if (!flag5) {
						text2 = text2.Substring(0, text2.Length - ".bnk".Length);
						bool flag6 = text2.IndexOf(Path.DirectorySeparatorChar) == 0;
						if (flag6) { text2 = text2.Substring(1); }
						text2 = prefix + ":" + text2;
                        if (ExpandStats.debugMode) {
                            Console.WriteLine(string.Format("{0}: Soundbank found, attempting to autoload: name='{1}' resource='{2}'", typeof(ResourceLoaderSoundbanks), text2, text));
                        }						
						using (Stream manifestResourceStream = assembly.GetManifestResourceStream(text)) {
							LoadSoundbankFromStream(manifestResourceStream, text2);
						}
					}
				}
			}
		}
        
		public void AutoloadFromPath(string path, string prefix) {
			bool flag = path == null;
			if (flag) {
				throw new ArgumentNullException("path", "Path cannot be null.");
			}
			bool flag2 = prefix == null;
			if (flag2) { throw new ArgumentNullException("prefix", "Prefix name cannot be null."); }
			prefix = prefix.Trim();
			bool flag3 = prefix == "";
			if (flag3) { throw new ArgumentException("Prefix name cannot be an empty (or whitespace only) string.", "prefix"); }
			path = path.Replace('/', Path.DirectorySeparatorChar);
			path = path.Replace('\\', Path.DirectorySeparatorChar);
			bool flag4 = !Directory.Exists(path);
			if (flag4) {
                if (ExpandStats.debugMode) {
                    Console.WriteLine(string.Format("{0}: No autoload directory in path, not autoloading anything. Path='{1}'.", typeof(ResourceLoaderSoundbanks), path));
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
                        Console.WriteLine(string.Format("{0}: Soundbank found, attempting to autoload: name='{1}' file='{2}'", typeof(ResourceLoaderSoundbanks), text2, text));
                    }
					using (FileStream fileStream = File.OpenRead(text)) { LoadSoundbankFromStream(fileStream, text2); }
				}
			}
		}
        
		private void LoadSoundbankFromStream(Stream stream, string name) {
			byte[] array = OtherUtilities.StreamToByteArray(stream);
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

	}
}
