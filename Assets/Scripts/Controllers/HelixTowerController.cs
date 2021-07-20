using HyperSlicer.Behaviours;
using HyperSlicer.Managers;
using UnityEngine;

namespace HyperSlicer.Controllers
{
    public class HelixTowerController : MonoBehaviour
    {
        [SerializeField] private RotationBehaviour rotationBehaviour = default;

        public RotationBehaviour RotationBehaviour { get => rotationBehaviour; }

        private void Awake()
        {
            GameManager.GameOver += OnGameOver;
            GameManager.LevelComplete += OnLevelComplete;
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