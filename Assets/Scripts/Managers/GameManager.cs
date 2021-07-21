using HyperSlicer.Controllers;
using System;
using System.Collections;
using UnityEngine;

namespace HyperSlicer.Managers
{
    public class LevelInfo
    {
        public LevelInfo(SawController saw, int helixDistance = 10, int currentLevelIndex = 1, int score = 0)
        {
            Saw = saw;
            HelixDistance = helixDistance;
            CurrentLevelIndex = currentLevelIndex;
            CurrentScore = score;
            
            HelixCount = 3 + currentLevelIndex;
        }
        
        public SawController Saw { get; }
        public int HelixCount { get; }
        public int HelixDistance { get; }
        public int CurrentLevelIndex { get; }
        public int CurrentScore { get; set; }
        public float StartDistance { get; set; }
        public float CurrentDistance { get; set; }
        public bool IsGameRunning { get; set; }
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SawController sawController = default;
        [SerializeField] private HelixTowerController helixTowerController = default;

        public static event Action<LevelInfo> LevelLoaded = default;
        public static event Action<LevelInfo> GameOver = default;
        public static event Action<LevelInfo> LevelComplete = default;
        public static event Action<LevelInfo> ScoreAdded = default;
        public static event Action<LevelInfo> ScoreMultiplied = default;
        public static event Action<LevelInfo> LevelProgressUpdated = default;

        public static LevelInfo LevelInfo { get; private set; } = default;

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
                sawController,
                10,
                PlayerPrefs.GetInt("Level", 1),
                PlayerPrefs.GetInt("Score", 0));

            LevelLoaded?.Invoke(LevelInfo);
            ScoreAdded?.Invoke(LevelInfo);

            StartCoroutine(IStart());

            LevelInfo.StartDistance = (helixTowerController.HelixFloorEnd.transform.position - sawController.transform.position).magnitude;
            LevelInfo.CurrentDistance = LevelInfo.StartDistance;
        }

        private void Update()
        {
            if(LevelInfo == null) return;
            if(LevelInfo.IsGameRunning == false) return;

            CalculateLevelDistance(LevelInfo);

#if UNITY_EDITOR
            MouseControl();
#else
            TouuchControls();
#endif
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
            levelInfo.IsGameRunning = false;
        }

        private IEnumerator IStart()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            LevelInfo.IsGameRunning = true;
        }
       
        private void OnLevelComplete(LevelInfo levelInfo)
        {
            levelInfo.IsGameRunning = false;
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
            if(multiplier == 0) multiplier = 1;

            LevelInfo.CurrentScore *= multiplier;

            ScoreMultiplied?.Invoke(LevelInfo);
        }

        private void CalculateLevelDistance(LevelInfo levelInfo)
        {
            levelInfo.CurrentDistance = (helixTowerController.HelixFloorEnd.transform.position - sawController.transform.position).magnitude;

            LevelProgressUpdated(levelInfo);
        }
    }
}