using UnityEngine;
using System.Collections;

public class LeverMechanism : MonoBehaviour
{
    //public Transform pivot; // Точка вращения/опускания рычага
    //public float moveDistance = 0.2f; // Насколько опускается рычаг
    //public float moveSpeed = 2f; // Скорость опускания
    //public GameObject linkedObject; // Объект, который активируется (например, дверь)

    //private bool isActivated = false;
    //private Transform activatorUnit; // Ворона, сидящая на рычаге

    //public void ActivateLever(Transform activatorTransform)
    //{
    //    if (isActivated) return;

    //    isActivated = true;
    //    activatorUnit = activatorTransform;
    //    StartCoroutine(LowerLever());
    //}

    //private IEnumerator LowerLever()
    //{
    //    float currentAngle = 0f;
    //    float targetAngle = 45f; // или -45f, зависит от направления
    //    float speed = 90f; // градусов в секунду

    //    while (currentAngle < targetAngle)
    //    {
    //        float step = speed * Time.deltaTime;
    //        leverArm.Rotate(step, 0, 0); // Вращение по локальной оси X (если палка стоит вертикально)
    //        currentAngle += step;
    //        yield return null;
    //    }
    //}

    [SerializeField] Transform pivot;
    [SerializeField] float currentAngle = 0f;
    [SerializeField] float targetAngle = 45f; // или -45f, зависит от направления
    [SerializeField] float speed = 90f; // градусов в секунду

    public void LowerLever()
    {
        StartCoroutine(LowerLeverCoroutine());
    }

    private IEnumerator LowerLeverCoroutine()
    {
        while (currentAngle < targetAngle)
        {
            float step = speed * Time.deltaTime;
            pivot.Rotate(step, 0, 0); // Вращение по локальной оси X (если палка стоит вертикально)
            currentAngle += step;
            yield return null;
        }
        // Вызов логики активации: открыть дверь, включить механизм и т.п.
    }
}
