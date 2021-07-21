using HyperSlicer.Managers;
using System;
using UnityEngine;

namespace HyperSlicer.Behaviours
{
    [Serializable]
    public class HelixEndPieceInfo
    {
        [SerializeField] private int scoreMultiplier = default;
        [SerializeField] private Color activeColor = default;

        public int ScoreMultiplier { get => scoreMultiplier; }
        public Color ActiveColor { get => activeColor; }
    }

    public class HelixFloorEndBehaviour : HelixFloorBehaviour
    {
        [SerializeField] private RotationBehaviour rotationBehaviour = default;
        [SerializeField] private HelixEndPieceInfo[] pieceInfos = new HelixEndPieceInfo[8];

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
            RandomizePieces();

            for(var i = 0; i < pieces.Count; i++)
            {
                var comboPiece = pieces[i] as ComboPieceBehaviour;
                if(comboPiece == null)
                    continue;

                comboPiece.UpdateScoreMultiplierText(pieceInfos[i].ScoreMultiplier);
                comboPiece.UpdateColors(pieceInfos[i].ActiveColor);
            }
        }

        private void Update()
        {
            if(rotationBehaviour == null) return; 
                
            rotationBehaviour.Rotate(Vector3.up);
        }
    }
}