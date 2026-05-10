using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Обов'язково для роботи з текстом

public class GameManager : MonoBehaviour
{
    // Робимо цей скрипт "Одинаком" (Singleton), щоб інші скрипти могли легко до нього звертатися
    public static GameManager Instance; 

    [Header("UI")]
    public TextMeshProUGUI scoreText; // Посилання на наш текстовий об'єкт
    public TextMeshProUGUI timerText; // Текст таймера
    public TextMeshProUGUI taskText; // Поточне завдання

    [Header("Results UI")]
    public GameObject resultsPanel;
    public TextMeshProUGUI resultsTitleText;
    public TextMeshProUGUI resultsScoreText;
    public TextMeshProUGUI resultsRatingText;
    public TextMeshProUGUI resultsStatsText;

    [Header("Timer")]
    public float levelTimeSeconds = 30f;

    [Header("Rating")]
    public int oneStarScore = 3;
    public int twoStarScore = 6;
    public int threeStarScore = 9;

    [Header("Scenes")]
    public string mainMenuSceneName = "";

    private int score = 0; // Наш лічильник очок
    private int correctDeliveries = 0;
    private int incorrectDeliveries = 0;
    private float timeRemaining;
    private bool timeUp;
    private bool resultsShown;

    public bool IsTimeUp => timeUp;

    void Awake()
    {
        // Налаштування Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        timeRemaining = levelTimeSeconds;
        UpdateTimerUi();
        UpdateScoreUi();

        if (resultsPanel != null)
        {
            resultsPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (timeUp)
        {
            return;
        }

        timeRemaining -= Time.deltaTime;
        UpdateTimerUi();
        if (timeRemaining <= 0f)
        {
            timeUp = true;
            Time.timeScale = 0f;
            Debug.Log("Час вийшов!");
            UpdateTimerUi();
            ShowResultsScreen();
        }
    }

    // Цей метод будуть викликати інші скрипти, коли треба додати очко
    public void AddScore()
    {
        if (timeUp)
        {
            return;
        }

        score += 1;
        correctDeliveries += 1;
        UpdateScoreUi();
    }

    public void AddIncorrectDelivery()
    {
        if (timeUp)
        {
            return;
        }

        incorrectDeliveries += 1;
    }

    public void SetCurrentTask(BoxColorType color)
    {
        if (taskText == null)
        {
            return;
        }

        taskText.text = "Доставте " + ToUkrColorName(color) + " коробку";
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrWhiteSpace(mainMenuSceneName))
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            Debug.LogWarning("Main menu scene name is not set.");
        }
    }

    void UpdateTimerUi()
    {
        if (timerText == null)
        {
            return;
        }

        float clamped = Mathf.Max(0f, timeRemaining);
        int totalSeconds = Mathf.CeilToInt(clamped);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        timerText.text = "Час: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void UpdateScoreUi()
    {
        if (scoreText == null)
        {
            return;
        }

        scoreText.text = "Доставлено: " + score;
    }

    void ShowResultsScreen()
    {
        if (resultsShown)
        {
            return;
        }

        resultsShown = true;
        Debug.Log("ShowResultsScreen: called");

        if (resultsPanel == null)
        {
            Debug.LogWarning("ShowResultsScreen: resultsPanel is not assigned.");
        }

        if (resultsPanel != null)
        {
            resultsPanel.SetActive(true);
            Debug.Log("ShowResultsScreen: resultsPanel activated.");
        }

        if (resultsTitleText != null)
        {
            resultsTitleText.text = "Час вичерпано!";
        }

        if (resultsScoreText != null)
        {
            resultsScoreText.text = "Рахунок: " + score;
        }

        if (resultsRatingText != null)
        {
            int stars = GetStarCount(score);
            resultsRatingText.text = "Зiрки: " + new string('*', stars);
        }

        if (resultsStatsText != null)
        {
            resultsStatsText.text = "Правильно: " + correctDeliveries + " | Неправильно: " + incorrectDeliveries;
        }
    }

    int GetStarCount(int totalScore)
    {
        if (totalScore >= threeStarScore)
        {
            return 3;
        }

        if (totalScore >= twoStarScore)
        {
            return 2;
        }

        return totalScore >= oneStarScore ? 1 : 0;
    }

    string ToUkrColorName(BoxColorType color)
    {
        switch (color)
        {
            case BoxColorType.Red:
                return "ЧЕРВОНУ";
            case BoxColorType.Blue:
                return "СИНЮ";
            default:
                return "ЗЕЛЕНУ";
        }
    }
}