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

    // Таймер атаки
    private float attackTimer = 0f;
    private float attackDuration = 0.3f; // Время атаки в секундах (настройте под свою анимацию)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. ПРОВЕРКА ТАЙМЕРА АТАКИ
        if (attackTimer > 0)
        {
            // Если мы атакуем, отнимаем время
            attackTimer -= Time.deltaTime;

            // Останавливаем персонажа на месте во время атаки (чтобы не скользил)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            // Выходим из Update, чтобы не считывать движение, пока бьем
            return;
        }

        // 2. ДВИЖЕНИЕ
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 3. ПОВОРОТ
        if (moveInput > 0 && !isFacingRight) Flip();
        else if (moveInput < 0 && isFacingRight) Flip();

        // 4. ПРЫЖОК
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 5. АТАКА (Левая кнопка мыши)
        if (Input.GetMouseButtonDown(0))
        {
            attackTimer = attackDuration; // Запускаем таймер атаки
            animator.Play("Player_Attack"); // Включаем анимацию
            return; // Выходим, чтобы в этом кадре не включилась другая анимация
        }

        // 6. АНИМАЦИИ (только если не атакуем)
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
