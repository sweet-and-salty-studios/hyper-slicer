using HyperSlicer.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HyperSlicer.UI
{
    public class LevelProgressDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentLevelText = default; 
        [SerializeField] private TMP_Text nextLevelText = default; 
        [SerializeField] private Slider slider = default;

        public void UpdateLevelTexts(int currentLevel, int nextLevel)
        {
            currentLevelText.text = currentLevel.ToString();
            nextLevelText.text = nextLevel.ToString();
        }

        public void UpdateProgress(float maximum, float current)
        {
            slider.value = Mathf.Clamp01(current / maximum);
        }
    }
}
