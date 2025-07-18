using UnityEngine;
using System.Collections;

public class MaceBallController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float startSpeed = 2f;
    public float speedIncreasePerSecond = 0.5f;

    private float currentSpeed;
    private float timeSinceServe;
    private Rigidbody2D rb;
    public AudioClip hitSound;
    private AudioSource audioSource;

    private bool serveToPlayer = true;

    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DelayedStart());
        currentSpeed = startSpeed;
        audioSource = GetComponent<AudioSource>();
        
    }
    public void ResetBall(bool playerServe)
{
    transform.position = Vector2.zero;

    Vector2 direction;
    if (serveToPlayer)
    {
        direction = new Vector2(-1, 1).normalized;
    }
    else
    {
        direction = new Vector2(1, 1).normalized;
    }

   rb.linearVelocity = direction * startSpeed;
    serveToPlayer = !serveToPlayer; // Меняем сторону подачи
    timeSinceServe = 0f;
}
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(hitSound);
        // Проверка — если мяч почти не двигается по Y
        if (Mathf.Abs(rb.linearVelocity.y) < 0.5f)
        {
            Vector2 fixedVelocity = rb.linearVelocity;
            fixedVelocity.y = Random.Range(-1f, 1f); // Случайный вертикальный толчок
            rb.linearVelocity = fixedVelocity.normalized * currentSpeed;
        }
    }
IEnumerator DelayedStart()
{
    yield return new WaitForSeconds(0.1f); // Подождём, пока всё инициализируется
    ResetBall(scoreManager.lastGoalByPlayer); // Теперь переменная уже точно установлена
}

}