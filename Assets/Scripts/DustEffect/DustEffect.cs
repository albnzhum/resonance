using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DustEffect : MonoBehaviour
{
    [SerializeField] List<GameObject> surfaces;
    private List<IDustySurface> _surfaces = new();

    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float maxLength = 7;
    [SerializeField] float playerCheckRadius = 8;
    private ParticleSystem.ShapeModule shape;
    private bool raised;
    private Transform player;

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
            shape.length += speed * Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(3);

        CheckDustySurfaces();

        while (shape.length > 0)
        {
            shape.length -= speed * Time.deltaTime;

            yield return null;
        }
    }

    private void CheckDustySurfaces()
    {
        foreach(IDustySurface surface in _surfaces)
            if (surface != null)
                surface.BecomeDusty();
    }

    private void Start()
    {
        shape = dustParticles.shape;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        foreach (GameObject obj in surfaces)
        {
            IDustySurface[] temp = obj.GetComponents<MonoBehaviour>().OfType<IDustySurface>().ToArray();
            if (temp != null)
                _surfaces.Add(temp[0]);
            else
                Debug.Log("A component which realise IDustySurface, not found on object");
        }
    }

    private void FixedUpdate()
    {
        if (player && Vector3.Distance(player.position, transform.position) < playerCheckRadius)
            Raise();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 255, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, playerCheckRadius);
    }
}
