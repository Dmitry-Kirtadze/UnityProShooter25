using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class RecoilController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform cameraTransform; // ������ ������
    [SerializeField] private Transform weapon;
    [SerializeField] public PlayerController pc;
    [SerializeField] private float returnSpeed = 5f;    // �������� �������� ������
    [SerializeField] private float maxRecoil = 15f;    // ����������� ������

    private Vector3 currentRecoil; // ������� ����������� ������
    private Vector3 targetRecoil;  // �������� ������
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        weapon = GetComponent<Transform>();
    }
    private void Update()
    {
       
        if (photonView.IsMine)
        {
            pc = GetComponent<PlayerController>();
            cameraTransform = pc.playerCamera.GetComponent<Transform>();
            // ������ ���������� ������ � �������� ���������
            currentRecoil = Vector3.Lerp(currentRecoil, Vector3.zero, returnSpeed * Time.deltaTime);
            weapon.rotation = Quaternion.Lerp(currentRecoil, Vector3.zero, returnSpeed * Time.deltaTime);
            // ��������� ������ ������ �� ��������� � �����������
            cameraTransform.localEulerAngles -= currentRecoil;
        }
    }

    public void ApplyRecoil(string weaponType)
    {
        if (weaponType == "Melee") return; // ������ �������� ��� �� ����� ������

        float verticalRecoil = 0f;
        float horizontalRecoil = 0f;

        if (weaponType == "Pistol")
        {
            verticalRecoil = Random.Range(3f, 5f);   // ��������� ������ �����
            horizontalRecoil = Random.Range(-0.5f, 0.5f); // ������ ����� � �������
        }
        else if (weaponType == "AK47")
        {
            verticalRecoil = Random.Range(10f, 20f);   // ������� ������ �����
            horizontalRecoil = Random.Range(-1f, 1f); // ����� ������ ����������
        }

        // ������������ ������ �� ���������
        verticalRecoil = Mathf.Clamp(verticalRecoil, 0f, maxRecoil);

        // ��������� ���������� ������
        targetRecoil = new Vector3(verticalRecoil, horizontalRecoil, 0f);
        currentRecoil = targetRecoil;  // ���������� ��������� ������
    }
}
