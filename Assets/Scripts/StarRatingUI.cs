using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Відображає рейтинг зірок через Image-компоненти.
/// Прикріпи до будь-якого GameObject у ResultsPanel.
/// Призначай спрайти StarFilled і StarEmpty в інспекторі.
/// </summary>
public class StarRatingUI : MonoBehaviour
{
    [Header("Спрайти")]
    public Sprite starFilled;   // PNG заповненої зірки
    public Sprite starEmpty;    // PNG порожньої зірки

    [Header("Image-компоненти (3 зірки)")]
    public Image star1;
    public Image star2;
    public Image star3;

    /// <summary>Встановлює кількість заповнених зірок (0–3).</summary>
    public void SetStars(int count)
    {
        count = Mathf.Clamp(count, 0, 3);
        SetStar(star1, count >= 1);
        SetStar(star2, count >= 2);
        SetStar(star3, count >= 3);
    }

    void SetStar(Image img, bool filled)
    {
        if (img == null) return;
        img.sprite = filled ? starFilled : starEmpty;
    }
}
