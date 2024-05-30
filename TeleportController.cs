using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform teleportPoint; // �����, ���� ����� ����������������� ������
    public KeyCode teleportKey = KeyCode.E; // ������� ��� ������������

    private bool isPlayerNearby = false; // ���� ��� �������� ���������� ������� �����
    private Transform playerTransform; // ������ �� ��������� �������

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(teleportKey))
        {
            TeleportPlayer(); // ������������ �������
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) // �������� �� ������� ���������� PlayerController
        {
            isPlayerNearby = true; // ������ ����� � ������ ������������
            playerTransform = other.transform; // ���������� ������ �� ��������� �������
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) // �������� �� ������� ���������� PlayerController
        {
            isPlayerNearby = false; // ������ ���� �� ����� ������������
            playerTransform = null; // ������� ������ �� ��������� �������
        }
    }

    void TeleportPlayer()
    {
        if (playerTransform != null)
        {
            playerTransform.position = teleportPoint.position; // ����������� ������� � ����� ������������
        }
    }
}
