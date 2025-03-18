using UnityEngine;

public class FireSource : MonoBehaviour
{
    ParticleSystem sparks;

    private void Awake()
    {
        sparks = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sparks.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sparks.Stop();
        }
    }
}
