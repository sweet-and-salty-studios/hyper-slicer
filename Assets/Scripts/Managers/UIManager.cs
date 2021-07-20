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
            GameManager.ScoreModified += OnScoreModified;
            GameManager.LevelProgressUpdated += OnLevelProgressUpdated;
        }

        private void OnDestroy()
        {
            GameManager.GameOver -= OnGameOver;
            GameManager.LevelComplete -= OnLevelComplete;
            GameManager.ScoreModified -= OnScoreModified;
            GameManager.LevelProgressUpdated -= OnLevelProgressUpdated;
        }

        private void OnGameOver()
        {
            gameOverPanel.gameObject.SetActive(true);
        }

        private void OnLevelComplete()
        {
            levelCompletePanel.gameObject.SetActive(true);
        }

        private void OnScoreModified(int newScore)
        {
            controlPanel.ScoreDisplay.UpdateScoreText(newScore);
        }

        private void OnLevelProgressUpdated(float maxDistance, float currentDistance)
        {
            controlPanel.LevelProgressDisplay.UpdateProgress(maxDistance, currentDistance);
        }
    }
}