using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    PlayerController _controller;
    
    public ParticleSystem ripple;
    private RaycastHit isGround;

    private bool inWater;

    public bool IsRunning { get { return _controller.IsRunning; } }

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (inWater) ripple.gameObject.SetActive(true);
        else ripple.gameObject.SetActive(false);
        
        Physics.Raycast(transform.position, Vector3.down, out isGround, 2.7f, LayerMask.GetMask("Ground"));
        float height = cc.height + cc.radius;
        inWater = Physics.Raycast(transform.position + Vector3.up * height, Vector3.down, height * 2,
            LayerMask.GetMask("Water"));
        
        if (isGround.collider) ripple.transform.position = transform.position + transform.forward;
        else ripple.transform.position = transform.position;
        
        Shader.SetGlobalVector("_Player", transform.position);
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
        
        if (collision.gameObject.layer == 4)
        {
            //CreateRipple(-180, 180, 2, 5, 3f, 3);
            ripple.Emit(transform.position, Vector3.zero, 5, 0.1f, Color.white);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            //CreateRipple(-180, 180, 2, 5, 3f, 3);
            ripple.Emit(transform.position, Vector3.zero, 5, 0.1f, Color.white);
        }
    }
}
