using UnityEngine;

public class PlayerShieldController : MonoBehaviour
{
    public float minX = -2.2f;
    public float maxX = 2.2f;

    private Camera mainCamera;
    private float halfHeight;

    private void Start()
    {
        mainCamera = Camera.main;
        halfHeight = mainCamera.orthographicSize;
    }

    private void Update()
    {
        Vector3 mouseViewportPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        // ⛔ если мышка вне экрана — ничего не делать
        if (mouseViewportPos.y < 0 || mouseViewportPos.y > 1 || mouseViewportPos.x < 0 || mouseViewportPos.x > 1)
            return;

        // получаем координаты мыши в мире
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        // ограничиваем щит: он не должен подниматься выше середины поля
        float middleY = mainCamera.transform.position.y;
        float minY = -halfHeight + 1f;     // нижняя граница камеры
        float maxY = middleY - 1f; // поднимаемся не до самой середины

        float clampedX = Mathf.Clamp(mouseWorldPos.x, minX, maxX);
        float clampedY = Mathf.Clamp(mouseWorldPos.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, 0);
    }
}
