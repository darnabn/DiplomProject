using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text speakerNameText; // Привязка к TextMeshPro объекту
    public TMP_Text dialogueText; // Привязка к TextMeshPro объекту
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f; // Скорость печати
    public EnemyController enemyController; // Привязка к контроллеру противника

    private Queue<DialogueLine> lines;
    private bool isTyping = false;
    private bool dialogueActive = false;

    void Start()
    {
        lines = new Queue<DialogueLine>();
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialogueActive && Input.GetMouseButtonDown(0) && !isTyping)
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(DialogueScriptableObject dialogueSO)
    {
        Debug.Log("Starting dialogue...");
        if (dialogueSO == null)
        {
            Debug.LogError("DialogueScriptableObject is null!");
            return;
        }

        lines.Clear();

        foreach (DialogueLine line in dialogueSO.lines)
        {
            lines.Enqueue(line);
        }

        dialogueActive = true;
        dialoguePanel.SetActive(true);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = lines.Dequeue();
        speakerNameText.text = line.speakerName;
        StartCoroutine(TypeSentence(line.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        Debug.Log("Ending dialogue...");
        dialogueActive = false;
        dialoguePanel.SetActive(false);

        // Уведомляем контроллер противника, что диалог завершен
        enemyController.EndDialogue();
    }
}
