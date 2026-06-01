using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // ── Конфігурація рівня ──────────────────────────────────────────────────
    [Header("Конфігурація рівня (ScriptableObject)")]
    public LevelConfig config;

    // ── HUD ──────────────────────────────────────────────────────────────────
    [Header("HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI levelNumberText;

    // ── Екран результатів ─────────────────────────────────────────────────────
    [Header("Екран результатів")]
    public GameObject resultsPanel;
    public TextMeshProUGUI resultsTitleText;
    public TextMeshProUGUI resultsScoreText;
    // public TextMeshProUGUI resultsRatingText;
    public StarRatingUI starRatingUI;          // PNG-зірки
    public TextMeshProUGUI resultsStatsText;
    public GameObject nextLevelButton;

    // ── Параметри (fallback без Config) ──────────────────────────────────────
    [Header("Параметри (fallback без Config)")]
    public float levelTimeSeconds = 60f;
    public int deliveriesToWin = 10;
    public int oneStarScore = 4;
    public int twoStarScore = 7;
    public int threeStarScore = 10;
    public string mainMenuSceneName = "MainMenu";
    public string nextLevelSceneName = "";

    // ── Стан ─────────────────────────────────────────────────────────────────
    private int score;
    private int correctDeliveries;
    private int incorrectDeliveries;
    private float timeRemaining;
    private bool gameOver;
    private bool isWin;
    private bool resultsShown;

    // ── Публічні властивості ─────────────────────────────────────────────────
    public bool IsGameOver => gameOver;
    public bool IsTimeUp => gameOver && !isWin;
    public LevelConfig Config => config;

    // ═════════════════════════════════════════════════════════════════════════

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) { Destroy(gameObject); return; }
    }

    void Start()
    {
        ApplyConfig();

        timeRemaining = levelTimeSeconds;
        UpdateTimerUI();
        UpdateScoreUI();
        UpdateLevelNumberUI();

        if (resultsPanel != null) resultsPanel.SetActive(false);
        if (nextLevelButton != null) nextLevelButton.SetActive(false);
    }

    void ApplyConfig()
    {
        if (config == null) return;

        levelTimeSeconds   = config.levelTimeSeconds;
        deliveriesToWin    = config.deliveriesToWin;
        oneStarScore       = config.oneStarScore;
        twoStarScore       = config.twoStarScore;
        threeStarScore     = config.threeStarScore;
        mainMenuSceneName  = config.mainMenuSceneName;
        nextLevelSceneName = config.nextLevelSceneName;

        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (player != null) player.SetSpeed(config.playerSpeed);
    }

    // ── Update ────────────────────────────────────────────────────────────────

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerUI();

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGame(false);
        }
    }

    // ── Публічні методи для інших скриптів ───────────────────────────────────

    public void AddScore()
    {
        if (gameOver) return;
        score++;
        correctDeliveries++;
        UpdateScoreUI();

        if (deliveriesToWin > 0 && score >= deliveriesToWin)
            EndGame(true);
    }

    public void AddIncorrectDelivery()
    {
        if (gameOver) return;
        incorrectDeliveries++;
    }

    public void SetCurrentTask(BoxColorType color)
    {
        if (taskText == null) return;
        taskText.text = "Доставте " + ToUkrColorName(color) + " коробку";
    }

    // ── Навігація ─────────────────────────────────────────────────────────────

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(!string.IsNullOrWhiteSpace(nextLevelSceneName)
            ? nextLevelSceneName
            : mainMenuSceneName);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // ── Внутрішні методи ──────────────────────────────────────────────────────

    void EndGame(bool win)
    {
        if (gameOver) return;
        gameOver = true;
        isWin = win;
        Time.timeScale = 0f;
        ShowResultsScreen(win);
    }

    void ShowResultsScreen(bool win)
    {
        if (resultsShown) return;
        resultsShown = true;

        if (resultsPanel != null) resultsPanel.SetActive(true);

        if (resultsTitleText != null)
            resultsTitleText.text = win ? "Перемога!" : "Час вичерпано!";

        if (resultsScoreText != null)
            resultsScoreText.text = "Рахунок: " + score;

        int stars = GetStarCount(score);

        // if (resultsRatingText != null)
        //     resultsRatingText.text = new string('★', stars) + new string('☆', 3 - stars);

        if (starRatingUI != null)
            starRatingUI.SetStars(stars);

        if (resultsStatsText != null)
            resultsStatsText.text = "Правильно: " + correctDeliveries + " | Неправильно: " + incorrectDeliveries;

        if (nextLevelButton != null)
            nextLevelButton.SetActive(win && !string.IsNullOrWhiteSpace(nextLevelSceneName));
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;
        float clamped = Mathf.Max(0f, timeRemaining);
        int total = Mathf.CeilToInt(clamped);
        timerText.text = "Час: " + (total / 60).ToString("00") + ":" + (total % 60).ToString("00");
    }

    void UpdateScoreUI()
    {
        if (scoreText == null) return;
        string target = deliveriesToWin > 0 ? "/" + deliveriesToWin : "";
        scoreText.text = "Доставлено: " + score + target;
    }

    void UpdateLevelNumberUI()
    {
        if (levelNumberText == null || config == null) return;
        levelNumberText.text = "Рівень " + config.levelNumber;
    }

    int GetStarCount(int s)
    {
        if (s >= threeStarScore) return 3;
        if (s >= twoStarScore) return 2;
        return s >= oneStarScore ? 1 : 0;
    }

    string ToUkrColorName(BoxColorType color)
    {
        switch (color)
        {
            case BoxColorType.Red:  return "ЧЕРВОНУ";
            case BoxColorType.Blue: return "СИНЮ";
            default:                return "ЗЕЛЕНУ";
        }
    }
}