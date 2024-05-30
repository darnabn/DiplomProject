// FootstepSystem.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    public AudioClip[] stone;
    public AudioClip[] grass;

    private PlayerController playerController;

    void Start()
    {
        // �������� ������ �� ��������� PlayerController ������ ������� FootstepSystem
        playerController = GetComponentInChildren<PlayerController>();

        if (playerController == null)
        {
            // ���� ������ �� �������, ������ ��������������
            Debug.LogWarning("PlayerController component not found in FootstepSystem object.");
        }
    }

    void Update()
    {
        // ���������, ��� �� ��������������� ������ playerController
        if (playerController != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 2))
            {
                if (hit.collider.CompareTag("Grass"))
                {
                    // �������� ������ ������ ����� �� ����� � PlayerController
                    playerController.SetFootstepSounds(grass);
                }
                else if (hit.collider.CompareTag("Stone"))
                {
                    // �������� ������ ������ ����� �� ����� � PlayerController
                    playerController.SetFootstepSounds(stone);
                }
            }
        }
    }
}
