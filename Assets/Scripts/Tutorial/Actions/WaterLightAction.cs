using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLightAction : MonoBehaviour, ITutorialAction
{
    private bool isCompleted = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isCompleted)
            {
                isCompleted = true;
                OnActionCompleted?.Invoke();
                
            }
        }
    }

    public event Action OnActionCompleted;
    public void StartAction()
    {
        isCompleted = false;
    }
}
