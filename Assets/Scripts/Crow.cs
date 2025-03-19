using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] Transform _distination;

    [SerializeField] float flySpeed = 5f;  // Скорость полета вороны
    [SerializeField] float heightOffset = 2f;  // Отступ по высоте (чтобы ворона летела снизу вверх)
    private bool isFlying = false;

    void Update()
    {
        // Если ворона летит, продолжаем движение
        if (isFlying)
        {
            FlyTowardsLever();
        }
    }

    // Этот метод вызывается при шелесте листьев
    public void StartFlying()
    {

        // Рассчитываем цель (точка назначения рычага + смещение по высоте)
        Vector3 targetPosition = _distination.position + Vector3.up * heightOffset;

        // Логика активации полета
        isFlying = true;
    }

    private void FlyTowardsLever()
    {
        // Рассчитываем текущую позицию вороны
        Vector3 targetPosition = _distination.position + Vector3.up * heightOffset;

        // Двигаем ворону в сторону рычага с учетом смещения по высоте
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);

        // Проверка на достижение цели
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isFlying = false;  // Полет завершен
        }
    }
}
