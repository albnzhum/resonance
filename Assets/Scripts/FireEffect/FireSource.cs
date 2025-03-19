using UnityEngine;
using System.Collections;

public class FireSource : MonoBehaviour
{
    [SerializeField] ParticleSystem sparks;
    [SerializeField] AudioSource sparksAudioSource;
    [SerializeField] float restartInterval = 0.5f;
    private Coroutine restartCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sparks.Play();
            sparksAudioSource.Play();
            restartCoroutine = StartCoroutine(RestartParticles());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (restartCoroutine != null)
            {
                StopCoroutine(restartCoroutine);
                restartCoroutine = null;
            }
            sparks.Stop();
            sparksAudioSource.Stop();
        }
    }

    IEnumerator RestartParticles()
    {
        while (true)
        {
            yield return new WaitForSeconds(restartInterval);
            sparks.Stop();
            sparks.Play();
        }
    }
}