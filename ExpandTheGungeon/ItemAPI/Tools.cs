using System;
using System.IO;
using UnityEngine;

namespace ExpandTheGungeon {

    public static class Tools {

        public static bool verbose = false;
        
        public static string modID = "EX";

        private static string defaultLog = Path.Combine(ETGMod.ResourcesDirectory, modID + "_customItemsLog.txt");

        public static void Init() {
			bool flag = File.Exists(defaultLog);
			if (flag) { File.Delete(defaultLog); }
		}

		public static void Print<T>(T obj, string color = "FFFFFF", bool force = false) {
			bool flag = verbose || force;
			if (flag) {
				ETGModConsole.Log(string.Concat(new string[] {
					"<color=#",
					color,
					">",
					modID,
					": ",
					obj.ToString(),
					"</color>"
				}), false);
			}
            Log(obj.ToString());
		}

		public static void PrintRaw<T>(T obj, bool force = false) {
			bool flag = verbose || force;
			if (flag) {
				ETGModConsole.Log(obj.ToString(), false);
			}
            Log(obj.ToString());
		}

		public static void PrintError<T>(T obj, string color = "FF0000") {
			ETGModConsole.Log(string.Concat(new string[] {
				"<color=#",
				color,
				">",
				modID,
				": ",
				obj.ToString(),
				"</color>"
			}), false);
            Log(obj.ToString());
		}

		public static void PrintException(Exception e, string color = "FF0000") {
			ETGModConsole.Log(string.Concat(new string[] {
				"<color=#",
				color,
				">",
				modID,
				": ",
				e.Message,
				"</color>"
			}), false);
			ETGModConsole.Log(e.StackTrace, false);
            Log(e.Message);
            Log("\t" + e.StackTrace);
		}

		public static void Log<T>(T obj) {
			using (StreamWriter streamWriter = new StreamWriter(Path.Combine(ETGMod.ResourcesDirectory, defaultLog), true)) { streamWriter.WriteLine(obj.ToString()); }
		}

		public static void Log<T>(T obj, string fileName) {
			bool flag = !verbose;
			if (!flag) {
				using (StreamWriter streamWriter = new StreamWriter(Path.Combine(ETGMod.ResourcesDirectory, fileName), true)) {
					streamWriter.WriteLine(obj.ToString());
				}
			}
		}

        public static void LogStringToFile(string text, string fileName) {			
			using (StreamWriter streamWriter = new StreamWriter(Path.Combine(ETGMod.ResourcesDirectory, fileName), true)) { streamWriter.WriteLine(text); }
		}

		public static void Dissect(this GameObject obj) {
			Print(obj.name + " Components:", "FFFFFF", false);
			foreach (Component component in obj.GetComponents<Component>()) {
				Print("    " + component.GetType(), "FFFFFF", false);
			}
		}

		public static void ShowHitBox(this SpeculativeRigidbody body) {
			PixelCollider hitboxPixelCollider = body.HitboxPixelCollider;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gameObject.name = "HitboxDisplay";
			gameObject.transform.SetParent(body.transform);
			Print(body.name ?? "", "FFFFFF", false);
			Print(string.Format("    Offset: {0}, Dimesions: {1}", hitboxPixelCollider.Offset, hitboxPixelCollider.Dimensions), "FFFFFF", false);
			gameObject.transform.localScale = new Vector3(hitboxPixelCollider.Dimensions.x / 16f, hitboxPixelCollider.Dimensions.y / 16f, 1f);
			Vector3 localPosition = new Vector3(hitboxPixelCollider.Offset.x + hitboxPixelCollider.Dimensions.x * 0.5f, hitboxPixelCollider.Offset.y + hitboxPixelCollider.Dimensions.y * 0.5f, -16f) / 16f;
			gameObject.transform.localPosition = localPosition;
		}

		public static void HideHitBox(this SpeculativeRigidbody body) {
			Transform transform = body.transform.Find("HitboxDisplay");
			bool flag = transform;
			if (flag) { UnityEngine.Object.Destroy(transform); }
		}

		public static void ExportTexture(Texture texture) {
			File.WriteAllBytes(Path.Combine(ETGMod.ResourcesDirectory, texture.name + ".png"), ImageConversion.EncodeToPNG((Texture2D)texture));
		}

        // public static T GetEnumValue<T>(string val) where T : Enum { return (T)Enum.Parse(typeof(T), val.ToUpper()); }

        // This came from KyleTheScientest's SpriteWork mod.
        public static void DumpSpecificSpriteCollection(tk2dSpriteCollectionData sprites, bool debugMode = false, bool overrideFlag = false) {
            string collectionName = sprites.spriteCollectionName;
            // if (string.IsNullOrEmpty(collectionName)) { collectionName = Guid.NewGuid().ToString(); }
            string text = "DUMPsprites/" + collectionName;            
            string path = Path.Combine(ETGMod.ResourcesDirectory, text.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");
            if (debugMode) {
                ETGModConsole.Log("[ExpandTheGungeon] Debug: Current Sprite Collection Length: " + sprites.spriteDefinitions.Length.ToString());
            }
            if (!File.Exists(path)) {
                Texture2D texture2D = null;
                Texture2D texture2D2 = null;
                Color[] array = null;                
                for (int i = 0; i < sprites.spriteDefinitions.Length; i++) {
                    tk2dSpriteDefinition tk2dSpriteDefinition = sprites.spriteDefinitions[i];
                    Texture2D texture2D3 = tk2dSpriteDefinition.material.mainTexture as Texture2D;                    
                    if (texture2D3 != null || !tk2dSpriteDefinition.Valid || (tk2dSpriteDefinition.materialInst != null && overrideFlag)) {
                        try {
                            string spriteName = tk2dSpriteDefinition.name;
                            // if (string.IsNullOrEmpty(tk2dSpriteDefinition.name)) { spriteName = Guid.NewGuid().ToString(); }
                            string text2 = text + "/" + tk2dSpriteDefinition.name;
                            if (texture2D != texture2D3) {
                                if (debugMode) {
                                    ETGModConsole.Log("[ExpandTheGungeon] Debug: Dumping Sprite Atlas to file.");
                                }
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
                                if (debugMode) {
                                    ETGModConsole.Log("[ExpandTheGungeon] Debug: flag8 at SpriteID " + i.ToString());
                                }
                                texture2D4 = new Texture2D(num9, num10);
                                texture2D4.SetPixels(texture2D2.GetPixels(num5, num6, num9, num10));
                            } else {
                                bool flag9 = tk2dSpriteDefinition.uvs[0].x == tk2dSpriteDefinition.uvs[1].x;
                                if (flag9) {
                                    int num11 = num10;
                                    num10 = num9;
                                    num9 = num11;
                                    if (debugMode) {
                                        ETGModConsole.Log("[ExpandTheGungeon] Debug: flag9 at SpriteID " + i.ToString());
                                    }
                                }
                                if (debugMode) {
                                    ETGModConsole.Log("[ExpandTheGungeon] Debug: Prepering to dump sprite texture at SpriteID " + i.ToString());
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
                                if (debugMode) {
                                    ETGModConsole.Log("[ExpandTheGungeon] Debug: Prepering to set pixels for texture2D4...");
                                }
                                for (int k = 0; k < num10; k++) {
                                    double num23 = k / (double)num10;
                                    for (int l = 0; l < num9; l++) {
                                        double num24 = l / (double)num9;
                                        double num25 = num24 * num19 + num23 * num20;
                                        double num26 = num24 * num21 + num23 * num22;
                                        double num27 = Math.Round(tk2dSpriteDefinition.uvs[0].y * texture2D3.height + num26) * texture2D3.width + Math.Round(tk2dSpriteDefinition.uvs[0].x * texture2D3.width + num25);
                                        texture2D4.SetPixel(l, k, array[(int)num27]);
                                    }
                                }
                            }
                            if (debugMode) {
                                ETGModConsole.Log("[ExpandTheGungeon] Debug: Writing sprite texture to file...");
                            }
                            path = Path.Combine(ETGMod.ResourcesDirectory, text2.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");
                            bool flag10 = !File.Exists(path);
                            if (flag10) {
                                Directory.GetParent(path).Create();
                                File.WriteAllBytes(path, ImageConversion.EncodeToPNG(texture2D4));
                            }
                        } catch (Exception ex) {
                            if (debugMode) {
                                ETGModConsole.Log("Exception occured while processing sprite id '" + i.ToString() + "'!");
                                Debug.LogException(ex);
                            }
                        }
                    }
                }
            }
        }
    }
}

