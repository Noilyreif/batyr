using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        // Находим компонент Animator на манекене
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Манекен получил урон: {damage}. Осталось HP: {currentHealth}");

        // Если есть Animator, запускаем анимацию удара (HIT)
        if (anim != null)
        {
            anim.SetTrigger("Hit");
        }
    }
}