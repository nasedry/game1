using UnityEngine;

/// <summary>
/// Допоміжний скрипт для налагодження та тестування гри.
/// Натисніть клавіші для тестування різних функцій.
/// </summary>
public class DebugHelper : MonoBehaviour
{
    void Update()
    {
        // Клавіша T для тестування добавлення очок
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(10);
                Debug.Log("Test: Added 10 points");
            }
        }

        // Клавіша R для перезагрузки сцени
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance?.RestartGame();
            Debug.Log("Test: Restarting game");
        }

        // Клавіша D для вивода статусу гри
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (GameManager.instance != null)
            {
                Debug.Log($"Game Status: Paused={GameManager.instance.IsPaused()}, Ended={GameManager.instance.IsGameEnded()}");
            }
        }

        // Клавіша B для тестування доставки коробки
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.BoxDelivered();
                Debug.Log("Test: Box delivered");
            }
        }
    }
}
