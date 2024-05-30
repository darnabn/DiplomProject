using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTextZone : MonoBehaviour
{
    public GameObject textObject; // ���������� ��� ��������� ������ ���� ����� �������� Unity

    void Start()
    {
        textObject.SetActive(false); // ���������, ��� ����� ���������� �����
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ��������������, ��� � ������ ������ ���������� ��� "Player"
        {
            textObject.SetActive(true); // ���������� �����, ����� ����� ������ � ���� ����������
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ��������������, ��� � ������ ������ ���������� ��� "Player"
        {
            textObject.SetActive(false); // �������� �����, ����� ����� ������� �� ���� ����������
        }
    }
}
