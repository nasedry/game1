using UnityEngine;
using TMPro; // Обов'язково для роботи з текстом

public class GameManager : MonoBehaviour
{
    // Робимо цей скрипт "Одинаком" (Singleton), щоб інші скрипти могли легко до нього звертатися
    public static GameManager Instance; 

    [Header("UI")]
    public TextMeshProUGUI scoreText; // Посилання на наш текстовий об'єкт

    private int score = 0; // Наш лічильник очок

    void Awake()
    {
        // Налаштування Singleton
        if (Instance == null) 
        {
            Instance = this;
        }
    }

    // Цей метод будуть викликати інші скрипти, коли треба додати очко
    public void AddScore()
    {
        score += 1;
        scoreText.text = "Доставлено: " + score;
    }
}