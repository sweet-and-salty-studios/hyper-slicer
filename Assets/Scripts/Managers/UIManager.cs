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

        private IEnumerator IDelayAction(Action action)
        {
            yield return new WaitForSeconds(4);

            if(action!= null) action.Invoke();
        }

        private void OnGameOver()
        {
            StartCoroutine(IDelayAction(() => gameOverPanel.gameObject.SetActive(true)));
        }

        private void OnLevelComplete()
        {
            StartCoroutine(IDelayAction(() => levelCompletePanel.gameObject.SetActive(true)));
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