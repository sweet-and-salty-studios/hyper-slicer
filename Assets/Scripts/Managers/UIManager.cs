using HyperSlicer.Utilities.Helpers;
using UnityEngine;

namespace HyperSlicer.Managers
{
    public class UIManager : Singelton<UIManager>
    {
        [SerializeField] private ControlPanel controlPanel = default;

        public ControlPanel ControlPanel { get => controlPanel; }
    }
}