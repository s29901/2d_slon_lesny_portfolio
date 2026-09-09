using UnityEngine;

public class SceneEntryHandler : MonoBehaviour
{
    public GameObject Player;
    public GameObject duze_info;

    void Start()
    {
        // Проверим, есть ли позиция для восстановления
        if (PlayerMemory.HasSavedPosition && Player != null)
        {
            Debug.Log("ВОССТАНАВЛИВАЕМ ПОЗИЦИЮ: " + PlayerMemory.LastPosition);

            // Отключаем скрипт движения временно
            var movement = Player.GetComponent<Target>();
            if (movement != null) movement.enabled = false;

            // Жёстко задаём позицию
            Player.transform.position = PlayerMemory.LastPosition;
            
            Debug.Log("ПОЗИЦИЯ ПРИМЕНЕНА ЖЁСТКО: " + Player.transform.position);
            

            // Включаем обратно движение через немного времени
            StartCoroutine(ReenableMovement(movement));
        }
        else
        {
            Debug.Log("ПОЗИЦИЯ НЕ СОХРАНЕНА. ИГРА ЗАПУЩЕНА С НУЛЯ.");
        }

        if (duze_info != null)
        {
            duze_info.SetActive(false);
        }
    }

    // Корутина для включения движения через кадр
    System.Collections.IEnumerator ReenableMovement(Target movement)
    {
        yield return new WaitForEndOfFrame();
        if (movement != null) movement.enabled = true;
    }
}