using ExpandTheGungeon.ExpandUtilities;
using System.Reflection;
using UnityEngine;
using UnityEngine.Video;

namespace ExpandTheGungeon.ExpandComponents {

    [RequireComponent(typeof(VideoPlayer))]

    public class ExpandGlitchScreenFXController : BraveBehaviour {

        public ExpandGlitchScreenFXController() {
            AnimationLayer = AnimationType.None;

            m_yScanline = 0;
            m_xScanline = 0;
            m_xShift = 0; // 0.025f;
            m_xShiftIntensity = 500;
            m_colorBleedToggle = 0;

            m_SetupComplete = false;
        }

        public Material VHSScreenMaterial;

        public VideoClip VHSClip;
        public VideoClip OldFilmClip;
        public VideoPlayer TexturePlayer;
        public Texture2D VHSClip_Empty;
        
        public AnimationType AnimationLayer;

        public enum AnimationType { VHS, OldFilm, None }

        private float m_yScanline;
        private float m_xScanline;
        private float m_xShift;
        private float m_xShiftIntensity;
        private float m_colorBleedToggle;

        private bool m_SetupComplete;

        private void Start() {
            VHSScreenMaterial = new Material(ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Shader>("ExpandVHSPostProcessEffect"));

            switch (AnimationLayer) {
                case AnimationType.VHS:
                    TexturePlayer = GetComponent<VideoPlayer>();
                    VHSClip = ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<VideoClip>("VHSAnimation");
                    m_colorBleedToggle = 1;
                    VHSScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
                    TexturePlayer.Play();
                    TexturePlayer.isLooping = true;
                    TexturePlayer.renderMode = VideoRenderMode.APIOnly;
                    TexturePlayer.clip = VHSClip;
                    break;
                case AnimationType.OldFilm:
                    TexturePlayer = GetComponent<VideoPlayer>();
                    OldFilmClip = ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<VideoClip>("OldFilm");
                    m_colorBleedToggle = 0;
                    VHSScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
                    TexturePlayer.Play();
                    TexturePlayer.isLooping = true;
                    TexturePlayer.renderMode = VideoRenderMode.APIOnly;
                    TexturePlayer.clip = OldFilmClip;
                    // SetOldFilmSepiaTone();
                    Pixelator.Instance.SetFreezeFramePower(1, false);
                    break;
                case AnimationType.None:
                    VHSClip_Empty = ResourceManager.LoadAssetBundle("ExpandSharedAuto").LoadAsset<Texture2D>("EmptyVHSTexture");
                    VHSScreenMaterial.SetTexture("_VHSTex", VHSClip_Empty);
                    break;
            }
           
            Pixelator.Instance.RegisterAdditionalRenderPass(VHSScreenMaterial);
            m_SetupComplete = true;
        }

        private void Update() {
            if (!m_SetupComplete) { return; }
            
            if (AnimationLayer == AnimationType.VHS | AnimationLayer == AnimationType.OldFilm) {
                VHSScreenMaterial.SetTexture("_VHSTex", TexturePlayer.texture);
            }

            if (AnimationLayer == AnimationType.None | AnimationLayer == AnimationType.VHS) {
                if (m_yScanline >= 1) { m_yScanline = Random.value; }
                if (m_xScanline <= 0 || Random.value < 0.05) { m_xScanline = Random.value; }
                m_yScanline = (m_yScanline + BraveTime.DeltaTime * 0.01f);
                m_xScanline = (m_xScanline - BraveTime.DeltaTime * 0.1f);
            }

            if (AnimationLayer == AnimationType.OldFilm) {
                m_xShiftIntensity = Random.Range(150, 500);
                m_xShift = 0;
            } else {
                m_xShiftIntensity = Random.Range(250, 500);
                m_xShift = Random.Range(0, 0.001f);
            }

            VHSScreenMaterial.SetFloat("_yScanline", m_yScanline);
            VHSScreenMaterial.SetFloat("_xScanline", m_xScanline);
            VHSScreenMaterial.SetFloat("_xShift", m_xShift);
            VHSScreenMaterial.SetFloat("_xShiftIntensity", m_xShiftIntensity);
            VHSScreenMaterial.SetFloat("_colorBleedToggle", m_colorBleedToggle);
        }


        public void SetOldFilmSepiaTone() {
            Pixelator m_Pixilater = Pixelator.Instance;
            Material m_fadeMaterial = ReflectionHelpers.ReflectGetField<Material>(typeof(Pixelator), "m_fadeMaterial", m_Pixilater);
            int m_saturationID = ReflectionHelpers.ReflectGetField<int>(typeof(Pixelator), "m_saturationID", m_Pixilater);

            FieldInfo m_gammaAdjustment = typeof(Pixelator).GetField("m_gammaAdjustment", BindingFlags.Instance | BindingFlags.NonPublic);
            
            m_gammaAdjustment.SetValue(m_Pixilater, Mathf.Lerp((GameManager.Options.LightingQuality != GameOptions.GenericHighMedLowOption.LOW) ? 0f : -0.1f, -0.35f, 1));

            m_fadeMaterial.SetColor("_SaturationColor", Color.Lerp(new Color(1f, 1f, 1f), new Color(0.825f, 0.7f, 0.3f), 1));
            m_Pixilater.saturation = Mathf.Lerp(1f, 0f, 1);
            m_gammaAdjustment.SetValue(m_Pixilater, Mathf.Lerp((GameManager.Options.LightingQuality != GameOptions.GenericHighMedLowOption.LOW) ? 0f : -0.1f, -0.6f, 1));
            
            m_fadeMaterial.SetFloat(m_saturationID, m_Pixilater.saturation);
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            if (AnimationLayer == AnimationType.VHS | AnimationLayer == AnimationType.OldFilm) { TexturePlayer.Stop(); }
            m_SetupComplete = false;
            if (AnimationLayer == AnimationType.OldFilm) { Pixelator.Instance.SetFreezeFramePower(0, false); }
            Pixelator.Instance.DeregisterAdditionalRenderPass(VHSScreenMaterial);
        }
    }
}

