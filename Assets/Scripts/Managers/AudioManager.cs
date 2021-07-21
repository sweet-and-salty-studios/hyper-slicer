using System;
using System.Collections;
using UnityEngine;

namespace HyperSlicer.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip victoryClip = default;
        [SerializeField] private AudioClip sliceClip = default;
        [SerializeField] private AudioClip explosionClip = default;
        [Space]
        [SerializeField] private AudioSource musicSource = default;

        private void Awake()
        {
            GameManager.LevelLoaded += OnLevelLoaded;
            GameManager.LevelComplete += OnLevelComplete;
            GameManager.GameOver += OnGameOver;
            GameManager.ScoreAdded += OnScoreAdded;
        }

        private void OnDestroy()
        {
            GameManager.LevelLoaded -= OnLevelLoaded;
            GameManager.LevelComplete -= OnLevelComplete;
            GameManager.GameOver -= OnGameOver;
            GameManager.ScoreAdded -= OnScoreAdded;
        }

        private void OnLevelLoaded(LevelInfo levelInfo)
        {
            musicSource.Play();
        }

        private void OnScoreAdded(LevelInfo levelInfo)
        {
            AudioSource.PlayClipAtPoint(sliceClip, levelInfo.Saw.transform.position);
        }

        private void OnGameOver(LevelInfo levelInfo)
        {
            musicSource.Stop();

            AudioSource.PlayClipAtPoint(explosionClip, levelInfo.Saw.transform.position);
        }

        private void OnLevelComplete(LevelInfo levelInfo)
        {
            musicSource.Stop();

            StartCoroutine(IDelayAction(() =>
            {
                AudioSource.PlayClipAtPoint(victoryClip, levelInfo.Saw.transform.position);
            }));
        }

        private IEnumerator IDelayAction(Action action)
        {
            yield return new WaitForSeconds(4);

            if(action != null)
                action.Invoke();
        }
    }
}