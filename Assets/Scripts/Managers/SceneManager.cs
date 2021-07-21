using System;
using System.Collections;
using UnityEngine;

namespace HyperSlicer.Managers
{
    public class SceneManager : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.GameOver += LoadCurrentScene;
        }

        private void OnDestroy()
        {
            GameManager.GameOver -= LoadCurrentScene;
        }

        private IEnumerator IDelayAction(Action action)
        {
            yield return new WaitForSeconds(4);

            if(action != null)
                action.Invoke();
        }

        private void LoadCurrentScene(LevelInfo levelInfo)
        {
            StartCoroutine(IDelayAction(() => LoadCurrentScene()));
        }

        public void LoadCurrentScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitApplication()
        {
#if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}