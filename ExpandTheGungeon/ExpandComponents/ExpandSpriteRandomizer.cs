using System.Collections.Generic;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandSpriteRandomizer : BraveBehaviour {

        public ExpandSpriteRandomizer() {
            SpriteList = new List<string>();

            m_SelectedSprite = string.Empty;
        }

        public List<string> SpriteList;
        public tk2dSpriteCollectionData SpriteCollection;

        string m_SelectedSprite;

        public void Awake() { }

        public void Start() {
            if (!sprite | SpriteList == null | SpriteList.Count <= 1) {
                Destroy(this);
                return;
            }
            SpriteList = SpriteList.Shuffle();
            m_SelectedSprite = BraveUtility.RandomElement(SpriteList);
            if (SpriteCollection) {
                sprite.SetSprite(SpriteCollection, m_SelectedSprite);
            } else {
                sprite.SetSprite(m_SelectedSprite);
            }
            Destroy(this);
            return;
        }

        public void Update() { }
        
        protected override void OnDestroy() {
            base.OnDestroy();
        }
    }
}

