using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerObserver : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<PointerEventData> PointerClicked;
    public event Action<PointerEventData> PointerEntered;
    public event Action<PointerEventData> PointerExited;

    public void OnPointerClick(PointerEventData eventData) => PointerClicked?.Invoke(eventData);

    public void OnPointerEnter(PointerEventData eventData) => PointerEntered?.Invoke(eventData);

    public void OnPointerExit(PointerEventData eventData) => PointerExited?.Invoke(eventData);
}
