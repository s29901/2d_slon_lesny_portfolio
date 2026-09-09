using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowX : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float offsetX = 0f; // Смещение по оси X

    void Update()
    {
        if (player != null)
        {
            // Камера следует за игроком только по оси X
            transform.position = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }
}

