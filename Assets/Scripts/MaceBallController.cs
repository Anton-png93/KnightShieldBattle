using UnityEngine;
using System.Collections;

public class MaceBallController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DelayedStart());
        
    }
    public void ResetBall(bool playerServe)
{
    // Сбрасываем позицию мяча в центр
    transform.position = Vector2.zero;

    // Определяем направление удара
    Vector2 direction;
    if (playerServe)
    {
        // Если забил игрок — подаём вправо (на компьютер)
        direction = new Vector2(1, 1).normalized;
    }
    else
    {
        // Если забил компьютер — подаём влево (на игрока)
        direction = new Vector2(-1, 1).normalized;
    }

    // Применяем скорость
    rb.linearVelocity = direction * speed;
}
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверка — если мяч почти не двигается по Y
        if (Mathf.Abs(rb.linearVelocity.y) < 0.5f)
        {
            Vector2 fixedVelocity = rb.linearVelocity;
            fixedVelocity.y = Random.Range(-1f, 1f); // Случайный вертикальный толчок
            rb.linearVelocity = fixedVelocity.normalized * speed;
        }
    }
IEnumerator DelayedStart()
{
    yield return new WaitForSeconds(0.1f); // Подождём, пока всё инициализируется
    ResetBall(scoreManager.lastGoalByPlayer); // Теперь переменная уже точно установлена
}

}