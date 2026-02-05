using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SnapPoint[] allPieces;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private GameObject confettiShower;
    public static event Action OnLevelCompleted;

    private void OnEnable()
    {
        SnapController.OnAnyPieceSnapChanged += IsAnyPieceSnapChanged;
    }

    private void OnDisable()
    {
        SnapController.OnAnyPieceSnapChanged -= IsAnyPieceSnapChanged;
    }

    private void IsAnyPieceSnapChanged()
    {
        CheckForLevelComplete();
    }

    private void CheckForLevelComplete()
    {
        // All snap point is occupied then level completed...
        bool isCompleted = true;
        for (int i = 0; i < allPieces.Length; i++)
        {
            if (!allPieces[i].IsOccupied)
            {
                isCompleted = false;
                break;
            }
        }

        if (!isCompleted) return;
        OnLevelCompleted?.Invoke();
        SoundManager.Instance.PlayLevelComplete();
        cameraAnimator.SetTrigger("LevelComplete");
        confettiShower.SetActive(true);
    }
}
