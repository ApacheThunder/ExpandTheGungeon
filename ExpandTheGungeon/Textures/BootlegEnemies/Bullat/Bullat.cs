using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.Resources {

    public class Bullat : MonoBehaviour {

        public static tk2dSpriteCollectionData BootlegBullatCollection;

        public static List<Texture2D> BootlegBullatTextures;

        public static void Init(AIActor sourceActor = null) {

            if (sourceActor == null) { sourceActor = EnemyDatabase.GetOrLoadByGuid("2feb50a6a40f4f50982e89fd276f6f15"); }

            if (BootlegBullatTextures == null) {
                BootlegBullatTextures = new List<Texture2D>() {
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_charge_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_charge_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_charge_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_charge_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_die_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_idle_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_idle_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_idle_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_idle_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_idle_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\Bullat\\bullat_idle_006.png")
                };
            }

            BootlegBullatCollection = ExpandUtility.BuildSpriteCollection(sourceActor.sprite.Collection, null, BootlegBullatTextures, ShaderCache.Acquire("tk2d/BlendVertexColorUnlitTilted"), false);
            // DontDestroyOnLoad(BootlegBullatCollection);
        }
    }
}

