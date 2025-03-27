using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    [SerializeField] private LightAction lightAction;
    [SerializeField] private Animator animator;

    public void StopTimeline()
    {
        lightAction.EndAction();
    }
}
