using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithBounds : MonoBehaviour
{
    public Transform player; // Игрок
    public Transform leftBoundary; // Левая граница
    public Transform rightBoundary; // Правая граница
    public float offsetX = 0f; // Смещение по X

    private float cameraHalfWidth;

    void Start()
    {
        // Вычисляем половину ширины камеры (с учётом ортографической проекции)
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

  /*  void Update()
    {
        if (player != null)
        {
            // Камера следует за игроком только по оси X
            transform.position = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }*/ 
    void LateUpdate()
    {
        if (player != null)
        {
            // Получаем позицию игрока по X с учётом смещения
            float targetX = player.position.x + offsetX;

            // Ограничиваем движение камеры между границами
            float leftLimit = leftBoundary.position.x + cameraHalfWidth;
            float rightLimit = rightBoundary.position.x - cameraHalfWidth;

            // Ограничиваем X координату камеры
            float clampedX = Mathf.Clamp(targetX, leftLimit, rightLimit);

            // Обновляем позицию камеры
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
}
