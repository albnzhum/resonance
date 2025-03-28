using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LeavesFire : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] FireController fireController;

    bool isDestroy = false;
    AudioSource leavesAudioSource;

    public void ParticlesOnFire()
    {
        leavesAudioSource = GetComponent<AudioSource>();
        StartCoroutine(DestroyLeaves());
    }

    IEnumerator DestroyLeaves()
    {
        if (!isDestroy)
        {
            isDestroy = true;
            leavesAudioSource.Play();

            float fadeInDuration = 1f;
            float targetVolume = 0.2f;
            float elapsedTime = 0f;

            while (elapsedTime < fadeInDuration)
            {
                elapsedTime += Time.deltaTime;
                leavesAudioSource.volume = Mathf.Lerp(0f, targetVolume, elapsedTime / fadeInDuration);
                yield return null;
            }
            leavesAudioSource.volume = targetVolume;
            fireParticles.Play();

            yield return new WaitForSeconds(3f);

            fireController.StartBurn();

            float fadeOutDuration = 1f;
            elapsedTime = 0f;

            while (elapsedTime < fadeOutDuration)
            {
                elapsedTime += Time.deltaTime;
                leavesAudioSource.volume = Mathf.Lerp(targetVolume, 0f, elapsedTime / fadeOutDuration);
                yield return null;
            }
            leavesAudioSource.volume = 0f;

            fireParticles.Stop();
            Destroy(transform.parent.gameObject);
        }
    }
}
