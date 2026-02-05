using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;

public class Piece : MonoBehaviour
{
    private IDraggable draggable;
    private IRotatable rotatable;
    private ISnappable snappable;
    private DragController dragController;
    private SnapController snapController;
    
    private bool isDragging;
    private bool isDragAvailable = true;

    private void OnEnable()
    {
        LevelManager.OnLevelCompleted += LevelCompleted;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelCompleted -= LevelCompleted;
    }

    private void LevelCompleted()
    {
        isDragAvailable = false;
    }

    private void Awake()
    {
        draggable = GetComponent<IDraggable>();
        rotatable = GetComponent<IRotatable>();
        snappable = GetComponent<ISnappable>();
        dragController = GetComponentInParent<DragController>();
        snapController = GetComponentInParent<SnapController>();
    }

    private void OnMouseDown()
    {
        if (!isDragAvailable) return;
        isDragging = true;
        SoundManager.Instance.PlayDragStartSound();
        draggable?.BeginDrag(InputRaycaster.GetMouseRay());
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;
        draggable?.Drag(InputRaycaster.GetMouseRay());
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;
        
        draggable?.EndDrag();
        bool snapped = snappable != null && snappable.TrySnap();

        if (!snapped)
        {
            SoundManager.Instance.PlaySnapToOriginalPos();
            dragController?.RestoreLastPosition();
            snapController?.ReleaseSnapIfOccupied();
        }
        
        isDragging = false;
    }

    private void Update()
    {
        if (!isDragging) return;
        // Rotate by pressing R key in keyboard...
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotatable?.Rotate();
        }
    }
}
