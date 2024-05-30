using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public DialogueLine[] lines; // Массив строк диалога
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName; // Имя говорящего
    [TextArea(3, 10)]
    public string sentence; // Строка диалога
}
