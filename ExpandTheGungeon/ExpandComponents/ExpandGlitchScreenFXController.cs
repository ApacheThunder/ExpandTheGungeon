using Dungeonator;
using UnityEngine;
using UnityEngine.Video;

namespace ExpandTheGungeon.ExpandComponents {

    [RequireComponent(typeof(VideoPlayer))]

    public class ExpandGlitchScreenFXController : BraveBehaviour {

        public ExpandGlitchScreenFXController() {
            shaderType = ShaderType.VHSBasic;
            isRoomSpecific = false;
            ParentRoomIsSecretGlitchRoom = false;
            UseCorruptionAmbience = true;
            enableVHSScanlineDistortion = false;

            // For Glitch Shader
            GlitchUpdatedExternally = true;
            GlitchIntensityIsRandom = false;
            GlitchUpdateFrequencyIsRandom = false;
            GlitchAmount = 0.5f;
            GlitchRandom = 0.1f;
            GlitchUpdateFrequency = 0.05f;
            GlitchMapTexture = "EX_GlitchMap";

            m_yScanline = 0;
            m_xScanline = 0;
            m_xShift = 0; // 0.025f;
            m_xShiftIntensity = 500;
            m_colorBleedToggle = 0;
            
            m_SetupComplete = false;
            m_MaterialRegistered = false;
        }

        public Material ScreenMaterial;

        // This only used for VHS Shader currently
        public VideoClip VHSClip;
        public VideoClip OldFilmClip;
        public VideoPlayer TexturePlayer;
        public Texture2D VHSClip_Empty;

        public bool enableVHSScanlineDistortion;
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
        

        public RoomHandler ParentRoom;

        public ShaderType shaderType;

        public enum ShaderType { VHS, VHSOldFilm, VHSBasic, Glitch }

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
                    ScreenMaterial = new Material(ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Shader>("ExpandVHSPostProcessEffect"));
                    TexturePlayer = GetComponent<VideoPlayer>();
                    VHSClip = ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<VideoClip>("VHSAnimation");
                    m_colorBleedToggle = 1;
                    ScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
                    if (!enableVHSScanlineDistortion) { ScreenMaterial.SetFloat("_enableScanlineDistortion", 0); }
                    TexturePlayer.Play();
                    TexturePlayer.isLooping = true;
                    TexturePlayer.renderMode = VideoRenderMode.APIOnly;
                    TexturePlayer.clip = VHSClip;
                    break;
                case ShaderType.VHSOldFilm:
                    ScreenMaterial = new Material(ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Shader>("ExpandVHSPostProcessEffect"));
                    TexturePlayer = GetComponent<VideoPlayer>();
                    OldFilmClip = ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<VideoClip>("OldFilm");
                    m_colorBleedToggle = 0;
                    ScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
                    if (!enableVHSScanlineDistortion) { ScreenMaterial.SetFloat("_enableScanlineDistortion", 0); }
                    TexturePlayer.Play();
                    TexturePlayer.isLooping = true;
                    TexturePlayer.renderMode = VideoRenderMode.APIOnly;
                    TexturePlayer.clip = OldFilmClip;
                    Pixelator.Instance.SetFreezeFramePower(1, false);
                    break;
                case ShaderType.VHSBasic:
                    ScreenMaterial = new Material(ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Shader>("ExpandVHSPostProcessEffect"));
                    VHSClip_Empty = ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Texture2D>("EmptyVHSTexture");
                    ScreenMaterial.SetTexture("_VHSTex", VHSClip_Empty);
                    if (!enableVHSScanlineDistortion) { ScreenMaterial.SetFloat("_enableScanlineDistortion", 0); }
                    m_colorBleedToggle = 0;
                    break;
                case ShaderType.Glitch:
                    ScreenMaterial = new Material(ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Shader>("ExpandGlitchScreen"));
                    ScreenMaterial.SetTexture("_GlitchMap", ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Texture2D>(GlitchMapTexture));
                    GlitchRandom = Random.Range(-1.0f, 1.0f);
                    ScreenMaterial.SetFloat("_GlitchAmount", Mathf.Clamp(GlitchAmount, 0f, 1f));
                    ScreenMaterial.SetFloat("_GlitchRandom", GlitchRandom);
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
            }
        }

        protected override void OnDestroy() {
            try { 
                switch (shaderType) {
                    case ShaderType.VHS:
                        TexturePlayer.Stop();
                        break;
                    case ShaderType.VHSOldFilm:
                        Pixelator.Instance.SetFreezeFramePower(0, false);
                        Pixelator.Instance.SetVignettePower(0);
                        TexturePlayer.Stop();
                        break;
                    case ShaderType.VHSBasic:
                        // Nothing extra needed for now
                        break;
                    case ShaderType.Glitch:
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

