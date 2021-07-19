using HyperSlicer.UI;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private LevelProgressDisplay levelProgressDisplay = default;

    public LevelProgressDisplay LevelProgressDisplay { get => levelProgressDisplay; }
}
