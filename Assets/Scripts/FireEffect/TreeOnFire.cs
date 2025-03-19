using System.Collections;
using UnityEngine;

public class TreeOnFire : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] ParticleSystem smokeParticles;

    bool isDestroy = false;
    AudioSource treeAudioSource;
    GameObject treeObj;

    private void Awake()
    {
        treeObj = transform.parent.gameObject;
    }

    public void ParticlesOnFire()
    {
        treeAudioSource = treeObj.GetComponent<AudioSource>();
        StartCoroutine(DestroyTree());
    }

    IEnumerator DestroyTree()
    {
        if (!isDestroy)
        {
            isDestroy = true;
            Debug.Log("Tree on fire");
            treeAudioSource.Play();
            smokeParticles.Play();

            float fadeInDuration = 1f;
            float targetVolume = 0.5f;
            float elapsedTime = 0f;

            while (elapsedTime < fadeInDuration)
            {
                elapsedTime += Time.deltaTime;
                treeAudioSource.volume = Mathf.Lerp(0f, targetVolume, elapsedTime / fadeInDuration);
                yield return null;
            }
            treeAudioSource.volume = targetVolume;
            fireParticles.Play();

            yield return new WaitForSeconds(6f);

            float fadeOutDuration = 1f;
            elapsedTime = 0f;

            while (elapsedTime < fadeOutDuration)
            {
                elapsedTime += Time.deltaTime;
                treeAudioSource.volume = Mathf.Lerp(targetVolume, 0f, elapsedTime / fadeOutDuration);
                yield return null;
            }
            treeAudioSource.volume = 0f;

            Destroy(treeObj);
        }
    }
}