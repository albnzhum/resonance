using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOnFire : MonoBehaviour
{
    ParticleSystem ps;
    bool isDestroy = false;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void ParticlesOnFire()
    {

        StartCoroutine(DestroyTree());
    }

    IEnumerator DestroyTree()
    {
        if (!isDestroy)
        {
            isDestroy = true;
            ps.Play();
            yield return new WaitForSeconds(ps.main.duration);
            Destroy(gameObject);
        }
    }
}
