using HyperSlicer.Managers;
using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class HelixFloorEndBehaviour : HelixFloorBehaviour
    {
        [SerializeField] private RotationBehaviour rotationBehaviour = default;
        [SerializeField] private int[] scoreMultipliers = new int[8];

        protected override void Awake()
        {
            base.Awake();

            GameManager.LevelComplete += OnLevelComplete;
        }

        private void OnDestroy()
        {
            GameManager.LevelComplete -= OnLevelComplete;
        }

        private void OnLevelComplete()
        {
            Destroy(rotationBehaviour);
        }

        protected override void Start()
        {
            for(var i = 0; i < pieces.Count; i++)
            {
                var comboPiece = pieces[i] as ComboPieceBehaviour;
                if(comboPiece == null) continue;

                comboPiece.UpdateScoreMultiplierText(scoreMultipliers[i]);
            }
        }

        private void Update()
        {
            if(rotationBehaviour == null) return; 
                
            rotationBehaviour.Rotate(Vector3.up);
        }
    }
}