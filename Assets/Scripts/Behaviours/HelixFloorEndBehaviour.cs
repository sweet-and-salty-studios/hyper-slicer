using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class HelixFloorEndBehaviour : HelixFloorBehaviour
    {
        [SerializeField] private int[] scoreMultipliers = new int[8];

        public string ScoreMultiplier { get; internal set; }

        protected override void Start()
        {
            for(int i = 0; i < pieces.Count; i++)
            {
                Debug.LogWarning(pieces[i], pieces[i].gameObject);
            }
        }
    }
}