using HyperSlicer.Behaviours;
using HyperSlicer.Managers;
using System;
using UnityEngine;

namespace HyperSlicer.Controllers
{
    public class HelixTowerController : MonoBehaviour
    {
        [Space]
        [Header("References")]
        [SerializeField] private HelixFloorStartBehaviour helixFloorStartPrefab = default;
        [SerializeField] private HelixFloorBehaviour helixFloorPrefab = default;
        [SerializeField] private HelixFloorEndBehaviour helixFloorEndPrefab = default;
        [SerializeField] private SlicableBehaviour baseSlicablePrefab = default;
        [SerializeField] private Transform HelixFloorsContainer = default;
        [SerializeField] private RotationBehaviour rotationBehaviour = default;

        public RotationBehaviour RotationBehaviour { get => rotationBehaviour; }

        public HelixFloorEndBehaviour HelixFloorEnd { get; private set; } = default;

        private void Awake()
        {
            GameManager.LevelLoaded += OnLevelLoaded;
            GameManager.GameOver += OnGameOver;
            GameManager.LevelComplete += OnLevelComplete;

            HelixFloorEnd = GetComponentInChildren<HelixFloorEndBehaviour>(true);
        }

        private void OnDestroy()
        {
            GameManager.LevelLoaded -= OnLevelLoaded;
            GameManager.GameOver -= OnGameOver;
            GameManager.LevelComplete -= OnLevelComplete;
        }

        private void OnGameOver(LevelInfo levelInfo)
        {
            Destroy(this);
        }

        private void OnLevelComplete(LevelInfo levelInfo)
        {
            Destroy(this);
        }
        private void OnLevelLoaded(LevelInfo levelInfo)
        {
            CreateLevel(levelInfo);
        }

        public void CreateLevel(LevelInfo levelInfo)
        {
            var helixHeight = -levelInfo.HelixDistance;
            var lastHelixHeight = 0f;

            Instantiate(helixFloorStartPrefab, HelixFloorsContainer);

            for(var i = 0; i < levelInfo.HelixCount; i++)
            {
                var helixFloorIntsance = Instantiate(helixFloorPrefab, Vector3.up * helixHeight, Quaternion.identity, HelixFloorsContainer);

                helixFloorIntsance.RandomizeSlicables(baseSlicablePrefab);

                helixHeight -= levelInfo.HelixDistance;

                lastHelixHeight = helixHeight;
            }

            HelixFloorEnd = Instantiate(helixFloorEndPrefab, Vector3.up * lastHelixHeight, Quaternion.identity, transform);
        }
    }
}