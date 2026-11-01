using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float lifetime = 0.5f; // Время жизни эффекта

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}