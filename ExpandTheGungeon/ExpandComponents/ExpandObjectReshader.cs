using ExpandTheGungeon.ExpandUtilities;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandObjectReshader : BraveBehaviour {

        public ExpandObjectReshader() {
            Colors = new List<Color>() { new Color(0, 1, 0, 0.6f) };
            ColorOverrideName = "tint";
            ChanceToNotReshade = false;
            ReshadeSkipOdds = 0.35f;
        }

        public bool ChanceToNotReshade;
        public float ReshadeSkipOdds;
        public List<Color> Colors;
        public string ColorOverrideName;

        private Color m_SelectedColor;

        public void Start() {
            if (!sprite | Colors == null | Colors.Count == 0) return;

            if (ChanceToNotReshade && Random.value < ReshadeSkipOdds) return;

            m_SelectedColor = Colors[0];
            
            if (Colors.Count > 1) {
                Colors = Colors.Shuffle();
                m_SelectedColor = BraveUtility.RandomElement(Colors);
            }

            ExpandUtility.DisableSuperTinting(sprite);

            if (gameActor) {
                gameActor.RegisterOverrideColor(m_SelectedColor, ColorOverrideName);
            } else {
                sprite.usesOverrideMaterial = true;
                sprite.renderer.material.SetColor("_OverrideColor", m_SelectedColor);
            }
        }

    }
}

