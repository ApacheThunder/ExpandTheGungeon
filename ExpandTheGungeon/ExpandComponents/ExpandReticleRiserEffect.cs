using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandReticleRiserEffect : MonoBehaviour {

        public ExpandReticleRiserEffect() {
            NumRisers = 4;
            RiserHeight = 1f;
            RiseTime = 1.5f;

            UpdateSpriteDefinitions = false;
            CurrentSpriteName = string.Empty;
        }

        public bool UpdateSpriteDefinitions;

        public string CurrentSpriteName;


        public int NumRisers;

        public float RiserHeight;
        public float RiseTime;

        private tk2dSprite m_sprite;

        private tk2dSprite[] m_risers;

        private Shader m_shader;

        private float m_localElapsed;


        private void Start() {
            m_sprite = GetComponent<tk2dSprite>();
            m_sprite.usesOverrideMaterial = true;
            m_shader = ShaderCache.Acquire("tk2d/BlendVertexColorUnlitTilted");
            m_sprite.renderer.material.shader = m_shader;
            GameObject gameObject = Instantiate(base.gameObject);
            Destroy(gameObject.GetComponent<ExpandReticleRiserEffect>());
            m_risers = new tk2dSprite[NumRisers];
            m_risers[0] = gameObject.GetComponent<tk2dSprite>();
            for (int i = 0; i < NumRisers - 1; i++) { m_risers[i + 1] = Instantiate(gameObject).GetComponent<tk2dSprite>(); }
            OnSpawned();
        }

        private void OnSpawned() {
            m_localElapsed = 0f;
            if (m_risers != null) {
                for (int i = 0; i < m_risers.Length; i++) {
                    m_risers[i].transform.parent = transform;
                    m_risers[i].transform.localPosition = Vector3.zero;
                    m_risers[i].transform.localRotation = Quaternion.identity;
                    m_risers[i].usesOverrideMaterial = true;
                    m_risers[i].renderer.material.shader = m_shader;
                    m_risers[i].gameObject.SetLayerRecursively(LayerMask.NameToLayer("FG_Critical"));
                }
            }
        }

        private void Update() {
            if (!m_sprite) { return; }
            m_localElapsed += BraveTime.DeltaTime;
            if (UpdateSpriteDefinitions && !string.IsNullOrEmpty(CurrentSpriteName)) { m_sprite.SetSprite(CurrentSpriteName); }
            m_sprite.ForceRotationRebuild();
            m_sprite.UpdateZDepth();
            m_sprite.renderer.material.shader = m_shader;
            if (m_risers != null) {
                for (int i = 0; i < m_risers.Length; i++) {
                    if (UpdateSpriteDefinitions && !string.IsNullOrEmpty(CurrentSpriteName)) { m_risers[i].SetSprite(CurrentSpriteName); }
                    float num = Mathf.Max(0f, m_localElapsed - RiseTime / NumRisers * i);
                    float t = num % RiseTime / RiseTime;
                    m_risers[i].color = Color.Lerp(new Color(1f, 1f, 1f, 0.75f), new Color(1f, 1f, 1f, 0f), t);
                    float y = Mathf.Lerp(0f, RiserHeight, t);
                    m_risers[i].transform.localPosition = Vector3.zero;
                    m_risers[i].transform.position += Vector3.zero.WithY(y);
                    m_risers[i].ForceRotationRebuild();
                    m_risers[i].UpdateZDepth();
                    m_risers[i].renderer.material.shader = m_shader;
                }
            }
        }
    }
}


