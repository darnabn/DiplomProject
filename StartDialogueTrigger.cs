using UnityEngine;

public class StartDialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public DialogueScriptableObject initialDialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(initialDialogue);
        }
    }
}