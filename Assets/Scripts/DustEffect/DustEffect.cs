using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float maxLength = 7;
    private ParticleSystem.ShapeModule shape;
    private bool raised;
    private Collider[] hitColliders;

    private void Raise()
    {
        if (raised) return;

        raised = true;
        shape.length = 0;
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (shape.length < maxLength)
        {
            shape.length += speed;

            yield return null;
        }

        yield return new WaitForSeconds(3);

        CheckDustySurfaces();

        while (shape.length > 0)
        {
            shape.length -= speed;

            yield return null;
        }
    }

    private void CheckDustySurfaces()
    {
        foreach(Collider coll in hitColliders)
        {
            if (coll.TryGetComponent(out IDustySurface surface))
                surface.BecomeDusty();
        }
    }

    private void Start()
    {
        shape = dustParticles.shape;
    }

    private void FixedUpdate()
    {
        hitColliders = Physics.OverlapBox(transform.position, new Vector3(8, 1, 8));
        if (!raised)
            foreach (Collider coll in hitColliders)
                if (coll.CompareTag("Player"))
                    Raise();
    }
}
