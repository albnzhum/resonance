using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Transform playerTransform;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float initialFOV;

    private void Awake()
    {
        initialPosition = mainCamera.transform.position;
        initialRotation = mainCamera.transform.rotation;
        initialFOV = mainCamera.fieldOfView;
    }
    

    public float GetFieldOfView()
    {
        return mainCamera.fieldOfView;
    }

    public IEnumerator ZoomTo(Transform target, float targetFOV, float duration)
    {
        // Сохраняем начальные параметры
        float startFOV = mainCamera.fieldOfView;
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;

        // Вычисляем конечную ориентацию камеры, чтобы целевой объект был в центре
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Плавно изменяем поле зрения (зум)
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            // Плавно поворачиваем камеру, чтобы целевой объект был в центре
            mainCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            yield return null;
        }

        // Устанавливаем конечные значения
        mainCamera.fieldOfView = targetFOV;
        mainCamera.transform.rotation = targetRotation;
    }

    public IEnumerator ZoomBack(float initialFOV, float duration)
    {
        // Сохраняем текущие параметры
        float startFOV = mainCamera.fieldOfView;
        Quaternion startRotation = mainCamera.transform.rotation;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Плавно возвращаем поле зрения
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, initialFOV, t);

            // Плавно возвращаем поворот камеры
            //mainCamera.transform.rotation = Quaternion.Slerp(startRotation, initialRotation, t);

            yield return null;
        }

        // Устанавливаем исходные значения
        mainCamera.fieldOfView = initialFOV;
        //mainCamera.transform.rotation = initialRotation;
    }
}
