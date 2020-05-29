using System;
using System.Collections;
using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using ExpandTheGungeon.ExpandUtilities;
using System.IO;

namespace ExpandTheGungeon.ExpandObjects {

    public class ExpandCustomSpriteCollections : MonoBehaviour {

        public static GameObject ShotgunReskinObject;

        public static void InitShotgunKinCollection() {
            
            string basePath = Path.Combine(ETGMod.ResourcesDirectory, "PlayableShotgunMan");

            if (!File.Exists(basePath + "/" + "Playable_Shotgun_Man_Swap.png") |
                !File.Exists(basePath + "/" + "ShotgunManSpriteCollection.txt") |
                !File.Exists(basePath + "/" + "ShotgunManAnimationData.txt"))
            {
                return;
            }
            
            AssetBundle expandSharedAssets1 = ResourceManager.LoadAssetBundle("ExpandSharedAuto");

            ShotgunReskinObject = expandSharedAssets1.LoadAsset<GameObject>("Playable_Shotgun_Man_Swap_Rebuild");
            
            string TexturePath = basePath + "/" + "Playable_Shotgun_Man_Swap.png";
            string SpriteCollectionPath = File.ReadAllText(basePath + "/" + "ShotgunManSpriteCollection.txt");
            string SpriteAnimationPath = File.ReadAllText(basePath + "/" + "ShotgunManAnimationData.txt");


            Texture2D ShotgunManTexture = ResourceExtractor.GetTextureFromFile(basePath, "Playable_Shotgun_Man_Swap.png");
            
            Material PlayerMaterial = new Material(Shader.Find("Brave/PlayerShader"));
            PlayerMaterial.mainTexture = ShotgunManTexture;
            
            tk2dSpriteCollectionData ShotgunSpriteCollection = ShotgunReskinObject.AddComponent<tk2dSpriteCollectionData>();
            tk2dSpriteAnimation ShotgunSpriteAnimation = ShotgunReskinObject.AddComponent<tk2dSpriteAnimation>();

            JsonUtility.FromJsonOverwrite(SpriteCollectionPath, ShotgunSpriteCollection);
            JsonUtility.FromJsonOverwrite(SpriteAnimationPath, ShotgunSpriteAnimation);

            ShotgunSpriteCollection.textures[0] = ShotgunManTexture;
            ShotgunSpriteCollection.materials[0] = PlayerMaterial;

            foreach (tk2dSpriteDefinition spriteDefinition in ShotgunSpriteCollection.spriteDefinitions) {
                if (spriteDefinition.material != null) { spriteDefinition.material = PlayerMaterial; }
            }

            foreach (tk2dSpriteAnimationClip clip in ShotgunSpriteAnimation.clips) {
                foreach (tk2dSpriteAnimationFrame frame in clip.frames) { frame.spriteCollection = ShotgunSpriteCollection; }
            }

            expandSharedAssets1 = null;
            // DontDestroyOnLoad(ShotgunReskinObject);
        }
    }
}

