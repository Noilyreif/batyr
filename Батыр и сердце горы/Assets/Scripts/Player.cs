using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;

    // Ссылка на скрипт атаки
    private PlayerAttack playerAttack;

    private float attackTimer = 0f;
    private float attackDuration = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Находим скрипт атаки на этом же объекте
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        // --- ПРОВЕРКА НА ПАУЗУ ---
        if (PauseManager.isPaused)
        {
            // Останавливаем движение по горизонтали
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            // Переключаем на анимацию покоя, чтобы персонаж не застывал в ударе
            if (animator != null) animator.Play("Player_Idle");

            return; // Выходим из Update, не обрабатывая ввод
        }
        // -------------------------

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0 && !isFacingRight) Flip();
        else if (moveInput < 0 && isFacingRight) Flip();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // АТАКА
        if (Input.GetMouseButtonDown(0))
        {
            attackTimer = attackDuration;
            animator.Play("Player_Attack");

            // ВЫЗЫВАЕМ СКРИПТ АТАКИ (создание эффекта и нанесение урона)
            if (playerAttack != null)
            {
                playerAttack.Attack();
            }

            return;
        }

        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0) animator.Play("Player_Idle");
            else animator.Play("Player_Run");
        }
        else
        {
            if (rb.linearVelocity.y > 0) animator.Play("Player_Jump");
            else animator.Play("Player_Fall");
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}