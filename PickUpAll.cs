using UnityEngine;

public class PickUpAll : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public Transform handPosition; // ��������� ���� ��� ������� ������� ����
    public Transform cameraTransform; // ��������� ���� ��� ������� ������� ������
    private bool isPickedUp = false; // ���� ��� ��������, ������ �� ����

    private Vector3 originalPosition; // �������� ������� �������
    private Quaternion originalRotation; // �������� ������� �������

    void Start()
    {
        // ���������� �������� ������� � �������� �������
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // �������� ������� ������ "E" � ��������� �� ����� � ���� ��������
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            
                PickUp();
            
            
        }
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            
                Drop();
            
        }

        // ���� ���� ������, ��������� �� �������� ����
        if (isPickedUp)
        {
            FollowHand();
        }
    }

    // ����� ��� �������� �����
    private void PickUp()
    {
        // ������ �������� �����
        Debug.Log("���� ������!");

        // ����������� ����� � ������� ����
        transform.position = handPosition.position;
        transform.rotation = handPosition.rotation;
        transform.parent = handPosition; // ���������� ������ ����� �������� ��� ����

        isPickedUp = true; // ���������� ���� �������� �����
    }

    // ����� ��� ���������� �� �����
    private void FollowHand()
    {
        transform.position = handPosition.position;
        transform.rotation = handPosition.rotation;
    }

    // ����� ��� ������ �����
    private void Drop()
    {
        // ������ ������ �����
        Debug.Log("���� �������!");

        // ���������� ��������� ������� ��� �����
        transform.parent = null;

        // ���������� ������� ����� ��� ������� (��������)
        Transform playerTransform = handPosition.root; // ��������������, ��� handPosition �������� �������� �������� ������
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 1.0f, playerTransform.position.z); // ���������� ���� ��� �������

        // �������������� �������� ���������� �������
        transform.rotation = originalRotation;

        isPickedUp = false; // �������� ���� �������� �����
    }

    // �������, ����� ������ ������ � �������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("����� � ���� �������� �����");
        }
    }

    // �������, ����� ������ ������� �� ��������
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("����� ����� �� ���� �������� �����");
        }
    }
}
