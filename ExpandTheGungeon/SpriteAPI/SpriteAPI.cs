using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.SpriteAPI {

	public static class SpriteSerializer {

        private static tk2dSpriteCollectionData newCollection;
        private static RuntimeAtlasPacker AtlasPacker;
        
        public static void SerializeSpriteCollection(string CollectionName, List<string> spriteNames, int defaultSize, int Xres, int Yres) {
            int PreviousDefaultSize = RuntimeAtlasPage.DefaultSize;

            GameObject m_TempObject = new GameObject(CollectionName);
            newCollection = GenerateNewSpriteCollection(m_TempObject);
            AtlasPacker = new RuntimeAtlasPacker(Xres, Yres);
            RuntimeAtlasPage.DefaultSize = defaultSize;

            AddSpriteToObject(m_TempObject, ExpandAssets.LoadAsset<Texture2D>(spriteNames[0]));
            if (spriteNames.Count > 0) {
                for (int i = 1; i < spriteNames.Count; i++) {
                    AddSpriteToCollection(ExpandAssets.LoadAsset<Texture2D>(spriteNames[i]), newCollection);
                }
            }

            ExpandAssets.DumpSpriteCollection(newCollection);
            ExpandAssets.SaveStringToFile(JsonUtility.ToJson(newCollection), ETGMod.ResourcesDirectory, CollectionName + ".txt");
            RuntimeAtlasPage.DefaultSize = PreviousDefaultSize;
            newCollection = null;
            AtlasPacker = null;
        }

        /*public static void DeserializeSpriteCollectionFromAssetBundle(GameObject targetObject, string serializedCollectionJSONName, string atlasTextureName, string MainSprite = null, Material overrideMaterial = null) {
            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle(ExpandTheGungeon.ModAssetBundleName);
            tk2dSprite m_Sprite = targetObject.AddComponent<tk2dSprite>();
            m_Sprite.Collection = targetObject.AddComponent<tk2dSpriteCollectionData>();
            JsonUtility.FromJsonOverwrite(ExpandUtility.DeserializeJSONDataFromAssetBundle(expandSharedAssets1, serializedCollectionJSONName, string.Empty, string.Empty), m_Sprite.Collection);
            if (overrideMaterial) {
                m_Sprite.Collection.material = new Material(overrideMaterial);
            } else {
                m_Sprite.Collection.material = new Material(ShaderCache.Acquire(PlayerController.DefaultShaderName));
            }
            m_Sprite.Collection.material.mainTexture = expandSharedAssets1.LoadAsset<Texture2D>(atlasTextureName);
            m_Sprite.Collection.materials = new Material[] { m_Sprite.Collection.material };
            foreach (tk2dSpriteDefinition definition in m_Sprite.Collection.spriteDefinitions) {
                definition.material = m_Sprite.Collection.materials[0];
                definition.materialInst = m_Sprite.Collection.materials[0];
            }
            if (!string.IsNullOrEmpty(MainSprite)) {
                m_Sprite.SetSprite(MainSprite);
            } else {
                m_Sprite.SetSprite(m_Sprite.Collection.FirstValidDefinition.name);
            }
            m_Sprite.Collection.InitDictionary();
            m_Sprite.Collection.InitMaterialIds();
            expandSharedAssets1 = null;
        }*/

        public static void DeserializeSpriteCollectionFromAssetBundle(GameObject targetObject, AssetBundle expandSharedAssets1, Texture2D spriteAtlas, string serializedCollectionJSONName, Material overrideMaterial = null) {
            tk2dSpriteCollectionData m_CollectionData = targetObject.AddComponent<tk2dSpriteCollectionData>();
            JsonUtility.FromJsonOverwrite(ExpandUtility.DeserializeJSONDataFromAssetBundle(expandSharedAssets1, serializedCollectionJSONName, string.Empty, string.Empty), m_CollectionData);
            if (overrideMaterial) {
                m_CollectionData.material = new Material(overrideMaterial);
            } else {
                m_CollectionData.material = new Material(ShaderCache.Acquire(PlayerController.DefaultShaderName));
            }
            m_CollectionData.material.mainTexture = spriteAtlas;
            m_CollectionData.materials = new Material[] { m_CollectionData.material };
            foreach (tk2dSpriteDefinition definition in m_CollectionData.spriteDefinitions) {
                definition.material = m_CollectionData.materials[0];
                definition.materialInst = m_CollectionData.materials[0];
            }
            m_CollectionData.InitDictionary();
            m_CollectionData.InitMaterialIds();
        }


        public static void AddSpriteToObject(GameObject targetObject, Texture2D sourceTexture) {
            AtlasPacker = new RuntimeAtlasPacker();
            newCollection = GenerateNewSpriteCollection(targetObject);
            SpriteFromTexture(sourceTexture, targetObject);
        }
        
        public static GameObject SpriteFromTexture(Texture2D existingTexture, GameObject obj) {
			tk2dSprite tk2dSprite = obj.AddComponent<tk2dSprite>();
            int num = AddSpriteToCollection(existingTexture, newCollection);
			tk2dSprite.SetSprite(newCollection, num);
			tk2dSprite.SortingOrder = 0;
			obj.GetComponent<BraveBehaviour>().sprite = tk2dSprite;
			return obj;
		}
        
        public static int AddSpriteToCollection(Texture2D existingTexture, tk2dSpriteCollectionData collection) {
			tk2dSpriteDefinition tk2dSpriteDefinition = ConstructDefinition(existingTexture);
			tk2dSpriteDefinition.name = existingTexture.name;
			return AddSpriteToCollection(tk2dSpriteDefinition, collection);
		}

        public static void AddSpritesToCollection(AssetBundle assetSource, List<string> AssetNames, tk2dSpriteCollectionData collection) {
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
        
		public static int AddSpriteToCollection(tk2dSpriteDefinition spriteDefinition, tk2dSpriteCollectionData collection) {
			tk2dSpriteDefinition[] spriteDefinitions = collection.spriteDefinitions;
			tk2dSpriteDefinition[] array = spriteDefinitions.Concat(new tk2dSpriteDefinition[] { spriteDefinition }).ToArray();
			collection.spriteDefinitions = array;
			FieldInfo field = typeof(tk2dSpriteCollectionData).GetField("spriteNameLookupDict", BindingFlags.Instance | BindingFlags.NonPublic);
			field.SetValue(collection, null);
			collection.InitDictionary();
			return array.Length - 1;
		}

        public static tk2dSpriteCollectionData GenerateNewSpriteCollection(GameObject targetObject, string CollectionName = null, tk2dSpriteDefinition[] spriteDefinitions = null ,bool initDictionary = false) {
            tk2dSpriteCollectionData newCollection = targetObject.AddComponent<tk2dSpriteCollectionData>();
            newCollection.version = 3;
            //newCollection.materialIdsValid = true;
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

       
        public static tk2dSpriteDefinition ConstructDefinition(Texture2D texture) {
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
        
		public static T CopyFrom<T>(this Component comp, T other) where T : Component {
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

		public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component { return go.AddComponent<T>().CopyFrom(toAdd); }
	}
}

