using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandDungeonCollections : MonoBehaviour {

        public static tk2dSpriteCollectionData ENV_Tileset_Belly(GameObject TargetObject, AssetBundle sharedAssets) {
            
            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();
            
            Material m_LitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTk2dCustomFalloffCutout"));
            m_LitCutout.mainTexture = ExpandPrefabs.ENV_Tileset_Belly_Texture;
            m_LitCutout.SetFloat("_Cutoff", 0.5f);
            m_LitCutout.SetFloat("_MaxValue", 1);
            m_LitCutout.SetFloat("_Perpendicular", 1);

            Material m_LitBlend = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTK2dCustomFalloff"));
            m_LitBlend.mainTexture = ExpandPrefabs.ENV_Tileset_Belly_Texture;
            m_LitBlend.SetFloat("_Cutoff", 0.5f);
            m_LitBlend.SetFloat("_Perpendicular", 1);

            Material m_UnlitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveUnlitCutout"));
            m_UnlitCutout.mainTexture = ExpandPrefabs.ENV_Tileset_Belly_Texture;
            m_UnlitCutout.SetFloat("_Cutoff", 0.5f);
            m_UnlitCutout.SetFloat("_Perpendicular", 1);

            JsonUtility.FromJsonOverwrite(ResourceExtractor.BuildStringFromEmbeddedResource("SerializedData/BellyAssets/ENV_Tileset_Belly.txt"), m_NewDungeonCollection);

            string[] m_BellyMaterialTable = ResourceExtractor.GetLinesFromEmbeddedResource("SerializedData/BellyAssets/ENV_Tileset_Belly_MaterialTable.txt");
            

            for (int i = 0; i < m_NewDungeonCollection.spriteDefinitions.Length; i++) {
                if (m_BellyMaterialTable[i].Contains("lit cutout")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutout;
                } else if (m_BellyMaterialTable[i].Contains("lit blend")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitBlend;
                } else if (m_BellyMaterialTable[i].Contains("unlit cutout")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_UnlitCutout;
                } else {
                    Debug.Log("[ExpandTheGungeon] ERROR: sprite id " + i + " did not have a matching material name in lookup table!");
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutout;
                }
            }
                        
            m_NewDungeonCollection.materials = new Material[] { m_LitCutout, m_LitBlend, m_UnlitCutout };
            m_NewDungeonCollection.textures = new Texture[] { ExpandPrefabs.ENV_Tileset_Belly_Texture };

            sharedAssets = null;

            return m_NewDungeonCollection;
        }
                
        public static tk2dSpriteCollectionData ENV_Tileset_West(GameObject TargetObject, AssetBundle sharedAssets) {
            
            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(ResourceExtractor.BuildStringFromEmbeddedResource("SerializedData/WestAssets/ENV_Tileset_West.txt"), m_NewDungeonCollection);
                        
            Material m_LitCutout = new Material(sharedAssets.LoadAsset<Material>("lit cutout"));
            m_LitCutout.mainTexture = ExpandPrefabs.ENV_Tileset_West_Texture;

            Material m_LitBlend = new Material(sharedAssets.LoadAsset<Material>("lit blend"));
            m_LitBlend.mainTexture = ExpandPrefabs.ENV_Tileset_West_Texture;

            Material m_UnlitTransparent = new Material(sharedAssets.LoadAsset<Material>("unlit transparent"));
            m_UnlitTransparent.mainTexture = ExpandPrefabs.ENV_Tileset_West_Texture;

            string[] m_WestMaterialTable = ResourceExtractor.GetLinesFromEmbeddedResource("SerializedData/WestAssets/ENV_Tileset_West_MaterialTable.txt");

            for (int i = 0; i < m_NewDungeonCollection.spriteDefinitions.Length; i++) {
                if (m_WestMaterialTable[i].Contains("lit cutout")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutout;
                } else if (m_WestMaterialTable[i].Contains("lit blend")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitBlend;
                } else if (m_WestMaterialTable[i].Contains("unlit transparent")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_UnlitTransparent;
                } else {
                    Debug.Log("[ExpandTheGungeon] ERROR: sprite id " + i + " did not have a matching material name in lookup table!");
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutout;
                }
            }

            m_NewDungeonCollection.materials = new Material[] { m_LitCutout, m_LitBlend, m_UnlitTransparent };

            m_NewDungeonCollection.textures = new Texture[] { ExpandPrefabs.ENV_Tileset_West_Texture };
            
            sharedAssets = null;

            return m_NewDungeonCollection;
        }

	}
}

