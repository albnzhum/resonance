using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField] LoadingScreen loadingScreen;

    private void OnEnable()
    {
        loadingScreen = LoadingScreen.instance;
        //_animator.StartPlayback();
    }

    public void StartGame()
    {
        StartCoroutine(LoadManagerScene());
    }

    IEnumerator LoadManagerScene()
    {
        AsyncOperation managerLoad = SceneManager.LoadSceneAsync("Manager", LoadSceneMode.Additive);
    
        while (!managerLoad.isDone)
        {
            yield return null;
        }

        // После загрузки "Manager" удаляем "Menu"
        SceneManager.UnloadSceneAsync("Menu");

        // Начинаем загрузку следующей сцены
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Environment_Free", LoadSceneMode.Additive);
        loadingScreen.LoadAnimation(asyncLoad);
        //asyncLoad.allowSceneActivation = false; 

        while (asyncLoad.progress < 0.9f)
        {
            Debug.Log($"Загрузка: {asyncLoad.progress * 100}%");
            yield return null;
        }

        Debug.Log("Загрузка завершена! Ожидание активации...");
        yield return new WaitForSeconds(2); // Можно заменить на проверку UI-графика

        //asyncLoad.allowSceneActivation = true;
    }
    

    public void Quit()
    {
        Application.Quit();
    }
}
