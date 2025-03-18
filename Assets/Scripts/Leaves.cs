using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour, IInteractableTouching
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clip;

    public void Interact()
    {
        _audioSource.PlayOneShot(_clip);
        Debug.Log("листья");
    }
}
