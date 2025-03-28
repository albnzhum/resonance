using System;
using UnityEngine;


public class BirdLandedAction : MonoBehaviour, ITutorialAction
{
    [SerializeField] private Bird bird;
    
    public event Action OnActionCompleted;
    private bool isCompleted = false;
    public void StartAction()
    {
        isCompleted = false;
        //bird.OnEndFly += OnEndFly;
    }

    private void OnEndFly()
    {
        isCompleted = true;
        //OnActionCompleted?.Invoke();
        
       // bird.OnEndFly -= OnEndFly;
    }
}