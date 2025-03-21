using UnityEngine;
using System.Collections;

public class LeverMechanism : MonoBehaviour
{
    //public Transform pivot; // ����� ��������/��������� ������
    //public float moveDistance = 0.2f; // ��������� ���������� �����
    //public float moveSpeed = 2f; // �������� ���������
    //public GameObject linkedObject; // ������, ������� ������������ (��������, �����)

    //private bool isActivated = false;
    //private Transform activatorUnit; // ������, ������� �� ������

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
    //    float targetAngle = 45f; // ��� -45f, ������� �� �����������
    //    float speed = 90f; // �������� � �������

    //    while (currentAngle < targetAngle)
    //    {
    //        float step = speed * Time.deltaTime;
    //        leverArm.Rotate(step, 0, 0); // �������� �� ��������� ��� X (���� ����� ����� �����������)
    //        currentAngle += step;
    //        yield return null;
    //    }
    //}

    [SerializeField] Transform pivot;
    [SerializeField] float currentAngle = 0f;
    [SerializeField] float targetAngle = 45f; // ��� -45f, ������� �� �����������
    [SerializeField] float speed = 90f; // �������� � �������

    public void LowerLever()
    {
        StartCoroutine(LowerLeverCoroutine());
    }

    private IEnumerator LowerLeverCoroutine()
    {
        while (currentAngle < targetAngle)
        {
            float step = speed * Time.deltaTime;
            pivot.Rotate(step, 0, 0); // �������� �� ��������� ��� X (���� ����� ����� �����������)
            currentAngle += step;
            yield return null;
        }
        // ����� ������ ���������: ������� �����, �������� �������� � �.�.
    }
}
