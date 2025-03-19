using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    [SerializeField] private string[] resolutions;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private TMP_Text resolutionText;

    private int currentResolution = 0;
    
    public int CurrentResolution => currentResolution;

    private void OnEnable()
    {
        leftArrow.onClick.AddListener(OnLeftArrowClicked);
        rightArrow.onClick.AddListener(OnRightArrowClicked);
    }

    private void OnDisable()
    {
        leftArrow.onClick.RemoveListener(OnLeftArrowClicked);
        rightArrow.onClick.RemoveListener(OnRightArrowClicked);
    }

    private void OnLeftArrowClicked()
    {
        if (currentResolution != 0)
        {
            currentResolution--;
        }
        else
        {
            currentResolution = resolutions.Length - 1;
        }
        
        resolutionText.text = resolutions[currentResolution];
    }

    private void OnRightArrowClicked()
    {
        if (currentResolution != resolutions.Length - 1)
        {
            currentResolution++;
        }
        else
        {
            currentResolution = 0;
        }
        
        resolutionText.text = resolutions[currentResolution];
    }

    public void SetResolution(int resolution)
    {
        if (resolution > resolutions.Length - 1 || resolution < 0) return;
        
        currentResolution = resolution;
        resolutionText.text = resolutions[currentResolution];
    }
}
