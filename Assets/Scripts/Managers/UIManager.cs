using HyperSlicer.UI;
using UnityEngine;

namespace HyperSlicer.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ControlPanel controlPanel = default;
        [SerializeField] private GameOverPanel gameOverPanel = default;
        [SerializeField] private LevelCompletePanel levelCompletePanel = default;

        private void Awake()
        {
            GameManager.GameOver += OnGameOver;
            GameManager.LevelComplete += OnLevelComplete;
            GameManager.ModifyScore += OnScoreModified;
        }

        private void OnDestroy()
        {
            GameManager.GameOver -= OnGameOver;
            GameManager.LevelComplete -= OnLevelComplete;
            GameManager.ModifyScore -= OnScoreModified;
        }

        public void OnGameOver()
        {
            gameOverPanel.gameObject.SetActive(true);
        }

        public void OnLevelComplete()
        {
            levelCompletePanel.gameObject.SetActive(true);
        }

        public void OnScoreModified(int newScore)
        {
            controlPanel.ScoreDisplay.UpdateScoreText(newScore);
        }
    }
}