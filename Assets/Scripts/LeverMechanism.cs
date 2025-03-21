using UnityEngine;
using System.Collections;

public class LeverMechanism : MonoBehaviour
{
    public Transform pivot; // Точка вращения/опускания рычага
    public float moveDistance = 0.2f; // Насколько опускается рычаг
    public float moveSpeed = 2f; // Скорость опускания
    public GameObject linkedObject; // Объект, который активируется (например, дверь)

    private bool isActivated = false;
    private Transform activatorUnit; // Ворона, сидящая на рычаге

    public void ActivateLever(Transform activatorTransform)
    {
        if (isActivated) return;

        isActivated = true;
        activatorUnit = activatorTransform;
        StartCoroutine(LowerLever());
    }

    private IEnumerator LowerLever()
    {
        Vector3 startPos = pivot.position;
        Vector3 endPos = startPos + Vector3.down * moveDistance;

        float elapsedTime = 0;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * moveSpeed;
            pivot.position = Vector3.Lerp(startPos, endPos, elapsedTime);

            yield return null;
        }

        if (linkedObject)
        {
            linkedObject.SetActive(false);
        }
    }
}
