using UnityEngine;

public class ToggleInfoPanel : MonoBehaviour
{
    [Tooltip("Панель, которую надо открывать/закрывать")]
    public GameObject infoPanel;

    [Tooltip("Игрок или корень объектов, которые нужно скрывать при открытой панели")]
    public GameObject playerRoot;

    public void Toggle()
    {
        // 1) Логируем факт вызова и текущее состояние панели
        Debug.Log($"[ToggleInfoPanel] Toggle() called on {infoPanel.name}, wasActive = {infoPanel.activeSelf}");

        if (infoPanel == null)
        {
            Debug.LogWarning("[ToggleInfoPanel] infoPanel == null!");
            return;
        }

        // 2) Переключаем активность
        bool nowOpen = !infoPanel.activeSelf;
        infoPanel.SetActive(nowOpen);

        // 3) Если хотим, чтобы панель всегда была сверху в Canvas
        var rt = infoPanel.GetComponent<RectTransform>();
        if (rt != null)
            rt.SetAsLastSibling();

        // 4) Скрываем/показываем игрока
        if (playerRoot != null)
            playerRoot.SetActive(!nowOpen);

        // 5) Логируем результат
        Debug.Log($"[ToggleInfoPanel] { (nowOpen ? "Opened" : "Closed") } {infoPanel.name}");
    }

    private void Start()
    {
        // Сразу скрываем панель, если случайно забыли
        if (infoPanel != null && infoPanel.activeSelf)
            infoPanel.SetActive(false);
    }
}

