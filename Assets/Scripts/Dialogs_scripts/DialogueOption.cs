using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    [Tooltip("Текст варианта, который увидит игрок")]
    public string text;

    [Tooltip("Индекс следующего узла в массиве lines")]
    public int nextLineIndex;
}

