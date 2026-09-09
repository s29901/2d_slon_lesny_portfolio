using UnityEngine;

public class InfoZoneTrigger : MonoBehaviour
{
    [Tooltip("UI-кнопка, которую будем включать/выключать")]
    public GameObject infoButton;

    void Start()
    {
        // Скрываем кнопку в начале
        if (infoButton != null)
            infoButton.SetActive(false);
    }

    // Когда игрок заходит в зону
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && infoButton != null)
            infoButton.SetActive(true);
    }

    // Когда игрок выходит из зоны
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && infoButton != null)
            infoButton.SetActive(false);
    }
}