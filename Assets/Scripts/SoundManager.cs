using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clipDragStart;
    [SerializeField] private AudioClip clipSnapToOriginalPos;
    [SerializeField] private AudioClip clipSnapToPoint;
    [SerializeField] private AudioClip clipLevelComplete;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private static bool IsSoundOn()
    {
        return PlayerPrefs.GetInt("IsSoundAvailable", 1) == 1;
    }

    public void PlayDragStartSound()
    {
        if (!IsSoundOn()) return;
        audioSource.PlayOneShot(clipDragStart);
    }
    
    public void PlaySnapToOriginalPos()
    {
        if (!IsSoundOn()) return;
        audioSource.PlayOneShot(clipSnapToOriginalPos);
    }
    
    public void PlaySnapToPoint()
    {
        if (!IsSoundOn()) return;
        audioSource.PlayOneShot(clipSnapToPoint);
    }
    
    public void PlayLevelComplete()
    {
        if (!IsSoundOn()) return;
        audioSource.PlayOneShot(clipLevelComplete);
    }
}
