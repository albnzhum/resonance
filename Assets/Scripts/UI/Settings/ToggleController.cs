using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Text text;

    private bool _isOn = true;
    public bool IsOn => _isOn;

    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            text.text = "ON";
        }
        else
        {
            text.text = "OFF";
        }
        
        _isOn = isOn;
    }

    public void SetOnOff(bool isOn)
    {
        toggle.isOn = isOn;
        _isOn = isOn;
        
        text.text = isOn ? "ON" : "OFF";
    }
}
