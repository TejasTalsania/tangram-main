using UnityEngine;
using Interface;

public class RotateController : MonoBehaviour, IRotatable
{
    [SerializeField] private float rotationStep = 45f;

    public void Rotate()
    {
        transform.Rotate(Vector3.up, rotationStep);
    }
}
