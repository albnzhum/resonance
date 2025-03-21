using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ������ ��� �������� ����� �������.
/// </summary>
public static class SceneLoadAsync
{
    private static LoadingScreen loadingScreen;

    /// <summary>
    /// �������� ����� (� ������� ��������)
    /// </summary>
    /// <param name="sceneIndex">������ �����</param>
    public static YieldInstruction LoadScene(int sceneIndex, LoadSceneMode mode = LoadSceneMode.Single)
    {
        YieldInstruction waitFor = SceneManager.LoadSceneAsync(sceneIndex, mode);
        
        return loadingScreen.LoadAnimation(waitFor);
    }

    /// <summary>
    /// �������� ����� (� ������� ��������)
    /// </summary>
    /// <param name="sceneName">�������� �����</param>
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
