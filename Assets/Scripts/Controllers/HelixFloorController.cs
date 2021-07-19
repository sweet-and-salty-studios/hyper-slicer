using System.Collections.Generic;
using Tower.Behaviours;
using UnityEngine;

namespace Tower.Controllers
{
    public class HelixFloorController : MonoBehaviour
    {
        private readonly List<PieceBehaviour> pieces = new List<PieceBehaviour>();

        private void Awake()
        {
            pieces.AddRange(GetComponentsInChildren<PieceBehaviour>());
        }

        private void Start()
        {
            for(var i = 0; i < Random.Range(1, 4); i++)
            {
                var piece = pieces[i];
                piece.gameObject.SetActive(false);
                pieces.Remove(piece);
            }
        }
    }

}