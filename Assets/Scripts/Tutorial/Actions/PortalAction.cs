using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalAction : MonoBehaviour, ITutorialAction
{
    public event Action OnActionCompleted;
    private bool isCompleted = false;
    public void StartAction()
    {
        isCompleted = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCompleted)
        {
            isCompleted = true;
            OnActionCompleted?.Invoke();

            StartCoroutine(LoadAsyncScene());
        }
    }
    
    IEnumerator LoadAsyncScene()
    {

        YieldInstruction sceneLoading = SceneLoadAsync.LoadScene("Level 5", LoadSceneMode.Additive);

        yield return sceneLoading;
        SceneManager.UnloadSceneAsync("0_Tutorial");
        
        Debug.Log("Загрузка завершена! Ожидание активации...");
        yield return new WaitForSeconds(2);
    }
}
