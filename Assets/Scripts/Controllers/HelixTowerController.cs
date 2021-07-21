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
            GameManager.GameOver += OnGameOver;
            GameManager.LevelComplete += OnLevelComplete;

            HelixFloorEnd = GetComponentInChildren<HelixFloorEndBehaviour>(true);
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