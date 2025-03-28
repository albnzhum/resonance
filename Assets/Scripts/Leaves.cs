using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaves : MonoBehaviour, IInteractableTouching
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _clip;
    [SerializeField] Bird _bird;

    [SerializeField][Range(0, 1)] float _highVolume; 
    [SerializeField][Range(0, 1)] float _lowVolume;

    private BurningObject _burnObject;
    
    public Action OnInteract;

    private void Awake()
    {
        _burnObject = GetComponent<BurningObject>();
    }

    private void Start()
    {
        if (_burnObject != null) _burnObject.onBurn += _bird.TakeOff;
    }

    public void Interact(Player player)
    {
        if (player.IsRunning)
        {
            _audioSource.volume = _highVolume;
            _bird.TakeOff();
            
            OnInteract?.Invoke();
        }
        else
        {
            _audioSource.volume = _lowVolume;
        }
        _audioSource.PlayOneShot(_clip);     
        Debug.Log("листья");
    }



}
