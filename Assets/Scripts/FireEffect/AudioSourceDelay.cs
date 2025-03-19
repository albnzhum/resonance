using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceDelay : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float randomDelay = Random.Range(0f, 0.5f);
        audioSource.PlayDelayed(randomDelay);
    }
}
