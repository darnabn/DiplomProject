using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public DialogueLine[] lines; // ������ ����� �������
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName; // ��� ����������
    [TextArea(3, 10)]
    public string sentence; // ������ �������
}
