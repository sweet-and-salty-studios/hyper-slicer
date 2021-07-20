using HyperSlicer.Behaviours;
using HyperSlicer.Managers;
using System;
using UnityEngine;

namespace HyperSlicer.Controllers
{
    public class HelixTowerController : MonoBehaviour
    {
        [SerializeField] private RotationBehaviour rotationBehaviour = default;

        public RotationBehaviour RotationBehaviour { get => rotationBehaviour; }

        [SerializeField] private HelixFloorBehaviour[] helixFloors = default;
        public HelixFloorEndBehaviour HelixFloorEnd { get; private set; } = default;

        private void Awake()
        {
            GameManager.GameOver += OnGameOver;
            GameManager.LevelComplete += OnLevelComplete;

            helixFloors = GetComponentsInChildren<HelixFloorBehaviour>(true);
            HelixFloorEnd = GetComponentInChildren<HelixFloorEndBehaviour>(true);
        }

        private void OnDestroy()
        {
            GameManager.GameOver -= OnGameOver;
            GameManager.LevelComplete -= OnLevelComplete;
        }

        private void OnGameOver()
        {
            Destroy(this);
        }

        private void OnLevelComplete()
        {
            Destroy(this);
        }
    }
}