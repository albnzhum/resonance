using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimationController : MonoBehaviour
{
    public Action onTakeOffEnd;
    public Action onLand;
    public Action onLandEnd;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();   
    }

    public void OnTakeOffEnd()
    {
        onTakeOffEnd?.Invoke();
    }

    public void OnLandEnd()
    {
        onLandEnd?.Invoke();
    }

    public void OnLand()
    {
        onLand?.Invoke();
    }

    public void Fly()
    {
        _animator.SetTrigger("Fly");
    }
    
    public void TakeOff()
    {
        _animator.SetTrigger("TakeOff");
    }

    public void Land()
    {
        _animator.SetTrigger("Land");
    }
}
