using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismTest : MonoBehaviour
{
    public void StartAction()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("Color", Color.green);
    }
}
