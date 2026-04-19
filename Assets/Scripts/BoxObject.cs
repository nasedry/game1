using UnityEngine;

public class BoxObject : MonoBehaviour
{
    [SerializeField] private string boxColor = "Red";
    public bool isCarried = false;

    private Color[] colorMap = new Color[]
    {
        new Color(1f, 0f, 0f, 1f),      // Red
        new Color(0f, 1f, 0f, 1f),      // Green
        new Color(0f, 0f, 1f, 1f),      // Blue
        new Color(1f, 1f, 0f, 1f),      // Yellow
        new Color(1f, 0.5f, 0f, 1f),    // Orange
    };

    private string[] colorNames = new string[] { "Red", "Green", "Blue", "Yellow", "Orange" };

    void Start()
    {
        UpdateVisuals();
    }

    public string GetBoxColor()
    {
        return boxColor;
    }

    public void SetBoxColor(string color)
    {
        boxColor = color;
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            for (int i = 0; i < colorNames.Length; i++)
            {
                if (colorNames[i] == boxColor)
                {
                    sr.color = colorMap[i];
                    break;
                }
            }
        }
    }

    public void OnDelivered()
    {
        // Called when delivered to correct zone
        Destroy(gameObject);
    }
}