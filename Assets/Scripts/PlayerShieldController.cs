using UnityEngine;

public class PlayerShieldController : MonoBehaviour
{
    public float minX = -2.6f;
    public float maxX = 2.6f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float clampedX = Mathf.Clamp(mouseWorldPos.x, minX, maxX);
        Vector2 newPos = new Vector2(clampedX, rb.position.y);

        rb.MovePosition(newPos);

        Debug.Log("Mouse X: " + mouseWorldPos.x + " | Clamped: " + clampedX);
    }
}