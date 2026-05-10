using UnityEngine;

public enum BoxColorType
{
    Green,
    Red,
    Blue
}

public class BoxColor : MonoBehaviour
{
    [Header("Label")]
    public SpriteRenderer labelRenderer;

    [Header("State")]
    public BoxColorType currentColor = BoxColorType.Green;

    public void SetColor(BoxColorType color)
    {
        currentColor = color;
        ApplyColor();
    }

    void Awake()
    {
        ApplyColor();
    }

    void ApplyColor()
    {
        if (labelRenderer == null)
        {
            return;
        }

        labelRenderer.color = ToUnityColor(currentColor);
    }

    Color ToUnityColor(BoxColorType color)
    {
        switch (color)
        {
            case BoxColorType.Red:
                return Color.red;
            case BoxColorType.Blue:
                return Color.blue;
            default:
                return Color.green;
        }
    }
}
