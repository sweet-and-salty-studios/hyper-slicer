using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class DestroyBehaviour : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        //private void OnBecameVisible()
        //{
        //    Debug.LogWarning("OnBecameVisible");
        //}
    }
}