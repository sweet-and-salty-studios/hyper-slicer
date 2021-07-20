using UnityEngine;
using HyperSlicer.Utilities.MeshSlicing;
using HyperSlicer.Behaviours;
using HyperSlicer.Managers;

namespace HyperSlicer.Controllers
{
    public class SawController : MonoBehaviour
    {
        [SerializeField] private Material crossSectionMaterial = default;
        [SerializeField] private RotationBehaviour rotationBehaviour = default;

        public AntiGravityBehaviour AntiGravity { get; private set; } = default;

        private void Awake()
        {
            AntiGravity = GetComponent<AntiGravityBehaviour>();

            GameManager.GameOver += OnGameOver;
            GameManager.LevelComplete += OnLevelComplete;
        }

        private void OnDestroy()
        {
            GameManager.GameOver -= OnGameOver;
            GameManager.LevelComplete -= OnLevelComplete;
        }

        private void OnGameOver()
        {
            gameObject.SetActive(false);
        }
        private void OnLevelComplete()
        {
            AntiGravity.Shutdown();
        }

        private void Update()
        {
            rotationBehaviour.Rotate(Vector3.up);
        }

        private void OnTriggerEnter(Collider other)
        {
            switch(other.gameObject.layer)
            {
                case 8:
                    GameManager.EndGame();
                    break;
                case 10:
                    var comboPiece = other.GetComponent<ComboPieceBehaviour>();
                    if(comboPiece == null)
                        return;
                    transform.position = other.ClosestPointOnBounds(transform.position);

                    GameManager.CompleteLevel();
                    break;
                default:
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            switch(other.gameObject.layer)
            {
                case 9:
                    GameManager.ModifyScore(1);
                    SliceObject(other.gameObject);
                    break;
                default:
                    break;
            }
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