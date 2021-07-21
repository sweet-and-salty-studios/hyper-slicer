using HyperSlicer.Controllers;
using System;
using System.Collections;
using UnityEngine;

namespace HyperSlicer.Managers
{
    public class LevelInfo
    {
        public LevelInfo(int helixDistance = 10, int currentLevelIndex = 1, int score = 0)
        {
            HelixDistance = helixDistance;
            CurrentLevelIndex = currentLevelIndex;
            CurrentScore = score;
            
            HelixCount = 3 + currentLevelIndex;
        }
        
        public int HelixCount { get; }
        public int HelixDistance { get; }
        public int CurrentLevelIndex { get; }
        public int CurrentScore { get; set; }
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SawController sawController = default;
        [SerializeField] private HelixTowerController helixTowerController = default;

        private bool isGameRunning = default;

        public static event Action<LevelInfo> LevelLoaded = default;
        public static event Action<LevelInfo> GameOver = default;
        public static event Action<LevelInfo> LevelComplete = default;
        public static event Action<LevelInfo> ScoreAdded = default;
        public static event Action<LevelInfo> ScoreMultiplied = default;
        public static event Action<float, float> LevelProgressUpdated = default;

        public static LevelInfo LevelInfo { get; private set; } = default;

        private float currentLevelDistance = default;
        private float startLevelDistance = default;

        private void Awake()
        {
            GameOver += OnGameOver;
            LevelComplete += OnLevelComplete;
        }

        private void OnDestroy()
        {
            GameOver -= OnGameOver;
            LevelComplete -= OnLevelComplete;
        }

        private void Start()
        {
            LevelInfo = new LevelInfo(
                10, 
                PlayerPrefs.GetInt("Level", 1),
                PlayerPrefs.GetInt("Score", 0));
            LevelLoaded?.Invoke(LevelInfo);
            ScoreAdded?.Invoke(LevelInfo);

            currentLevelDistance = (helixTowerController.HelixFloorEnd.transform.position - sawController.transform.position).magnitude;
            startLevelDistance = currentLevelDistance;

            StartCoroutine(IStart());
        }

        private void Update()
        {
            if(isGameRunning == false)
                return;

#if UNITY_EDITOR
            MouseControl();
#else
            TouuchControls();
#endif
            CalculateLevelDistance();
        }

#if UNITY_EDITOR
        private void MouseControl()
        {
            if(InputManager.IsTouchDown)
            {
                if(sawController != null)
                    sawController.AntiGravity.Activate();
            }

            if(InputManager.IsTouchHeld)
            {
                if(helixTowerController == null)
                    return;
                helixTowerController.RotationBehaviour.Rotate(Vector3.up * -InputManager.HorizontalSwipeAxisRaw);
            }

            if(InputManager.IsTouchUp)
            {
                if(sawController != null)
                    sawController.AntiGravity.Deactivate();
            }
        }
#else
        private void TouuchControls()
        {
            var currentTouch = Input.GetTouch(0);
            switch(currentTouch.phase)
            {
                case TouchPhase.Began:
                    if(sawController != null)
                        sawController.AntiGravity.Activate();
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if(helixTowerController != null)
                        helixTowerController.RotationBehaviour.Rotate(Vector3.up * -InputManager.HorizontalSwipeAxisRaw);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if(sawController != null)
                        sawController.AntiGravity.Deactivate();
                    break;
                default:
                    break;
            }
        }
#endif
        private void OnGameOver(LevelInfo levelInfo)
        {
            isGameRunning = false;
        }

        private IEnumerator IStart()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            isGameRunning = true;
        }
       
        private void OnLevelComplete(LevelInfo levelInfo)
        {
            isGameRunning = false;
            PlayerPrefs.SetInt("Level", levelInfo.CurrentLevelIndex + 1);
            PlayerPrefs.SetInt("Score", levelInfo.CurrentScore);
        }

        public static void CompleteLevel()
        {
            LevelComplete?.Invoke(LevelInfo);
        }

        public static void EndGame()
        {
            GameOver?.Invoke(LevelInfo);
        }

        public static void ModifyScore(int amount)
        {
            LevelInfo.CurrentScore += amount;

            ScoreAdded?.Invoke(LevelInfo);
        }

        public static void MultipliedScore(int multiplier)
        {
            LevelInfo.CurrentScore *= multiplier;

            ScoreMultiplied?.Invoke(LevelInfo);
        }

        public void CalculateLevelDistance()
        {
            currentLevelDistance = (helixTowerController.HelixFloorEnd.transform.position - sawController.transform.position).magnitude;

            LevelProgressUpdated(startLevelDistance, currentLevelDistance);
        }
    }
}