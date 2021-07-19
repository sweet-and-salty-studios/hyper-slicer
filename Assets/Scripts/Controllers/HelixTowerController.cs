using System;
using UnityEngine;

namespace HyperSlicer.Controllers
{
    public class HelixTowerController : MonoBehaviour
    {
        [SerializeField] private Transform helixFloorsContainer = default;
        [SerializeField] private float rotationSpeed = default;

        public void Rotate(float x, float y, float v3)
        {
            helixFloorsContainer.Rotate(x, y * rotationSpeed * Time.deltaTime, v3);
        }
    }
}