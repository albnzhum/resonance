using UnityEngine;
using System.Collections;

public class LeverMechanism : MonoBehaviour
{
    public Transform pivot; // ����� ��������/��������� ������
    public float moveDistance = 0.2f; // ��������� ���������� �����
    public float moveSpeed = 2f; // �������� ���������
    public GameObject linkedObject; // ������, ������� ������������ (��������, �����)

    private bool isActivated = false;
    private Transform activatorUnit; // ������, ������� �� ������

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
