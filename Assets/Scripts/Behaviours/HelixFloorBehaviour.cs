using System.Collections.Generic;
using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class HelixFloorBehaviour : MonoBehaviour
    {
        protected readonly List<PieceBehaviour> pieces = new List<PieceBehaviour>();

        protected virtual void Awake()
        {
            var foo = GetComponentsInChildren<PieceBehaviour>();
            pieces.AddRange(foo);
        }

        protected virtual void Start()
        {
            RemovePieces(Random.Range(1, 4));
        }

        protected void RemovePieces(int amount)
        {
            for(var i = 0; i < amount; i++)
            {
                var piece = pieces[i];
                piece.gameObject.SetActive(false);
                pieces.Remove(piece);
            }
        }
    }
}