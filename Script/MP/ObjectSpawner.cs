using UnityEngine;
using Photon.Pun;

public class ObjectSpawner : MonoBehaviourPunCallbacks
{
    public GameObject[] objectsToSpawn; // ������ �������� ��� ������
    public Transform[] spawnPoints; // ������ �����, ��� ����� �������� �������
    public float spawnInterval = 5f; // �������� ����� �������� � ��������

    private float nextSpawnTime = 0f;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            nextSpawnTime = Time.time + spawnInterval; // ������������� ����� ��� ������� ������
        }
    }

    private void Update()
    {
        // ���������, ���� ������� ����� ������ ��� ����� ������� ���������� ������
        if (PhotonNetwork.IsConnected && Time.time >= nextSpawnTime)
        {
            SpawnRandomObject();
            nextSpawnTime = Time.time + spawnInterval; // ��������� ����� ��� ���������� ������
        }
    }

    private void SpawnRandomObject()
    {
        // �������� ��������� ������ �� �������
        int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

        // �������� ��������� ����� ��� ������
        int randomPointIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomPointIndex];

        // ������� ������ ����� Photon � �������������� PhotonNetwork.Instantiate
        PhotonNetwork.Instantiate(objectToSpawn.name, spawnPoint.position, spawnPoint.rotation);
    }
}
