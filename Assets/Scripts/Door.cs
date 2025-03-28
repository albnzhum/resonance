using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] KeyController keyController;
    string _interactText = "Открыть дверь (нужен ключ)";

    public string GetInteractText()
    {
        return _interactText;
    }

    public void Interact()
    {
        if (keyController.GetKey())
        {
            SceneLoadAsync.LoadScene(0);
        }
    }
}
