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

        public bool isRoomSpecific;
        public bool ParentRoomIsSecretGlitchRoom;

        public RoomHandler ParentRoom;

        public ShaderType shaderType;

        public enum ShaderType { VHS, VHSOldFilm, VHSBasic }

        private bool m_SetupComplete;
        private bool m_MaterialRegistered;

        // For VHS Shader only
        private float m_yScanline;
        private float m_xScanline;
        private float m_xShift;
        private float m_xShiftIntensity;
        private float m_colorBleedToggle;        
        

        private void Start() {
            switch (shaderType) {
                case ShaderType.VHS:
                    ScreenMaterial = new Material(ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Shader>("ExpandVHSPostProcessEffect"));
                    TexturePlayer = GetComponent<VideoPlayer>();
                    VHSClip = ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<VideoClip>("VHSAnimation");
                    m_colorBleedToggle = 1;
                    ScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
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
                    m_colorBleedToggle = 0;
                    break;
            }
            if (isRoomSpecific && !ParentRoomIsSecretGlitchRoom) {
                AkSoundEngine.PostEvent("Play_EX_CorruptionAmbience_01", gameObject);
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
                    ScreenMaterial.SetFloat("_yScanline", m_yScanline);
                    ScreenMaterial.SetFloat("_xScanline", m_xScanline);
                    ScreenMaterial.SetFloat("_xShift", m_xShift);
                    ScreenMaterial.SetFloat("_xShiftIntensity", m_xShiftIntensity);
                    ScreenMaterial.SetFloat("_colorBleedToggle", m_colorBleedToggle);
                    break;
                case ShaderType.VHSBasic:
                    if (m_yScanline >= 1) { m_yScanline = Random.value; }
                    if (m_xScanline <= 0 || Random.value < 0.05) { m_xScanline = Random.value; }
                    m_yScanline += (BraveTime.DeltaTime * 0.01f);
                    m_xScanline -= (BraveTime.DeltaTime * 0.1f);
                    m_xShiftIntensity = Random.Range(200, 500);
                    m_xShift = Random.Range(0, 0.002f);
                    ScreenMaterial.SetFloat("_yScanline", m_yScanline);
                    ScreenMaterial.SetFloat("_xScanline", m_xScanline);
                    ScreenMaterial.SetFloat("_xShift", m_xShift);
                    ScreenMaterial.SetFloat("_xShiftIntensity", m_xShiftIntensity);
                    ScreenMaterial.SetFloat("_colorBleedToggle", m_colorBleedToggle);
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
                        TexturePlayer.Stop();
                        break;
                    case ShaderType.VHSBasic:
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

