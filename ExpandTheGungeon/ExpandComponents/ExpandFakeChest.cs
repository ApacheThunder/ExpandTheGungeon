using System;
using System.Collections;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandObjects;
using ExpandTheGungeon.ExpandUtilities;
using System.IO;
using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandFakeChest : DungeonPlaceableBehaviour, IPlaceConfigurable, IPlayerInteractable {

        public ExpandFakeChest() {
            chestType = ChestType.RickRoll;
            BaseOutlineColor = Color.black;
            switchOnAnimName = "RickRollSwitch_TurnOn";
            switchOffAnimName = "RickRollSwitch_TurnOff";
            confettiNames = new string[] {
                "Global VFX/Confetti_Blue_001",
                "Global VFX/Confetti_Yellow_001",
                "Global VFX/Confetti_Green_001"
            };

            SurpriseChestEnemySpawnPool = new List<string>() {
                "01972dee89fc4404a5c408d50007dad5", // bullet_kin
                "05891b158cd542b1a5f3df30fb67a7ff", // arrow_head
                "4d37ce3d666b4ddda8039929225b7ede", // grenade_kin
                "8bb5578fba374e8aae8e10b754e61d62", // cardinal
                "f905765488874846b7ff257ff81d6d0c", // fungun
                "37340393f97f41b2822bc02d14654172", // creech
                "5f3abc2d561b4b9c9e72b879c6f10c7e", // fallen_bullet_kin
                "1bd8e49f93614e76b140077ff2e33f2b", // ashen_shotgun_kin
                "1a78cfb776f54641b832e92c44021cf2", // ashen_bullet_kin
                "2752019b770f473193b08b4005dc781f", // veteran_shotgun_kin
                "70216cae6c1346309d86d4a0b4603045", // veteran_bullet_kin
                "d4dd2b2bbda64cc9bcec534b4e920518", // bullet_kings_toadie_revenge
                "a9cc6a4e9b3d46ea871e70a03c9f77d4", // marines_past_imp
                "6b7ef9e5d05b4f96b04f05ef4a0d1b18", // rubber_kin
                "98fdf153a4dd4d51bf0bafe43f3c77ff", // tazie
                "be0683affb0e41bbb699cb7125fdded6", // mouser
                "8b43a5c59b854eb780f9ab669ec26b7a", // dragun_egg_slimeguy
                "d1c9781fdac54d9e8498ed89210a0238", // tiny_blobulord
                "76bc43539fc24648bff4568c75c686d1", // chicken
                "226fd90be3a64958a5b13cb0a4f43e97", // musket_kin
                "6f818f482a5c47fd8f38cce101f6566c", // bullet_kin_pirate
                "143be8c9bbb84e3fb3ab98bcd4cf5e5b", // bullet_kin_fish
                "06f5623a351c4f28bc8c6cda56004b80", // bullet_kin_fish_blue
                "ff4f54ce606e4604bf8d467c1279be3e", // bullet_kin_broccoli
                "39e6f47a16ab4c86bec4b12984aece4c", // bullet_kin_knight
                "f020570a42164e2699dcf57cac8a495c", // bullet_kin_kaliber
                "37de0df92697431baa47894a075ba7e9", // bullet_kin_candle
                "5861e5a077244905a8c25c2b7b4d6ebb", // bullet_kin_cowboy
                "906d71ccc1934c02a6f4ff2e9c07c9ec", // bullet_kin_officetie
                "9eba44a0ea6c4ea386ff02286dd0e6bd", // bullet_kin_officesuit
                "05cb719e0178478685dc610f8b3e8bfc" // bullet_kin_vest
            };

            IsBroken = false;
            m_configured = false;
            m_Opened = false;
        }

        public ChestType chestType;

        public enum ChestType { MusicSwitch, RickRoll, SurpriseChest};

        public Color BaseOutlineColor;
        
        public bool IsBroken;

        public string openAnimName;
        public string breakAnimName;

        public string switchOnAnimName;
        public string switchOffAnimName;

        public string[] confettiNames;
        public List<string> SurpriseChestEnemySpawnPool;

        public GameObject RickRollAnimationObject;
        public GameObject MinimapIconPrefab;

        private bool m_configured;
        private bool m_Opened;

        private GameObject minimapIconInstance;

        private RoomHandler m_room;
        private RoomHandler m_registeredIconRoom;


        public void Interact(PlayerController player) {
            switch (chestType) {
                case ChestType.MusicSwitch:
                    ToggleSwitch();
                    return;
                case ChestType.RickRoll:
                    OpenAsRickRoll(player);
                    return;
                case ChestType.SurpriseChest:
                    OpenAsSurpriseChest(player);
                    return;
            }
            return;
        }

        public void OpenAsSurpriseChest(PlayerController player) {
            if (player) {
                if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }
                m_Opened = true;
                m_room.DeregisterInteractable(this);
                spriteAnimator.Play(openAnimName);
                player.TriggerItemAcquisition();
                if (majorBreakable) { majorBreakable.SpawnItemOnBreak = false; }
                StartCoroutine(DoSurprise());
            }
        }
        
        public void OpenAsRickRoll(PlayerController player) {
            if (player) {
                if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }
                m_Opened = true;
                m_room.DeregisterInteractable(this);
                MajorBreakable component = GetComponent<MajorBreakable>();
                if (component) { component.usesTemporaryZeroHitPointsState = false; }
                AkSoundEngine.PostEvent("play_obj_chest_open_01", gameObject);
                spriteAnimator.Play(openAnimName);
                player.TriggerItemAcquisition();
                if (majorBreakable) {
                    majorBreakable.OnBreak = (Action)Delegate.Remove(majorBreakable.OnBreak, new Action(OnBroken));
                    majorBreakable.SpawnItemOnBreak = false;
                }
                StartCoroutine(DoRickRoll());
            }
        }

        public void ToggleSwitch() {
            m_Opened = true;
            AkSoundEngine.PostEvent("Play_OBJ_plate_press_01", gameObject);
            GameManager.Instance.StartCoroutine(DoToggleSwitch());
            return;
        }

        private void DoConfetti(Vector2 startPosition) {
            // Vector2 vector = (startPosition + new Vector2(-0.75f, -0.25f));
            Vector2 vector = (startPosition + new Vector2(-0.75f, 0.25f));
            for (int i = 0; i < 16; i++) {
                GameObject ConfettiObject = (GameObject)ResourceCache.Acquire(BraveUtility.RandomElement(confettiNames));
                if (ConfettiObject) {
                    WaftingDebrisObject component = Instantiate(ConfettiObject).GetComponent<WaftingDebrisObject>();
                    if (component) {
                        component.sprite.PlaceAtPositionByAnchor(vector.ToVector3ZUp(0f) + new Vector3(0.5f, 0.5f, 0f), tk2dBaseSprite.Anchor.MiddleCenter);
                        Vector2 insideUnitCircle = UnityEngine.Random.insideUnitCircle;
                        insideUnitCircle.y = -Mathf.Abs(insideUnitCircle.y);
                        component.Trigger(insideUnitCircle.ToVector3ZUp(1.5f) * UnityEngine.Random.Range(0.5f, 2f), 0.5f, 0f);
                    }
                }
            }
        }

        private IEnumerator DoSurprise() {
            yield return new WaitForSeconds(0.7f);
            AkSoundEngine.PostEvent("play_obj_chest_open_01", gameObject);
            DoConfetti(sprite.WorldBottomCenter);
            if (UnityEngine.Random.value < 0.3) {
                PickupObject.ItemQuality targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.B : PickupObject.ItemQuality.C) : PickupObject.ItemQuality.B;
                GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                if (item) { LootEngine.SpawnItem(item.gameObject, (sprite.WorldCenter + new Vector2(0, 1)), new Vector2(0, 1), 0f, true, false, false); }
            } else {
                SurpriseChestEnemySpawnPool = SurpriseChestEnemySpawnPool.Shuffle();
                AIActor Enemy = AIActor.Spawn(EnemyDatabase.GetOrLoadByGuid(BraveUtility.RandomElement(SurpriseChestEnemySpawnPool)), sprite.WorldCenter + new Vector2(0, 1), m_room, true, AIActor.AwakenAnimationType.Spawn, true);
                if (Enemy && !Enemy.IgnoreForRoomClear) {
                    PickupObject.ItemQuality targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.C : PickupObject.ItemQuality.D) : PickupObject.ItemQuality.C;
                    GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
                    PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
                    if (item) { Enemy.AdditionalSafeItemDrops.Add(item); }
                }
                yield return null;
                if (Enemy && !Enemy.IgnoreForRoomClear) { m_room.SealRoom(); }
            }
            yield break;
        }


        private IEnumerator DoRickRoll() {
            yield return new WaitForSeconds(0.1f);
            if (!m_room.IsSealed) {
                m_room.npcSealState = RoomHandler.NPCSealState.SealAll;
                m_room.SealRoom();
            }
            GameObject m_RickRollInstance = Instantiate(RickRollAnimationObject, (transform.position + new Vector3(0.1f, 0.5f, 0)), Quaternion.identity);
            Vector3 RickScale = new Vector2(0.2f, 0.2f).ToVector3ZUp(1f);
            m_RickRollInstance.layer = LayerMask.NameToLayer("Unpixelated");
            m_RickRollInstance.transform.localScale = RickScale;
            if (majorBreakable) { majorBreakable.TemporarilyInvulnerable = true; }
            if (sprite) { sprite.HeightOffGround = -2f; sprite.UpdateZDepth(); }
            if (m_room.area.PrototypeRoomCategory != PrototypeDungeonRoom.RoomCategory.SECRET) { GameManager.Instance.StartCoroutine(SpwanEnemyAirDrop()); }
            tk2dSpriteAnimator m_RickRollAnimator = m_RickRollInstance.GetComponent<tk2dSpriteAnimator>();
            m_RickRollAnimator.Play("RickRollAnimation_Rise");
            while (m_RickRollAnimator.IsPlaying("RickRollAnimation_Rise")) { yield return null; }
            m_RickRollAnimator.Play("RickRollAnimation");
            if (!ExpandStats.youtubeSafeMode) {
                AkSoundEngine.PostEvent("Play_EX_RickRollMusic_01", gameObject);
            } else {
                GameManager.Instance.StartCoroutine(DoYouTubeSafeAnnouncement(new Vector3(1.5f, 3.5f)));
            }
            yield return new WaitForSeconds(10);
            Destroy(m_RickRollInstance);
            if (majorBreakable) { majorBreakable.TemporarilyInvulnerable = false; }
            yield return null;
            spriteAnimator.Play(breakAnimName);
            specRigidbody.enabled = false;
            IsBroken = true;
            Transform shadowTransform = transform.Find("Expand_RickRollChestShadow");
            if (shadowTransform != null) { Destroy(shadowTransform.gameObject); }
            if (m_room.npcSealState != RoomHandler.NPCSealState.SealNone) { m_room.npcSealState = RoomHandler.NPCSealState.SealNone; }
            Exploder.DoDefaultExplosion(sprite.WorldCenter, Vector2.zero, null, true, CoreDamageTypes.None, false);
            yield return null;
            PickupObject.ItemQuality targetQuality = (UnityEngine.Random.value >= 0.2f) ? ((!BraveUtility.RandomBool()) ? PickupObject.ItemQuality.B : PickupObject.ItemQuality.C) : PickupObject.ItemQuality.B;
            GenericLootTable lootTable = (!BraveUtility.RandomBool()) ? GameManager.Instance.RewardManager.GunsLootTable : GameManager.Instance.RewardManager.ItemsLootTable;
            PickupObject item = LootEngine.GetItemOfTypeAndQuality<PickupObject>(targetQuality, lootTable, false);
            if (item) { LootEngine.SpawnItem(item.gameObject, sprite.WorldCenter, Vector2.zero, 0f, true, true, false); }
            yield break;
        }

        private IEnumerator DoYouTubeSafeAnnouncement(Vector3 DialogBoxOffset) {
            string[] m_Lyrics = new string[] {
                "Never gonna give you up!",
                "Never gonna let you down!",
                "Never gonna run around and desert you!",
                "Never gonna make you cry!"
            };
            yield return new WaitForSeconds(0.5f);
            TextBoxManager.ShowTextBox(transform.position + DialogBoxOffset, transform, 5f, m_Lyrics[0], "rickrolldialog1", false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
            yield return new WaitForSeconds(2);
            TextBoxManager.ClearTextBox(transform);
            TextBoxManager.ShowTextBox(transform.position + DialogBoxOffset, transform, 5f, m_Lyrics[1], "rickrolldialog2", false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
            yield return new WaitForSeconds(2);
            TextBoxManager.ClearTextBox(transform);
            TextBoxManager.ShowTextBox(transform.position + DialogBoxOffset, transform, 5f, m_Lyrics[2], "rickrolldialog3", false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
            yield return new WaitForSeconds(2);
            TextBoxManager.ClearTextBox(transform);
            TextBoxManager.ShowTextBox(transform.position + DialogBoxOffset, transform, 5f, m_Lyrics[3], "rickrolldialog4", false, TextBoxManager.BoxSlideOrientation.NO_ADJUSTMENT, false, false);
            yield return new WaitForSeconds(2);
            TextBoxManager.ClearTextBox(transform);
            yield break;
        }

        private IEnumerator DoToggleSwitch() {
            if (ExpandStats.youtubeSafeMode) {
                m_room.DeregisterInteractable(this);
                spriteAnimator.Play(switchOnAnimName);
                ExpandStats.youtubeSafeMode = false;
                yield return new WaitForSeconds(2f);
            } else if (!ExpandStats.youtubeSafeMode) {
                spriteAnimator.Play(switchOffAnimName);
                ExpandStats.youtubeSafeMode = true;
                yield return new WaitForSeconds(2f);
            }
            m_room.RegisterInteractable(this);
            m_Opened = false;
            ExportSettings();
            yield break;
        }

        private void ExportSettings() {

            string CachedJSONText = string.Empty;

            ExpandCachedStats cachedStats = ScriptableObject.CreateInstance<ExpandCachedStats>();
            
            CachedJSONText = JsonUtility.ToJson(cachedStats);

            if (File.Exists(Path.Combine(ETGMod.ResourcesDirectory, ExpandTheGungeon.ModSettingsFileName))) {
                File.Delete(Path.Combine(ETGMod.ResourcesDirectory, ExpandTheGungeon.ModSettingsFileName));
            }
            
            ResourceExtractor.SaveStringToFile(CachedJSONText, ETGMod.ResourcesDirectory, ExpandTheGungeon.ModSettingsFileName);

            ETGModConsole.Log("[ExpandTheGungeon] Settings have been saved!");

            return;
        }


        private IEnumerator SpwanEnemyAirDrop(float delay = 0.4f) {
            Vector3 RoomOffset = m_room.area.basePosition.ToVector3();
            string EnemyGUID1 = "88b6b6a93d4b4234a67844ef4728382c"; // bandana_bullet_kin
            string EnemyGUID2 = "4d37ce3d666b4ddda8039929225b7ede"; // grenade_kin
            string EnemyGUID3 = "01972dee89fc4404a5c408d50007dad5"; // bullet_kin
            string EnemyGUID4 = "128db2f0781141bcb505d8f00f9e4d47"; // red_shotgun_kin

            if (UnityEngine.Random.value <= 0.5f) { EnemyGUID1 = ExpandCustomEnemyDatabase.BootlegBulletManBandanaGUID; }
            if (UnityEngine.Random.value <= 0.1f) { EnemyGUID2 = ExpandCustomEnemyDatabase.BootlegShotgunManBlueGUID; }
            if (UnityEngine.Random.value <= 0.5f) { EnemyGUID3 = ExpandCustomEnemyDatabase.BootlegBulletManGUID; }
            if (UnityEngine.Random.value <= 0.5f) { EnemyGUID4 = ExpandCustomEnemyDatabase.BootlegShotgunManRedGUID; }

            yield return new WaitForSeconds(delay);

            GameObject eCrateInstance1 = ExpandUtility.SpawnAirDrop(m_room, (RoomOffset + new Vector3(4, 3, 0)), null, ExpandUtility.GenerateDungeonPlacable(null, true, EnemyGUID: EnemyGUID1));
            GameObject eCrateInstance2 = ExpandUtility.SpawnAirDrop(m_room, (RoomOffset + new Vector3(4, 9, 0)), null, ExpandUtility.GenerateDungeonPlacable(null, true, EnemyGUID: EnemyGUID2), 0.2f);
            GameObject eCrateInstance3 = ExpandUtility.SpawnAirDrop(m_room, (RoomOffset + new Vector3(13, 3, 0)), null, ExpandUtility.GenerateDungeonPlacable(null, true, EnemyGUID: EnemyGUID3));
            GameObject eCrateInstance4 = ExpandUtility.SpawnAirDrop(m_room, (RoomOffset + new Vector3(13, 9, 0)), null, ExpandUtility.GenerateDungeonPlacable(null, true, EnemyGUID: EnemyGUID4), 0.2f);
            
            yield break;
        }


        private void Awake() {
            SpriteOutlineManager.AddOutlineToSprite(sprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            if (chestType == ChestType.RickRoll | chestType == ChestType.SurpriseChest) {
                if (majorBreakable && majorBreakable.DamageReduction > 1000f) { majorBreakable.ReportZeroDamage = true; }
                // majorBreakable.InvulnerableToEnemyBullets = true;
            }
        }

        // public void Start() { }
        // private void Update() { }

        public void ConfigureOnPlacement(RoomHandler room) {
            m_room = room;
            
            switch (chestType) {
                case ChestType.MusicSwitch:
                    if (ExpandStats.youtubeSafeMode) {
                        sprite.SetSprite("music_switch_idle_off_001");
                    } else {
                        sprite.SetSprite("music_switch_idle_on_001");
                    }
                    m_configured = true;
                    return;
                case ChestType.RickRoll:
                    if (UnityEngine.Random.value <= 0.01f) {
                        Vector3 SpawnPosition = (transform.position + new Vector3(0.5f, 0, 0));
                        GameObject RealRainbowChestObject = Instantiate(GameManager.Instance.RewardManager.Rainbow_Chest.gameObject, SpawnPosition, Quaternion.identity);
                        if (RealRainbowChestObject && RealRainbowChestObject.GetComponent<Chest>()) {
                            Chest RealRainbowChest = RealRainbowChestObject.GetComponent<Chest>();                        
                            RealRainbowChest.ConfigureOnPlacement(m_room);
                            m_room.RegisterInteractable(RealRainbowChest);
                            ExpandRickRollSpawnNote NoteSpawner = RealRainbowChest.gameObject.AddComponent<ExpandRickRollSpawnNote>();
                            NoteSpawner.ParentRoom = m_room;
                            NoteSpawner.CachedSpawnLocation = (transform.position - new Vector3(0, 1.5f, 0));
                            NoteSpawner.CachedSpawnLocation += new Vector3(1, 0, 0);
                            NoteSpawner.Configured = true;
                            Destroy(gameObject);
                            return;
                        }
                    }
                    Initialize();
                    if (!m_configured) { RegisterFakeChestOnMinimap(room); }
                    m_configured = true;
                    return;
                case ChestType.SurpriseChest:
                    Initialize();
                    if (!m_configured) { RegisterFakeChestOnMinimap(room); }
                    m_configured = true;
                    return;
            }
        }
        
        protected void Initialize() {
            specRigidbody.Initialize();
            specRigidbody.PreventPiercing = true;
            MajorBreakable component = gameObject.GetComponent<MajorBreakable>();
            switch (chestType) {
                case ChestType.MusicSwitch:
                    return;
                case ChestType.RickRoll:
                    if (component) {
                        MajorBreakable majorBreakable = component;
                        majorBreakable.OnBreak = (Action)Delegate.Combine(majorBreakable.OnBreak, new Action(OnBroken));
                    }
                    IntVector2 intVector = specRigidbody.UnitBottomLeft.ToIntVector2(VectorConversions.Floor);
                    IntVector2 intVector2 = specRigidbody.UnitTopRight.ToIntVector2(VectorConversions.Floor);
                    for (int i = intVector.x; i <= intVector2.x; i++) {
                        for (int j = intVector.y; j <= intVector2.y; j++) {
                            GameManager.Instance.Dungeon.data[new IntVector2(i, j)].isOccupied = true;
                        }
                    }
                    sprite.usesOverrideMaterial = true;
                    sprite.renderer.material.shader = ShaderCache.Acquire("Brave/Internal/RainbowChestShader");
                    return;
                case ChestType.SurpriseChest:
                    if (component) {
                        MajorBreakable majorBreakable = component;
                        majorBreakable.OnBreak = (Action)Delegate.Combine(majorBreakable.OnBreak, new Action(OnBroken));
                    }
                    IntVector2 intVector3 = specRigidbody.UnitBottomLeft.ToIntVector2(VectorConversions.Floor);
                    IntVector2 intVector4 = specRigidbody.UnitTopRight.ToIntVector2(VectorConversions.Floor);
                    for (int i = intVector3.x; i <= intVector4.x; i++) {
                        for (int j = intVector3.y; j <= intVector4.y; j++) {
                            GameManager.Instance.Dungeon.data[new IntVector2(i, j)].isOccupied = true;
                        }
                    }
                    spriteAnimator.Play("coop_chest_knock");
                    return;
            }
        }
        
        public void RegisterFakeChestOnMinimap(RoomHandler r) {
            m_registeredIconRoom = r;
            GameObject iconPrefab = MinimapIconPrefab ?? (BraveResources.Load("Global Prefabs/Minimap_Treasure_Icon", ".prefab") as GameObject);
            minimapIconInstance = Minimap.Instance.RegisterRoomIcon(r, iconPrefab, false);
        }

        public void DeregisterChestOnMinimap() {
            if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }
        }

        private void OnBroken() {
            spriteAnimator.Play(breakAnimName);
            specRigidbody.enabled = false;
            IsBroken = true;
            string ShadowObjectName = "Expand_RickRollChestShadow";
            if (chestType == ChestType.SurpriseChest) { ShadowObjectName = "Expand_SurpriseChestShadow"; }
            Transform shadowTransform = transform.Find(ShadowObjectName);
            if (shadowTransform != null) { Destroy(shadowTransform.gameObject); }
            if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }
            if (!m_Opened) {
                m_room.DeregisterInteractable(this);
                if (m_registeredIconRoom != null) { Minimap.Instance.DeregisterRoomIcon(m_registeredIconRoom, minimapIconInstance); }
                bool GoldJunkFlag = GameStatsManager.Instance.GetFlag(GungeonFlags.ITEMSPECIFIC_GOLD_JUNK);
                float ChestDowngradeChance = GameManager.Instance.RewardManager.ChestDowngradeChance;
                float ChestHalfHeartChance = GameManager.Instance.RewardManager.ChestHalfHeartChance;
                float ChestExplosionChance = GameManager.Instance.RewardManager.ChestExplosionChance;
                float ChestJunkChance = GameManager.Instance.RewardManager.ChestJunkChance;
                float Multiplier = (!GoldJunkFlag) ? 0f : 0.005f;
                bool flag3 = GameStatsManager.Instance.GetFlag(GungeonFlags.ITEMSPECIFIC_SER_JUNKAN_UNLOCKED);
                float ChestJunkanChance = (!flag3 || Chest.HasDroppedSerJunkanThisSession) ? 0f : GameManager.Instance.RewardManager.ChestJunkanUnlockedChance;
                if (GameManager.Instance.PrimaryPlayer && GameManager.Instance.PrimaryPlayer.carriedConsumables.KeyBullets > 0) {
                    ChestJunkChance *= GameManager.Instance.RewardManager.HasKeyJunkMultiplier;
                }
                if (SackKnightController.HasJunkan()) {
                    ChestJunkChance *= GameManager.Instance.RewardManager.HasJunkanJunkMultiplier;
                    Multiplier *= 3f;
                }
                ChestJunkChance -= Multiplier;
                float AllChances = Multiplier + ChestDowngradeChance + ChestHalfHeartChance + ChestExplosionChance + ChestJunkChance + ChestJunkanChance;
                float ChestExplosionChanceMultiplier = (UnityEngine.Random.value * AllChances);
                if (ChestExplosionChanceMultiplier > ChestDowngradeChance + ChestHalfHeartChance + ChestJunkChance + ChestJunkanChance) {
                    Exploder.DoDefaultExplosion(sprite.WorldCenter, Vector2.zero, null, false, CoreDamageTypes.None, false);
                }
            }
        }

        public void OnEnteredRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.AddOutlineToSprite(sprite, Color.white, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public void OnExitRange(PlayerController interactor) {
            if (!this) { return; }
            SpriteOutlineManager.RemoveOutlineFromSprite(sprite, false);
            SpriteOutlineManager.AddOutlineToSprite(sprite, BaseOutlineColor, 0.1f, 0f, SpriteOutlineManager.OutlineType.NORMAL);
            sprite.UpdateZDepth();
        }

        public float GetDistanceToPoint(Vector2 point) {
            Bounds bounds = sprite.GetBounds();
            bounds.SetMinMax(bounds.min + transform.position, bounds.max + transform.position);
            float num = Mathf.Max(Mathf.Min(point.x, bounds.max.x), bounds.min.x);
            float num2 = Mathf.Max(Mathf.Min(point.y, bounds.max.y), bounds.min.y);
            return Mathf.Sqrt((point.x - num) * (point.x - num) + (point.y - num2) * (point.y - num2));
        }

        public float GetOverrideMaxDistance() { return -1f; }
        
        public string GetAnimationState(PlayerController interactor, out bool shouldBeFlipped) {
            shouldBeFlipped = false;
            return string.Empty;
        }
        
        protected override void OnDestroy() {
            base.OnDestroy();
            switch (chestType) {
                case ChestType.MusicSwitch:
                    break;
                case ChestType.RickRoll:
                    AkSoundEngine.PostEvent("Stop_SND_OBJ", gameObject);
                    AkSoundEngine.PostEvent("Stop_EX_RickRollMusic_01", gameObject);
                    break;
                case ChestType.SurpriseChest:
                    break;
            }
        }
    }

    public class ExpandRickRollSpawnNote : BraveBehaviour {

        public ExpandRickRollSpawnNote() { Configured = false; }
        
        public bool Configured;
        public RoomHandler ParentRoom;
        public Vector3 CachedSpawnLocation;

        private void Start() { }
        private void Update() {
            if (Configured) {
                MajorBreakable m_Breakable = majorBreakable;
                if (m_Breakable) { m_Breakable.OnBreak += OnBrokenInSadness; }
                Configured = false;
            }
        }

        private void OnBrokenInSadness() {
            Chest chest = gameObject.GetComponent<Chest>();
            bool chestWasOpened = false;
            if (chest) { chestWasOpened = chest.pickedUp; }
            if (!chestWasOpened) {
                string CustomText = "{wb}Never gonna give you up!{w}\r\nYou did give up a real rainbow chest this time though.\r\n{wb}-Apache Thunder{w}";
                ExpandUtility.SpawnCustomBowlerNote(GameManager.Instance.RewardManager.BowlerNoteOtherSource, CachedSpawnLocation, ParentRoom, CustomText, true);
            }
            return;
        }
        
        protected override void OnDestroy() { base.OnDestroy(); }

    }
}

