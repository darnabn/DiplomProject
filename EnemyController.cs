using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float viewDistance = 10f; // ���������� ���������
    public float viewAngle = 60f; // ���� ���������
    public float playerViewAngle = 45f; // ���� ��������� ������ (���� �������� �� ����������)
    public LayerMask hidingLayer; // ���� ��� ����, ��� ����� ����� ���������
    public Transform dialogueFocusPoint; // ����� ����������� ������
    public CameraController playerCameraController; // ���������� ������ ������
    public DialogueManager dialogueManager; // �������� ��������
    public DialogueScriptableObject initialDialogue; // ��������� ������

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isDialogueActive = false;
    private bool isChasing = false;
    private bool hasSeenPlayer = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDialogueActive) return; // ���� ������ �������, ��������� ��� �� �����������

        if (isChasing)
        {
            if (IsPlayerLookingAtEnemy())
            {
                // ���������� �������������, ���� ����� ������� �� ����������
                navMeshAgent.ResetPath();
                animator.SetBool("walking", false);
            }
            else
            {
                // ���� ��������� ���������� ������
                navMeshAgent.SetDestination(player.position);
                if (Vector3.Distance(transform.position, player.position) < 2f)
                {
                    // ���� ��������� ������ ������, ��������� ��������������� �������� (��������, �������� ������)
                    Debug.Log("Enemy caught the player!");
                }
            }
        }
        else if (CanSeePlayer())
        {
            // ���� ��������� ����� ������ � ������ ��� �� �����
            if (!hasSeenPlayer)
            {
                ShowDialogue();
                hasSeenPlayer = true;
            }
            else if (!IsPlayerLookingAtEnemy() && IsPlayerMoving())
            {
                // ���� ��������� ����� ������, ����� �������� � �� ������� �� ����������, ��������� ���������� ���
                isChasing = true;
                navMeshAgent.SetDestination(player.position);
                Debug.Log("Player is visible, moving, and not looking at enemy. Enemy is chasing.");
            }
        }

        // ������������� ��������� ��������� � ����������� �� ��������� ����������
        animator.SetBool("walking", navMeshAgent.velocity.magnitude > 0.1f);
    }

    // ��������, ����� �� ��������� ������
    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // ���������, ��������� �� ����� � �������� ���� ���������
        if (angleToPlayer < viewAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewDistance))
            {
                if (hit.transform == player)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // ��������, ������� �� ����� �� ����������
    bool IsPlayerLookingAtEnemy()
    {
        Vector3 directionToEnemy = transform.position - player.position;
        float angleToEnemy = Vector3.Angle(player.forward, directionToEnemy);

        // ���������, ��������� �� ��������� � �������� ���� ��������� ������
        return angleToEnemy < playerViewAngle / 2f;
    }

    // ��������, ��������� �� �����
    bool IsPlayerMoving()
    {
        return player.GetComponent<CharacterController>().velocity.magnitude > 0.1f;
    }

    // ������� ��� ������ ����������� ����
    public void ShowDialogue()
    {
        isDialogueActive = true;
        navMeshAgent.isStopped = true;
        playerCameraController.LockCamera(dialogueFocusPoint);

        // ��������� ������ ����� ���������� ��������
        dialogueManager.StartDialogue(initialDialogue);
    }

    // ������� ��� ���������� �������
    public void EndDialogue()
    {
        isDialogueActive = false;
        navMeshAgent.isStopped = false;

        // �������� ������� ���������� �������
        playerCameraController.UnlockCamera();
    }
}
