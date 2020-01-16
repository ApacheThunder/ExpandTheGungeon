using System.Collections.Generic;
using Dungeonator;
using UnityEngine;
using System;

namespace ExpandTheGungeon.ExpandComponents {

    public class ExpandBooRoomChallengeComponent : ChallengeModifier {
        
        public float ConeAngle;

        public Shader DarknessEffectShader;

        private Material m_material;

        private void Start() {
            if (Pixelator.Instance.AdditionalCoreStackRenderPass == null) {
                m_material = new Material(DarknessEffectShader);
                Pixelator.Instance.AdditionalCoreStackRenderPass = m_material;
            }
        }

        private Vector4 GetCenterPointInScreenUV(Vector2 centerPoint) {
            Vector3 vector = GameManager.Instance.MainCameraController.Camera.WorldToViewportPoint(centerPoint.ToVector3ZUp(0f));
            return new Vector4(vector.x, vector.y, 0f, 0f);
        }

        private void LateUpdate() {
            if (m_material != null) {
                float num = GameManager.Instance.PrimaryPlayer.FacingDirection;
                if (num > 270f) { num -= 360f; }
                if (num < -270f) { num += 360f; }
                m_material.SetFloat("_ConeAngle", ConeAngle);
                Vector4 centerPointInScreenUV = GetCenterPointInScreenUV(GameManager.Instance.PrimaryPlayer.CenterPosition);
                centerPointInScreenUV.z = num;
                Vector4 value = centerPointInScreenUV;
                if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                    num = GameManager.Instance.SecondaryPlayer.FacingDirection;
                    if (num > 270f) { num -= 360f; }
                    if (num < -270f) { num += 360f; }
                    value = GetCenterPointInScreenUV(GameManager.Instance.SecondaryPlayer.CenterPosition);
                    value.z = num;
                }
                m_material.SetVector("_Player1ScreenPosition", centerPointInScreenUV);
                m_material.SetVector("_Player2ScreenPosition", value);
            }
        }

        private void Update() {
            RoomHandler currentRoom = GameManager.Instance.PrimaryPlayer.CurrentRoom;
            List<AIActor> activeEnemies = currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
            Vector2 vector = Vector2.zero;
            Vector2 b = Vector2.zero;
            float num;
            if (GameManager.Instance.PrimaryPlayer.CurrentGun && !GameManager.Instance.PrimaryPlayer.IsGhost) {
                num = GameManager.Instance.PrimaryPlayer.CurrentGun.CurrentAngle;
            } else {
                num = BraveMathCollege.Atan2Degrees(GameManager.Instance.PrimaryPlayer.unadjustedAimPoint.XY());
            }
            vector = GameManager.Instance.PrimaryPlayer.CenterPosition;
            float a;
            if (GameManager.Instance.CurrentGameType == GameManager.GameType.COOP_2_PLAYER) {
                if (GameManager.Instance.SecondaryPlayer.CurrentGun && !GameManager.Instance.SecondaryPlayer.IsGhost) {
                    a = GameManager.Instance.SecondaryPlayer.CurrentGun.CurrentAngle;
                } else {
                    a = BraveMathCollege.Atan2Degrees(GameManager.Instance.SecondaryPlayer.unadjustedAimPoint.XY());
                }
                b = GameManager.Instance.SecondaryPlayer.CenterPosition;
            } else {
                b = vector;
                a = num;
            }
            for (int i = 0; i < activeEnemies.Count; i++) {
                AIActor aiactor = activeEnemies[i];
                if (aiactor && aiactor.healthHaver && aiactor.IsNormalEnemy && !aiactor.healthHaver.IsBoss && !aiactor.healthHaver.IsDead) {
                    Vector2 centerPosition = aiactor.CenterPosition;
                    float b2 = BraveMathCollege.Atan2Degrees(centerPosition - vector);
                    float b3 = BraveMathCollege.Atan2Degrees(centerPosition - b);
                    bool flag = BraveMathCollege.AbsAngleBetween(num, b2) < ConeAngle || BraveMathCollege.AbsAngleBetween(a, b3) < ConeAngle;
                    if (flag) {
                        if (aiactor.behaviorSpeculator) { aiactor.behaviorSpeculator.Stun(0.25f, false); }
                        if (aiactor.IsBlackPhantom) { aiactor.UnbecomeBlackPhantom(); }
                        aiactor.RegisterOverrideColor(new Color(0.4f, 0.4f, 0.33f, 1), "Turn to Stone");
                    } else if (!aiactor.IsBlackPhantom) {
                        aiactor.DeregisterOverrideColor("Turn to Stone");
                        aiactor.BecomeBlackPhantom();
                    }
                }
            }
        }



        private void OnDestroy() {
            if (m_material != null && Pixelator.Instance) { Pixelator.Instance.AdditionalCoreStackRenderPass = null; }
        }
    }
}

