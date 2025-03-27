using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAction : MonoBehaviour, ITutorialAction
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject timeline;
    [SerializeField] private Animator timelineAnimator;
    
    public event Action OnActionCompleted;
    public void StartAction()
    {
        player.gameObject.SetActive(false);
        timeline.gameObject.SetActive(true);
    }

    public void EndAction()
    {
        timeline.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        OnActionCompleted.Invoke();
        
        Destroy(gameObject);
    }
}
