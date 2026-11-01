using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Настройки атаки")]
    public int attackDamage = 20;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    void Update()
    {
        // Проверяем нажатие ЛЕВОЙ кнопки мыши (0 - левая, 1 - правая)
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Если точка атаки не задана, выводим ошибку в консоль и останавливаемся
        if (attackPoint == null)
        {
            Debug.LogError("Ошибка: Точка атаки (Attack Point) не назначена в инспекторе!");
            return;
        }

        // Ищем все коллайдеры на слое Enemy в радиусе атаки
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        bool hitSomething = false; // Проверка, попали ли мы хоть во что-то

        // Перебираем всех врагов, в которых попали
        foreach (Collider2D enemy in hitEnemies)
        {
            // Ищем на объекте скрипт Health
            Health enemyHealth = enemy.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                hitSomething = true;
            }
        }

        // Если удар не попал ни в кого, пишем в консоль (полезно для отладки)
        if (!hitSomething)
        {
            Debug.Log("Удар в пустоту (не попал в манекен).");
        }
    }

    // Этот метод рисует красный круг в окне Scene, чтобы вы видели радиус атаки
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}