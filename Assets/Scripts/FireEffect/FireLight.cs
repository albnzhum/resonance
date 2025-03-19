using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLight : MonoBehaviour
{
    Light fireLight;
    [SerializeField] private float minIntensity = 0.2f;
    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private float speed = 1f;

    private void Awake()
    {
        fireLight = GetComponent<Light>();
    }

    private void Start()
    {
        StartCoroutine(ChangeIntensity());
    }

    IEnumerator ChangeIntensity()
    {
        float time = 0f;
        while (true)
        {
            time += Time.deltaTime * speed;
            float t = (Mathf.Sin(time) + 1f) / 2f;
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
            fireLight.intensity = intensity;
            yield return null;
        }
    }
}