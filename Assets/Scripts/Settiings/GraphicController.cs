using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicController : MonoBehaviour
{
    [Header("UI objects")]
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Slider qualitySlider;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    public bool GetFullscreenToggle() => fullscreenToggle.isOn;
    public int GetQualityValue() => (int)qualitySlider.value;
    public int GetResolutionValue() => resolutionDropdown.value;

    private List<int> resolutionsWidth = new List<int>()
    {3840, 2560, 1920, 1280 };
    private List<int> resolutionsHeight = new List<int>()
    {2160, 1440, 1080, 720 };

    public void SaveResolutionData()
    {
        SetResolution(fullscreenToggle.isOn, resolutionDropdown.value, (int)qualitySlider.value);
    }

    public void SetResolution(bool fullScreen, int numOfRes, int qualityLvl)
    {
        fullscreenToggle.isOn = fullScreen;
        qualitySlider.value = qualityLvl;
        resolutionDropdown.value = numOfRes;
        Screen.SetResolution(resolutionsWidth[numOfRes], resolutionsHeight[numOfRes], fullScreen);
        QualitySettings.SetQualityLevel(qualityLvl);
    }
}
