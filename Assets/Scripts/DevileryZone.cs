using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    public BoxColorType zoneColor = BoxColorType.Green;

    // Цей метод автоматично спрацьовує щокадру, поки якийсь об'єкт стоїть у зоні
    void OnTriggerStay2D(Collider2D other)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsTimeUp)
        {
            return;
        }

        // Перевіряємо, чи об'єкт, який зайшов у зону, має тег "Box"
        if (!other.CompareTag("Box"))
        {
            return;
        }

        // Перевіряємо, чи коробка лежить на землі, а не в руках гравця.
        // Якщо parent == null, значить гравець її скинув (відв'язав).
        if (other.transform.parent != null)
        {
            return;
        }

        BoxColor boxColor = other.GetComponent<BoxColor>();
        if (boxColor == null)
        {
            return;
        }

        bool isCorrect = boxColor.currentColor == zoneColor;
        if (isCorrect)
        {
            Debug.Log("Коробку успішно доставлено в зону!");
            GameManager.Instance.AddScore();
        }
        else
        {
            Debug.Log("Коробку доставлено в неправильну зону!");
            GameManager.Instance.AddIncorrectDelivery();
        }

        if (BoxPool.Instance != null)
        {
            BoxPool.Instance.ReturnBox(other.gameObject);
            BoxPool.Instance.SpawnBoxAtRandom();
        }
    }
}