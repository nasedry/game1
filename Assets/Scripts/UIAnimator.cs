using System.Collections;
using UnityEngine;

/// <summary>
/// Додає просту анімацію появи/зникнення до панелі UI.
/// Прикріпіть до будь-якого GameObject з RectTransform.
/// Викличте ShowPanel() / HidePanel() замість SetActive().
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class UIAnimator : MonoBehaviour
{
    [Header("Налаштування анімації")]
    public float animDuration = 0.25f;
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private CanvasGroup canvasGroup;
    private RectTransform rect;
    private Coroutine currentAnim;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
    }

    /// <summary>Активує об'єкт і програє анімацію появи.</summary>
    public void ShowPanel()
    {
        if (currentAnim != null) StopCoroutine(currentAnim);
        gameObject.SetActive(true);
        currentAnim = StartCoroutine(Animate(0f, 1f));
    }

    /// <summary>Програє анімацію зникнення, потім деактивує об'єкт.</summary>
    public void HidePanel()
    {
        if (currentAnim != null) StopCoroutine(currentAnim);
        currentAnim = StartCoroutine(Animate(1f, 0f, deactivateOnFinish: true));
    }

    IEnumerator Animate(float from, float to, bool deactivateOnFinish = false)
    {
        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += Time.unscaledDeltaTime; // unscaled — працює під час паузи
            float t = Mathf.Clamp01(elapsed / animDuration);
            float value = scaleCurve.Evaluate(t);
            float lerped = Mathf.Lerp(from, to, value);

            canvasGroup.alpha = lerped;
            rect.localScale = Vector3.one * lerped;
            yield return null;
        }

        canvasGroup.alpha = to;
        rect.localScale = Vector3.one * to;

        if (deactivateOnFinish)
            gameObject.SetActive(false);
    }
}
