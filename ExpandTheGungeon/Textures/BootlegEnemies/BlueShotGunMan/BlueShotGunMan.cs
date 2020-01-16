using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.Resources {

    public class BlueShotGunMan : MonoBehaviour {

        public static tk2dSpriteCollectionData BootlegBlueShotGunManCollection;

        public static List<Texture2D> BootlegBlueShotGunManTextures;

        public static void Init(AIActor sourceActor = null) {

            if (sourceActor == null) { sourceActor = EnemyDatabase.GetOrLoadByGuid("128db2f0781141bcb505d8f00f9e4d47"); }

            if (BootlegBlueShotGunManTextures == null) {
                BootlegBlueShotGunManTextures = new List<Texture2D>() {
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_front_north_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_front_north_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_front_north_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_burst_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_burst_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_burst_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_burst_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_burst_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_burst_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_surprise_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_surprise_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_surprise_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_surprise_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_spawn_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_spawn_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_spawn_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_right_back_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_right_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_right_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_right_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_right_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_right_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_left_back_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_left_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_left_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_left_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_left_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_run_eft_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_right_run005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_right_run_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_right_run_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_right_run_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_right_run_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_right_run_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_pitfall_right_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_pitfall_right_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_pitfall_right_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_pitfall_right_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_pitfall_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_left_run_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_left_run_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_left_run_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_left_run_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_left_run_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_left_run_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_idle_front_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_idle_front_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_idle_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_idle_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_hit_right_forward_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_hit_right_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_hit_left_forward_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_hit_left_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_side_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_side_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_side_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_side_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_side_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_front_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_front_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_front_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_front_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_forward_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_forward_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_right_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_side_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_side_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_side_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_side_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_side_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_front_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_front_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_front_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_front_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_forward_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_forward_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_left_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_front_north_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BlueShotGunMan\\shotgunguy_death_front_north_004.png")
                };
            }
            BootlegBlueShotGunManCollection = ExpandUtility.BuildSpriteCollection(sourceActor.sprite.Collection, null, BootlegBlueShotGunManTextures, ShaderCache.Acquire("tk2d/BlendVertexColorUnlitTilted"), false);
            // DontDestroyOnLoad(BootlegBlueShotGunManCollection);
        }
    }
}

