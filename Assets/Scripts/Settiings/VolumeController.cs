using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] AudioMixer mainMixer;

    [Header("Toggles")]
    [SerializeField] Toggle effectsActive;
    [SerializeField] Toggle musicActive;

    [Header("Audio sliders")]
    [SerializeField] Slider effectsSlider;
    [SerializeField] Slider musicSlider;

    public bool GetEffectsToggle() => effectsActive.isOn;
    public bool GetMusicToggle() => musicActive.isOn;
    public float GetEffectsValue() => effectsSlider.value;
    public float GetMusicValue() => musicSlider.value;

    public void SetAudioLvl(int numOfSlider)
    {
        switch (numOfSlider)
        {
            case 0:
                SetAudioLvlBySlider(0, effectsSlider.value, effectsActive.isOn);
                break;
            case 1:
                SetAudioLvlBySlider(1, musicSlider.value, musicActive.isOn);
                break;
        }
    }

    public void SetAudioLvlBySlider(int numOfSlider, float sliderValue, bool toggleActive)
    {
        switch (numOfSlider)
        {
            case 0:
                mainMixer.SetFloat("EffectsVolume", sliderValue);
                effectsActive.isOn = toggleActive;
                effectsActive.GetComponent<ToggleSliderController>().SetupSlider();
                effectsSlider.value = sliderValue;
                break;
            case 1:
                mainMixer.SetFloat("MusicVolume", sliderValue);
                musicActive.isOn = toggleActive;
                musicActive.GetComponent<ToggleSliderController>().SetupSlider();
                musicSlider.value = sliderValue;
                break;
        }
    }
}
