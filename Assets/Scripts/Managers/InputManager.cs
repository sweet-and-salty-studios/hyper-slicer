using HyperSlicer.Utilities.Helpers;
using UnityEngine;

namespace HyperSlicer.Managers
{
    public class InputManager : Singelton<InputManager>
    {
        private readonly string horizontalMouseAxis = "Mouse X";

        public bool IsTouchDown { get => Input.GetMouseButtonDown(0); }
        public bool IsTouchHeld { get => Input.GetMouseButton(0); }
        public bool IsTouchUp { get => Input.GetMouseButtonUp(0); }
        public float HorizontalSwipeAxisRaw { get => Input.GetAxisRaw(horizontalMouseAxis); }
    }
}