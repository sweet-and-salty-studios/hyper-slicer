using UnityEngine;
using HyperSlicer.Utilities.MeshSlicing;
using HyperSlicer.Behaviours;
using HyperSlicer.Managers;

namespace HyperSlicer.Controllers
{
    public class SawController : MonoBehaviour
    {
        [SerializeField] private Material crossSectionMaterial = default;
        [SerializeField] private float rotateSpeedMultiplier = default;
        [SerializeField] private Transform modelTransform = default;
        
        public AntiGravityBehaviour AntiGravity { get; private set; } = default;

        private void Awake()
        {
            AntiGravity = GetComponent<AntiGravityBehaviour>();
        }

        private void Update()
        {
            modelTransform.Rotate(Vector3.up * rotateSpeedMultiplier);
        }

        private void OnTriggerEnter(Collider other)
        {
            switch(other.gameObject.layer)
            {
                case 8: Die(); break;
                default: break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch(other.gameObject.layer)
            {
                case 9: SliceObject(other.gameObject); break;
                default: break;
            }
        }

        private void Die()
        {
            gameObject.SetActive(false);

            GameManager.Instance.LoadCurrentScene();
        }

        private void SliceObject(GameObject sliceableGameObject)
        {
            var sliceableHull = sliceableGameObject.Slice(transform.position, transform.up, crossSectionMaterial);
            if(sliceableHull == null)
                return;

            var upperObject = sliceableHull.CreateUpperHull(sliceableGameObject, crossSectionMaterial);
            var lowerObject = sliceableHull.CreateLowerHull(sliceableGameObject, crossSectionMaterial);

            var sliceableParent = sliceableGameObject.transform.parent;
            sliceableGameObject.AddComponent<DestroyBehaviour>();
            sliceableGameObject.SetActive(false);

            upperObject.transform.SetParent(sliceableParent, false);
            var rigidbody = upperObject.AddComponent<Rigidbody>();
            upperObject.AddComponent<DestroyBehaviour>();
            rigidbody.AddForce(Vector3.right + Vector3.up * rigidbody.velocity.y, ForceMode.Impulse);
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            lowerObject.transform.SetParent(sliceableParent, false);
            lowerObject.AddComponent<DestroyBehaviour>();
            rigidbody = lowerObject.AddComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.left + Vector3.up * rigidbody.velocity.y, ForceMode.Impulse);
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }
}