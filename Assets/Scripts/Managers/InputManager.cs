using UnityEngine;

namespace HyperSlicer.Managers
{
    public class InputManager : MonoBehaviour
    {
        private readonly static string horizontalMouseAxis = "Mouse X";

        public static bool IsTouchDown { get => Input.GetMouseButtonDown(0); }
        public static bool IsTouchHeld { get => Input.GetMouseButton(0); }
        public static bool IsTouchUp { get => Input.GetMouseButtonUp(0); }
        public static float HorizontalSwipeAxisRaw { get => Input.GetAxisRaw(horizontalMouseAxis); }
    }
}