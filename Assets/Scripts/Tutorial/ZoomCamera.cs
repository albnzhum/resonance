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
        if (target == null)
        {
            Debug.LogError("Target for zoom is null!");
            yield break;
        }

        // Сохраняем начальные параметры
        float startFOV = mainCamera.fieldOfView;
        Vector3 startPosition = mainCamera.transform.position;
        Quaternion startRotation = mainCamera.transform.rotation;

        // Вычисляем конечную ориентацию камеры, чтобы целевой объект был в центре
        // Используем позицию самой камеры (mainCamera.transform.position), а не родительского объекта
        Quaternion targetRotation = Quaternion.LookRotation(target.position - mainCamera.transform.position);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Плавно изменяем поле зрения (зум)
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            // Плавно поворачиваем камеру, чтобы целевой объект был в центре
            mainCamera.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            // Проверяем, находится ли объект в центре экрана
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(target.position);
           // Debug.Log($"Target screen position during zoom: {screenPoint} (should be close to (0.5, 0.5, z))");

            yield return null;
        }

        // Устанавливаем конечные значения
        mainCamera.fieldOfView = targetFOV;
        mainCamera.transform.rotation = targetRotation;

        // Проверяем финальное положение объекта на экране
        Vector3 screenPointFinal = mainCamera.WorldToViewportPoint(target.position);
        //Debug.Log($"Target screen position after zoom: {screenPointFinal} (should be close to (0.5, 0.5, z))");

        // Корректируем поворот, если объект не в центре
        if (Mathf.Abs(screenPointFinal.x - 0.5f) > 0.01f || Mathf.Abs(screenPointFinal.y - 0.5f) > 0.01f)
        {
            //Debug.LogWarning("Target is not perfectly centered, adjusting rotation...");
            mainCamera.transform.rotation = Quaternion.LookRotation(target.position - mainCamera.transform.position);
        }
    }

    public IEnumerator ZoomBack(float initialFOV, float duration)
    {
        // Сохраняем текущие параметры
        float startFOV = mainCamera.fieldOfView;
        //Quaternion startRotation = mainCamera.transform.rotation;

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
       // mainCamera.transform.rotation = initialRotation;
    }
}
