using UnityEngine;

[System.Serializable]
public class DialogueQuestion
{
    [Tooltip("Текст вопроса, который увидит игрок")]
    public string questionText;

    [TextArea(2, 4)]
    [Tooltip("Текст ответа от Гвидона")]
    public string answerText;

    [HideInInspector]
    public bool asked = false;
}