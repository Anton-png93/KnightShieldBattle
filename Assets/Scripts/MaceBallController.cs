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
    private Vector2 direction;
    

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

    serveToPlayer = playerServe;

    serveToPlayer = playerServe; // ⬅ ПЕРЕНЕСЛИ НАВЕРХ

   // direction уже объявлен выше
    if (serveToPlayer)
    {
        direction = new Vector2(-1, 1).normalized;
    }
    else
    {
        direction = new Vector2(1, 1).normalized;
    }

    rb.linearVelocity = direction * startSpeed;
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
           rb.linearVelocity = direction * startSpeed;
        }
    }
IEnumerator DelayedStart()
{
    yield return new WaitForSeconds(0.1f); // Подождём, пока всё инициализируется
   
}

}