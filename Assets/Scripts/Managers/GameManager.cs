using HyperSlicer.Controllers;
using System;
using System.Collections;
using UnityEngine;

namespace HyperSlicer.Managers
{
    [Serializable]
    public class LevelInfo
    {
        [SerializeField] private int helixCount = default;
        [SerializeField] [Range(5, 20)] private int helixDistance = default;

        public int HelixCount { get => helixCount; }
        public int HelixDistance { get => helixDistance; }
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] LevelInfo levelInfo = default;
        [SerializeField] private SawController sawController = default;
        [SerializeField] private HelixTowerController helixTowerController = default;

        private bool isGameRunning = default;

        public static event Action GameOver = default;
        public static event Action LevelComplete = default;
        public static event Action<int> ScoreModified = default;
        public static event Action<float, float> LevelProgressUpdated = default;

        private static int score = default;
        private float currentLevelDistance = default;
        private float startLevelDistance = default;

        private void Awake()
        {
            GameOver += OnGameOver;
            LevelComplete += OnGameOver;
        }

        private void OnDestroy()
        {
            GameOver -= OnGameOver;
            LevelComplete -= OnGameOver;
        }

        private void Start()
        {
            helixTowerController.CreateLevel(levelInfo);

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
        private void OnGameOver()
        {
            isGameRunning = false;
        }

        private IEnumerator IStart()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            isGameRunning = true;
        }

        public static void CompleteLevel()
        {
            LevelComplete?.Invoke();
        }

        public static void EndGame()
        {
            GameOver?.Invoke();
        }

        public static void ModifyScore(int amount)
        {
            score += amount;

            ScoreModified?.Invoke(score);
        }

        public static void MultipliedScore(int multiplier)
        {
            score *= multiplier;

            ScoreModified?.Invoke(score);
        }

        public void CalculateLevelDistance()
        {
            currentLevelDistance = (helixTowerController.HelixFloorEnd.transform.position - sawController.transform.position).magnitude;

            LevelProgressUpdated(startLevelDistance, currentLevelDistance);
        }
    }
}