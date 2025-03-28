using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] GameObject keyImgObj;

    private bool havekey = false;

    public bool GetKey() => havekey;

    public void SetKey()
    {
        if (!havekey)
        {
            havekey = true;
            keyImgObj.SetActive(true);
        }
    }
}
