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

    // Если мяч ударился о щит
    if (collision.gameObject.CompareTag("PlayerShield") || collision.gameObject.CompareTag("EnemyShield"))
    {
        // Берём точку контакта
        float hitX = collision.contacts[0].point.x;
        float shieldX = collision.collider.bounds.center.x;
        float shieldWidth = collision.collider.bounds.size.x;

        // Вычисляем смещение от центра щита (-1 = левый край, 0 = центр, 1 = правый край)
        float offset = (hitX - shieldX) / (shieldWidth / 2f);

        // Делаем новый вектор скорости: X зависит от места удара, Y остаётся направленным вверх/вниз
        Vector2 newVelocity = new Vector2(offset, rb.linearVelocity.y > 0 ? 1 : -1);

        // Нормализуем и применяем текущую скорость
        rb.linearVelocity = newVelocity.normalized * currentSpeed;
    }
}

    IEnumerator DelayedStart()
    {
        // Ждём чуть‑чуть, чтобы всё успело инициализироваться
        yield return new WaitForSeconds(0.1f);

        // Запускаем мяч с последнего направления
        ResetBall(scoreManager.lastGoalByPlayer);
    }
}