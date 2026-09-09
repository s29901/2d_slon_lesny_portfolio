using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenerInfoTab : MonoBehaviour

{
    [Tooltip("Тот самый большой Info-Panel, который будем открывать/закрывать")]
    public GameObject infoPanel;

    [Tooltip("Игрок или всё, что скрываем пока открыто окно")]
    public GameObject playerRoot;

    private bool isOpen = false;

    // этот метод будем привязывать к OnClick каждой кнопки
    public void ToggleInfo()
    {
        isOpen = !isOpen;
        infoPanel.SetActive(isOpen);
        if (playerRoot != null)
            playerRoot.SetActive(!isOpen);
        Debug.Log($"[InfoTabOpener] {(isOpen? "Open":"Close")} {infoPanel.name}");
    }
}

