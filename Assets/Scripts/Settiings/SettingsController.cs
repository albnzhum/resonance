using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [Header("Settings Controllers")]
    [SerializeField] VolumeController volumeController;
    [SerializeField] GraphicController graphicController;

    private string jsonPath;
    private string saveFileName = "saves.json";
    private SettingsData settingsData;

    private void OnEnable()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        jsonPath = Path.Combine(Application.streamingAssetsPath, saveFileName);

        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            settingsData = JsonUtility.FromJson<SettingsData>(json);
            SetupConfig(settingsData);
        }
        else
        {
            SaveConfig();
        }
    }


    private void SetupConfig(SettingsData settingsData)
    {
        volumeController.SetAudioLvlBySlider(0, settingsData.effectsVolume, settingsData.effectsActive);
        volumeController.SetAudioLvlBySlider(1, settingsData.musicVolume, settingsData.musicActive);
        graphicController.SetResolution(settingsData.isFullscreen, settingsData.resolutionValue, settingsData.qualityLvl);
    }

    public void SaveConfig()
    {
        if (settingsData == null)
        {
            settingsData = new SettingsData();
        }

        settingsData.effectsActive = volumeController.GetEffectsToggle();
        settingsData.musicActive = volumeController.GetMusicToggle();
        settingsData.effectsVolume = volumeController.GetEffectsValue();
        settingsData.musicVolume = volumeController.GetMusicValue();
        settingsData.isFullscreen = graphicController.GetFullscreenToggle();
        settingsData.qualityLvl = graphicController.GetQualityValue();
        settingsData.resolutionValue = graphicController.GetResolutionValue();
        string configFilePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
        string json = JsonUtility.ToJson(settingsData);
        File.WriteAllText(configFilePath, json);
        SetupConfig(settingsData);
    }
}
