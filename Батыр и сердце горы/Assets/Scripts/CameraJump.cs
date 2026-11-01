using UnityEngine;

public class CameraJump : MonoBehaviour
{
    [Header("Ссылка на игрока")]
    public Transform player;

    [Header("Точка перехода (координата X)")]
    public float jumpPointX = 22f;

    [Header("Позиции камеры")]
    public Vector3 location1Pos = new Vector3(0f, 0f, -10f);
    public Vector3 location2Pos = new Vector3(22.57f, 0f, -10f);

    void Update()
    {
        if (player == null) return;

        // Если игрок зашел за точку перехода (вправо)
        if (player.position.x >= jumpPointX)
        {
            // Ставим камеру во вторую локацию
            transform.position = location2Pos;
        }
        else
        {
            // Иначе камера остается в первой локации
            transform.position = location1Pos;
        }
    }
}