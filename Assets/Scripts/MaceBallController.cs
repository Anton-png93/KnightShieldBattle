using UnityEngine;

public class MaceBallController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()

    {
        rb = GetComponent<Rigidbody2D>();

        // Начальный вектор направления — по диагонали вверх-вправо
        Vector2 direction = new Vector2(1, 1).normalized;
        rb.linearVelocity = direction * speed;
    }
    public void ResetBall()
{
    // Сбрасываем позицию мяча в центр
    transform.position = Vector2.zero;

    // Выдаём новое направление — снова вверх и вправо
    Vector2 direction = new Vector2(1, 1).normalized;

    // Применяем скорость
    rb.linearVelocity = direction * speed;
}
}