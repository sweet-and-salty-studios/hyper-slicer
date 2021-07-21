using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class RotationBehaviour : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = default;

        public void Rotate(Vector3 direction)
        {
            if(direction == Vector3.zero) return;

            transform.Rotate(direction * rotationSpeed * Time.deltaTime);
        }
    }
}