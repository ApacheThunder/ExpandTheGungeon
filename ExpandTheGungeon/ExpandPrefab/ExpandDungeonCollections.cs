using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandDungeonCollections {

        public static tk2dSpriteCollectionData ENV_Tileset_Jungle(GameObject TargetObject, Texture2D tileSetTexture, AssetBundle sharedAssets, AssetBundle expandSharedAssets1) {
            
            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();
            JsonUtility.FromJsonOverwrite(ExpandAssets.DeserializeJSONDataFromAssetBundle(expandSharedAssets1, "TilesetData/Jungle/ENV_Tileset_Jungle"), m_NewDungeonCollection);

            Material m_LitCutoutPerp = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTk2dCustomFalloffCutout"));
            m_LitCutoutPerp.mainTexture = tileSetTexture;
            m_LitCutoutPerp.SetFloat("_Cutoff", 0.5f);
            m_LitCutoutPerp.SetFloat("_MaxValue", 1);
            m_LitCutoutPerp.SetFloat("_Perpendicular", 1);

            Material m_LitBlend = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTK2dCustomFalloff"));
            m_LitBlend.mainTexture = tileSetTexture;
            m_LitBlend.SetFloat("_Cutoff", 0.5f);
            m_LitBlend.SetFloat("_Perpendicular", 1);

            m_NewDungeonCollection.materials = new Material[] { m_LitCutoutPerp, m_LitBlend };
            m_NewDungeonCollection.textures = new Texture[] { tileSetTexture };
            
            string[] m_MaterialTable = ExpandAssets.GetLinesFromAssetBundle(expandSharedAssets1, "ExpandSerializedData/TilesetData/Jungle/ENV_Tileset_Jungle_MaterialTable");
            
            for (int i = 0; i < m_NewDungeonCollection.spriteDefinitions.Length; i++) {
                if (m_MaterialTable[i].Contains("lit cutout perp")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutoutPerp;
                } else if (m_MaterialTable[i].Contains("lit blend")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitBlend;
                } else {
                    Debug.Log("[ExpandTheGungeon] ERROR: sprite id " + i + " did not have a matching material name in lookup table!");
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutoutPerp;
                }
            }
            return m_NewDungeonCollection;
        }

        public static tk2dSpriteCollectionData ENV_Tileset_Belly(GameObject TargetObject, Texture2D tileSetTexture, AssetBundle sharedAssets, AssetBundle expandSharedAssets1) {
            
            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();
            Material m_LitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTk2dCustomFalloffCutout"));
            m_LitCutout.mainTexture = tileSetTexture;
            m_LitCutout.SetFloat("_Cutoff", 0.5f);
            m_LitCutout.SetFloat("_MaxValue", 1);
            m_LitCutout.SetFloat("_Perpendicular", 1);

            Material m_LitBlend = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTK2dCustomFalloff"));
            m_LitBlend.mainTexture = tileSetTexture;
            m_LitBlend.SetFloat("_Cutoff", 0.5f);
            m_LitBlend.SetFloat("_Perpendicular", 1);

            Material m_UnlitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveUnlitCutout"));
            m_UnlitCutout.mainTexture = tileSetTexture;
            m_UnlitCutout.SetFloat("_Cutoff", 0.5f);
            m_UnlitCutout.SetFloat("_Perpendicular", 1);

            JsonUtility.FromJsonOverwrite(ExpandAssets.DeserializeJSONDataFromAssetBundle(expandSharedAssets1, "TilesetData/Belly/ENV_Tileset_Belly"), m_NewDungeonCollection);

            string[] m_BellyMaterialTable = ExpandAssets.GetLinesFromAssetBundle(expandSharedAssets1, "ExpandSerializedData/TilesetData/Belly/ENV_Tileset_Belly_MaterialTable");
            

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
            m_NewDungeonCollection.textures = new Texture[] { tileSetTexture };

            sharedAssets = null;

            return m_NewDungeonCollection;
        }
                
        public static tk2dSpriteCollectionData ENV_Tileset_West(GameObject TargetObject, Texture2D tileSetTexture, AssetBundle sharedAssets, AssetBundle expandSharedAssets1) {
            
            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();

            JsonUtility.FromJsonOverwrite(ExpandAssets.DeserializeJSONDataFromAssetBundle(expandSharedAssets1, "TilesetData/West/ENV_Tileset_West"), m_NewDungeonCollection);
                        
            Material m_LitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTk2dCustomFalloffCutout"));
            Material m_LitBlend = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTK2dCustomFalloff"));
            Material m_UnlitTransparent = new Material(sharedAssets.LoadAsset<Shader>("BraveUnlitCutout"));
            m_LitCutout.mainTexture = tileSetTexture;
            m_LitCutout.SetFloat("_Cutoff", 0.5f);
            m_LitCutout.SetFloat("_MaxValue", 1);
            m_LitCutout.SetFloat("_Perpendicular", 1);
            m_LitBlend.mainTexture = tileSetTexture;
            m_LitBlend.SetFloat("_Cutoff", 0.5f);
            m_LitBlend.SetFloat("_Perpendicular", 1);
            m_UnlitTransparent.mainTexture = tileSetTexture;
            m_UnlitTransparent.SetFloat("_Cutoff", 0.5f);
            m_UnlitTransparent.SetFloat("_Perpendicular", 1);

            string[] m_WestMaterialTable = ExpandAssets.GetLinesFromAssetBundle(expandSharedAssets1, "ExpandSerializedData/TilesetData/West/ENV_Tileset_West_MaterialTable");

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

            m_NewDungeonCollection.textures = new Texture[] { tileSetTexture };
            
            sharedAssets = null;

            return m_NewDungeonCollection;
        }

        public static tk2dSpriteCollectionData ENV_Tileset_Phobos(GameObject TargetObject, Texture2D tileSetTexture, AssetBundle sharedAssets, AssetBundle expandSharedAssets1) {

            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();
            JsonUtility.FromJsonOverwrite(ExpandAssets.DeserializeJSONDataFromAssetBundle(expandSharedAssets1, "TilesetData/Phobos/ENV_Tileset_Phobos"), m_NewDungeonCollection);

            Material m_LitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTk2dCustomFalloffCutout"));
            Material m_LitBlend = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTK2dCustomFalloff"));
            Material m_Unlit = new Material(sharedAssets.LoadAsset<Shader>("BraveUnlitCutout"));
            m_LitCutout.mainTexture = tileSetTexture;
            m_LitCutout.SetFloat("_Cutoff", 0.5f);
            m_LitCutout.SetFloat("_MaxValue", 1);
            m_LitCutout.SetFloat("_Perpendicular", 1);
            m_LitBlend.mainTexture = tileSetTexture;
            m_LitBlend.mainTexture = tileSetTexture;
            m_LitBlend.SetFloat("_Cutoff", 0.5f);
            m_LitBlend.SetFloat("_Perpendicular", 1);
            m_Unlit.mainTexture = tileSetTexture;
            m_Unlit.SetFloat("_Cutoff", 0.5f);
            m_Unlit.SetFloat("_Perpendicular", 1);


            string[] m_WestMaterialTable = ExpandAssets.GetLinesFromAssetBundle(expandSharedAssets1, "ExpandSerializedData/TilesetData/Phobos/ENV_Tileset_Phobos_MaterialTable");

            for (int i = 0; i < m_NewDungeonCollection.spriteDefinitions.Length; i++) {
                if (m_WestMaterialTable[i].Contains("lit cutout")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutout;
                } else if (m_WestMaterialTable[i].Contains("lit blend")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitBlend;
                } else if (m_WestMaterialTable[i].Contains("unlit")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_Unlit;
                } else {
                    Debug.Log("[ExpandTheGungeon] ERROR: sprite id " + i + " did not have a matching material name in lookup table!");
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutout;
                }
            }

            m_NewDungeonCollection.materials = new Material[] { m_LitCutout, m_LitBlend, m_Unlit};
            m_NewDungeonCollection.textures = new Texture[] { tileSetTexture };
            
            sharedAssets = null;

            return m_NewDungeonCollection;
        }

        public static tk2dSpriteCollectionData ENV_Tileset_Office(GameObject TargetObject, Texture2D tileSetTexture, AssetBundle sharedAssets, AssetBundle expandSharedAssets1) {

            tk2dSpriteCollectionData m_NewDungeonCollection = TargetObject.AddComponent<tk2dSpriteCollectionData>();
            JsonUtility.FromJsonOverwrite(ExpandAssets.DeserializeJSONDataFromAssetBundle(expandSharedAssets1, "TilesetData/Nakatomi/ENV_Tileset_Nakatomi"), m_NewDungeonCollection);

            Material m_LitCutout = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTk2dCustomFalloffCutout"));
            Material m_LitBlend = new Material(sharedAssets.LoadAsset<Shader>("BraveLitTK2dCustomFalloff"));
            Material m_Unlit = new Material(sharedAssets.LoadAsset<Shader>("BraveUnlitCutout"));
            m_LitCutout.mainTexture = tileSetTexture;
            m_LitCutout.SetFloat("_Cutoff", 0.5f);
            m_LitCutout.SetFloat("_MaxValue", 1);
            m_LitCutout.SetFloat("_Perpendicular", 1);
            m_LitBlend.mainTexture = tileSetTexture;
            m_LitBlend.SetFloat("_Cutoff", 0.5f);
            m_LitBlend.SetFloat("_Perpendicular", 1);
            m_Unlit.mainTexture = tileSetTexture;
            m_Unlit.SetFloat("_Cutoff", 0.5f);
            m_Unlit.SetFloat("_Perpendicular", 1);

            string[] m_WestMaterialTable = ExpandAssets.GetLinesFromAssetBundle(expandSharedAssets1, "ExpandSerializedData/TilesetData/Nakatomi/ENV_Tileset_Nakatomi_MaterialTable");

            for (int i = 0; i < m_NewDungeonCollection.spriteDefinitions.Length; i++) {
                if (m_WestMaterialTable[i].Contains("lit cutout")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutout;
                } else if (m_WestMaterialTable[i].Contains("lit blend")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitBlend;
                } else if (m_WestMaterialTable[i].Contains("unlit")) {
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_Unlit;
                } else {
                    Debug.Log("[ExpandTheGungeon] ERROR: sprite id " + i + " did not have a matching material name in lookup table!");
                    m_NewDungeonCollection.spriteDefinitions[i].material = m_LitCutout;
                }
            }

            m_NewDungeonCollection.materials = new Material[] { m_LitCutout, m_LitBlend, m_Unlit};
            m_NewDungeonCollection.textures = new Texture[] { tileSetTexture };
            
            sharedAssets = null;

            return m_NewDungeonCollection;
        }
    }
}

