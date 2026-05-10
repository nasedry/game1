using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string gameSceneName = "GameScene";

    public void StartGame()
    {
        if (!string.IsNullOrWhiteSpace(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogWarning("MainMenuController: gameSceneName is empty.");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("ExitGame called.");
    }
}
