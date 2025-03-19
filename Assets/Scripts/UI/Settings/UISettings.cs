using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UIMenu menu;

    private void OnDisable()
    {
        animator.SetBool("IsClose", false);
    }

    public void CloseSettings()
    {
        animator.SetBool("IsClose", true);
    }

    public void OpenMenu()
    {
        menu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
