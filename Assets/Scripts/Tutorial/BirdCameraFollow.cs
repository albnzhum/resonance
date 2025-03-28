using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCameraFollow : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private Camera mainCamera; // Камера, которая будет следовать за птицей
    [SerializeField] private float followSpeed = 5f; // Скорость следования камеры
    [SerializeField] private Vector3 followOffset = new Vector3(0, 2, -5); // Смещение камеры относительно птицы
    [SerializeField] private float lookAtSpeed = 5f;
    [SerializeField] private GameStateManager gameStateManager;// Скорость поворота камеры

    private Transform followTarget; // Текущая цель для следования (птица)
    private Transform playerTransform; // Игрок, чтобы вернуться к нему после следования

    private bool isFollowing = false;
    

    private void LateUpdate()
    {
        if (isFollowing && followTarget != null)
        {
            // Вычисляем желаемую позицию камеры с учётом смещения
            Vector3 desiredPosition = followTarget.position + followOffset;

            // Плавно перемещаем камеру к желаемой позиции
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPosition, followSpeed * Time.deltaTime);

            // Плавно поворачиваем камеру, чтобы птица была в центре
            Quaternion desiredRotation = Quaternion.LookRotation(followTarget.position - mainCamera.transform.position);
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, desiredRotation, lookAtSpeed * Time.deltaTime);

            // Проверяем, находится ли птица в центре экрана
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(followTarget.position);
            //Debug.Log($"Follow target screen position: {screenPoint} (should be close to (0.5, 0.5, z))");

            // Корректируем поворот, если птица не в центре
            if (Mathf.Abs(screenPoint.x - 0.5f) > 0.01f || Mathf.Abs(screenPoint.y - 0.5f) > 0.01f)
            {
                Debug.LogWarning("Target is not perfectly centered, adjusting rotation...");
                mainCamera.transform.rotation = Quaternion.LookRotation(followTarget.position - mainCamera.transform.position);
            }
        }
    }

    public void SetPlayer(Transform player)
    {
        playerTransform = player;
        followTarget = playerTransform;
    }

    public void FollowTarget(Transform target)
    {
        playerCamera.gameObject.SetActive(false);
        
        mainCamera = Instantiate(playerCamera);
        mainCamera.gameObject.SetActive(true);
        
        followTarget = target;
        isFollowing = true;
        Debug.Log($"Camera now following: {target.name}");
        gameStateManager.ChangeState(GameState.UI);
    }

    public void StopFollowing()
    {
        Destroy(mainCamera.gameObject);
        playerCamera.gameObject.SetActive(true);
        
        isFollowing = false;
        gameStateManager.ChangeState(GameState.Gameplay);
        followTarget = playerTransform; // Возвращаем следование за игроком

        Debug.Log("Camera stopped following target, returning to player.");
    }
}
