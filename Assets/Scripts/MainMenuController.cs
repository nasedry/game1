using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Сцени")]
    public string level1SceneName = "GameScene";
    public string level2SceneName = "GameScene2";

    // ── Кнопки головного меню ────────────────────────────────────────────────

    /// <summary>Запустити перший рівень.</summary>
    public void StartGame()
    {
        LoadLevel(level1SceneName);
    }

    /// <summary>Запустити другий рівень (кнопка вибору рівня).</summary>
    public void StartLevel2()
    {
        LoadLevel(level2SceneName);
    }

    /// <summary>Завантажити сцену за назвою.</summary>
    public void LoadLevel(string sceneName)
    {
        if (!string.IsNullOrWhiteSpace(sceneName))
            SceneManager.LoadScene(sceneName);
        else
            Debug.LogWarning("MainMenuController: sceneName is empty.");
    }

    /// <summary>Вийти з гри.</summary>
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("ExitGame called.");
    }
}
