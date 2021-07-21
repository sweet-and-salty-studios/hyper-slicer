using System.Collections.Generic;
using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class HelixFloorBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform middleColumn;
        [SerializeField] private List<Transform> slicableSpawnPoints = new List<Transform>();

        protected readonly List<PieceBehaviour> pieces = new List<PieceBehaviour>();

        protected virtual void Awake()
        {
            pieces.AddRange(GetComponentsInChildren<PieceBehaviour>());
        }

        protected virtual void Start()
        {
            RandomizePieces();
            RemovePieces(Random.Range(4, pieces.Count - 1));
        }

        protected void RemovePieces(int amount)
        {
            for(var i = 0; i < amount; i++)
            {
                var piece = pieces[i];
                piece.gameObject.SetActive(false);
            }
        }

        protected void RandomizePieces()
        {
            for(int i = 0; i < pieces.Count; i++)
            {
                var temp = pieces[i];
                var randomIndex = Random.Range(i, pieces.Count);
                pieces[i] = pieces[randomIndex];
                pieces[randomIndex] = temp;
            }
        }

        public void RandomizeSlicables(SlicableBehaviour prefab)
        {
            var lastSlicableY = 0f;

            foreach(var slicableSpawnPoint in slicableSpawnPoints)
            {
                for(int i = 0; i < 4; i++)
                {
                    var slicable = Instantiate(prefab, Vector3.zero, Random.rotation, slicableSpawnPoint);

                    slicable.transform.localPosition = Vector3.up * lastSlicableY;

                    lastSlicableY = slicable.transform.localPosition.y - 2;
                }
            }
        }
    }
}