using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOnFire : MonoBehaviour
{
    bool isDestroy = false;

    public void ParticlesOnFire()
    {

        StartCoroutine(DestroyTree());
    }

    IEnumerator DestroyTree()
    {
        if (!isDestroy)
        {
            isDestroy = true;
            Debug.Log("Tree on fire");
            yield return new WaitForSeconds(2f);
            Destroy(transform.parent.gameObject);
        }
    }
}
