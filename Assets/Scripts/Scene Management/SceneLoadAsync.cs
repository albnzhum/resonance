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
    /// <param name="sceneIndex">Индекс сцены</param>
    public static YieldInstruction LoadScene(int sceneIndex, LoadSceneMode mode = LoadSceneMode.Single)
    {
        YieldInstruction waitFor = SceneManager.LoadSceneAsync(sceneIndex, mode);
        
        return loadingScreen.LoadAnimation(waitFor);
    }

    /// <summary>
    /// Загрузка сцены (с экраном загрузки)
    /// </summary>
    /// <param name="sceneName">Название сцены</param>
    public static YieldInstruction LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        YieldInstruction waitFor = SceneManager.LoadSceneAsync(sceneName, mode);

        return loadingScreen.LoadAnimation(waitFor);
    }

    static SceneLoadAsync()
    {
        loadingScreen = LoadingScreen.instance;
    }
}
