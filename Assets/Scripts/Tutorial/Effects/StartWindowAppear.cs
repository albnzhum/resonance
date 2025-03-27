using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartWindowAppear : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _header;
    [SerializeField] private Image divider;
    [SerializeField] private TMP_Text _content;
    [SerializeField] private Button _button;
    
    [Header("Animation")]
    [SerializeField] private Animator _animator;
    
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private TutorialSystem _tutorialSystem;

    private TMP_Text _buttonText;

    private void OnEnable()
    {
        _buttonText = _button.GetComponentInChildren<TMP_Text>();
    }

    public void Close()
    {
        _animator.SetBool("IsClose", true);
        Disappear();
    }

    public void StartTutorial()
    {
        _tutorialSystem.StartTutorial();
    }
    
    private void Disappear()
    {
        Sequence fadeSequence = DOTween.Sequence();
        
        fadeSequence.Append(_header.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad));
        fadeSequence.Join(divider.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad));
        fadeSequence.Join(_content.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad));
        fadeSequence.Join(_button.image.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad));
        fadeSequence.Join(_buttonText.DOFade(0f, fadeDuration).SetEase(Ease.InOutQuad));
    }
}
