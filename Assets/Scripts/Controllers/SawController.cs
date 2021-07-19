using System;
using UnityEngine;
using Utilities.MeshSlicing;

namespace Tower.Controllers
{
    public class SawController : MonoBehaviour
    {
        [SerializeField] private Material crossSectionMaterial = default;
        [SerializeField] private float sliceImpulseForceMultiplier = default;
        [SerializeField] private float rotateSpeedMultiplier = default;
        
        private Rigidbody rb = default;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Deactivate();
        }

        private void Update()
        {
            transform.Rotate(Vector3.down * rotateSpeedMultiplier);
        }

        public void Activate()
        {
            rb.useGravity = true;
        }

        public void Deactivate()
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
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
        }

        private void SliceObject(GameObject sliceableGameObject)
        {
            var sliceableHull = sliceableGameObject.Slice(transform.position, transform.up, crossSectionMaterial);
            if(sliceableHull == null)
                return;

            var upperObject = sliceableHull.CreateUpperHull(sliceableGameObject, crossSectionMaterial);
            var lowerObject = sliceableHull.CreateLowerHull(sliceableGameObject, crossSectionMaterial);

            var sliceableParent = sliceableGameObject.transform.parent;
            upperObject.transform.SetParent(sliceableParent, false);
            //upperObject.layer = sliceableGameObject.layer;
            var rigidbody = upperObject.AddComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.right + Vector3.up * sliceImpulseForceMultiplier, ForceMode.Impulse);
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            lowerObject.transform.SetParent(sliceableParent, false);
            //lowerObject.layer = sliceableGameObject.layer;
            rigidbody = lowerObject.AddComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.left + Vector3.up * sliceImpulseForceMultiplier, ForceMode.Impulse);
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            sliceableGameObject.SetActive(false);
            Destroy(sliceableGameObject, 1);
            Destroy(upperObject, 4);
            Destroy(lowerObject, 4);
        }
    }
}