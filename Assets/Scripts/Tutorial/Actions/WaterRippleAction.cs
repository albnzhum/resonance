using System;
using UnityEngine;

public class WaterRippleAction : MonoBehaviour, ITutorialAction
{
    public event Action OnActionCompleted;
    private bool _isCompleted = false;

    public void StartAction()
    {
        _isCompleted = false;
    }

    private void Update()
    {
        if (!_isCompleted)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SimulateRipple();
            }
        }
    }

    public void SimulateRipple() // Вызывается при ряби воды
    {
        if (!_isCompleted)
        {
            _isCompleted = true;
            OnActionCompleted?.Invoke();
        }
    }
}