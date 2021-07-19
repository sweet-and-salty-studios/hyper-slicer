using HyperSlicer.Controllers;
using HyperSlicer.Utilities.Helpers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HyperSlicer.Managers
{
    public class GameManager : Singelton<GameManager>
    {
        [SerializeField] private SawController sawController = default;
        [SerializeField] private HelixTowerController helixTowerController = default;
        private bool isGameRunning = default;

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

            if(InputManager.Instance.IsTouchDown)
            {
                sawController.AntiGravity.Activate();
            }

            if(InputManager.Instance.IsTouchHeld)
            {
                helixTowerController.Rotate(0, InputManager.Instance.HorizontalSwipeAxisRaw, 0);
            }

            if(InputManager.Instance.IsTouchUp)
            {
                sawController.AntiGravity.Deactivate();
            }

            //UIManager.Instance.ControlPanel
        }

        public void LoadCurrentScene()
        {
            StartCoroutine(ILoadCurrentScene());
        }

        private IEnumerator ILoadCurrentScene()
        {
            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}