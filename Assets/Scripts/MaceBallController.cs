using UnityEngine;
using System.Collections;

public class MaceBallController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float startSpeed = 3.5f;
    public float speedIncreasePerSecond = 0.5f;

    private float currentSpeed;
    private float timeSinceServe;
    private Rigidbody2D rb;
    public AudioClip hitSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DelayedStart());
        currentSpeed = startSpeed;
        audioSource = GetComponent<AudioSource>();
    }

    public void ResetBall(bool fromPlayer)
    {
        rb.linearVelocity = Vector2.zero;

        if (fromPlayer)
        {
            // Находим щит игрока
            Transform playerShield = GameObject.FindWithTag("PlayerShield").transform;

            // Шар появляется чуть выше щита игрока
            transform.position = new Vector2(playerShield.position.x, playerShield.position.y + 1f);

            rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * startSpeed;
        }
        else
        {
            // Находим щит врага
            Transform enemyShield = GameObject.FindWithTag("EnemyShield").transform;

            // Шар появляется чуть ниже щита врага
            transform.position = new Vector2(enemyShield.position.x, enemyShield.position.y - 1f);

            rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), -1f).normalized * startSpeed;
        }

        timeSinceServe = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSound);

        // Берём текущую скорость
        Vector2 velocity = rb.linearVelocity;

        // Если мяч летит почти по прямой (почти горизонтально) → задаём минимальный угол
        if (Mathf.Abs(velocity.y) < 0.3f)
        {
            velocity.y = Mathf.Sign(velocity.y) * 0.3f;
        }

        // Обновляем скорость
        rb.linearVelocity = velocity.normalized * currentSpeed;
    }

    IEnumerator DelayedStart()
    {
        // Ждём чуть‑чуть, чтобы всё успело инициализироваться
        yield return new WaitForSeconds(0.1f);

        // Запускаем мяч с последнего направления
        ResetBall(scoreManager.lastGoalByPlayer);
    }
}