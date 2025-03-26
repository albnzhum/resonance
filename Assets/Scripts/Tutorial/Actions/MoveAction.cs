using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour, ITutorialAction
{
    public event Action OnActionCompleted;
    private bool _isCompleted = false;

    public void StartAction()
    {
        _isCompleted = false;
    }

    private void Update()
    {
        if (!_isCompleted && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
                              Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            _isCompleted = true;
            OnActionCompleted?.Invoke();
        }
    }
}
