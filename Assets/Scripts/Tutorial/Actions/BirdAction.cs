using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAction : MonoBehaviour, ITutorialAction
{
    [SerializeField] private Bird bird;
    
    public event Action OnActionCompleted;
    private bool isCompleted = false;
    public void StartAction()
    {
        isCompleted = false;
        bird.OnStartFly += OnStartFly;
        bird.OnEndFly += OnEndFly;
    }

    private void OnEndFly()
    {
        isCompleted = true;
        OnActionCompleted?.Invoke();
    }

    private void OnStartFly()
    {
        bird.OnStartFly -= OnStartFly;
    }
}
