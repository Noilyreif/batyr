using UnityEngine;

public class CamMove : MonoBehaviour
{
    [Header("Цель")]
    public GameObject player;

    [Header("Смещение камеры")]
    [Tooltip("Сдвиг камеры по X. 0 = камера ровно на персонаже.")]
    public float offsetX = 0f;

    [Tooltip("Сдвиг камеры по Y. Увеличьте, если персонаж слишком низко, уменьшите, если слишком высоко.")]
    public float offsetY = 1.5f;

    [Header("Настройки плавности")]
    public float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    [Header("Ограничение по высоте (Y)")]
    [Tooltip("Ниже этого значения камера не опустится (защита от пустоты под землей)")]
    public float minY = 0f;

    [Tooltip("Выше этого значения камера не поднимется (защита от неба)")]
    public float maxY = 15f;

    void LateUpdate()
    {
        if (player != null)
        {
            float targetX = player.transform.position.x + offsetX;
            float targetY = player.transform.position.y + offsetY;

            targetY = Mathf.Clamp(targetY, minY, maxY);

            Vector3 targetPosition = new Vector3(targetX, targetY, -10f);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}