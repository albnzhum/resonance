using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSliderController : MonoBehaviour
{
    [SerializeField] Slider sliderForToggle;
    [SerializeField] Toggle toggleForSlider;

    public void SetupSlider()
    {
        sliderForToggle.interactable = toggleForSlider.isOn;
    }
}
