using UnityEngine;

public class HideInCloset : MonoBehaviour
{
    public Transform hidePosition; // �������, ���� ������������ �����, ����� ���������
    private bool isHiding = false;
    private Transform player;
    private CharacterController playerController;
    private Vector3 initialPosition; // ��������� ������� ������ ����� �������

    public Renderer closetRenderer; // Renderer �����
    public Material normalMaterial; // ���������� �������� (������������)
    public Material transparentMaterial; // ���������� ��������

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<CharacterController>();

        // ���������� ���������� �������� �� ���������
        closetRenderer.material = normalMaterial;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHiding)
            {
                Hide();
            }
            else
            {
                Unhide();
            }
        }
    }

    void Hide()
    {
        Debug.Log("Player is hiding");
        // ��������� ��������� ������� ������
        initialPosition = player.position;

        // ��������� ���������� �������
        playerController.enabled = false;
        player.position = hidePosition.position;
        isHiding = true;

        // ������� ���� ����������
        closetRenderer.material = transparentMaterial;
    }

    void Unhide()
    {
        Debug.Log("Player is unhiding");
        // �������� ���������� �������
        playerController.enabled = true;
        // ������� ������ �� ���������� �������
        player.position = initialPosition;
        isHiding = false;

        // ������� ���� ����� ����������
        closetRenderer.material = normalMaterial;
    }
}
