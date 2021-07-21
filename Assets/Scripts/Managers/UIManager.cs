using HyperSlicer.UI;
using System;
using System.Collections;
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
            GameManager.LevelLoaded += OnLevelLoaded;
            GameManager.GameOver += OnGameOver;
            GameManager.LevelComplete += OnLevelComplete;
            GameManager.ScoreAdded += OnScoreModified;
            GameManager.LevelProgressUpdated += OnLevelProgressUpdated;
        }

        private void OnDestroy()
        {
            GameManager.LevelLoaded -= OnLevelLoaded;
            GameManager.GameOver -= OnGameOver;
            GameManager.LevelComplete -= OnLevelComplete;
            GameManager.ScoreAdded -= OnScoreModified;
            GameManager.LevelProgressUpdated -= OnLevelProgressUpdated;
        }

        private IEnumerator IDelayAction(Action action)
        {
            yield return new WaitForSeconds(4);

            if(action!= null) action.Invoke();
        }

        private void OnLevelLoaded(LevelInfo levelInfo)
        {
            var currentLevelIndex = levelInfo.CurrentLevelIndex;
            controlPanel.LevelProgressDisplay.UpdateLevelTexts(currentLevelIndex, currentLevelIndex + 1);
        }

        private void OnGameOver(LevelInfo levelInfo)
        {
            StartCoroutine(IDelayAction(() => gameOverPanel.gameObject.SetActive(true)));
        }

        private void OnLevelComplete(LevelInfo levelInfo)
        {
            StartCoroutine(IDelayAction(() => levelCompletePanel.gameObject.SetActive(true)));
        }

        private void OnScoreModified(LevelInfo levelInfo)
        {
            controlPanel.ScoreDisplay.UpdateScoreText(levelInfo.CurrentScore);
        }

        private void OnLevelProgressUpdated(float maxDistance, float currentDistance)
        {
            controlPanel.LevelProgressDisplay.UpdateProgress(maxDistance, currentDistance);
        }
    }
}