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
            Initialize();
        }

        private void Initialize()
        {
            for(int i = 0; i < pieces.Count; i++)
            {
                var temp = pieces[i];
                var randomIndex = Random.Range(i, pieces.Count);
                pieces[i] = pieces[randomIndex];
                pieces[randomIndex] = temp;
            }

            for(var i = 0; i < pieces.Count; i++)
            {
                var comboPiece = pieces[i] as ComboPieceBehaviour;
                if(comboPiece == null)
                    continue;

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