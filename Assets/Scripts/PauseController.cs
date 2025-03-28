using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;

    private Scene activeScene;

    private void Start()
    {
        activeScene = SceneManager.GetActiveScene();
    }

    public void BackToMenu()
    {
        StartCoroutine(LoadAsyncScene());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Open();
        }
    }

    private void Open()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator LoadAsyncScene()
    {
        YieldInstruction sceneLoading = SceneLoadAsync.LoadScene("Menu", LoadSceneMode.Additive);

        yield return sceneLoading;
        SceneManager.UnloadSceneAsync(activeScene.name);

        Debug.Log("Загрузка завершена! Ожидание активации...");
        yield return new WaitForSeconds(2);
    }
}