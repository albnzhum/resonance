using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IInteractableTouching interactable;
        collision.gameObject.TryGetComponent(out interactable);
        if (interactable != null)
            interactable.Interact();
    }

    private void OnTriggerEnter(Collider collision)
    {
        IInteractableTouching interactable;
        collision.TryGetComponent(out interactable);
        if (interactable != null)
            interactable.Interact();
    }
}
