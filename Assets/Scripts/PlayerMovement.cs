using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Налаштування руху")]
    public float moveSpeed = 5f;
    
    [Header("Налаштування спрайту")]
    // ПОСИЛАННЯ: Перетягни сюди дочірній об'єкт, який містить твій SpriteRenderer
    public Transform characterSpriteTransform; 
    
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; 
        
        // Дуже важливо! Rigidbody2D самого персонажа НЕ повинен обертатися, 
        // щоб колізії зі стінами працювали правильно. Ми обертаємо тільки спрайт.
        rb.freezeRotation = true; 
        
        // Автоматична спроба знайти посилання на спрайт, якщо воно пусте
        if (characterSpriteTransform == null)
        {
            if (transform.childCount > 0)
            {
                // Наприклад, візьмемо першу дитину. В інспекторі Unity
                // переконайся, що спрайт (кепка персонажа) є дитиною цього об'єкта.
                characterSpriteTransform = transform.GetChild(0); 
                Debug.LogWarning("PlayerMovement: characterSpriteTransform автоматично встановлено на першу дитину. Перевір ієрархію!");
            }
            else
            {
                Debug.LogError("PlayerMovement: characterSpriteTransform пусте і немає дочірніх об'єктів для обертання.");
            }
        }
    }

    void Update()
    {
        // Зчитуємо ввід (W-A-S-D або стрілочки)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Нормалізація, щоб діагональ не була швидшою
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Власне рух
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        // Обертання спрайту ТІЛЬКИ коли гравець рухається
        if (movement != Vector2.zero)
        {
            float targetAngle = 0f;

            // Визначаємо кут обертання на основі вектора руху
            // Unity 2D обертання Z: проти годинникової стрілки (позитивний кут)
            // Припускаємо, що базовий спрайт (0 градусів) дивиться вгору

            if (movement.y > 0) // Up (W)
            {
                targetAngle = 90f;
            }
            else if (movement.y < 0) // Down (S)
            {
                targetAngle = -90f; // Оберт на 180 градусів (вниз)
            }
            else if (movement.x < 0) // Left (A)
            {
                targetAngle = 180f; // Оберт на 180 градусів проти годинникової стрілки
            }
            else if (movement.x > 0) // Right (D)
            {
                targetAngle = 0f; // Оберт на 0 градусів ЗА годинниковою стрілкою
            }

            // Миттєво встановлюємо обертання для дочірнього об'єкта (спрайту)
            if (characterSpriteTransform != null)
            {
                characterSpriteTransform.localEulerAngles = new Vector3(0, 0, targetAngle);
            }
        }
    }
}