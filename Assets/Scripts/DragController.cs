using System;
using DG.Tweening;
using UnityEngine;
using Interface;

public class DragController : MonoBehaviour, IDraggable
{
    private Plane dragPlane;
    private Vector3 offset;
    private bool isDragging;
    
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private Vector3 dragPosition;

    private void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    public void BeginDrag(Ray ray)
    {
        dragPlane = new Plane(Vector3.up, transform.position);

        if (dragPlane.Raycast(ray, out float enter))
        {
            offset = transform.position - ray.GetPoint(enter);
        }
    }

    public void Drag(Ray ray)
    {
        if (dragPlane.Raycast(ray, out float enter))
        {
            dragPosition = ray.GetPoint(enter) + offset;
            dragPosition.y = 0.05F;
            transform.position = dragPosition;
        }
    }

    public void EndDrag()
    {
        // currently nothing is happening here but we can add reliable code here if required so not removed...
    }

    public void RestoreLastPosition()
    {
        transform.DOMove(lastPosition, 0.1F).SetEase(Ease.Linear);
        transform.DORotateQuaternion(lastRotation, 0.1F).SetEase(Ease.Linear);
    }
}
