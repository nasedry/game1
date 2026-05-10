using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Налаштування взаємодії")]
    public float pickupRange = 1.5f; // Радіус, в якому гравець може взяти коробку
    public Transform holdPosition;   // Точка, куди кріпиться коробка (наш пустий об'єкт)

    private GameObject heldBox = null; // Посилання на коробку, яку ми зараз тримаємо

    void Update()
    {
        // Перевіряємо натискання клавіші E згідно з GDD
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldBox == null)
            {
                TryPickupBox(); // Якщо руки порожні - пробуємо взяти
            }
            else
            {
                DropBox();      // Якщо вже щось тримаємо - кидаємо
            }
        }
    }

    void TryPickupBox()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Box"))
            {
                heldBox = hitCollider.gameObject;
                heldBox.transform.SetParent(holdPosition);
                heldBox.transform.localPosition = Vector3.zero;

                // 1. Вимикаємо колайдер
                heldBox.GetComponent<Collider2D>().enabled = false;

                // 2. "Заморожуємо" фізику коробки, поки вона в руках
                Rigidbody2D boxRb = heldBox.GetComponent<Rigidbody2D>();
                if (boxRb != null)
                {
                    // Переводимо в Kinematic, щоб на неї не діяла фізика
                    boxRb.bodyType = RigidbodyType2D.Kinematic; 
                    boxRb.linearVelocity = Vector2.zero; 
                }

                break;
            }
        }
    }

    void DropBox()
    {
        Vector3 dropPos = holdPosition.position;
        heldBox.transform.SetParent(null);
        heldBox.transform.position = dropPos;

        // 1. Вмикаємо колайдер назад
        heldBox.GetComponent<Collider2D>().enabled = true;

        // 2. Вмикаємо фізику назад
        Rigidbody2D boxRb = heldBox.GetComponent<Rigidbody2D>();
        if (boxRb != null)
        {
            // Повертаємо тип Dynamic, щоб вона знову могла стикатися зі стінами та зонами
            boxRb.bodyType = RigidbodyType2D.Dynamic; 
            boxRb.linearVelocity = Vector2.zero;
            boxRb.angularVelocity = 0f;
        }

        heldBox = null;
    }

    // Цей метод малює коло радіусу взаємодії в редакторі Unity для зручності налаштування
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}