using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Скрипт для перехода между сценами.
/// </summary>
public static class SceneLoadAsync
{
    private static LoadingScreen loadingScreen;

    /// <summary>
    /// Загрузка сцены (с экраном загрузки)
    /// </summary>
    /// <param name="sceneIndex"></param>
    public static void LoadScene(int sceneIndex)
    {
        YieldInstruction waitFor = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.LoadAnimation(waitFor);
    }

    static SceneLoadAsync()
    {
        loadingScreen = LoadingScreen.instance;
    }
}
