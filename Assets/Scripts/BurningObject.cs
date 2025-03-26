using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningObject : MonoBehaviour
{
    [SerializeField] ParticleSystem _burn;
    [SerializeField] float _burnDuration;

    public Action onBurn;

    public void Burn()
    {
        onBurn?.Invoke();
        StartCoroutine(BurnCoroutine());
    }

    private IEnumerator BurnCoroutine()
    {
        _burn.Play();
        yield return new WaitForSecondsRealtime(_burnDuration);
        Destroy(gameObject);
    }
}
