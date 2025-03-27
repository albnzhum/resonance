using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("UI elements")] 
    [SerializeField] private GameObject _tutorialWindow;
    
    [SerializeField] private Animator animator;

    private bool isCompleted;

    public bool IsCompleted => isCompleted;

    public event Action OnStageCompleted;

    public void Show()
    {
        _tutorialWindow.SetActive(true);
    }

    public void Close()
    {
        animator.SetBool("IsClose", true);
        isCompleted = true;
    }

    // Этот метод вызывается из анимации
    public void CloseObject()
    {
        _tutorialWindow.SetActive(false);
        OnStageCompleted?.Invoke(); 
    }
}