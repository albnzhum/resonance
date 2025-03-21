using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    //private LoadingScreen loadingScreen;

    //private void OnEnable()
    //{
    //    loadingScreen = LoadingScreen.instance;
    //}

    public void StartGame()
    {
        StartCoroutine(LoadManagerScene());
    }

    IEnumerator LoadManagerScene()
    {
        // Асинхронно загружаем сцену с анимацией перехода
        YieldInstruction sceneLoading = SceneLoadAsync.LoadScene("Manager", LoadSceneMode.Additive);

        yield return sceneLoading;

        // После загрузки "Manager" удаляем "Menu"
        SceneManager.UnloadSceneAsync("Menu");

        // Начинаем загрузку следующей сцены
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        //asyncLoad.allowSceneActivation = false; 

        // Асинхронно загружаем сцену с анимацией перехода
        YieldInstruction sceneLoading = SceneLoadAsync.LoadScene("Environment_Free", LoadSceneMode.Additive);

        yield return sceneLoading;


        //while (asyncLoad.progress < 0.9f)
        //{
        //    Debug.Log($"Загрузка: {asyncLoad.progress * 100}%");
        //    yield return null;
        //}


        Debug.Log("Загрузка завершена! Ожидание активации...");
        yield return new WaitForSeconds(2); // Можно заменить на проверку UI-графика

        //asyncLoad.allowSceneActivation = true;
    }
    

    public void Quit()
    {
        Application.Quit();
    }
}
