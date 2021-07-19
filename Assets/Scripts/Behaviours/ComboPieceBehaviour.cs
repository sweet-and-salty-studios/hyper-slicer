using TMPro;
using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class ComboPieceBehaviour : MonoBehaviour
    {
        private TMP_Text scoreMultiplierText = default;

        public int ScoreMultiplier { get; private set; }

        private void Awake()
        {
            scoreMultiplierText = GetComponentInChildren<TMP_Text>(true);
        }

        public void UpdateScoreMultiplierText(int scoreMultiplier)
        {
            ScoreMultiplier = scoreMultiplier;
            scoreMultiplierText.text = $"{scoreMultiplier}x";
        }
    }
}