using ExpandTheGungeon.ExpandPrefab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.SpriteAPI {

	public static class SpriteSerializer {
        
        private static tk2dSpriteCollectionData newCollection;
        private static RuntimeAtlasPacker AtlasPacker;
        
        // Use to serialize an existing set of sprites in your mod.
        // Create a list of sprite names (case sensitive by the way) you want put into an atlas and this will output the atlas texture and JSON txt dump of the new sprite collection.
        // Note you may get exceptions if you don't give it a large enough atlas size to work with. I have been using the same defaultsize, Xres, and Yres.
        // Someone who better understands these fields could probably create better defaults but that's what worked for me.
        public static void SerializeSpriteCollection(string CollectionName, List<string> spriteNames, int Xres, int Yres, string pathOverride = null) {
            if (!ExpandSettings.spritesBundlePresent) {
                ETGModConsole.Log("[ExpandTheGungeon] Unserialized sprite textures stored in optional asset bundle but it is missing! Ensure you have it setup properly!");
                return;
            }
            GameObject m_TempObject = new GameObject(CollectionName);
            newCollection = GenerateNewSpriteCollection(m_TempObject, CollectionName);
            
            AtlasPacker = new RuntimeAtlasPacker(Xres, Yres);
            AddSpriteToObject(m_TempObject, ExpandAssets.LoadSpriteAsset<Texture2D>(spriteNames[0]));
            if (spriteNames.Count > 0) {
                for (int i = 1; i < spriteNames.Count; i++) {
                    AddSpriteToCollection(ExpandAssets.LoadSpriteAsset<Texture2D>(spriteNames[i]), newCollection);
                }
            }
            DumpSpriteCollection(newCollection, pathOverride);

            foreach (tk2dSpriteDefinition spriteDefinition in newCollection.spriteDefinitions) { spriteDefinition.material = null; }
            newCollection.material = null;
            newCollection.materials = null;
            newCollection.textures = null;

            if (!string.IsNullOrEmpty(pathOverride)) {
                SaveStringToFile(JsonUtility.ToJson(newCollection), pathOverride, CollectionName + ".txt");
            } else {
                SaveStringToFile(JsonUtility.ToJson(newCollection), ETGMod.ResourcesDirectory, CollectionName + ".txt");
            }
            UnityEngine.Object.Destroy(m_TempObject);
            newCollection = null;
            AtlasPacker = null;
        }

        public static void SerializeSpriteCollection(tk2dSpriteCollectionData collectionData, bool SaveTextures = false, string pathOverride = null) {
            if (SaveTextures) { DumpSpriteCollection(collectionData, pathOverride); }
            collectionData.material = null;
            collectionData.materials = null;
            collectionData.textures = null;
            foreach (tk2dSpriteDefinition spriteDefinition in collectionData.spriteDefinitions) { spriteDefinition.material = null; }
            if (!string.IsNullOrEmpty(pathOverride)) {
                SaveStringToFile(JsonUtility.ToJson(collectionData), pathOverride, collectionData.name + ".txt");
            } else {
                SaveStringToFile(JsonUtility.ToJson(collectionData), ETGMod.ResourcesDirectory, collectionData.name + ".txt");
            }
        }

        // Assigns a GameObject (loaded from an asset bundle in this version) with the attached tk2dSpriteCollectionData component to your chosen field.
        // Currently only setup a version that loads these assets from an asset bundle
        public static GameObject DeserializeSpriteCollectionFromAssetBundle(AssetBundle assetBundle, string GameObjectPath, string spriteAtlasPath, string serializedCollectionJSONName, bool addSpriteToObject = false, string spriteDefintionName = null, Material overrideMaterial = null) {
            GameObject targetObject = assetBundle.LoadAsset<GameObject>(GameObjectPath);
            Texture2D spriteAtlas = assetBundle.LoadAsset<Texture2D>(spriteAtlasPath);
            tk2dSpriteCollectionData m_CollectionData = targetObject.AddComponent<tk2dSpriteCollectionData>();
            JsonUtility.FromJsonOverwrite(DeserializeJSONDataFromAssetBundle(assetBundle, serializedCollectionJSONName), m_CollectionData);
            if (overrideMaterial) {
                m_CollectionData.materials = new Material[] { new Material(overrideMaterial) };                
            } else {
                m_CollectionData.materials = new Material[] { new Material(ShaderCache.Acquire(PlayerController.DefaultShaderName)) };
            }
            m_CollectionData.materials[0].mainTexture = spriteAtlas;
            foreach (tk2dSpriteDefinition definition in m_CollectionData.spriteDefinitions) { definition.material = m_CollectionData.materials[0]; }
            m_CollectionData.InitDictionary();
            m_CollectionData.inst.InitDictionary(); // Important you do this on inst (on a new collection) so that inst's init runs and the collection is in the correct state. Else things like items not having ammonocon sprites or GetBounds causing exceptiosn on instances spawned by SpawnVFX in some circumstances!
            if (addSpriteToObject) {
                tk2dSprite m_tk2dSprite = targetObject.AddComponent<tk2dSprite>();
                if (!string.IsNullOrEmpty(spriteDefintionName)) {
                    m_tk2dSprite.SetSprite(m_CollectionData, spriteDefintionName);
                } else {
                    m_tk2dSprite.SetSprite(m_CollectionData, 0);
                }
                m_tk2dSprite.SortingOrder = 0;
                targetObject.GetComponent<BraveBehaviour>().sprite = m_tk2dSprite;
            }
            return targetObject;
        }
        
        // Use this to add a sprite to your object instead of the one from ItemBuilder/SpriteBuilder!
        public static tk2dSprite AddSpriteToObject(GameObject obj, GameObject existingSpriteCollectionObject, string mainSpriteDefinitionName, tk2dBaseSprite.PerpendicularState spriteAlignment = tk2dBaseSprite.PerpendicularState.UNDEFINED) {
            tk2dSprite m_tk2dSprite = obj.AddComponent<tk2dSprite>();
            m_tk2dSprite.SetSprite(existingSpriteCollectionObject.GetComponent<tk2dSpriteCollectionData>(), mainSpriteDefinitionName);
            m_tk2dSprite.SortingOrder = 0;
            if (spriteAlignment != tk2dBaseSprite.PerpendicularState.UNDEFINED) { m_tk2dSprite.CachedPerpState = spriteAlignment; }
            obj.GetComponent<BraveBehaviour>().sprite = m_tk2dSprite;
            return m_tk2dSprite;
        }

        public static tk2dSprite AddSpriteToObject(GameObject obj, GameObject existingSpriteCollectionObject, int spriteID, tk2dBaseSprite.PerpendicularState spriteAlignment = tk2dBaseSprite.PerpendicularState.UNDEFINED) {
            tk2dSprite m_tk2dSprite = obj.AddComponent<tk2dSprite>();
            m_tk2dSprite.SetSprite(existingSpriteCollectionObject.GetComponent<tk2dSpriteCollectionData>(), spriteID);
            m_tk2dSprite.SortingOrder = 0;
            obj.GetComponent<BraveBehaviour>().sprite = m_tk2dSprite;
            return m_tk2dSprite;
        }

        public static tk2dSprite AddSpriteToObject(GameObject obj, tk2dSpriteCollectionData existingSpriteCollection, string mainSpriteDefinitionName, tk2dBaseSprite.PerpendicularState spriteAlignment = tk2dBaseSprite.PerpendicularState.UNDEFINED) {
            tk2dSprite m_tk2dSprite = obj.AddComponent<tk2dSprite>();
            m_tk2dSprite.SetSprite(existingSpriteCollection, mainSpriteDefinitionName);
            m_tk2dSprite.SortingOrder = 0;
            if (spriteAlignment != tk2dBaseSprite.PerpendicularState.UNDEFINED) { m_tk2dSprite.CachedPerpState = spriteAlignment; }
            obj.GetComponent<BraveBehaviour>().sprite = m_tk2dSprite;
            return m_tk2dSprite;
        }

        public static tk2dSprite AddSpriteToObject(GameObject obj, tk2dSpriteCollectionData existingSpriteCollection, int spriteID, tk2dBaseSprite.PerpendicularState spriteAlignment = tk2dBaseSprite.PerpendicularState.UNDEFINED) {
            tk2dSprite m_tk2dSprite = obj.AddComponent<tk2dSprite>();
            m_tk2dSprite.SetSprite(existingSpriteCollection, spriteID);
            m_tk2dSprite.SortingOrder = 0;
            if (spriteAlignment != tk2dBaseSprite.PerpendicularState.UNDEFINED) { m_tk2dSprite.CachedPerpState = spriteAlignment; }
            obj.GetComponent<BraveBehaviour>().sprite = m_tk2dSprite;
            return m_tk2dSprite;
        }

        public static void DumpSpriteCollection(tk2dSpriteCollectionData sprites, string pathOverride = null) {
            string text = "DUMPsprites/" + sprites.spriteCollectionName;
            string path = Path.Combine(ETGMod.ResourcesDirectory, text.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png");
            if (!string.IsNullOrEmpty(pathOverride)) { Path.Combine(pathOverride, text.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar) + ".png"); }
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

        public static string DeserializeJSONDataFromAssetBundle(AssetBundle bundle, string AssetPath) {
            string m_ResultAsset = string.Empty;
            try {
                m_ResultAsset = bundle.LoadAsset<TextAsset>(AssetPath).text;
            } catch (Exception ex) {
                ETGModConsole.Log("[ExpandTheGungeon] Error! Requested Text asset: " + AssetPath + " returned null! Ensure asset exists in asset bundle!", true);
                Debug.LogException(ex);
                return string.Empty;
            }
            if (!string.IsNullOrEmpty(m_ResultAsset)) {
                return m_ResultAsset;
            } else {
                ETGModConsole.Log("[ExpandTheGungeon] Error! Requested Text asset: " + AssetPath + " returned null! Ensure asset exists in asset bundle!", true);
                return string.Empty;
            }
        }

        public static void SaveStringToFile(string text, string filePath, string fileName) {
            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(filePath, fileName), true)) { streamWriter.WriteLine(text); }
        }



        // These methods meant for internal use by this class. If you need to use similar methods like these, use the ones from SpriteBuilder/ItemBuilder!
        private static void AddSpriteToObject(GameObject targetObject, Texture2D sourceTexture) { SpriteFromTexture(sourceTexture, targetObject); }

        private static GameObject SpriteFromTexture(Texture2D existingTexture, GameObject obj) {
			tk2dSprite tk2dSprite = obj.AddComponent<tk2dSprite>();
            int num = AddSpriteToCollection(existingTexture, newCollection);
			tk2dSprite.SetSprite(newCollection, num);
			tk2dSprite.SortingOrder = 0;
			obj.GetComponent<BraveBehaviour>().sprite = tk2dSprite;
			return obj;
		}
        
        private static int AddSpriteToCollection(Texture2D existingTexture, tk2dSpriteCollectionData collection) {
			tk2dSpriteDefinition tk2dSpriteDefinition = ConstructDefinition(existingTexture);
			tk2dSpriteDefinition.name = existingTexture.name;
			return AddSpriteToCollection(tk2dSpriteDefinition, collection);
		}

        private static void AddSpritesToCollection(AssetBundle assetSource, List<string> AssetNames, tk2dSpriteCollectionData collection) {
            List<Texture> m_Textures = new List<Texture>();
            foreach (string AssetName in AssetNames) { m_Textures.Add(assetSource.LoadAsset<Texture2D>(AssetName)); }

            if (m_Textures.Count > 0) {
                foreach (Texture2D texture in m_Textures) {
                    tk2dSpriteDefinition tk2dSpriteDefinition = ConstructDefinition(texture);
                    tk2dSpriteDefinition.name = texture.name;
                    AddSpriteToCollection(tk2dSpriteDefinition, collection);
                }
            }
        }

        private static int AddSpriteToCollection(tk2dSpriteDefinition spriteDefinition, tk2dSpriteCollectionData collection, bool InitDictionary = false) {
			tk2dSpriteDefinition[] spriteDefinitions = collection.spriteDefinitions;
			tk2dSpriteDefinition[] array = spriteDefinitions.Concat(new tk2dSpriteDefinition[] { spriteDefinition }).ToArray();
			collection.spriteDefinitions = array;
            // Default is off. We never need to call specific definitions by name from newCollection since it's only a temperary object about to be serialized.
            if (InitDictionary) {
                FieldInfo field = typeof(tk2dSpriteCollectionData).GetField("spriteNameLookupDict", BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(collection, null);
                collection.InitDictionary();
            }
			return array.Length - 1;
		}

        private static tk2dSpriteCollectionData GenerateNewSpriteCollection(GameObject targetObject, string CollectionName = null, tk2dSpriteDefinition[] spriteDefinitions = null , bool initDictionary = false) {
            tk2dSpriteCollectionData newCollection = targetObject.AddComponent<tk2dSpriteCollectionData>();
            newCollection.version = 3;
            newCollection.materialIdsValid = true;
            if (spriteDefinitions != null) { newCollection.spriteDefinitions = spriteDefinitions; }
            newCollection.spriteDefinitions = new tk2dSpriteDefinition[0];
            newCollection.premultipliedAlpha = false;
            newCollection.shouldGenerateTilemapReflectionData = false;
            newCollection.materials = new Material[0];
            newCollection.textures = new Texture[0];
            newCollection.pngTextures = new TextAsset[0];
            newCollection.textureFilterMode = FilterMode.Point;
            newCollection.textureMipMaps = false;
            newCollection.allowMultipleAtlases = false;
            newCollection.spriteCollectionGUID = Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(CollectionName)) {
                newCollection.spriteCollectionName = CollectionName;
            } else {
                newCollection.spriteCollectionName = targetObject.name + "_Collection";
            }
            newCollection.loadable = false;
            newCollection.buildKey = UnityEngine.Random.Range(800000000, 999999999);
            newCollection.dataGuid = Guid.NewGuid().ToString();
            newCollection.managedSpriteCollection = false;
            newCollection.hasPlatformData = false;
            newCollection.spriteCollectionPlatforms = new string[0];
            newCollection.spriteCollectionPlatformGUIDs = new string[0];
            if (initDictionary) { newCollection.InitDictionary(); }
            return newCollection;
        }


        private static tk2dSpriteDefinition ConstructDefinition(Texture2D texture) {
			RuntimeAtlasSegment runtimeAtlasSegment = AtlasPacker.Pack(texture, false);
			Material material = new Material(ShaderCache.Acquire(PlayerController.DefaultShaderName));
			material.mainTexture = runtimeAtlasSegment.texture;
			int width = texture.width;
			int height = texture.height;
			float num = 0f;
			float num2 = 0f;
			float num3 = width / 16f;
			float num4 = height / 16f;
			tk2dSpriteDefinition tk2dSpriteDefinition = new tk2dSpriteDefinition {
				normals = new Vector3[] {
					new Vector3(0f, 0f, -1f),
					new Vector3(0f, 0f, -1f),
					new Vector3(0f, 0f, -1f),
					new Vector3(0f, 0f, -1f)
				},
				tangents = new Vector4[] {
					new Vector4(1f, 0f, 0f, 1f),
					new Vector4(1f, 0f, 0f, 1f),
					new Vector4(1f, 0f, 0f, 1f),
					new Vector4(1f, 0f, 0f, 1f)
				},
				texelSize = new Vector2(0.0625f, 0.0625f),
				extractRegion = false,
				regionX = 0,
				regionY = 0,
				regionW = 0,
				regionH = 0,
				flipped = 0,
				complexGeometry = false,
				physicsEngine = tk2dSpriteDefinition.PhysicsEngine.Physics3D,
				colliderType = tk2dSpriteDefinition.ColliderType.None,
				collisionLayer = CollisionLayer.HighObstacle,
				position0 = new Vector3(num, num2, 0f),
				position1 = new Vector3(num + num3, num2, 0f),
				position2 = new Vector3(num, num2 + num4, 0f),
				position3 = new Vector3(num + num3, num2 + num4, 0f),
				material = material,
				materialInst = material,
				materialId = 0,
				uvs = runtimeAtlasSegment.uvs,
				boundsDataCenter = new Vector3(num3 / 2f, num4 / 2f, 0f),
				boundsDataExtents = new Vector3(num3, num4, 0f),
				untrimmedBoundsDataCenter = new Vector3(num3 / 2f, num4 / 2f, 0f),
				untrimmedBoundsDataExtents = new Vector3(num3, num4, 0f)
			};
			tk2dSpriteDefinition.name = texture.name;
			return tk2dSpriteDefinition;
		}

        /*private static T CopyFrom<T>(this Component comp, T other) where T : Component {
			Type type = comp.GetType();
			bool flag = type != other.GetType();
			T result;
			if (flag) {
                result = default(T);
			} else {
				PropertyInfo[] properties = type.GetProperties();
				foreach (PropertyInfo propertyInfo in properties) {
					bool canWrite = propertyInfo.CanWrite;
					if (canWrite) {
						try { propertyInfo.SetValue(comp, propertyInfo.GetValue(other, null), null); } catch { }
					}
				}
				FieldInfo[] fields = type.GetFields();
				foreach (FieldInfo fieldInfo in fields) { fieldInfo.SetValue(comp, fieldInfo.GetValue(other)); }
				result = (comp as T);
			}
			return result;
		}
        private static T AddComponent<T>(this GameObject go, T toAdd) where T : Component { return go.AddComponent<T>().CopyFrom(toAdd); }*/
	}
}

