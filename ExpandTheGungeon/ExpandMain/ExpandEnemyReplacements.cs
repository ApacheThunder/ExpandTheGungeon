using ExpandTheGungeon.ExpandPrefab;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandMain {

    public class ExpandEnemyReplacements {

        public static void Init(List<AGDEnemyReplacementTier> m_cachedReplacementTiers) {
            if (m_cachedReplacementTiers != null) {
                for (int i = 0; i < m_cachedReplacementTiers.Count; i++) {
                    if (m_cachedReplacementTiers[i].Name.ToLower().EndsWith("_exsewers") |
                        m_cachedReplacementTiers[i].Name.ToLower().EndsWith("_exbbbey") |
                        m_cachedReplacementTiers[i].Name.ToLower().EndsWith("_exbelly"))
                    {
                        m_cachedReplacementTiers.Remove(m_cachedReplacementTiers[i]);
                    } else if (m_cachedReplacementTiers[i].Name.ToLower().StartsWith("exhotshot")) {
                        m_cachedReplacementTiers.Remove(m_cachedReplacementTiers[i]);
                    }
                }
                if (!ExpandSettings.IsHardModeBuild) {
                    InitReplacementEnemiesForSewers(m_cachedReplacementTiers);
                    InitReplacementEnemiesForAbbey(m_cachedReplacementTiers);
                }
                InitReplacementEnemiesForBelly(m_cachedReplacementTiers);
                InitHotShotReplacements(m_cachedReplacementTiers);
            }
        }

        private static void InitHotShotReplacements(List<AGDEnemyReplacementTier> agdEnemyReplacementTiers) {
            List<string> hotShotShotGuns = new List<string>() { ExpandCustomEnemyDatabase.HotShotShotgunKinGUID };
            List<string> hotShotBulletKins = new List<string>() { ExpandCustomEnemyDatabase.HotShotBulletKinGUID };
            List<string> hotShotCultist = new List<string>() { ExpandCustomEnemyDatabase.HotShotCultistGUID };

            string nameAppend = "EXHotShot";
            List<GlobalDungeonData.ValidTilesets> ValidTilesets = new List<GlobalDungeonData.ValidTilesets>() {
                GlobalDungeonData.ValidTilesets.CASTLEGEON,
                GlobalDungeonData.ValidTilesets.SEWERGEON,
                GlobalDungeonData.ValidTilesets.JUNGLEGEON,
                GlobalDungeonData.ValidTilesets.GUNGEON,
                GlobalDungeonData.ValidTilesets.CATHEDRALGEON,
                GlobalDungeonData.ValidTilesets.BELLYGEON,
                GlobalDungeonData.ValidTilesets.MINEGEON,
                GlobalDungeonData.ValidTilesets.RATGEON,
                GlobalDungeonData.ValidTilesets.CATACOMBGEON,
                GlobalDungeonData.ValidTilesets.OFFICEGEON,
                GlobalDungeonData.ValidTilesets.WESTGEON,
                GlobalDungeonData.ValidTilesets.PHOBOSGEON,
                GlobalDungeonData.ValidTilesets.FORGEGEON,
                GlobalDungeonData.ValidTilesets.HELLGEON,
                GlobalDungeonData.ValidTilesets.SPACEGEON
            };
            List<AGDEnemyReplacementTier> CultistReplacements = GenerateEnemyReplacementTiers(nameAppend, new DungeonPrerequisite[0], ValidTilesets, gunCultestGUIDs, hotShotCultist, 0.2f);
            List<AGDEnemyReplacementTier> ShotgunReplacements = GenerateEnemyReplacementTiers(nameAppend, new DungeonPrerequisite[0], ValidTilesets, bulletKinTargetGUIDs, hotShotBulletKins, 0.1f);
            List<AGDEnemyReplacementTier> BulletKinReplacements = GenerateEnemyReplacementTiers(nameAppend, new DungeonPrerequisite[0], ValidTilesets, shotgunKin2TargetGUIDs, hotShotShotGuns, 0.05f);
            foreach (AGDEnemyReplacementTier tier in CultistReplacements) { agdEnemyReplacementTiers.Add(tier); }
            foreach (AGDEnemyReplacementTier tier in ShotgunReplacements) { agdEnemyReplacementTiers.Add(tier); }
            foreach (AGDEnemyReplacementTier tier in BulletKinReplacements) { agdEnemyReplacementTiers.Add(tier); }
        }

        private static void InitReplacementEnemiesForSewers(List<AGDEnemyReplacementTier> agdEnemyReplacementTiers) {            
            GlobalDungeonData.ValidTilesets TargetTileset = GlobalDungeonData.ValidTilesets.SEWERGEON;
            string nameAppend = "_EXSewers";
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("bulletKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, bulletKinSewersTargetGUIDs, bulletKinSewersReplacementGUIDs));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("rubberKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, musketKinTargetGUIDs, musketKinReplacementGUIDs, 0.7f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("grenadeKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, dynamiteKinOfficeTargetGUIDs, dynamiteKinOfficeReplacementGUIDs, 0.8f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("angryBookReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, booksOfficeTargetGUIDs, booksOfficeReplacementGUIDs));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("kingBullatReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, bullatGargoyleTargetGUIDs, bullatGargoyleReplacementGUIDs));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("bigEnemyReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, bigEnemySewersTargetGUIDs, bigEnemySewersReplacementGUIDs, 0.65f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("mutantBulletKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, mutantBulletKinSewersTargetGUIDs, mutantBulletKinSewersReplacementGUIDs));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("gunzookieReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, gunzookieTargetGUIDs, gunzookieReplacementGUIDs));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("shroomerReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, snakeofficeSewersTargetGUIDs, snakeofficeReplacementGUIDs));
            return;
        }

        private static void InitReplacementEnemiesForBelly(List<AGDEnemyReplacementTier> agdEnemyReplacementTiers) {            
            GlobalDungeonData.ValidTilesets TargetTileset = GlobalDungeonData.ValidTilesets.BELLYGEON;
            string nameAppend = "_EXBelly";
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("bulletKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, new List<string>() { "01972dee89fc4404a5c408d50007dad5" }, new List<string>() { ExpandCustomEnemyDatabase.AggressiveCronenbergGUID } ));
            return;
        }

        private static void InitReplacementEnemiesForAbbey(List<AGDEnemyReplacementTier> agdEnemyReplacementTiers) {
            GlobalDungeonData.ValidTilesets TargetTileset = GlobalDungeonData.ValidTilesets.CATHEDRALGEON;
            string nameAppend = "_EXAbbey";
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("bulletKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, bulletKinAbbeyTargetGUIDs, bulletKinAbbeyReplacementGUIDs, 0.75f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("rubberKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, musketKinTargetGUIDs, musketKinReplacementGUIDs, 0.5f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("grenadeKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, dynamiteKinOfficeTargetGUIDs, dynamiteKinOfficeReplacementGUIDs, 0.7f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("angryBookReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, booksOfficeTargetGUIDs, booksOfficeReplacementGUIDs, 0.8f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("kingBullatReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, bullatGargoyleTargetGUIDs, bullatGargoyleReplacementGUIDs, 0.6f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("bigEnemyReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, bigEnemySewersTargetGUIDs, bigEnemySewersReplacementGUIDs, 0.5f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("mutantBulletKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, mutantBulletKinSewersTargetGUIDs, mutantBulletKinSewersReplacementGUIDs, 0.5f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("gunzookieReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, gunzookieTargetGUIDs, gunzookieReplacementGUIDs, 0.5f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("shroomerReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, snakeofficeSewersTargetGUIDs, snakeofficeReplacementGUIDs, 0.6f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("redShotgunKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, RedShotgunTargetGUIDs, PirateShotGunKinReplacementGUIDs, 0.7f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("blueShotgunKinReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, BlueShotgunTargetGUIDs, CowboyShotGunKinReplacementGUIDs, 0.7f));
            agdEnemyReplacementTiers.Add(GenerateEnemyReplacementTier("gigiReplacement" + nameAppend, new DungeonPrerequisite[0], TargetTileset, gigiTargetGUIDs, gigiParrotReplacementGUIDs, 0.65f));
            return;
        }
        

        public static AGDEnemyReplacementTier GenerateEnemyReplacementTier(string m_name, DungeonPrerequisite[] m_Prereqs, GlobalDungeonData.ValidTilesets m_TargetTileset, List<string> m_TargetGuids, List<string> m_ReplacementGUIDs, float m_ChanceToReplace = 1) {
            AGDEnemyReplacementTier m_cachedEnemyReplacementTier = new AGDEnemyReplacementTier() {
                Name = m_name,
                Prereqs = m_Prereqs,
                TargetTileset = m_TargetTileset,
                ChanceToReplace = m_ChanceToReplace,
                MaxPerFloor = -1,
                MaxPerRun = -1,
                TargetAllNonSignatureEnemies = false,
                TargetAllSignatureEnemies = false,
                TargetGuids = m_TargetGuids,
                ReplacementGuids = m_ReplacementGUIDs,
                RoomMustHaveColumns = false,
                RoomMinEnemyCount = -1,
                RoomMaxEnemyCount = -1,
                RoomMinSize = -1,
                RemoveAllOtherEnemies = false,
                RoomCantContain = new List<string>()
            };
            return m_cachedEnemyReplacementTier;
        }
        
        public static List<AGDEnemyReplacementTier> GenerateEnemyReplacementTiers(string m_name, DungeonPrerequisite[] m_Prereqs, List<GlobalDungeonData.ValidTilesets> m_TargetTilesets, List<string> m_TargetGuids, List<string> m_ReplacementGUIDs, float m_ChanceToReplace = 1) {
            List<AGDEnemyReplacementTier> m_ReplacementTiers = new List<AGDEnemyReplacementTier>();
            foreach (GlobalDungeonData.ValidTilesets validTileset in m_TargetTilesets) {
                m_ReplacementTiers.Add(
                    new AGDEnemyReplacementTier() {
                        Name = m_name + "_" + validTileset.ToString(),
                        Prereqs = m_Prereqs,
                        TargetTileset = validTileset,
                        ChanceToReplace = m_ChanceToReplace,
                        MaxPerFloor = -1,
                        MaxPerRun = -1,
                        TargetAllNonSignatureEnemies = false,
                        TargetAllSignatureEnemies = false,
                        TargetGuids = m_TargetGuids,
                        ReplacementGuids = m_ReplacementGUIDs,
                        RoomMustHaveColumns = false,
                        RoomMinEnemyCount = -1,
                        RoomMaxEnemyCount = -1,
                        RoomMinSize = -1,
                        RemoveAllOtherEnemies = false,
                        RoomCantContain = new List<string>()
                    }
                );
            }
            return m_ReplacementTiers;
        }

        public static readonly List<string> gigiTargetGUIDs = new List<string>() { "ed37fa13e0fa4fcf8239643957c51293" }; // gigi
        public static readonly List<string> gigiParrotReplacementGUIDs = new List<string>() { "4b21a913e8c54056bc05cafecf9da880" }; // gigi_parrot

        public static readonly List<string> RedShotgunTargetGUIDs = new List<string>() { "128db2f0781141bcb505d8f00f9e4d47" }; // red_shotgun_kin
        public static readonly List<string> BlueShotgunTargetGUIDs = new List<string>() {
            "b54d89f9e802455cbb2b8a96a31e8259", // blue_shotgun_kin
            "7f665bd7151347e298e4d366f8818284", // mutant_shotgun_kin
            "2752019b770f473193b08b4005dc781f" // veteran_shotgun_kin
        };

        public static readonly List<string> PirateShotGunKinReplacementGUIDs = new List<string>() { "86dfc13486ee4f559189de53cfb84107" }; // pirate_shotgun_kin
        public static readonly List<string> CowboyShotGunKinReplacementGUIDs = new List<string>() { "ddf12a4881eb43cfba04f36dd6377abb" }; // cowboy_shotgun_kin

        public static readonly List<string> CowboyKinTargetGUIDs = new List<string>() {
            "95ec774b5a75467a9ab05fa230c0c143", // Skullmet
            "336190e29e8a4f75ab7486595b700d4a" // Skullet
        };
        public static readonly List<string> CowboyKinReplacementGUIDs = new List<string>() { "5861e5a077244905a8c25c2b7b4d6ebb" }; // bullet_kin_cowboy
        public static readonly List<string> snakeofficeReplacementGUIDs = new List<string>() { "e861e59012954ab2b9b6977da85cb83c" }; // snake_office
        public static readonly List<string> snakeofficeTargetGUIDs = new List<string>() { "43426a2e39584871b287ac31df04b544" }; // wizbang
        public static readonly List<string> snakeofficeSewersTargetGUIDs = new List<string>() { "e5cffcfabfae489da61062ea20539887" }; // shroomer
        public static readonly List<string> bulletKinReplacementGUIDs = new List<string>() {
            "6f818f482a5c47fd8f38cce101f6566c", // bullet_kin_pirate
            "ff4f54ce606e4604bf8d467c1279be3e", // bullet_kin_broccoli
            "39e6f47a16ab4c86bec4b12984aece4c", // bullet_kin_knight
            "f020570a42164e2699dcf57cac8a495c", // bullet_kin_kaliber
            "37de0df92697431baa47894a075ba7e9", // bullet_kin_candle
            "5861e5a077244905a8c25c2b7b4d6ebb", // bullet_kin_cowboy
            "906d71ccc1934c02a6f4ff2e9c07c9ec", // bullet_kin_officetie
            "9eba44a0ea6c4ea386ff02286dd0e6bd", // bullet_kin_officesuit
            "05cb719e0178478685dc610f8b3e8bfc", // bullet_kin_vest
            "3b0bd258b4c9432db3339665cc61c356" // cactus_kin

        };
        public static readonly List<string> bulletKinTargetGUIDs = new List<string>() {
            "01972dee89fc4404a5c408d50007dad5", // bullet_kin
            "db35531e66ce41cbb81d507a34366dfe", // ak47_bullet_kin
            "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin 
            "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
            "5f3abc2d561b4b9c9e72b879c6f10c7e", // fallen_bullet_kin
            "1a78cfb776f54641b832e92c44021cf2", // Ashen Bullet Kin
            "8bb5578fba374e8aae8e10b754e61d62" // cardinal
        };

        public static readonly List<string> bulletKinSewersReplacementGUIDs = new List<string>() {
            "143be8c9bbb84e3fb3ab98bcd4cf5e5b", // bullet_kin_fish
            "06f5623a351c4f28bc8c6cda56004b80" // bullet_kin_fish_blue
        };

        public static readonly List<string> bulletKinSewersTargetGUIDs = new List<string>() {
            "01972dee89fc4404a5c408d50007dad5", // bullet_kin
            "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
        };

        public static readonly List<string> bulletKinAbbeyTargetGUIDs = new List<string>() {
            "01972dee89fc4404a5c408d50007dad5", // bullet_kin
            "db35531e66ce41cbb81d507a34366dfe", // ak47_bullet_kin
            "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin 
            "88b6b6a93d4b4234a67844ef4728382c", // bandana_bullet_kin
            "8bb5578fba374e8aae8e10b754e61d62" // cardinal
        };
        public static readonly List<string> bulletKinAbbeyReplacementGUIDs = new List<string>() {
            "6f818f482a5c47fd8f38cce101f6566c", // bullet_kin_pirate
            "39e6f47a16ab4c86bec4b12984aece4c", // bullet_kin_knight
            "37de0df92697431baa47894a075ba7e9", // bullet_kin_candle
            "5861e5a077244905a8c25c2b7b4d6ebb", // bullet_kin_cowboy
            "05cb719e0178478685dc610f8b3e8bfc", // bullet_kin_vest
            "3b0bd258b4c9432db3339665cc61c356" // cactus_kin
        };

        public static readonly List<string> musketKinReplacementGUIDs = new List<string>() { "226fd90be3a64958a5b13cb0a4f43e97" }; // musket_kin
        public static readonly List<string> musketKinTargetGUIDs = new List<string>() {
            "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
            "98fdf153a4dd4d51bf0bafe43f3c77ff" // tazie
        };
        public static readonly List<string> dynamiteKinOfficeReplacementGUIDs = new List<string>() {
            "5f15093e6f684f4fb09d3e7e697216b4", // dynamite_kin_office
            "05cb719e0178478685dc610f8b3e8bfc" // bullet_kin_vest
        }; 
        public static readonly List<string> dynamiteKinOfficeTargetGUIDs = new List<string>() { "4d37ce3d666b4ddda8039929225b7ede" }; // grenade_kin

        public static readonly List<string> booksOfficeReplacementGUIDs = new List<string>() {
            "78e0951b097b46d89356f004dda27c42", // tablet_bookllet
            "216fd3dfb9da439d9bd7ba53e1c76462" // necronomicon_bookllet
        };
        public static readonly List<string> booksOfficeTargetGUIDs = new List<string>() {
            "6f22935656c54ccfb89fca30ad663a64", // blue_bookllet
            "a400523e535f41ac80a43ff6b06dc0bf", // green_bookllet
            "c0ff3744760c4a2eb0bb52ac162056e6" // bookllet
        };
        public static readonly List<string> bullatGargoyleReplacementGUIDs = new List<string>() { "981d358ffc69419bac918ca1bdf0c7f7" }; // bullat_gargoyle
        public static readonly List<string> bullatGargoyleTargetGUIDs = new List<string>() { "1a4872dafdb34fd29fe8ac90bd2cea67", }; // king_bullat
        public static readonly List<string> bigEnemyReplacementGUIDs = new List<string>() {
            "2b6854c0849b4b8fb98eb15519d7db1c", // bullet_kin_mech
            "df4e9fedb8764b5a876517431ca67b86", // bullet_kin_gal_titan_boss
            "1f290ea06a4c416cabc52d6b3cf47266", // bullet_kin_titan_boss
            "c4cf0620f71c4678bb8d77929fd4feff", // bullet_kin_titan
            "9215d1a221904c7386b481a171e52859" // lead_maiden_fridge
        };
        public static readonly List<string> bigEnemyTargetGUIDs = new List<string>() {
            "ec8ea75b557d4e7b8ceeaacdf6f8238c", // gun_nut
            "383175a55879441d90933b5c4e60cf6f", // spectre_gun_nut
            "463d16121f884984abe759de38418e48", // chain_gunner
            "3f6d6b0c4a7c4690807435c7b37c35a5", // agonizer
            "cd4a4b7f612a4ba9a720b9f97c52f38c", // lead_maiden
            "98ea2fe181ab4323ab6e9981955a9bca", // shambling_round
            "d5a7b95774cd41f080e517bea07bf495", // revolvenant
            "88f037c3f93b4362a040a87b30770407", // gunreaper
            "3e98ccecf7334ff2800188c417e67c15", // killithid
            "98ca70157c364750a60f5e0084f9d3e2", // phaser_spider
            "45192ff6d6cb43ed8f1a874ab6bef316", // misfire_beast
            "21dd14e5ca2a4a388adab5b11b69a1e1" // shelleton
        };

        public static readonly List<string> hardEnemyTargetGUIDs = new List<string>() {
            "7b0b1b6d9ce7405b86b75ce648025dd6", // Beadie
            "c4fba8def15e47b297865b18e36cbef8", // Gunjurer
            "5288e86d20184fa69c91ceb642d31474", // Gummy
            "8a9e9bedac014a829a48735da6daf3da", // Gunsinger
            "c50a862d19fc4d30baeba54795e8cb93", // Aged Gunsinger
            "9d50684ce2c044e880878e86dbada919", // Coaler
            "f905765488874846b7ff257ff81d6d0c", // Fungun
            "cf27dd464a504a428d87a8b2560ad40a", // Tombstoner
            "206405acad4d4c33aac6717d184dc8d4", // Apprentice Gunjurer
            "8b4a938cdbc64e64822e841e482ba3d2", // Jammomancer
            "ba657723b2904aa79f9e51bce7d23872", // Jamerlengo
            "57255ed50ee24794b7aac1ac3cfb8a95", // Gun Cultist
            "6e972cd3b11e4b429b888b488e308551", // gunzookie
            "8a9e9bedac014a829a48735da6daf3da" // gunzockie
        };

        public static readonly List<string> gunCultestGUIDs = new List<string>() { "57255ed50ee24794b7aac1ac3cfb8a95" }; // Gun Cultist

        public static readonly List<string> bigEnemySewersTargetGUIDs = new List<string>() {
            "cd4a4b7f612a4ba9a720b9f97c52f38c", // lead_maiden
            "ec8ea75b557d4e7b8ceeaacdf6f8238c", // gun_nut
            "463d16121f884984abe759de38418e48" // chain_gunner
        };

        public static readonly List<string> bigEnemySewersReplacementGUIDs = new List<string>() {
            "2b6854c0849b4b8fb98eb15519d7db1c", // bullet_kin_mech
            "9215d1a221904c7386b481a171e52859" // lead_maiden_fridge
        };

        public static readonly List<string> shotgunKinReplacementGUIDs = new List<string>() {
            "ddf12a4881eb43cfba04f36dd6377abb", // cowboy_shotgun_kin
            "86dfc13486ee4f559189de53cfb84107", // pirate_shotgun_kin
            "d4f4405e0ff34ab483966fd177f2ece3", // cylinder
            "534f1159e7cf4f6aa00aeea92459065e" // cylinder_red
        };
        public static readonly List<string> shotgunKinTargetGUIDs = new List<string>() {
            "128db2f0781141bcb505d8f00f9e4d47", // red_shotgun_kin
            "b54d89f9e802455cbb2b8a96a31e8259", // blue_shotgun_kin
            "1bd8e49f93614e76b140077ff2e33f2b", // ashen_shotgun_kin
            "7f665bd7151347e298e4d366f8818284" // mutant_shotgun_kin
        };
        public static readonly List<string> shotgunKin2TargetGUIDs = new List<string>() {
            "128db2f0781141bcb505d8f00f9e4d47", // red_shotgun_kin
            "b54d89f9e802455cbb2b8a96a31e8259", // blue_shotgun_kin
            "2752019b770f473193b08b4005dc781f", // veteran_shotgun_kin
            "1bd8e49f93614e76b140077ff2e33f2b", // ashen_shotgun_kin
            "7f665bd7151347e298e4d366f8818284" // mutant_shotgun_kin
        };
        public static readonly List<string> mutantBulletKinReplacementGUIDs = new List<string>() {
            "143be8c9bbb84e3fb3ab98bcd4cf5e5b", // bullet_kin_fish
            "06f5623a351c4f28bc8c6cda56004b80" // bullet_kin_fish_blue
        };
        public static readonly List<string> mutantBulletKinTargetGUIDs = new List<string>() { "d4a9836f8ab14f3fadd0f597438b1f1f",  }; // mutant_bullet_kin
        public static readonly List<string> mutantBulletKinSewersReplacementGUIDs = new List<string>() {
            "f020570a42164e2699dcf57cac8a495c", // bullet_kin_kaliber
            "ff4f54ce606e4604bf8d467c1279be3e" // bullet_kin_broccoli
        }; 
        public static readonly List<string> mutantBulletKinSewersTargetGUIDs = new List<string>() {
            "d4a9836f8ab14f3fadd0f597438b1f1f", // mutant_bullet_kin
            "7f665bd7151347e298e4d366f8818284" // mutant_shotgun_kin
        }; 

        public static readonly List<string> gunzookieTargetGUIDs = new List<string>() {
            "6e972cd3b11e4b429b888b488e308551", // gunzookie
            "8a9e9bedac014a829a48735da6daf3da" // gunzockie
        }; 
        public static readonly List<string> gunzookieReplacementGUIDs = new List<string>() { "80ab6cd15bfc46668a8844b2975c6c26" }; // gunzookie_office
    }
}

