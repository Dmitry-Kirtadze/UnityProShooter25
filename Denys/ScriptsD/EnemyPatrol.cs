using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 10f; // �������� ������������
    [SerializeField] private GameObject patrolIntoPlayer; // �������� ���� ������, ����� ���� �������� �� �������
    private int currentPointIndex = 0; // ������ ������� �����

    private Transform[] patrolPoints; // ������ ����� �������

    void Start()
    {
        // ���� patrolIntoPlayer �� �������� � ����������, ���� ������ � ����� "Player"
        if (patrolIntoPlayer == null)
        {
            patrolIntoPlayer = GameObject.FindGameObjectWithTag("Player");
        }

        // ������� ��� ������� � ����� "PatrolPoint" � ��������� �� �������
        GameObject[] points = GameObject.FindGameObjectsWithTag("PatrolPoint");
        if (points.Length == 0) return; // ���� ����� ���, ����� �� ������

        patrolPoints = new Transform[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            patrolPoints[i] = points[i].transform;
        }

        // ������������� ������� ����� � ������ ����� �������
        transform.position = patrolPoints[0].position;
    }

    void Update()
    {
        // ���� ��� �����, ������� �� ������
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        // ������� ������� �����
        Transform targetPoint = patrolPoints[currentPointIndex];

        // �������� �� �������� � ������
        if (patrolIntoPlayer != null && (transform.position - patrolIntoPlayer.transform.position).sqrMagnitude < 5 * 5)
        {
            // ������� ����� � ������
            transform.position = Vector3.MoveTowards(transform.position, patrolIntoPlayer.transform.position, enemySpeed * Time.deltaTime);
        }
        else
        {
            // ������� ����� � ������� ����� �������
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, enemySpeed * Time.deltaTime);

            // ���������, ������ �� ���� ����� ������� (���������� ������� ���������� ��� �����������)
            if ((transform.position - targetPoint.position).sqrMagnitude < 0.05f * 0.05f) 
            {
                // ������� � ��������� ����� �������
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }
        }
    }
}
