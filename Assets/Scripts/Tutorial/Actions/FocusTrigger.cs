using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FocusState
{
    Light,
    Water,
    Leaves
}

public class FocusTrigger : MonoBehaviour, ITutorialAction
{
    [SerializeField] private ZoomCamera tutorialSystem;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private FocusState focusState = FocusState.Light;

    [Header("Focus Object")] [SerializeField]
    private Transform focusObject; // Дальний объект для фокуса камеры

    [SerializeField] private float zoomDuration = 2f; // Длительность зума
    [SerializeField] private float focusDuration = 3f; // Длительность фокуса
    [SerializeField] private float zoomFieldOfView = 30f; // Уменьшенное поле зрения для зума

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCompleted)
        {
            OnActionCompleted?.Invoke();
            gameStateManager.ChangeState(GameState.UI);
            FocusOnDistantObject();
        }
    }

    public void FocusOnDistantObject()
    {
        StartCoroutine(FocusCameraOnObject());
    }

    private IEnumerator FocusCameraOnObject()
    {
        // Зум на объект
        float initialFOV = tutorialSystem.GetFieldOfView();
        yield return StartCoroutine(tutorialSystem.ZoomTo(focusObject, zoomFieldOfView, zoomDuration));

        // Задержка на фокусе
        yield return new WaitForSeconds(focusDuration);

        // Возврат камеры
        yield return StartCoroutine(tutorialSystem.ZoomBack(initialFOV, zoomDuration));

        isCompleted = true;
        gameStateManager.ChangeState(GameState.Gameplay);
    }

    public event Action OnActionCompleted;
    private bool isCompleted = false;

    public void StartAction()
    {
        switch (focusState)
        {
            case FocusState.Light:
                isCompleted = false;
                break;
            case FocusState.Water:
                OnActionCompleted?.Invoke();
                gameStateManager.ChangeState(GameState.UI);
                FocusOnDistantObject();
                break;
            case FocusState.Leaves:
                OnActionCompleted?.Invoke();
                gameStateManager.ChangeState(GameState.UI);
                FocusOnDistantObject();
                break;
            
        }
    }
}