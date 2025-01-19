using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    [SerializeField] private Camera myCamera;

    private void Awake()
    {
      
        myCamera = Camera.main;
   
    }

    private void LateUpdate()
    {
        if (myCamera != null)
        {
            // ����������� ������ �� ������
            Vector3 direction = transform.position - myCamera.transform.position;
            transform.LookAt(transform.position + direction);

            // ���� ���������, ��������� ������� �� 180 ��������
            transform.Rotate(Vector3.up * 180);
        }
    }
}
