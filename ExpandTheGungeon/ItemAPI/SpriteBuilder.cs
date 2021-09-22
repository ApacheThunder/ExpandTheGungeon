using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ExpandTheGungeon.ItemAPI {

	public static class SpriteBuilder {

        private static tk2dSpriteCollectionData itemCollection = PickupObjectDatabase.GetByEncounterName("singularity").sprite.Collection;

        public static tk2dSpriteCollectionData ammonomiconCollection = AmmonomiconController.ForceInstance.EncounterIconCollection;
        
        public static GameObject SpriteFromTexture(Texture2D existingTexture, GameObject obj = null) {
			bool flag = obj == null;
			if (flag) { obj = new GameObject(); }
			tk2dSprite tk2dSprite = obj.AddComponent<tk2dSprite>();
            int num = AddSpriteToCollection(existingTexture, itemCollection);
			tk2dSprite.SetSprite(itemCollection, num);
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

		public static int AddToAmmonomicon(tk2dSpriteDefinition spriteDefinition) {
			return AddSpriteToCollection(spriteDefinition, ammonomiconCollection);
		}

        public static int AddToAmmonomicon(tk2dSpriteDefinition spriteDefinition, Material overrideMaterial) {
            int m_spriteDefinition = AddSpriteToCollection(spriteDefinition, ammonomiconCollection);
            ammonomiconCollection.GetSpriteDefinition(spriteDefinition.name).material = overrideMaterial;
            return AddSpriteToCollection(spriteDefinition, ammonomiconCollection);
        }

        public static tk2dSpriteAnimationClip AddAnimation(tk2dSpriteAnimator animator, tk2dSpriteCollectionData collection, List<int> spriteIDs, string clipName, tk2dSpriteAnimationClip.WrapMode wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop, int frameRate = 15, int loopStart = 0, float minFidgetDuration = 0.5f, float maxFidgetDuration = 1) {
			if (animator.Library == null) {
                animator.Library = animator.gameObject.AddComponent<tk2dSpriteAnimation>();                
				animator.Library.clips = new tk2dSpriteAnimationClip[0];
			}
			List<tk2dSpriteAnimationFrame> list = new List<tk2dSpriteAnimationFrame>();
			for (int i = 0; i < spriteIDs.Count; i++) {
				tk2dSpriteDefinition spriteDefinition = collection.spriteDefinitions[spriteIDs[i]];
				if (spriteDefinition.Valid) {
					list.Add(new tk2dSpriteAnimationFrame {
						spriteCollection = collection,
						spriteId = spriteIDs[i],
                        invulnerableFrame = false
					});
				}
			}
			tk2dSpriteAnimationClip animationClip = new tk2dSpriteAnimationClip() {
                name = clipName,
                frames = list.ToArray(),
                fps = frameRate,
                wrapMode = wrapMode,
                loopStart = loopStart,
                minFidgetDuration = minFidgetDuration,
                maxFidgetDuration = maxFidgetDuration,
            };
			Array.Resize(ref animator.Library.clips, animator.Library.clips.Length + 1);
			animator.Library.clips[animator.Library.clips.Length - 1] = animationClip;
			return animationClip;
		}

		public static SpeculativeRigidbody SetUpSpeculativeRigidbody(this tk2dSprite sprite, IntVector2 offset, IntVector2 dimensions) {
			SpeculativeRigidbody orAddComponent = GameObjectExtensions.GetOrAddComponent<SpeculativeRigidbody>(sprite.gameObject);
			PixelCollider pixelCollider = new PixelCollider();
			pixelCollider.ColliderGenerationMode = 0;
			pixelCollider.CollisionLayer = CollisionLayer.EnemyCollider;
			pixelCollider.ManualWidth = dimensions.x;
			pixelCollider.ManualHeight = dimensions.y;
			pixelCollider.ManualOffsetX = offset.x;
			pixelCollider.ManualOffsetY = offset.y;
			orAddComponent.PixelColliders = new List<PixelCollider> { pixelCollider };
			return orAddComponent;
		}
       
        public static tk2dSpriteDefinition ConstructDefinition(Texture2D texture) {
			RuntimeAtlasSegment runtimeAtlasSegment = ETGMod.Assets.Packer.Pack(texture, false);
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

		public static tk2dSpriteCollectionData ConstructCollection(GameObject obj, string name, bool isFakePrefab = true) {
			tk2dSpriteCollectionData tk2dSpriteCollectionData = obj.AddComponent<tk2dSpriteCollectionData>();
            if (isFakePrefab) { UnityEngine.Object.DontDestroyOnLoad(obj); }
			tk2dSpriteCollectionData.assetName = name;
			tk2dSpriteCollectionData.spriteCollectionGUID = name;
			tk2dSpriteCollectionData.spriteCollectionName = name;
			tk2dSpriteCollectionData.spriteDefinitions = new tk2dSpriteDefinition[0];
			return tk2dSpriteCollectionData;
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

