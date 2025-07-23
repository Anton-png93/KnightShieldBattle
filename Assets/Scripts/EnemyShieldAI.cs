using UnityEngine;

public class EnemyShieldAI : MonoBehaviour
{
    public Transform maceBall;

    public float moveSpeed = 6f;        // Скорость щита
    public float predictionOffset = 1f; // Насколько выше целиться (атака)
    public float errorMargin = 0.2f;    // Погрешность

    private Rigidbody2D rb;

    private float topLimit;
    private float bottomLimit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float camHeight = Camera.main.orthographicSize;
        topLimit = camHeight - 1f;
        bottomLimit = 0f + 1f;
    }

    void Update()
    {
        if (maceBall == null) return;

        // Прогнозируем траекторию мяча — щит целится чуть выше шара
        float predictedY = maceBall.position.y + predictionOffset;

        // Добавляем небольшую ошибку
        predictedY += Random.Range(-errorMargin, errorMargin);

        // Ограничиваем по вертикали
        predictedY = Mathf.Clamp(predictedY, bottomLimit, topLimit);

        Vector2 targetPos = new Vector2(transform.position.x, predictedY);
        rb.MovePosition(Vector2.Lerp(rb.position, targetPos, moveSpeed * Time.deltaTime));
    }
}