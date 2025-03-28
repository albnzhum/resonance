using System;
using UnityEngine;

public class LeavesAction : MonoBehaviour, ITutorialAction
{
    public event Action OnActionCompleted;
    private bool _isCompleted = false;
    
    [SerializeField] private Leaves _leaves;

    public void StartAction()
    {
        _isCompleted = false;

        _leaves.OnInteract += RustleLeaves;
    }

    public void RustleLeaves() // Вызывается при взаимодействии с листьями
    {
        if (!_isCompleted)
        {
            _isCompleted = true;
            OnActionCompleted?.Invoke();
            
            _leaves.OnInteract -= RustleLeaves;
        }
    }
}