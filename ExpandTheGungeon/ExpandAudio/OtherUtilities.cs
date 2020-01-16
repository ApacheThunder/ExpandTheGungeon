using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ExpandTheGungeon.ExpandAudio {
	
	public static class OtherUtilities {
		
		public static bool IsUnityObjectActuallyNull(this UnityEngine.Object o) { return o == null; }
        
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

		public static void LogAllActiveAssemblies() {
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) { Console.WriteLine("Assembly: " + assembly.ToString()); }
		}
        
		public static void LogResourcesInAssembly(Assembly assembly) {
			foreach (string str in assembly.GetManifestResourceNames()) { Console.WriteLine("LogResourcesInAssembly(): '" + assembly.FullName + "': " + str); }
		}
        
		public static string BufferToString(byte[] buffer) { return Encoding.Default.GetString(buffer); }
        
		// public static int ExtraPlayerStatsEnums = 0;
	}
}
