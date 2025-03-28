using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FocusTriggerExit : MonoBehaviour, ITutorialAction
{
    public event Action OnActionCompleted;

    private bool isCompleted = false;
    
    [SerializeField] private FocusState focusState = FocusState.Light;

    public void StartAction()
    {
        switch (focusState)
        {
            case FocusState.Light:
                isCompleted = false;
                break;
            case FocusState.Water:
                isCompleted = false;
                break;
            case FocusState.Leaves:
                isCompleted = false;
                break;
        }
    }
    
    private IEnumerator DelayBeforeNextStage()
    {
        yield return new WaitForSeconds(10f); // Ждём 4 секунды
        
        OnActionCompleted?.Invoke();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnActionCompleted?.Invoke();
        }
    }
}
