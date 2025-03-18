using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject _interactUIContainer;
    [SerializeField] private TextMeshProUGUI _interactText;

    private PlayerInteract _playerInteract;

    private void Start()
    {
        _playerInteract = GetComponent<PlayerInteract>();
    }

    void Update()
    {
        if (_playerInteract != null)
        {
            InteractUIUpdate();
        }
    }

    private void Show(IInteractable interacteble)
    {
        _interactText.text = interacteble.GetInteractText();
        _interactUIContainer.SetActive(true);
    }

    private void Hide()
    {
        _interactText.text = string.Empty;
        _interactUIContainer.SetActive(false);
    }

    private void InteractUIUpdate()
    {
        var interactable = _playerInteract.GetInteractableObject();
        if (interactable != null)
        {
            Show(interactable);
        }
        else
        {
            Hide();
        }
    }
}