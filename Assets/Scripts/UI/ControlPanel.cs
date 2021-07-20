using UnityEngine;

namespace HyperSlicer.UI
{
    public class ControlPanel : MonoBehaviour
    {
        [SerializeField] private ScoreDisplay scoreDisplay = default;
        [SerializeField] private LevelProgressDisplay levelProgressDisplay = default;

        public ScoreDisplay ScoreDisplay { get => scoreDisplay; }
        public LevelProgressDisplay LevelProgressDisplay { get => levelProgressDisplay; }
    }
}