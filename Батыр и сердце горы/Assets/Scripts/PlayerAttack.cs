using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Настройки атаки")]
    public int attackDamage = 20;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Спецэффект")]
    public GameObject hitEffectPrefab;

    // Внимание: метод теперь public, чтобы его мог вызвать скрипт Player
    public void Attack()
    {
        // Если точка атаки не назначена - выходим
        if (attackPoint == null) return;

        // 1. Создаем эффект
        if (hitEffectPrefab != null)
        {
            // Создаем эффект в позиции точки атаки
            GameObject effect = Instantiate(hitEffectPrefab, attackPoint.position, attackPoint.rotation);

            // 2. Проверяем, куда смотрит персонаж (через масштаб X)
            // Если localScale.x меньше 0, значит персонаж повернут влево
            if (transform.localScale.x < 0)
            {
                Vector3 effectScale = effect.transform.localScale;
                // Разворачиваем эффект по оси X (зеркально)
                effectScale.x = -Mathf.Abs(effectScale.x);
                effect.transform.localScale = effectScale;
            }
        }

        // 3. Наносим урон врагам в радиусе
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    // Рисует красный круг в редакторе (для удобства настройки радиуса)
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}