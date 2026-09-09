using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [Tooltip("Имя говорящего (например, „M“ или „Gwidon“).")]
    public string speakerName;

    [TextArea(2,5)]
    [Tooltip("Текст реплики.")]
    public string dialogueText;
    
    [Tooltip("Если пустой — диалог продолжится автоматически «Далее». Если не пустой — покажутся эти варианты.")]
    public DialogueOption[] options;
}
