using UnityEngine;

public class EnemyShieldAI : MonoBehaviour
{
    public Transform maceBall;

    public float startSpeed = 3.5f;
    public float maxSpeed = 6f;
    public float speedGrowth = 0.2f;

    public float startError = 0.4f;
    public float minError = 0.02f;
    public float errorDecrease = 0.02f;

    private float currentSpeed;
    private float currentError;

    private Rigidbody2D rb;
    private ScoreManager scoreManager;
    [Range(0f, 1f)] public float difficulty = 0.5f; // 0 — легко, 1 — сложно

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, difficulty);
        currentError = Mathf.Lerp(startError, minError, difficulty);

        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        if (maceBall == null) return;

        // Реакция на счёт
        if (scoreManager != null && scoreManager.rightScore >= 5)
        {
            currentSpeed = Mathf.Min(maxSpeed, currentSpeed + speedGrowth * 2 * Time.deltaTime);
            currentError = Mathf.Max(minError, currentError - errorDecrease * 2 * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.Min(maxSpeed, currentSpeed + speedGrowth * Time.deltaTime);
            currentError = Mathf.Max(minError, currentError - errorDecrease * Time.deltaTime);
        }

        float targetX = maceBall.position.x + Random.Range(-currentError, currentError);
        Vector2 targetPos = new Vector2(targetX, rb.position.y);
        rb.MovePosition(Vector2.Lerp(rb.position, targetPos, currentSpeed * Time.deltaTime));
    }
}
