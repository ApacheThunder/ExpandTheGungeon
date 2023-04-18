using System;
using System.Collections;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandSpriteFadeOnSpawn : BraveBehaviour {

        [SerializeField]
        public float FadeTime;
        [SerializeField]
        public float FadeStartAlpha;
        [SerializeField]
        public Color FadeColor;        

        [NonSerialized]
        private Renderer m_Renderer;

        public ExpandSpriteFadeOnSpawn() {
            FadeTime = 0.2f;
            FadeStartAlpha = 0.5f;
            FadeColor = Color.black;
        }

        public void Start() {
            m_Renderer = renderer;
            StartCoroutine(HandleOverrideColorFade(FadeColor, FadeTime, FadeStartAlpha));
        }

        private IEnumerator HandleOverrideColorFade(Color targetColor, float duration, float startAlpha = 0f) {
            if (!m_Renderer) { yield break; }
            Color startColor = new Color(targetColor.r, targetColor.g, targetColor.b, startAlpha);
            float elapsed = 0f;
            while (elapsed < duration) {
                elapsed += BraveTime.DeltaTime;
                float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(elapsed / duration));
                Color current = Color.Lerp(startColor, targetColor, t);
                m_Renderer.material.SetColor("_OverrideColor", current);
                yield return null;
            }
            m_Renderer.material.SetColor("_OverrideColor", targetColor);
            yield break;
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

