using UnityEngine;
using System.Collections;
using TMPro;

public class LeverMechanism : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] Transform leverTopPoint;
    [SerializeField] float currentAngle = 0f;
    [SerializeField] float targetAngle = 45f;
    [SerializeField] float speed = 90f;

    public void LowerLeverByBird(Bird bird)
    {
        StartCoroutine(LowerLeverByBirdCoroutine(bird));
    }

    private IEnumerator LowerLeverByBirdCoroutine(Bird bird)
    {
        while (currentAngle < targetAngle)
        {
            float step = speed * Time.deltaTime;
            pivot.Rotate(0, 0, step);
            currentAngle += step;

            Vector3 offset = transform.position - bird.GetPawsPosition();

            Vector3 adjustedTarget = leverTopPoint.position + offset;

            bird.transform.position = adjustedTarget;

            yield return null;
        }
    }
}
