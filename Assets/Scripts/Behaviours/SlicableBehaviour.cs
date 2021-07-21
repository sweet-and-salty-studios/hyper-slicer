using HyperSlicer.Behaviours;
using UnityEngine;

public class SlicableBehaviour : MonoBehaviour
{
    private RotationBehaviour rotationBehaviour = default;
    private Vector3 randomRotation = default;

    private void Awake()
    {
        rotationBehaviour = GetComponent<RotationBehaviour>();
    }

    private void Start()
    {
        randomRotation = Random.rotation.eulerAngles;
    }

    private void Update()
    {
        if(rotationBehaviour != null) rotationBehaviour.Rotate(randomRotation);
    }
}
