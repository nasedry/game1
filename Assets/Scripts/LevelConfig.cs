using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "BoxDelivery/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Інформація про рівень")]
    public string levelName = "Рівень 1";
    public int levelNumber = 1;

    [Header("Гравець")]
    public float playerSpeed = 5f;

    [Header("Таймер")]
    public float levelTimeSeconds = 60f;

    [Header("Умова перемоги")]
    [Tooltip("Скільки коробок треба доставити для перемоги (0 = тільки таймер)")]
    public int deliveriesToWin = 10;

    [Header("Рейтинг зірок")]
    public int oneStarScore = 4;
    public int twoStarScore = 7;
    public int threeStarScore = 10;

    [Header("Навігація між сценами")]
    public string nextLevelSceneName = "";
    public string mainMenuSceneName = "MainMenu";
}
