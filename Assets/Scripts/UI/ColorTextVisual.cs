using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ColorTextVisual : MonoBehaviour
{
    [SerializeField] private PointerObserver pointerObserver;

    [SerializeField] private TMP_Text text;
    [SerializeField] private Color textColor;
    
    private Color defaultColor;
    private float duration = 0.5f;

    private void OnEnable()
    {
        defaultColor = text.color;
    }

    private void Awake()
    {
        pointerObserver.PointerEntered += OnPointerEnter;
        pointerObserver.PointerExited += OnPointerExit;
    }

    private void OnDisable()
    {
        pointerObserver.PointerEntered -= OnPointerEnter;
        pointerObserver.PointerExited -= OnPointerExit;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.DOColor(defaultColor, duration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.DOColor(textColor, duration);
    }
}
