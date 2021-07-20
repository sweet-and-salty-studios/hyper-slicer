using TMPro;
using UnityEngine;

namespace HyperSlicer.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text scoreText = default;

        public void UpdateScoreText(int value)
        {
            scoreText.text = value.ToString();
        }
    }
}
