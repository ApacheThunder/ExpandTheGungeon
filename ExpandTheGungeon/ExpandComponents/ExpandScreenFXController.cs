using Dungeonator;
using UnityEngine;
using UnityEngine.Video;

namespace ExpandTheGungeon.ExpandComponents {

    [RequireComponent(typeof(VideoPlayer))]

    public class ExpandScreenFXController : BraveBehaviour {

        public ExpandScreenFXController() {
            shaderType = ShaderType.VHSBasic;
            isRoomSpecific = false;
            ParentRoomIsSecretGlitchRoom = false;
            UseCorruptionAmbience = true;
            enableVHSScanlineDistortion = false;
            enableVHSColorBleed = false;

            // For Glitch Shader
            GlitchUpdatedExternally = true;
            GlitchIntensityIsRandom = false;
            GlitchUpdateFrequencyIsRandom = false;
            GlitchAmount = 0.5f;
            GlitchRandom = 0.1f;
            GlitchUpdateFrequency = 0.05f;
            GlitchMapTexture = "EX_GlitchMap";

            // Scanline Shader
            ScanlineThickness = 2;
            ScanlineIntensity = 0.4f;

            // CRT Shader
            bend = 3.5f;
            scanlineSize1 = 6;
            scanlineSpeed1 = -5;
            scanlineSize2 = 3f;
            scanlineSpeed2 = -2;
            scanlineAmount = 0.0095f;
            vignetteSize = 1.5f;
            vignetteSmoothness = 0.6f;
            vignetteEdgeRound = 8f;
            noiseSize = 200f;
            noiseAmount = 0.045f;

            redOffset = new Vector2(0, -0.002f);
            blueOffset = Vector2.zero;
            greenOffset = new Vector2(0, 0.002f);
            


            m_yScanline = 0;
            m_xScanline = 0;
            m_xShift = 0; // 0.025f;
            m_xShiftIntensity = 500;
            m_colorBleedToggle = 0;
            
            m_SetupComplete = false;
            m_MaterialRegistered = false;
        }
        
        // This only used for VHS Shader currently
        public VideoClip VHSClip;
        public VideoClip OldFilmClip;
        public Texture2D VHSScreenTexture;
        [System.NonSerialized]
        public VideoPlayer TexturePlayer;


        public bool enableVHSScanlineDistortion;
        public bool enableVHSColorBleed;
        public bool isRoomSpecific;
        public bool ParentRoomIsSecretGlitchRoom;
        public bool UseCorruptionAmbience;
        // For Glitch Shader
        public bool GlitchUpdatedExternally;
        public bool GlitchIntensityIsRandom;
        public bool GlitchUpdateFrequencyIsRandom;
        public float GlitchAmount;
        public float GlitchRandom;
        public float GlitchUpdateFrequency;
        public string GlitchMapTexture;
        public int ScanlineThickness;
        public float ScanlineIntensity;

        // CRT Shader Properties
        public float bend;
        public float scanlineSize1;
        public float scanlineSpeed1;
        public float scanlineSize2;
        public float scanlineSpeed2;
        public float scanlineAmount;
        public float vignetteSize;
        public float vignetteSmoothness;
        public float vignetteEdgeRound;
        public float noiseSize;
        public float noiseAmount;
        // CRT Shader Chromatic aberration amounts
        public Vector2 redOffset;
        public Vector2 blueOffset;
        public Vector2 greenOffset;

        [System.NonSerialized]
        public Material ScreenMaterial;
        [System.NonSerialized]
        public RoomHandler ParentRoom;

        public ShaderType shaderType;

        public enum ShaderType { VHS, VHSOldFilm, VHSBasic, Glitch, Scanlines, CRT }

        private bool m_SetupComplete;
        private bool m_MaterialRegistered;

        // For VHS Shader only
        private float m_yScanline;
        private float m_xScanline;
        private float m_xShift;
        private float m_xShiftIntensity;
        private float m_colorBleedToggle;

        // "Brave/Effects/Scanlines" // Useful shader reference I might use later.

        private void Start() {
            switch (shaderType) {
                case ShaderType.VHS:
                    // This shader doesn't appear to work on Linux for some reason.
                    if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { return; }
                    ScreenMaterial = new Material(ExpandAssets.LoadAsset<Shader>("ExpandVHSPostProcessEffect"));
                    TexturePlayer = GetComponent<VideoPlayer>();
                    m_colorBleedToggle = 0;
                    if (enableVHSColorBleed) { m_colorBleedToggle = 1; }
                    ScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
                    if (!enableVHSScanlineDistortion) { ScreenMaterial.SetFloat("_enableScanlineDistortion", 0); }
                    TexturePlayer.Play();
                    TexturePlayer.isLooping = true;
                    TexturePlayer.renderMode = VideoRenderMode.APIOnly;
                    TexturePlayer.clip = VHSClip;
                    break;
                case ShaderType.VHSOldFilm:
                    if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { return; }
                    ScreenMaterial = new Material(ExpandAssets.LoadAsset<Shader>("ExpandVHSPostProcessEffect"));
                    TexturePlayer = GetComponent<VideoPlayer>();
                    m_colorBleedToggle = 0;
                    if (enableVHSColorBleed) { m_colorBleedToggle = 1; }
                    ScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
                    if (!enableVHSScanlineDistortion) { ScreenMaterial.SetFloat("_enableScanlineDistortion", 0); }
                    TexturePlayer.Play();
                    TexturePlayer.isLooping = true;
                    TexturePlayer.renderMode = VideoRenderMode.APIOnly;
                    TexturePlayer.clip = OldFilmClip;
                    Pixelator.Instance.SetSaturationColorPower(Color.white, 1);
                    break;
                case ShaderType.VHSBasic:
                    if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { return; }
                    ScreenMaterial = new Material(ExpandAssets.LoadAsset<Shader>("ExpandVHSPostProcessEffect"));
                    if (!VHSScreenTexture) { VHSScreenTexture = ExpandAssets.LoadAsset<Texture2D>("EmptyVHSTexture"); }
                    ScreenMaterial.SetTexture("_VHSTex", VHSScreenTexture);
                    if (!enableVHSScanlineDistortion) { ScreenMaterial.SetFloat("_enableScanlineDistortion", 0); }
                    m_colorBleedToggle = 0;
                    if (enableVHSColorBleed) { m_colorBleedToggle = 1; }
                    break;
                case ShaderType.Glitch:
                    ScreenMaterial = new Material(ExpandAssets.LoadAsset<Shader>("ExpandGlitchScreen"));
                    ScreenMaterial.SetTexture("_GlitchMap", ExpandAssets.LoadAsset<Texture2D>(GlitchMapTexture));
                    GlitchRandom = Random.Range(-1.0f, 1.0f);
                    ScreenMaterial.SetFloat("_GlitchAmount", Mathf.Clamp(GlitchAmount, 0f, 1f));
                    ScreenMaterial.SetFloat("_GlitchRandom", GlitchRandom);
                    break;
                case ShaderType.Scanlines:
                    ScreenMaterial = new Material(ExpandAssets.LoadAsset<Shader>("ExpandScanlines"));
                    ScreenMaterial.SetInt("_ValueX", ScanlineThickness);
                    ScreenMaterial.SetFloat("_Intensity", ScanlineIntensity);
                    break;
                case ShaderType.CRT:
                    ScreenMaterial = new Material(ExpandAssets.LoadAsset<Shader>("ExpandCRT"));
                    ScreenMaterial.SetFloat("u_time", Time.fixedTime);
                    ScreenMaterial.SetFloat("u_bend", bend);
                    ScreenMaterial.SetFloat("u_scanline_size_1", scanlineSize1);
                    ScreenMaterial.SetFloat("u_scanline_speed_1", scanlineSpeed1);
                    ScreenMaterial.SetFloat("u_scanline_size_2", scanlineSize2);
                    ScreenMaterial.SetFloat("u_scanline_speed_2", scanlineSpeed2);
                    ScreenMaterial.SetFloat("u_scanline_amount", scanlineAmount);
                    ScreenMaterial.SetFloat("u_vignette_size", vignetteSize);
                    ScreenMaterial.SetFloat("u_vignette_smoothness", vignetteSmoothness);
                    ScreenMaterial.SetFloat("u_vignette_edge_round", vignetteEdgeRound);
                    ScreenMaterial.SetFloat("u_noise_size", noiseSize);
                    ScreenMaterial.SetFloat("u_noise_amount", noiseAmount);
                    ScreenMaterial.SetVector("u_red_offset", redOffset);
                    ScreenMaterial.SetVector("u_blue_offset", blueOffset);
                    ScreenMaterial.SetVector("u_green_offset", greenOffset);
                    break;
            }
            if (isRoomSpecific && !ParentRoomIsSecretGlitchRoom) {
                if (UseCorruptionAmbience) { AkSoundEngine.PostEvent("Play_EX_CorruptionAmbience_01", gameObject); }
            } else {
                Pixelator.Instance.RegisterAdditionalRenderPass(ScreenMaterial);
                m_MaterialRegistered = true;
            }
            m_SetupComplete = true;
        }

        private void Update() {
            if (!m_SetupComplete) { return; }
            if (isRoomSpecific && ParentRoom != null) {
                PlayerController player = GameManager.Instance.PrimaryPlayer;
                if (ParentRoom == player.CurrentRoom) {
                    if (!m_MaterialRegistered) {
                        Pixelator.Instance.RegisterAdditionalRenderPass(ScreenMaterial);
                        m_MaterialRegistered = true;
                    }
                } else {
                    if (m_MaterialRegistered) {
                        Pixelator.Instance.DeregisterAdditionalRenderPass(ScreenMaterial);
                        m_MaterialRegistered = false;
                    }
                    return;
                }
            }

            switch (shaderType) {
                case ShaderType.VHS:
                    if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { return; }
                    ScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
                    if (m_yScanline >= 1) { m_yScanline = Random.value; }
                    if (m_xScanline <= 0 || Random.value < 0.05) { m_xScanline = Random.value; }
                    m_yScanline += (BraveTime.DeltaTime * 0.01f);
                    m_xScanline -= (BraveTime.DeltaTime * 0.1f);
                    m_xShiftIntensity = Random.Range(250, 500);
                    m_xShift = Random.Range(0, 0.001f);
                    ScreenMaterial.SetFloat("_yScanline", m_yScanline);
                    ScreenMaterial.SetFloat("_xScanline", m_xScanline);
                    ScreenMaterial.SetFloat("_xShift", m_xShift);
                    ScreenMaterial.SetFloat("_xShiftIntensity", m_xShiftIntensity);
                    ScreenMaterial.SetFloat("_colorBleedToggle", m_colorBleedToggle);
                    break;
                case ShaderType.VHSOldFilm:
                    if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { return; }
                    ScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
                    m_xShiftIntensity = Random.Range(150, 500);
                    m_xShift = 0;
                    m_xScanline = Random.Range(0.4f, 0.8f);
                    ScreenMaterial.SetFloat("_xScanline", m_xScanline);
                    ScreenMaterial.SetFloat("_xShift", m_xShift);
                    ScreenMaterial.SetFloat("_xShiftIntensity", m_xShiftIntensity);
                    ScreenMaterial.SetFloat("_colorBleedToggle", m_colorBleedToggle);
                    break;
                case ShaderType.VHSBasic:
                    if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { return; }
                    if (enableVHSScanlineDistortion) {
                        if (m_yScanline >= 1) { m_yScanline = Random.value; }
                        m_yScanline += (BraveTime.DeltaTime * 0.01f);
                        ScreenMaterial.SetFloat("_yScanline", m_yScanline);
                    }
                    /*if (m_xScanline <= 0 || Random.value < 0.05) { m_xScanline = Random.value; }
                    m_xScanline -= (BraveTime.DeltaTime * 0.1f);*/
                    m_xScanline = Random.Range(0.4f, 0.6f);
                    m_xShiftIntensity = Random.Range(190, 500);
                    m_xShift = Random.Range(0, 0.002f);
                    ScreenMaterial.SetFloat("_xScanline", m_xScanline);
                    ScreenMaterial.SetFloat("_xShift", m_xShift);
                    ScreenMaterial.SetFloat("_xShiftIntensity", m_xShiftIntensity);
                    ScreenMaterial.SetFloat("_colorBleedToggle", m_colorBleedToggle);
                    break;
                case ShaderType.Glitch:
                    if (!GlitchUpdatedExternally) {
                        if (GlitchUpdateFrequencyIsRandom | Random.value < 0.4f) {
                            if (Random.value < 0.3f) {
                                ScreenMaterial.SetFloat("_GlitchAmount", Mathf.Clamp(Random.Range(0, 0.05f), 0f, 1f));
                            } else {
                                ScreenMaterial.SetFloat("_GlitchAmount", Mathf.Clamp(GlitchAmount, 0f, 1f));
                            }
                            if (Random.value < GlitchUpdateFrequency) {
                                GlitchRandom = Random.Range(-1.0f, 1.0f);
                                ScreenMaterial.SetFloat("_GlitchRandom", GlitchRandom);
                            }
                        }
                    } else {
                        ScreenMaterial.SetFloat("_GlitchAmount", Mathf.Clamp(GlitchAmount, 0f, 1f));
                        ScreenMaterial.SetFloat("_GlitchRandom", GlitchRandom);
                    }
                    break;
                case ShaderType.Scanlines:
                    break;
                case ShaderType.CRT:
                    ScreenMaterial.SetFloat("u_time", Time.fixedTime);
                    break;
            }
        }

        protected override void OnDestroy() {
            try { 
                switch (shaderType) {
                    case ShaderType.VHS:
                        if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { break; }
                        TexturePlayer.Stop();
                        break;
                    case ShaderType.VHSOldFilm:
                        if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { break; }
                        Pixelator.Instance.SetSaturationColorPower(Color.white, 0);
                        TexturePlayer.Stop();
                        break;
                    case ShaderType.VHSBasic:
                        if (Application.platform == RuntimePlatform.LinuxPlayer | Application.platform == RuntimePlatform.LinuxEditor) { break; }
                        // Nothing extra needed for now
                        break;
                    case ShaderType.Glitch:
                        // Nothing extra needed for now
                        break;
                    case ShaderType.Scanlines:
                        // Nothing extra needed for now
                        break;
                    case ShaderType.CRT:
                        // Nothing extra needed for now
                        break;
                }
                m_SetupComplete = false;
                if (m_MaterialRegistered) { Pixelator.Instance.DeregisterAdditionalRenderPass(ScreenMaterial); }
                base.OnDestroy();
            } catch (System.Exception) {
            }
        }
    }
}

