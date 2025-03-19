using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour, IInteractableTouching
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clip;
    [SerializeField] Crow _crow;

    [SerializeField][Range(0, 1)] float _highVolume; 
    [SerializeField][Range(0, 1)] float _lowVolume; 

    public void Interact(Player player)
    {
        if (player.IsRunning)
        {
            _audioSource.volume = _highVolume;
        }
        else
        {
            _audioSource.volume = _lowVolume;
        }
        _audioSource.PlayOneShot(_clip);
        _crow.StartFlying();
        Debug.Log("листья");
    }
}
