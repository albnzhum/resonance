using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] Transform _distination;

    [SerializeField] float flySpeed = 5f;  // �������� ������ ������
    [SerializeField] float heightOffset = 2f;  // ������ �� ������ (����� ������ ������ ����� �����)
    private bool isFlying = false;

    void Update()
    {
        // ���� ������ �����, ���������� ��������
        if (isFlying)
        {
            FlyTowardsLever();
        }
    }

    // ���� ����� ���������� ��� ������� �������
    public void StartFlying()
    {

        // ������������ ���� (����� ���������� ������ + �������� �� ������)
        Vector3 targetPosition = _distination.position + Vector3.up * heightOffset;

        // ������ ��������� ������
        isFlying = true;
    }

    private void FlyTowardsLever()
    {
        // ������������ ������� ������� ������
        Vector3 targetPosition = _distination.position + Vector3.up * heightOffset;

        // ������� ������ � ������� ������ � ������ �������� �� ������
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);

        // �������� �� ���������� ����
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isFlying = false;  // ����� ��������
        }
    }
}
