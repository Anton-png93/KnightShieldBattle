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

        // Мяч появляется чуть выше щита игрока (на 1 юнит выше)
        transform.position = new Vector2(playerShield.position.x, playerShield.position.y + 1f);

        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * startSpeed;
    }
    else
    {
        // Находим щит врага
        Transform enemyShield = GameObject.FindWithTag("EnemyShield").transform;

        // Мяч появляется чуть ниже щита врага (на 1 юнит ниже)
        transform.position = new Vector2(enemyShield.position.x, enemyShield.position.y - 1f);

        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), -1f).normalized * startSpeed;
    }

    timeSinceServe = 0f;
}

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSound);

        // Если мяч летит почти горизонтально — добавим ему немного Y, чтобы не застревал
        if (Mathf.Abs(rb.linearVelocity.y) < 0.5f)
        {
            Vector2 fixedVelocity = rb.linearVelocity;
            fixedVelocity.y = Random.Range(-1f, 1f);
            rb.linearVelocity = fixedVelocity.normalized * currentSpeed;
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.1f);
        ResetBall(scoreManager.lastGoalByPlayer); // запускаем после старта
    }
}