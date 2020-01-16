using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.Resources {

    public class BulletMan : MonoBehaviour {

        public static tk2dSpriteCollectionData BootlegBulletManCollection;

        public static List<Texture2D> BootlegBulletMan;

        public static void Init(AIActor sourceActor = null) {

            if (sourceActor == null) { sourceActor = EnemyDatabase.GetOrLoadByGuid("01972dee89fc4404a5c408d50007dad5"); }

            if (BootlegBulletMan == null) {
                BootlegBulletMan = new List<Texture2D>() {
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_left_leap_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_left_leap_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_left_leap_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_left_leap_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_left_leap_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_right_idle_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_right_idle_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_right_leap_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_right_leap_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_right_leap_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_right_leap_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_right_leap_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_back_south_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_back_south_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_back_south_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_back_south_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_front_north_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_front_north_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_front_north_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_front_north_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_front_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_front_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_front_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_front_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_front_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_side_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_side_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_side_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_left_side_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_front_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_front_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_front_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_front_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_front_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_side_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_side_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_side_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_death_right_side_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_die_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_die_left_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_die_left_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_die_left_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_die_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_die_right_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_die_right_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_die_right_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_hand_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_hit_back_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_hit_back_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_hit_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_hit_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_idle_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_idle_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_idle_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_idle_left_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_idle_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_idle_right_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_pitfall_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_pitfall_right_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_pitfall_right_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_pitfall_right_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_pitfall_right_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_left_back_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_run_right_back_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_shooting_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_shooting_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_spawn_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_spawn_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_spawn_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_surprise_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_surprise_left_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_surprise_left_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_surprise_left_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_surprise_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_surprise_right_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_surprise_right_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_surprise_right_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_left_idle_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\BootlegEnemies\\BulletMan\\bullet_cover_left_idle_002.png")
                };
            }            
            BootlegBulletManCollection = ExpandUtility.BuildSpriteCollection(sourceActor.sprite.Collection, null, BootlegBulletMan, ShaderCache.Acquire("tk2d/BlendVertexColorUnlitTilted"), false);
            // DontDestroyOnLoad(BootlegBulletManCollection);
        }
    }
}

