using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] private string zoneColor = "Red";

    private void OnTriggerEnter2D(Collider2D other)
    {
        BoxObject box = other.GetComponent<BoxObject>();

        if (box != null && !box.isCarried)
        {
            if (box.GetBoxColor() == zoneColor)
            {
                // Correct delivery
                if (GameManager.instance != null)
                {
                    GameManager.instance.AddScore(10);
                    GameManager.instance.BoxDelivered();
                }

                // Notify LevelManager if it exists
                LevelManager levelManager = FindObjectOfType<LevelManager>();
                if (levelManager != null)
                {
                    levelManager.BoxRemoved();
                }

                box.OnDelivered();
            }
            else
            {
                // Wrong delivery - small penalty
                if (GameManager.instance != null)
                {
                    GameManager.instance.AddScore(-5);
                }
                
                // Still remove the box but with penalty
                LevelManager levelManager = FindObjectOfType<LevelManager>();
                if (levelManager != null)
                {
                    levelManager.BoxRemoved();
                }
                
                box.OnDelivered();
            }
        }
    }

    public string GetZoneColor()
    {
        return zoneColor;
    }

    public void SetZoneColor(string color)
    {
        zoneColor = color;
        UpdateColor();
    }

    void UpdateColor()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color[] colorMap = new Color[]
            {
                new Color(1f, 0f, 0f, 0.3f),      // Red
                new Color(0f, 1f, 0f, 0.3f),      // Green
                new Color(0f, 0f, 1f, 0.3f),      // Blue
                new Color(1f, 1f, 0f, 0.3f),      // Yellow
                new Color(1f, 0.5f, 0f, 0.3f),    // Orange
            };

            string[] colorNames = new string[] { "Red", "Green", "Blue", "Yellow", "Orange" };

            for (int i = 0; i < colorNames.Length; i++)
            {
                if (colorNames[i] == zoneColor)
                {
                    sr.color = colorMap[i];
                    break;
                }
            }
        }
    }
}