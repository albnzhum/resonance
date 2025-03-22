using UnityEngine;
using Random = UnityEngine.Random;

public class RandomLoadingText : MonoBehaviour
{
    [SerializeField] private GameObject[] loadingTexts;
    
    private int currentLoadingText = 0;

    private void OnEnable()
    {
        currentLoadingText = Random.Range(0, loadingTexts.Length);
        
        loadingTexts[currentLoadingText].SetActive(true);
    }

    private void OnDisable()
    {
        for (int i = 0; i < loadingTexts.Length; i++)
        {
            loadingTexts[i].SetActive(false);
        }
    }
}
