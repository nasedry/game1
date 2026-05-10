using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    // Цей метод автоматично спрацьовує щокадру, поки якийсь об'єкт стоїть у зоні
    void OnTriggerStay2D(Collider2D other)
    {
        // Перевіряємо, чи об'єкт, який зайшов у зону, має тег "Box"
        if (other.CompareTag("Box"))
        {
            // Перевіряємо, чи коробка лежить на землі, а не в руках гравця.
            // Якщо parent == null, значить гравець її скинув (відв'язав).
            if (other.transform.parent == null)
            {
                // Виводимо повідомлення про успіх
                Debug.Log("Коробку успішно доставлено в зону!");

                GameManager.Instance.AddScore();
                
                // Знищуємо коробку, оскільки її доставлено
                Destroy(other.gameObject);

                // TODO: Пізніше тут ми викличемо GameManager, щоб додати очки
            }
        }
    }
}