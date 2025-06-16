using UnityEngine;

public class EnemyShieldAI : MonoBehaviour
{
    public Transform maceBall;
    public float speed = 2f;
    public float errorMargin = 0.5f;
    public float difficultyGrowth = 0.01f; // на сколько уменьшается ошибка со временем
    public float minErrorMargin = 0.05f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (maceBall == null) return;

        // Позиция с небольшой погрешностью
        float targetX = maceBall.position.x + Random.Range(-errorMargin, errorMargin);
        float newX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        Vector2 newPos = new Vector2(newX, rb.position.y);

        rb.MovePosition(newPos);

        // Постепенно уменьшаем погрешность, увеличивая точность
        errorMargin = Mathf.Max(minErrorMargin, errorMargin - difficultyGrowth * Time.deltaTime);
    }
}