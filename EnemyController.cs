using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float viewDistance = 10f; // –ассто€ние видимости
    public float viewAngle = 60f; // ”гол видимости
    public float playerViewAngle = 45f; // ”гол видимости игрока (если смотреть на противника)
    public LayerMask hidingLayer; // —лой дл€ мест, где игрок может пр€татьс€
    public Transform dialogueFocusPoint; // “очка фокусировки камеры
    public CameraController playerCameraController; //  онтроллер камеры игрока
    public DialogueManager dialogueManager; // ћенеджер диалогов
    public DialogueScriptableObject initialDialogue; // Ќачальный диалог

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
        if (isDialogueActive) return; // ≈сли диалог активен, остальной код не выполн€етс€

        if (isChasing)
        {
            if (IsPlayerLookingAtEnemy())
            {
                // ќстановить преследование, если игрок смотрит на противника
                navMeshAgent.ResetPath();
                animator.SetBool("walking", false);
            }
            else
            {
                // ≈сли противник преследует игрока
                navMeshAgent.SetDestination(player.position);
                if (Vector3.Distance(transform.position, player.position) < 2f)
                {
                    // ≈сли противник достиг игрока, выполните соответствующее действие (например, проигрыш игрока)
                    Debug.Log("Enemy caught the player!");
                }
            }
        }
        else if (CanSeePlayer())
        {
            // ≈сли противник видит игрока и диалог еще не начат
            if (!hasSeenPlayer)
            {
                ShowDialogue();
                hasSeenPlayer = true;
            }
            else if (!IsPlayerLookingAtEnemy() && IsPlayerMoving())
            {
                // ≈сли противник видит игрока, игрок движетс€ и не смотрит на противника, противник преследует его
                isChasing = true;
                navMeshAgent.SetDestination(player.position);
                Debug.Log("Player is visible, moving, and not looking at enemy. Enemy is chasing.");
            }
        }

        // ”станавливаем параметры аниматора в зависимости от состо€ни€ противника
        animator.SetBool("walking", navMeshAgent.velocity.magnitude > 0.1f);
    }

    // ѕроверка, видит ли противник игрока
    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // ѕровер€ем, находитс€ ли игрок в пределах угла видимости
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

    // ѕроверка, смотрит ли игрок на противника
    bool IsPlayerLookingAtEnemy()
    {
        Vector3 directionToEnemy = transform.position - player.position;
        float angleToEnemy = Vector3.Angle(player.forward, directionToEnemy);

        // ѕровер€ем, находитс€ ли противник в пределах угла видимости игрока
        return angleToEnemy < playerViewAngle / 2f;
    }

    // ѕроверка, двигаетс€ ли игрок
    bool IsPlayerMoving()
    {
        return player.GetComponent<CharacterController>().velocity.magnitude > 0.1f;
    }

    // ‘ункци€ дл€ показа диалогового окна
    public void ShowDialogue()
    {
        isDialogueActive = true;
        navMeshAgent.isStopped = true;
        playerCameraController.LockCamera(dialogueFocusPoint);

        // «апускаем диалог через диалоговый менеджер
        dialogueManager.StartDialogue(initialDialogue);
    }

    // ‘ункци€ дл€ завершени€ диалога
    public void EndDialogue()
    {
        isDialogueActive = false;
        navMeshAgent.isStopped = false;

        // ¬ключаем обычное управление камерой
        playerCameraController.UnlockCamera();
    }
}
