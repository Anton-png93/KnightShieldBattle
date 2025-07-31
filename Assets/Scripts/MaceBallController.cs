using UnityEngine;
using System.Collections;

public class MaceBallController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float startSpeed = 2f;               // стартовая скорость
    public float speedIncreasePerSecond = 0.5f; // можно включить позже, если нужно ускорение

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

        // Жёстко фиксируем верх и низ (чтобы шар не улетал за экран)
        float top = 2.5f;     // чуть ниже верхнего края
        float bottom = -2.5f; // чуть выше нижнего края

        if (fromPlayer)
        {
            // Мяч появляется снизу и летит вверх
            transform.position = new Vector2(0f, bottom);
            rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), 1f).normalized * startSpeed;
        }
        else
        {
            // Мяч появляется сверху и летит вниз
            transform.position = new Vector2(0f, top);
            rb.linearVelocity = new Vector2(Random.Range(-1f, 1f), -1f).normalized * startSpeed;
        }

        Debug.Log("Spawn position: " + transform.position + " | Velocity: " + rb.linearVelocity);

        timeSinceServe = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSound);
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.1f);
        ResetBall(scoreManager.lastGoalByPlayer);
    }
}