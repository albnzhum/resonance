using System;
using UnityEngine;

public class LeavesAction : MonoBehaviour, ITutorialAction
{
    public event Action OnActionCompleted;
    private bool _isCompleted = false;

    public void StartAction()
    {
        _isCompleted = false;
    }

    public void RustleLeaves() // Вызывается при взаимодействии с листьями
    {
        if (!_isCompleted)
        {
            _isCompleted = true;
            OnActionCompleted?.Invoke();
        }
    }
}