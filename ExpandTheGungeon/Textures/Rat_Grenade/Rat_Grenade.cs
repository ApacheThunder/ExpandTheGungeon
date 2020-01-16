using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.Resources {

    public class RatGrenade : MonoBehaviour {

        public static tk2dSpriteCollectionData RatGrenadeCollection;

        public static List<Texture2D> RatGrenadeTextures;

        public static void Init(AIActor sourceActor = null) {

            if (sourceActor == null) { sourceActor = EnemyDatabase.GetOrLoadByGuid("14ea47ff46b54bb4a98f91ffcffb656d"); }

            if (RatGrenadeTextures == null) {
                RatGrenadeTextures = new List<Texture2D>() {
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_front_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_front_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_front_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_front_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_front_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_front_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_left_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_left_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_left_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_left_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_left_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_right_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_right_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_right_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_right_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_right_006.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_front_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_front_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_front_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_front_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_left_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_left_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_left_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_left_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_right_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_right_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_right_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_idle_right_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_back_001.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_back_002.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_back_003.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_back_004.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_back_005.png"),
                    ResourceExtractor.GetTextureFromResource("Textures\\Rat_Grenade\\rat_candle_move_back_006.png")
                };
            }
            

            RatGrenadeCollection = ExpandUtility.BuildSpriteCollection(sourceActor.sprite.Collection, null, RatGrenadeTextures, null, true);
            // DontDestroyOnLoad(RatGrenadeCollection);
        }
    }
}

