using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlightDestination
{
    public Transform Target;
    public float Speed;
    public float DelayOnTarget;

    public FlightDestination(Transform target, float speed, float delayOnTarget = 0)
    {
        Target = target; 
        Speed = speed;
        DelayOnTarget = delayOnTarget;
    }
}
