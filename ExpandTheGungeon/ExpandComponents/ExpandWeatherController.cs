using Dungeonator;
using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandThunderStormPlacable : DungeonPlaceableBehaviour, IPlaceConfigurable {
        
        ExpandThunderStormPlacable() { 
            RainIntensity = 100;
            useCustomIntensity = true;
            enableLightning = false;
            isSecretFloor = true;
        }

        public bool useCustomIntensity;
        public float RainIntensity;
        public bool enableLightning;
        public bool isSecretFloor;

        public void Start() { }

        public void Update() { }

        public void ConfigureOnPlacement(RoomHandler room) {
            Dungeon dungeon = GameManager.Instance.Dungeon;
            ExpandWeatherController weatherController = dungeon.gameObject.AddComponent<ExpandWeatherController>();
            weatherController.RainIntensity = RainIntensity;
            weatherController.useCustomIntensity = useCustomIntensity;
            weatherController.enableLightning = enableLightning;
            weatherController.isSecretFloor = isSecretFloor;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }

    public class ExpandWeatherController : BraveBehaviour {

        public bool isActive;
        public bool isSecretFloor;
        public float MinTimeBetweenLightningStrikes;
        public float MaxTimeBetweenLightningStrikes;
        public float AmbientBoost;
        public float RainIntensity;
        public bool useCustomIntensity;
        public bool enableLightning;
        public bool isLocalToRoom;

        public ScreenShakeSettings ThunderShake;

        public Renderer[] LightningRenderers;

        private ThunderstormController m_StormController;
        
        private float m_lightningTimer;

        public ExpandWeatherController() {

            isActive = false;
            isSecretFloor = true;
            MinTimeBetweenLightningStrikes = 5;
            MaxTimeBetweenLightningStrikes = 10;
            AmbientBoost = 1;
            RainIntensity = 250;
            useCustomIntensity = true;
            enableLightning = false;
            isLocalToRoom = false;

            ThunderShake = new ScreenShakeSettings() {
                magnitude = 0.2f,
                speed = 3,
                time = 0,
                falloff = 0.5f,
                direction = new Vector2(1, 0),
                vibrationType = ScreenShakeSettings.VibrationType.Auto,
                simpleVibrationTime = Vibration.Time.Normal,
                simpleVibrationStrength = Vibration.Strength.Medium
            };

            m_lightningTimer = Random.Range(MinTimeBetweenLightningStrikes, MaxTimeBetweenLightningStrikes);
        }
        
        private void Start() {
            Dungeon m_dungeonPrefab = DungeonDatabase.GetOrLoadByName("finalscenario_guide");
            DungeonFlow dungeonFlowPrefab = m_dungeonPrefab.PatternSettings.flows[0];
            PrototypeDungeonRoom GuidePastRoom = dungeonFlowPrefab.AllNodes[0].overrideExactRoom;
            GameObject GuidePastRoomObject = GuidePastRoom.placedObjects[0].nonenemyBehaviour.gameObject;
            GameObject m_RainPrefab = GuidePastRoomObject.transform.Find("Rain").gameObject;

            GameObject m_ThunderStorm = Instantiate(m_RainPrefab);
            m_ThunderStorm.name = "ExpandRain";
            m_StormController = m_ThunderStorm.GetComponent<ThunderstormController>();
            ParticleSystem m_CachedParticleSystem = m_StormController.RainSystemTransform.GetComponent<ParticleSystem>();
            if (useCustomIntensity) { BraveUtility.SetEmissionRate(m_CachedParticleSystem, RainIntensity); }
            m_StormController.DecayVertical = isLocalToRoom;
            m_StormController.DoLighting = false;
            LightningRenderers = m_StormController.LightningRenderers;
            m_ThunderStorm.transform.parent = gameObject.transform;

            dungeonFlowPrefab = null;
            m_dungeonPrefab = null;

            isActive = true;
        }

        private void Update() {
            
            if (GameManager.Instance.IsLoadingLevel) { return; }

            if (isSecretFloor) {
                PlayerController player = GameManager.Instance.PrimaryPlayer;
                if (player) { CheckForWeatherFX(player, RainIntensity); }
            }

            if (!isActive) { return; }

            if (enableLightning) {
                m_lightningTimer -= ((!GameManager.IsBossIntro) ? BraveTime.DeltaTime : GameManager.INVARIANT_DELTA_TIME);

                if (m_lightningTimer <= 0 && isActive) {
                    StartCoroutine(DoLightningStrike());
                    if (LightningRenderers != null) {
                        for (int i = 0; i < LightningRenderers.Length; i++) { StartCoroutine(ProcessLightningRenderer(LightningRenderers[i])); }
                    }
                    StartCoroutine(HandleLightningAmbientBoost());
                    m_lightningTimer = Random.Range(MinTimeBetweenLightningStrikes, MaxTimeBetweenLightningStrikes);
                }
            }
            
        }
        
        private void CheckForWeatherFX(PlayerController player, float RainIntensity) {
            try {
                GameManager.Instance.StartCoroutine(ToggleRainFX(player, RainIntensity));
            } catch (System.Exception ex) {
                if (ExpandSettings.debugMode) {
                    Debug.Log("[WARNING] Exception caught while checking room for WeatherFX toggle!...");
                    Debug.LogException(ex);                    
                    ETGModConsole.Log("[WARNING] Exception caught while checking room for WeatherFX toggle!...");
                    ETGModConsole.Log("Exception has been logged.");
                }
                return;
            }
        }

        private IEnumerator ToggleRainFX(PlayerController player, float cachedRate) {
            
            if (!m_StormController) { yield break; }
            
            bool Active = true;
            /*if (ExpandLists.InvalidRatFloorRainRooms.Contains(player.CurrentRoom.GetRoomName()) | 
                player.CurrentRoom.IsShop | 
                player.CurrentRoom.area.PrototypeRoomCategory == PrototypeDungeonRoom.RoomCategory.BOSS)
            {
                Active = false;
            }*/
            yield return null;
            if (!Active) {
                if (!m_StormController.enabled && !isActive) { yield break; }
                AkSoundEngine.PostEvent("Stop_ENV_rain_loop_01", m_StormController.gameObject);
                BraveUtility.SetEmissionRate(m_StormController.RainSystemTransform.GetComponent<ParticleSystem>(), 0f);
                yield return new WaitForSeconds(2f);
                m_StormController.enabled = false;
                isActive = false;
                yield return null;
                yield break;
            } else {
                if (m_StormController.enabled && isActive) { yield break; }
                BraveUtility.SetEmissionRate(m_StormController.RainSystemTransform.GetComponent<ParticleSystem>(), cachedRate);
                m_StormController.enabled = true;
                isActive = true;
                yield break;
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
            yield return null;
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

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

