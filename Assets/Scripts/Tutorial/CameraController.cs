using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _uiCamera;
    [SerializeField] private GameObject _playerCamera;

    public void ActivePlayerCamera()
    {
        _uiCamera.enabled = false;
        _uiCamera.gameObject.SetActive(false);
        
        _playerCamera.gameObject.SetActive(true);
    }

    public void DeactivePlayerCamera()
    {
        _playerCamera.gameObject.SetActive(false);
        
        _uiCamera.gameObject.SetActive(true);
        _uiCamera.enabled = true;
    }
}
