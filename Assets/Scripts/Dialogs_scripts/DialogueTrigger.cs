using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Tooltip("Сюда перетащи DialogueManager")]
    public DialogueManager dialogueManager;

    void OnMouseDown()
    {
        dialogueManager.StartDialogue();
    }
}