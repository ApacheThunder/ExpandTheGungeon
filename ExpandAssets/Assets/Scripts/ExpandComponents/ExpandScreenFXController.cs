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

        private void Start() { }

        private void Update() { }

        protected override void OnDestroy() { }
	}
}

