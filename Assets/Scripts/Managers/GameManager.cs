using Tower.Controllers;
using UnityEngine;

namespace Tower.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SawController sawController = default;
        [SerializeField] private HelixTowerController helixTowerController = default;
        [SerializeField] private float rotationSpeed = default;
        private readonly string horizontalMouseAxis = "Mouse X";

        private void Start()
        {
#if UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                sawController.Deactivate();
            }

            if(Input.GetMouseButton(0))
            {
                helixTowerController.transform.Rotate(0, Input.GetAxisRaw(horizontalMouseAxis) * rotationSpeed * Time.deltaTime, 0);
            }

            if(Input.GetMouseButtonUp(0))
            {
                sawController.Activate();
            }
        }
    }
}