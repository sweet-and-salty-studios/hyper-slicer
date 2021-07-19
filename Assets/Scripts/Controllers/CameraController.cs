using UnityEngine;

namespace HyperSlicer.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target = default;
        [SerializeField] private float smoothTime = default;
        [SerializeField] private Vector3 offset = default;

        private Vector3 currentPosition = default;
        private Vector3 targetPosition = default;
        private Vector3 velocity = default;

        private void Update()
        {
            currentPosition = transform.position;
            targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime);
        }
    }
}