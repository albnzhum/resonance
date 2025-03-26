using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicController : MonoBehaviour
{
    [Header("UI objects")]
    [SerializeField] ToggleController fullscreenToggle;
    [SerializeField] Slider qualitySlider;
    [SerializeField] DropdownController resolutionDropdown;

    public bool GetFullscreenToggle() => fullscreenToggle.IsOn;
    public int GetQualityValue() => (int)qualitySlider.value;
    public int GetResolutionValue() => resolutionDropdown.CurrentResolution;

    private List<int> resolutionsWidth = new List<int>()
    {3840, 2560, 1920, 1280 };
    private List<int> resolutionsHeight = new List<int>()
    {2160, 1440, 1080, 720 };

    public void SaveResolutionData()
    {
        SetResolution(fullscreenToggle.IsOn, resolutionDropdown.CurrentResolution, (int)qualitySlider.value);
    }

    public void SetResolution(bool fullScreen, int numOfRes, int qualityLvl)
    {
        fullscreenToggle.SetOnOff(fullScreen);
        qualitySlider.value = qualityLvl;
        resolutionDropdown.SetResolution(numOfRes);
        Screen.SetResolution(resolutionsWidth[numOfRes], resolutionsHeight[numOfRes], fullScreen);
        QualitySettings.SetQualityLevel(qualityLvl);
    }
}
