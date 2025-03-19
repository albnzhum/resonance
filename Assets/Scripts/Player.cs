using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerController _controller;

    public bool IsRunning { get { return _controller.IsRunning; } }

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IInteractableTouching interactable;
        collision.gameObject.TryGetComponent(out interactable);
        if (interactable != null)
            interactable.Interact(this);
    }

    private void OnTriggerEnter(Collider collision)
    {
        IInteractableTouching interactable;
        collision.TryGetComponent(out interactable);
        if (interactable != null)
            interactable.Interact(this);
    }
}
