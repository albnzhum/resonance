using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageButtonVisual : MonoBehaviour
{
    [SerializeField] private PointerObserver pointerObserver;
    
    private Animator _textHover;
    private bool _isHovering;
    private static readonly int IsHovering = Animator.StringToHash("IsHovering");

    private void Awake()
    {
        _textHover = GetComponentInChildren<Animator>();

        pointerObserver.PointerEntered += OnPointerEnter;
        pointerObserver.PointerExited += OnPointerExit;
    }

    private void OnDisable()
    {
        _textHover.SetBool(IsHovering, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        _textHover.SetBool(IsHovering, _isHovering);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
        _textHover.SetBool(IsHovering, _isHovering);
    }
}
