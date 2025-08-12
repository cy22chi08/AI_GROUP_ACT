using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea]
    public string text;
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string npcName;
    public DialogueLine[] lines;
}
