using HyperSlicer.Controllers;
using System.Collections;
using UnityEngine;

namespace HyperSlicer.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SawController sawController = default;
        [SerializeField] private HelixTowerController helixTowerController = default;
        [SerializeField] private float rotationSpeed = default;
        private bool isGameRunning = default;
        private readonly string horizontalMouseAxis = "Mouse X";

        private IEnumerator IStart()
        {
#if UNITY_EDITOR
            Cursor.lockState = CursorLockMode.Locked;
#endif
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            isGameRunning = true;
        }

        private void Start()
        {
            StartCoroutine(IStart());
        }

        private void Update()
        {
            if(isGameRunning == false) return;

            if(Input.GetMouseButtonDown(0))
            {
                sawController.AntiGravity.Activate();
            }

            if(Input.GetMouseButton(0))
            {
                helixTowerController.transform.Rotate(0, Input.GetAxisRaw(horizontalMouseAxis) * rotationSpeed * Time.deltaTime, 0);
            }

            if(Input.GetMouseButtonUp(0))
            {
                sawController.AntiGravity.Deactivate();
            }
        }
    }
}