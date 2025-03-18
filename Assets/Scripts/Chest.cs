using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    string _interactText = "открыть сундук";

    public string GetInteractText()
    {
        return _interactText;
    }

    public void Interact()
    {
        Debug.Log(_interactText);
    }

}
