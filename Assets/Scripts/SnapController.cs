using System;
using UnityEngine;
using System.Collections.Generic;
using Interface;
using DG.Tweening;

public class SnapController : MonoBehaviour, ISnappable
{
    [SerializeField] private List<SnapPoint> snapPoints;
    [SerializeField] private float snapDistance = 0.3f;
    [SerializeField] private float snapAngle = 5f;
    
    private SnapPoint currentSnapPoint;
    // for square or Parallelogram some angle looks same so that this is required...
    [SerializeField] private float[] allowedYRotations;
    
    public static event Action OnAnyPieceSnapChanged;

    public bool TrySnap()
    {
        SnapPoint bestPoint = null;
        float bestScore = float.MaxValue;

        foreach (var point in snapPoints)
        {
            // if point is occupied and current snap point of this piece is not same then do nothing as this point is already occupied by some other piece... 
            if (point.IsOccupied && point != currentSnapPoint)
                continue;

            // if distance and angle matched then only snap to point...
            float distance = Vector3.Distance(transform.position, point.transform.position);
            if (distance > snapDistance)
                continue;

            bool rotationMatch = false;

            foreach (float rot in allowedYRotations)
            {
                Quaternion validRotation = point.transform.rotation * Quaternion.Euler(0, rot, 0);

                if (Quaternion.Angle(transform.rotation, validRotation) <= snapAngle)
                {
                    rotationMatch = true;
                    break;
                }
            }

            if (!rotationMatch)
                continue;
            
            bestPoint = point;
        }

        if (bestPoint == null)
            return false;

        // Piece snaps to it's associated snap point...
        Vector3 pos = bestPoint.transform.position;
        pos.y = 0F;
        transform.DOMove(pos, 0.1F).SetEase(Ease.Linear);
        transform.rotation = bestPoint.transform.rotation;

        // if already snapped piece snaps to another point then we have to unoccupy last snap...
        if (currentSnapPoint != null && currentSnapPoint != bestPoint)
        {
            currentSnapPoint.Release();
        }
        
        SoundManager.Instance.PlaySnapToPoint();
        bestPoint.Occupy();
        currentSnapPoint = bestPoint;
        OnAnyPieceSnapChanged?.Invoke();
        
        return true;
    }

    public void ReleaseSnapIfOccupied()
    {
        if (currentSnapPoint != null)
        {
            currentSnapPoint.Release();
            OnAnyPieceSnapChanged?.Invoke();
        }
    }
}
