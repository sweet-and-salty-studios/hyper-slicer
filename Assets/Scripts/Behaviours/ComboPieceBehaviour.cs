using HyperSlicer.Utilities.LeanTween;
using TMPro;
using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class ComboPieceBehaviour : PieceBehaviour
    {
        private TMP_Text scoreMultiplierText = default;
        private Color activeColor = default;

        public int ScoreMultiplier { get; private set; }

        private MeshRenderer meshRenderer = default;

        private void Awake()
        {
            scoreMultiplierText = GetComponentInChildren<TMP_Text>(true);
            meshRenderer = GetComponentInChildren<MeshRenderer>(true);
        }

        public void UpdateScoreMultiplierText(int scoreMultiplier)
        {
            ScoreMultiplier = scoreMultiplier;
            scoreMultiplierText.text = $"{scoreMultiplier}x";
        }

        public void UpdateColors(Color activeColor)
        {
            this.activeColor = activeColor;

            meshRenderer.material.color = activeColor;
        }

        public void AniamateColor()
        {
            LeanTween.cancel(gameObject);
            LeanTween.color(gameObject, Color.white, 0.2f)
                .setLoopPingPong();
        }
    }
}