using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("UI elements")] 
    [SerializeField] private GameObject _tutorialWindow;

    [SerializeField] private Button _closeButton;

    private bool isCompleted;

    public bool IsCompleted => isCompleted;

    public event Action OnStageCompleted;

    public void Show()
    {
        _tutorialWindow.SetActive(true);
        _closeButton.onClick.AddListener(CompleteStage);
    }

    public void Close()
    {
        _tutorialWindow.SetActive(false);
        isCompleted = true;
    }

    private void CompleteStage()
    {
        _closeButton.onClick.RemoveListener(CompleteStage);
        OnStageCompleted?.Invoke();
    }

    //открывается начало -> кнопка начать
    //идет подписка на событие -> событие вызвано -> отписка от предыдущего, подписка на следующее
    //перемещение -> отслеживается событие  перемещения -> событие вызвано, происходит отписка, следующая система подписывается на это же событие
}