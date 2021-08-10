using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

	public static class ResourceExtractor {

        private static string nameSpace = "ExpandTheGungeon";

        private static string debugDirectory = Path.Combine(ETGMod.ResourcesDirectory, "debug");
        private static string spritesDirectory = Path.Combine(ETGMod.ResourcesDirectory, "sprites");
        private static string modDirectory = Path.Combine(ETGMod.ModsDirectory, nameSpace);


        public static List<Texture2D> GetTexturesFromDebugFolder(string folder) {
			string path = Path.Combine(debugDirectory, folder);
			bool flag = !Directory.Exists(path);
			List<Texture2D> result;
			if (flag) {
                result = null;
			} else {
				List<Texture2D> list = new List<Texture2D>();
				foreach (string path2 in Directory.GetFiles(path)) {
					Texture2D item = BytesToTexture(File.ReadAllBytes(path2), Path.GetFileName(path2).Replace(".png", ""));
					list.Add(item);
				}
				result = list;
			}
			return result;
		}

        public static List<Texture2D> GetTexturesFromFolder(string folder) {
			string path = Path.Combine(spritesDirectory, folder);
			bool flag = !Directory.Exists(path);
			List<Texture2D> result;
			if (flag) {
                result = null;
			} else {
				List<Texture2D> list = new List<Texture2D>();
				foreach (string path2 in Directory.GetFiles(path)) {
					Texture2D item = BytesToTexture(File.ReadAllBytes(path2), Path.GetFileName(path2).Replace(".png", ""));
					list.Add(item);
				}
				result = list;
			}
			return result;
		}

        public static Texture2D GetTextureFromFile(string filePath, string fileName) {
            Texture2D result = null;
            string TextureFile = (filePath + "/" + fileName);
            if (File.Exists(TextureFile)) {
                result = BytesToTexture(File.ReadAllBytes(TextureFile), Path.GetFileName(TextureFile).Replace(".png", string.Empty));
            }
            return result;
		}

        public static Texture2D BytesToTexture(byte[] bytes, string resourceName) {
            Texture2D texture2D = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            ImageConversion.LoadImage(texture2D, bytes);
            texture2D.filterMode = FilterMode.Point;
            texture2D.name = resourceName;
            texture2D.Apply();
            return texture2D;
        }

        /*public static Texture2D GetTextureFromFile(string fileName) {
			fileName = fileName.Replace(".png", "");
			string text = Path.Combine(spritesDirectory, fileName + ".png");
			bool flag = !File.Exists(text);
			Texture2D result;
			if (flag) {
				ETGModConsole.Log("<color=#FF0000FF>" + text + " not found. </color>", false);
				result = null;
			} else {
				Texture2D texture2D = BytesToTexture(File.ReadAllBytes(text), fileName);
				result = texture2D;
			}
			return result;
		}

		public static List<string> GetCollectionFiles() {
			List<string> list = new List<string>();
			foreach (string text in Directory.GetFiles(spritesDirectory)) {
				bool flag = text.EndsWith(".png");
				if (flag) { list.Add(Path.GetFileName(text).Replace(".png", "")); }
			}
			return list;
		}

		

		public static List<string> GetResourceFolders() {
			List<string> list = new List<string>();
			string path = Path.Combine(ETGMod.ResourcesDirectory, "sprites");
			bool flag = Directory.Exists(path);
			if (flag) {
				foreach (string path2 in Directory.GetDirectories(path)) { list.Add(Path.GetFileName(path2)); }
			}
			return list;
		}*/

        public static AssetBundle GetAssetBundleFromResource(string AssetBundleName) {
            string name = AssetBundleName;
            name = name.Replace("/", ".");
            name = name.Replace("\\", ".");
            byte[] assetBundleBytes = ExtractEmbeddedResource($"{nameSpace}." + name);
            if (assetBundleBytes == null) {
                ETGModConsole.Log("No bytes found in " + name, false);
                return null;
            }
            return AssetBundle.LoadFromMemory(assetBundleBytes);
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

        public static Texture2D GetTextureFromResource(string texturePath, int Width = 1, int Height = 1) {
            string file = texturePath;
            file = file.Replace("/", ".");
            file = file.Replace("\\", ".");
            byte[] bytes = ExtractEmbeddedResource($"{nameSpace}." + file);
            if (bytes == null) {
                ETGModConsole.Log("No bytes found in " + file);
                return null;
            }
            Texture2D texture = new Texture2D(Width, Height, TextureFormat.RGBA32, false);
            ImageConversion.LoadImage(texture, bytes);
            texture.filterMode = FilterMode.Point;

            string name = file.Substring(0, file.LastIndexOf('.'));
            if (name.LastIndexOf('.') >= 0) { name = name.Substring(name.LastIndexOf('.') + 1); }
            texture.name = name;
            // texture.name = textureName;
            return texture;
        }

        public static string[] GetLinesFromEmbeddedResource(string filePath) {
            filePath = filePath.Replace("/", ".");
            filePath = filePath.Replace("\\", ".");
            string text = BytesToString(ExtractEmbeddedResource(string.Format("{0}.", nameSpace) + filePath));
            return text.Split(new char[] { '\n' });
        }

        public static string BuildStringFromEmbeddedResource(string filePath) {
            filePath = filePath.Replace("/", ".");
            filePath = filePath.Replace("\\", ".");
            return BytesToString(ExtractEmbeddedResource(string.Format("{0}.", nameSpace) + filePath));
        }

        public static List<string> BuildStringListFromEmbeddedResource(string filePath) {
            List<string> m_CachedList = new List<string>();

            if(string.IsNullOrEmpty(filePath)) { return m_CachedList; }

            filePath = filePath.Replace("/", ".");
            filePath = filePath.Replace("\\", ".");
            string text = BytesToString(ExtractEmbeddedResource(string.Format("{0}.", nameSpace) + filePath));

            if (string.IsNullOrEmpty(text)) { return m_CachedList; }

            string[] m_CachedStringArray = text.Split(new char[] { '\n' });

            if (m_CachedStringArray == null | m_CachedStringArray.Length <= 0) { return m_CachedList; }

            foreach (string textString in m_CachedStringArray) { m_CachedList.Add(textString); }

            return m_CachedList;
        }

        public static string BytesToString(byte[] bytes) { return Encoding.UTF8.GetString(bytes, 0, bytes.Length); }

        public static void SaveStringToFile(string text, string filePath, string fileName) {
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(filePath, fileName), true)) { streamWriter.WriteLine(text); }
        }

        public static byte[] ExtractEmbeddedResource(string filename) {
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			byte[] result;
			using (Stream manifestResourceStream = callingAssembly.GetManifestResourceStream(filename)) {
				bool flag = manifestResourceStream == null;
				if (flag) {
					result = null;
				} else {
					byte[] array = new byte[manifestResourceStream.Length];
					manifestResourceStream.Read(array, 0, array.Length);
					result = array;
				}
			}
			return result;
		}
                
        public static string[] GetResourceNames() {
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			string[] manifestResourceNames = callingAssembly.GetManifestResourceNames();
			bool flag = manifestResourceNames == null;
			string[] result;
			if (flag) {
				ETGModConsole.Log("No resources found.", false);
				result = null;
			} else {
				result = manifestResourceNames;
			}
			return result;
		}        
	}
}

