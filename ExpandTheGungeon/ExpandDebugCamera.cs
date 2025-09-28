using ExpandTheGungeon.ExpandUtilities;
using ExpandTheGungeon.ItemAPI;
using System;
using UnityEngine;
using UnityEngine.UI;


namespace ExpandTheGungeon {

    public class ExpandDebugCamera : BraveBehaviour {

        public static bool DebugCameraEnabled = false;
        public static float ResetAfterLoadTime = 3;
        public static float DebugViewCoverage = 0.1f;
        public static Vector2 DebugViewPortPosition = new Vector2(140, 85);
        public static Vector2 FoyerCameraPosition = new Vector3(27, 25);

        public static void SetInitialCameraPosition(Pixelator pixelator, CameraController mainCamera) {
            mainCamera.SetManualControl(true, false);
            pixelator.DoOcclusionLayer = false;
            Instance.Init();
        }

        private static ExpandDebugCamera m_instance;

        public static ExpandDebugCamera Instance {
            get {
                if (!m_instance) {
                    GameObject m_NewDebugObject = new GameObject("EX Debug Camera Manager", new Type[] { typeof(ExpandDebugCamera) }) { layer = 22 };
                    m_instance = m_NewDebugObject.GetComponent<ExpandDebugCamera>();
                }
                return m_instance;
            }
        }
        

        private bool m_SkipOcclusionEnable;
        private bool m_FoyerTimerSet;
        private bool m_IsActive;
        private float m_EndTimer;
        
        private GameManager m_GameManager;
        private Pixelator m_Pixelator;

        private GameObject m_CameraObject;
        private CameraController m_MainCamera;

        

        public static void ClearLoadScreenBackground(FoyerPreloader Instance) {
            if (Instance.Throbber && !Instance.Throbber.IsVisible) Instance.Throbber.IsVisible = true;
            if (Instance.RatThrobber && Instance.RatThrobber.IsVisible) Instance.RatThrobber.IsVisible = false;
            
            GameObject m_LoadScreenUIRoot = null;

            for (int i = 0; i < Instance.gameObject.transform.childCount; i++) {
                if (Instance.gameObject.transform.GetChild(i).gameObject.name.Contains("weird named camera")) {
                    Instance.gameObject.transform.GetChild(i).gameObject.GetComponent<Camera>().enabled = false;
                } else if (Instance.gameObject.transform.GetChild(i).gameObject.name.Contains("FadeCanvas")) {
                    Instance.gameObject.transform.GetChild(i).gameObject.GetComponentInChildren<Image>().enabled = false;
                }
            }
            
            if (m_LoadScreenUIRoot) {
                for (int i = 0; i < m_LoadScreenUIRoot.transform.childCount; i++) {
                    if (m_LoadScreenUIRoot.transform.GetChild(i).gameObject.name.Contains("FadeCanvas")) {
                        m_LoadScreenUIRoot.transform.GetChild(i).gameObject.GetComponentInChildren<Image>().enabled = false;
                    }
                }
            }
            if (!m_instance)ExpandDebugCamera.Instance.Init();
            
        }

        public void Init() {
            DontDestroyOnLoad(gameObject);

            m_SkipOcclusionEnable = false;
            m_IsActive = false;
            
            m_EndTimer = ResetAfterLoadTime;

            if (Pixelator.Instance) {
                m_Pixelator = Pixelator.Instance;
                m_Pixelator.DoOcclusionLayer = false;
                m_Pixelator.KillAllFades = true;
            }

            m_CameraObject = GameObject.Find("Main Camera");

            if (m_CameraObject) {
                m_MainCamera = m_CameraObject.GetComponent<CameraController>();
            } else if (Camera.main) {
                m_MainCamera = Camera.main.GetComponent<CameraController>();
            }


            if (GameManager.Instance) m_GameManager = GameManager.Instance;


            if (m_MainCamera) {
                m_MainCamera.SetManualControl(true, false);
                m_MainCamera.OverridePosition = DebugViewPortPosition;
                m_MainCamera.SetZoomScaleImmediate(DebugViewCoverage);
            }
            
            m_FoyerTimerSet = false;
            m_IsActive = true;
        }
        

        public void Update() {
            if (!m_IsActive)return;
            
            if (m_Pixelator) {
                m_Pixelator.DoOcclusionLayer = false;
                if (!m_GameManager.IsFoyer)m_Pixelator.KillAllFades = true;
            }
            
            if (m_MainCamera) {
                m_MainCamera.SetManualControl(true, false);
                m_MainCamera.OverridePosition = DebugViewPortPosition;
                m_MainCamera.SetZoomScaleImmediate(DebugViewCoverage);
            } else {
                m_CameraObject = GameObject.Find("Main Camera");

                if (m_CameraObject) {
                    m_MainCamera = m_CameraObject.GetComponent<CameraController>();
                } else if (Camera.main) {
                    m_MainCamera = Camera.main.GetComponent<CameraController>();
                }

                if (!m_CameraObject) return;
                                
                m_MainCamera.SetManualControl(true, false);
                m_MainCamera.OverridePosition = DebugViewPortPosition;
                m_MainCamera.SetZoomScaleImmediate(DebugViewCoverage);
                m_MainCamera.gameObject.transform.position = DebugViewPortPosition;
            }
            if (m_GameManager.IsFoyer && !m_FoyerTimerSet) {
                m_FoyerTimerSet = true;
                m_EndTimer = 1.5f;
            }
        }

        public void LateUpdate() {
            if (!m_IsActive) return;
            if (!m_Pixelator)m_Pixelator = Pixelator.Instance;
            DoPostLoadCheck();
        }

        private void DoPostLoadCheck() {
            if (!m_GameManager) {
                m_GameManager = GameManager.Instance;
                return;
            }
            if (m_GameManager.IsLoadingLevel) return;
            m_EndTimer -= BraveTime.DeltaTime;
            if (m_EndTimer <= 0) {
                m_EndTimer = 99;
                DoReset();
                return;
            }
        }

        private void DoReset(bool destroyOnExit = true) {
            m_IsActive = false;
            if (!m_GameManager | !m_Pixelator) return;

            if (ExpandSettings.debugMode && m_Pixelator.slavedCameras != null && m_Pixelator.slavedCameras.Count > 0) {                                
                Texture2D m_Texture = ExpandUtility.GenerateTexture2DFromRenderTexture(Pixelator.Instance.slavedCameras[0].activeTexture);
                ExpandUtility.DumpTexture2DToFile(m_Texture, m_GameManager.Dungeon.gameObject.name, true);
            }


            if (m_MainCamera) {
                if (m_GameManager.IsFoyer)m_MainCamera.OverridePosition = FoyerCameraPosition;
                m_MainCamera.SetZoomScaleImmediate(1);
                if (!m_GameManager.IsFoyer && !m_GameManager.IsSelectingCharacter) m_MainCamera.SetManualControl(false, false);
            }

            if (m_GameManager.PrimaryPlayer && m_GameManager.PrimaryPlayer.HasPassiveItem(ThirdEye.ThirdEyeID)) {
                m_SkipOcclusionEnable = true;
            }

            if (!m_SkipOcclusionEnable && m_GameManager.PrimaryPlayer &&
                m_GameManager.GetOtherPlayer(m_GameManager.PrimaryPlayer) &&
                m_GameManager.GetOtherPlayer(m_GameManager.PrimaryPlayer).HasPassiveItem(ThirdEye.ThirdEyeID)
                ) {
                m_SkipOcclusionEnable = true;
            }

            if (!m_SkipOcclusionEnable && !m_GameManager.IsFoyer)m_Pixelator.DoOcclusionLayer = true;

            m_Pixelator.KillAllFades = false;

            if (m_GameManager.IsFoyer) m_Pixelator.FadeToBlack(0.5f, true);
            
            if (destroyOnExit) Destroy(gameObject);
        }

        protected override void OnDestroy() {
            if (m_IsActive)DoReset(false);
            base.OnDestroy();
        }
    }
}

