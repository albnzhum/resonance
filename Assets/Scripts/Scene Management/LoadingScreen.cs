using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

/// <summary>
/// Анимированный экран загрузки
/// </summary>
public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    [SerializeField] Image loadScreenBackground;    // фон экрана загрузки
    [SerializeField] RectTransform loadScreenImage; // нестатичное изображение во время загрузки
    [SerializeField] float fadeSpeed = 0.1f;
    [SerializeField] float rotateSpeed = 10f;
    
    [SerializeField] private GameObject[] loadingTexts;
    [SerializeField] private GameObject divider;
    
    private int currentLoadingText = 0;

    private void SetText()
    {
        divider.SetActive(true);
        currentLoadingText = Random.Range(0, loadingTexts.Length);
        
        loadingTexts[currentLoadingText].SetActive(true);
    }

    private void DeleteText()
    {
        divider.SetActive(false);
        for (int i = 0; i < loadingTexts.Length; i++)
        {
            loadingTexts[i].SetActive(false);
        }
    }

    public YieldInstruction LoadAnimation(YieldInstruction waitFor)
    {
        StopAllCoroutines();

        return StartCoroutine(LoadAnimationEnum(waitFor));
    }

    private IEnumerator LoadAnimationEnum(YieldInstruction waitFor)
    {
        Coroutine rotateSquare = StartCoroutine(RotateSquare());
        Coroutine moveSquare = StartCoroutine(MoveSquare(Vector2.zero));

        loadScreenBackground.color = new Color(loadScreenBackground.color.r, loadScreenBackground.color.g, loadScreenBackground.color.b, 1);
        SetText();
        yield return waitFor;
        yield return new WaitForSeconds(2);

        StartCoroutine(UnloadAnimationEnum(rotateSquare, moveSquare));
    }

    private IEnumerator UnloadAnimationEnum(Coroutine rotateSquare, Coroutine moveSquare)
    {
        if (rotateSquare != null) StopCoroutine(rotateSquare);
        if (moveSquare != null) StopCoroutine(moveSquare);

        moveSquare = StartCoroutine(MoveSquare(Vector2.up * Screen.height));
        DeleteText();

        for (float i = 1; i >= 0; i -= fadeSpeed / 2)
        {
            loadScreenBackground.color = new Color(loadScreenBackground.color.r, loadScreenBackground.color.g, loadScreenBackground.color.b, i);
            yield return null;
        }

        yield return moveSquare;
    }

    private IEnumerator RotateSquare()
    {
        while (true)
        {
            loadScreenImage.Rotate(0, 0, rotateSpeed);
            yield return null;
        }
    }

    private IEnumerator MoveSquare(Vector2 to)
    {
        float start = Time.time;

        while (Vector3.Distance(loadScreenImage.anchoredPosition, to) > 1)
        {
            loadScreenImage.anchoredPosition = Vector2.Lerp(loadScreenImage.anchoredPosition, to, (Time.time  - start) * fadeSpeed * 2);

            yield return null;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        instance = this;
    }
}
