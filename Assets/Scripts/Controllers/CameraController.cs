using UnityEngine;

namespace Tower.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target = default;
        [SerializeField] private float movementSpeedMultiplier = default;
        [SerializeField] private float smoothTime = default;

        private Vector3 currentPosition = default;
        private Vector3 targetPosition = default;
        private Vector3 velocity = default;
        private float zOffset = default;

        private void Start()
        {
            zOffset = transform.position.z;
        }
        
        private void Update()
        {
            currentPosition = new Vector3(transform.position.x, transform.position.y, zOffset);
            targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, zOffset);
            transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime);
        }
    }
}