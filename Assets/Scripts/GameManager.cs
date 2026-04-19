using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Settings")]
    public float levelTime = 60f;
    public int boxesToWin = 3;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI deliveredText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI pauseText;

    private float currentTime;
    private int score = 0;
    private int delivered = 0;
    private bool gameEnded = false;
    private bool isPaused = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentTime = levelTime;
        UpdateUI();

        if (messageText != null)
            messageText.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (gameEnded || isPaused) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }

        UpdateUI();
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        
        if (pauseText != null)
        {
            pauseText.text = isPaused ? "PAUSED\nPress ESC to continue" : "";
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void BoxDelivered()
    {
        delivered++;
        UpdateUI();

        if (delivered >= boxesToWin)
        {
            WinGame();
        }
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(currentTime);

        if (deliveredText != null)
            deliveredText.text = "Delivered: " + delivered + "/" + boxesToWin;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public bool IsGameEnded()
    {
        return gameEnded;
    }

    void GameOver()
    {
        gameEnded = true;
        Time.timeScale = 0f;

        if (messageText != null)
            messageText.text = "GAME OVER";
    }

    void WinGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;

        if (messageText != null)
            messageText.text = "YOU WIN";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}