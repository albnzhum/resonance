using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDustySurface : MonoBehaviour, IDustySurface
{
    public void BecomeDusty()
    {
        Debug.Log("object became dusty");
        transform.localScale *= 5;
    }
}
