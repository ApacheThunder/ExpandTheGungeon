using Dungeonator;
using System.Collections;
using UnityEngine;
using ExpandTheGungeon.ExpandObjects;

namespace ExpandTheGungeon.ExpandComponents {

    public class ChaosWeatherPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public bool useCustomIntensity;
        public float RainIntensity;

        private RoomHandler ParentRoom;
        private GameObject m_ChaosLightning;
        private GameObject m_ThunderStorm;        
        private bool m_StormIsActive;
        private bool m_StormInitialized;
        private float m_cachedEmissionRate;
        private PlayerController CurrentPlayer;

        ChaosWeatherPlacable() {
            m_StormIsActive = false;
            m_StormInitialized = false;
            RainIntensity = 600f;
            useCustomIntensity = false;            
        }

        public void Start() { }

        public void Update() {            
            if (ParentRoom != null && gameObject.transform.position != null) {
                if (gameObject.transform.position != Vector3.zero) {
                    if (CurrentPlayer.CurrentRoom == ParentRoom && !m_StormIsActive) {
                        if (!m_StormInitialized) {
                            m_StormInitialized = true;
                            InitStorm();
                        } else {
                            StartCoroutine(ToggleRainFx(true));
                        }
                    } else if (CurrentPlayer.CurrentRoom != ParentRoom && m_StormIsActive) {
                        StartCoroutine(ToggleRainFx(false));
                    }
                }
            }                      
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            CurrentPlayer = GameManager.Instance.PrimaryPlayer;
            if (room != null) { ParentRoom = room; }
        }

        private IEnumerator ToggleRainFx(bool Active) {
            if (m_ChaosLightning == null | m_ThunderStorm == null) { yield break; }
            if (!Active) {
                m_StormIsActive = false;
                ExpandWeatherController CurrentWeather = m_ChaosLightning.GetComponent<ExpandWeatherController>();
                CurrentWeather.isActive = false;
                ThunderstormController m_ThunderStormComponent = m_ThunderStorm.GetComponent<ThunderstormController>();
                AkSoundEngine.PostEvent("Stop_ENV_rain_loop_01", m_ThunderStormComponent.gameObject);
                ParticleSystem rainParticles = m_ThunderStormComponent.RainSystemTransform.GetComponent<ParticleSystem>();
                BraveUtility.SetEmissionRate(rainParticles, 0f);
                yield return new WaitForSeconds(2f);
                m_ThunderStormComponent.enabled = false;
                m_ThunderStorm.SetActive(false);
                yield return null;
                yield break;
            } else {                
                m_ThunderStorm.SetActive(true);
                yield return null;
                ThunderstormController m_ThunderStormComponent = m_ThunderStorm.GetComponent<ThunderstormController>();                
                m_ThunderStormComponent.enabled = true;
                ParticleSystem rainParticles = m_ThunderStormComponent.RainSystemTransform.GetComponent<ParticleSystem>();
                BraveUtility.SetEmissionRate(rainParticles, m_cachedEmissionRate);
                ExpandWeatherController CurrentWeather = m_ChaosLightning.GetComponent<ExpandWeatherController>();
                CurrentWeather.isActive = true;
                m_StormIsActive = true;
                yield break;
            }
        }

        private void InitStorm() {            
            Dungeon m_dungeonPrefab = DungeonDatabase.GetOrLoadByName("finalscenario_guide");
            DungeonFlow dungeonFlowPrefab = m_dungeonPrefab.PatternSettings.flows[0];
            PrototypeDungeonRoom GuidePastRoom = dungeonFlowPrefab.AllNodes[0].overrideExactRoom;
            GameObject GuidePastRoomObject = GuidePastRoom.placedObjects[0].nonenemyBehaviour.gameObject;
            GameObject m_RainPrefab = GuidePastRoomObject.transform.Find("Rain").gameObject;
            m_ThunderStorm = Instantiate(m_RainPrefab);
            m_ThunderStorm.name = "ChaosRain";
            ThunderstormController stormController = m_ThunderStorm.GetComponent<ThunderstormController>();
            ParticleSystem m_CachedParticleSystem = stormController.RainSystemTransform.GetComponent<ParticleSystem>();
            if (useCustomIntensity) { BraveUtility.SetEmissionRate(m_CachedParticleSystem, RainIntensity); }
            m_cachedEmissionRate = m_CachedParticleSystem.emission.rate.constant;
            stormController.DecayVertical = false;
            stormController.DoLighting = false;
            m_ChaosLightning = new GameObject("ChaosLightning");
            m_ChaosLightning.AddComponent<ExpandWeatherController>();
            ExpandWeatherController LightningComponent = m_ChaosLightning.GetComponent<ExpandWeatherController>();
            LightningComponent.LightningRenderers = stormController.LightningRenderers;
            dungeonFlowPrefab = null;
            m_dungeonPrefab = null;           
        }

        public void DestroyStorm() {
            if (m_ThunderStorm != null) { Destroy(m_ThunderStorm); }
            if (m_ChaosLightning != null) { Destroy(m_ChaosLightning); }            
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }

    public class ExpandWeatherController : MonoBehaviour {

        public static void AddRainStormToFloor(string dungeon, float RainIntensity = 1f, bool useCustomIntensity = false) {
            if (string.IsNullOrEmpty(dungeon)) { return; }
            GameObject dungeonObject = GameObject.Find(dungeon);            
            if (dungeonObject == null) { dungeonObject = GameObject.Find(dungeon + "(Clone)"); }
            if (dungeonObject == null) { return; }
            Dungeon m_dungeonPrefab = DungeonDatabase.GetOrLoadByName("finalscenario_guide");
            DungeonFlow dungeonFlowPrefab = m_dungeonPrefab.PatternSettings.flows[0];
            PrototypeDungeonRoom GuidePastRoom = dungeonFlowPrefab.AllNodes[0].overrideExactRoom;
            GameObject GuidePastRoomObject = GuidePastRoom.placedObjects[0].nonenemyBehaviour.gameObject;
            GameObject m_RainPrefab = GuidePastRoomObject.transform.Find("Rain").gameObject;
            GameObject m_ThunderStorm = Instantiate(m_RainPrefab);
            m_ThunderStorm.name = "ChaosRain";
            ThunderstormController stormController = m_ThunderStorm.GetComponent<ThunderstormController>();
            ParticleSystem m_CachedParticleSystem = stormController.RainSystemTransform.GetComponent<ParticleSystem>();
            if (useCustomIntensity) { BraveUtility.SetEmissionRate(m_CachedParticleSystem, RainIntensity); }
            stormController.DecayVertical = false;
            stormController.DoLighting = false;
            GameObject m_ChaosLightning = new GameObject("ChaosLightning");
            m_ChaosLightning.name = "ChaosLightning";
            m_ChaosLightning.AddComponent<ExpandWeatherController>();
            ExpandWeatherController LightningComponent = m_ChaosLightning.GetComponent<ExpandWeatherController>();
            LightningComponent.LightningRenderers = stormController.LightningRenderers;
            dungeonFlowPrefab = null;
            m_dungeonPrefab = null;
            return;
        }

        public static void AddRainStormToRoom(RoomHandler targetRoom, IntVector2 targetLocation, float RainIntensity = 1f, bool useCustomIntensity = false) {
            if (targetRoom == null | targetLocation == null) { return; }
            GameObject m_RainObject = new GameObject("RainFXObject");
            m_RainObject.AddComponent<ChaosWeatherPlacable>();
            GameObject m_Rain = DungeonPlaceableUtility.InstantiateDungeonPlaceable(m_RainObject, targetRoom, targetLocation, true);
            ChaosWeatherPlacable m_RainComponent = m_Rain.GetComponent<ChaosWeatherPlacable>();
            if (useCustomIntensity) {
                m_RainComponent.useCustomIntensity = true;
                m_RainComponent.RainIntensity = RainIntensity;
            }
            m_RainComponent.ConfigureOnPlacement(targetRoom);
            return;
        }

        public ScreenShakeSettings ThunderShake = new ScreenShakeSettings() {
            magnitude = 0.2f,
            speed = 3,
            time = 0,
            falloff = 0.5f,
            direction = new Vector2(1, 0),
            vibrationType = ScreenShakeSettings.VibrationType.Auto,
            simpleVibrationTime = Vibration.Time.Normal,
            simpleVibrationStrength = Vibration.Strength.Medium
        };

        public bool isActive;
        public float MinTimeBetweenLightningStrikes = 5f;
        public float MaxTimeBetweenLightningStrikes = 10f;
        public float AmbientBoost = 1.5f;
        
        public Renderer[] LightningRenderers;

        // private Vector3 m_lastCameraPosition;
        // private Transform m_mainCameraTransform;        
        private float m_lightningTimer;        

        private void Start() {
            isActive = true;
            m_lightningTimer = UnityEngine.Random.Range(MinTimeBetweenLightningStrikes, MaxTimeBetweenLightningStrikes);
        }

        private void Update() {
    	    if (GameManager.Instance.IsLoadingLevel) { return; }
            m_lightningTimer -= ((!GameManager.IsBossIntro) ? BraveTime.DeltaTime : GameManager.INVARIANT_DELTA_TIME);
            if (m_lightningTimer <= 0f && isActive) {
                StartCoroutine(DoLightningStrike());
                if (LightningRenderers != null) {
                    for (int i = 0; i < LightningRenderers.Length; i++) { StartCoroutine(ProcessLightningRenderer(LightningRenderers[i])); }
                }
                StartCoroutine(HandleLightningAmbientBoost());
                m_lightningTimer = UnityEngine.Random.Range(MinTimeBetweenLightningStrikes, MaxTimeBetweenLightningStrikes);
            }
        }
    
    	protected IEnumerator HandleLightningAmbientBoost() {
            Color cachedAmbient = RenderSettings.ambientLight;
            Color modAmbient = new Color(cachedAmbient.r + AmbientBoost, cachedAmbient.g + AmbientBoost, cachedAmbient.b + AmbientBoost);
    		GameManager.Instance.Dungeon.OverrideAmbientLight = true;
    		for (int i = 0; i < 2; i++) {
    			float elapsed = 0f;
    			float duration = 0.15f * (i + 1);
    			while (elapsed < duration) {
    				elapsed += GameManager.INVARIANT_DELTA_TIME;
    				float t = elapsed / duration;
    				GameManager.Instance.Dungeon.OverrideAmbientColor = Color.Lerp(modAmbient, cachedAmbient, t);
    				yield return null;
    			}
    		}
    		GameManager.Instance.Dungeon.OverrideAmbientLight = false;
    		yield break;
    	}

        protected IEnumerator ProcessLightningRenderer(Renderer target) {
            target.enabled = true;
            yield return StartCoroutine(InvariantWait(0.05f));
            target.enabled = false;
            yield return StartCoroutine(InvariantWait(0.1f));
            target.enabled = true;
            yield return StartCoroutine(InvariantWait(0.1f));
            target.enabled = false;
            yield break;
        }

        protected IEnumerator InvariantWait(float duration) {
		    float elapsed = 0f;
		    while (elapsed < duration) {
			elapsed += GameManager.INVARIANT_DELTA_TIME;
			    yield return null;
		    }
		    yield break;
	    }

        protected IEnumerator DoLightningStrike() {
            AkSoundEngine.PostEvent("Play_ENV_thunder_flash_01", GameManager.Instance.PrimaryPlayer.gameObject);
            PlatformInterface.SetAlienFXColor(new Color(1f, 1f, 1f, 1f), 0.25f);
            yield return new WaitForSeconds(0.25f);
            GameManager.Instance.MainCameraController.DoScreenShake(ThunderShake, null, false);
    		yield break;
    	}
    }

    public class ExpandRatFloorRainController : BraveBehaviour {
        
        private ThunderstormController m_StormController;
        private ParticleSystem m_CachedParticleSystem;
        private ExpandWeatherController m_LightningController;

        private void Start() {            
            m_StormController = GameObject.Find("ChaosRain").GetComponent<ThunderstormController>();
            m_LightningController = GameObject.Find("ChaosLightning").GetComponent<ExpandWeatherController>();

            if (m_StormController != null) { m_CachedParticleSystem = m_StormController.RainSystemTransform.GetComponent<ParticleSystem>(); }
        }

        private void Update() {

            if (Dungeon.IsGenerating | GameManager.Instance.IsLoadingLevel) {
                return;
            } else if (GameManager.Instance.Dungeon.tileIndices.tilesetId == GlobalDungeonData.ValidTilesets.PHOBOSGEON) {
                PlayerController player = GameManager.Instance.BestActivePlayer;
                if (player) { CheckForWeatherFX(player, 500); }
            }
        }

        private void CheckForWeatherFX(PlayerController player, float RainIntensity) {
            try {
                GameManager.Instance.StartCoroutine(ToggleRainFX(player, RainIntensity));
            } catch (System.Exception ex) {
                if (ExpandStats.debugMode) {
                    Debug.Log("[WARNING] Exception caught while checking room for WeatherFX toggle!...");
                    Debug.LogException(ex);                    
                    ETGModConsole.Log("[WARNING] Exception caught while checking room for WeatherFX toggle!...");
                    ETGModConsole.Log("Exception has been logged.");
                }
                return;
            }
        }

        private IEnumerator ToggleRainFX(PlayerController player, float cachedRate) {
            
            if (m_StormController == null | m_LightningController == null) { yield break; }
            
            bool Active = true;
            if (ExpandLists.InvalidRatFloorRainRooms.Contains(player.CurrentRoom.GetRoomName()) | 
                player.CurrentRoom.IsShop | 
                player.CurrentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS)
            {
                Active = false;
            }
            yield return null;
            if (!Active) {
                if (!m_StormController.enabled && !m_LightningController.isActive) { yield break; }
                AkSoundEngine.PostEvent("Stop_ENV_rain_loop_01", m_StormController.gameObject);
                BraveUtility.SetEmissionRate(m_CachedParticleSystem, 0f);
                yield return new WaitForSeconds(2f);
                m_StormController.enabled = false;
                m_LightningController.isActive = false;
                yield return null;
                yield break;
            } else {
                if (m_StormController.enabled && m_LightningController.isActive) { yield break; }
                BraveUtility.SetEmissionRate(m_CachedParticleSystem, cachedRate);
                m_StormController.enabled = true;
                m_LightningController.isActive = true;
                yield break;
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

