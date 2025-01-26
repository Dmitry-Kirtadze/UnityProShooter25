using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 5f; // �������� ������������
    [SerializeField] private GameObject patrolIntoPlayer; // �������� ���� ������, ����� ���� �������� �� �������
    [SerializeField] private float EnemyPatrolDistance = 6f;
    private int currentPointIndex = 0; // ������ ������� �����

    private Transform[] patrolPoints; // ������ ����� �������

    void Start()
    {
        // ���� patrolIntoPlayer �� �������� � ����������, �������� ����� ������ � ����� "Player"
        if (patrolIntoPlayer == null)
        {
            StartCoroutine(FindPlayerWhenAvailable());
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

    // ��������, ������� ����� ������ ������, ���� ��� ��� ���
    private IEnumerator FindPlayerWhenAvailable()
    {
        while (patrolIntoPlayer == null)
        {
            patrolIntoPlayer = GameObject.FindGameObjectWithTag("Player");

            if (patrolIntoPlayer != null)
            {
                break; // ���� ����� ������, ������� �� �����
            }

            yield return null; // ���� 1 ���� � ������� �����
        }

        if (patrolIntoPlayer == null)
        {
            Debug.LogError("Player object not found! Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        // ���� ��� �����, ������� �� ������
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        // ������� ������� �����
        Transform targetPoint = patrolPoints[currentPointIndex];

        // �������� �� �������� � ������
        if (patrolIntoPlayer != null && (transform.position - patrolIntoPlayer.transform.position).sqrMagnitude < EnemyPatrolDistance * EnemyPatrolDistance)
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
