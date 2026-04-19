using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxBoxesAtOnce = 5;

    private float spawnTimer = 0f;
    private int boxesSpawned = 0;
    private string[] colorNames = new string[] { "Red", "Green", "Blue", "Yellow", "Orange" };

    void Start()
    {
        if (boxPrefab == null)
        {
            Debug.LogError("Box prefab is not assigned!");
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
        }
    }

    void Update()
    {
        if (GameManager.instance != null && (GameManager.instance.IsGameEnded() || GameManager.instance.IsPaused()))
            return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && boxesSpawned < maxBoxesAtOnce)
        {
            SpawnBox();
            spawnTimer = 0f;
        }
    }

    void SpawnBox()
    {
        if (boxPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
            return;

        // Choose random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        // Spawn box
        GameObject newBox = Instantiate(boxPrefab, spawnPoint.position, Quaternion.identity);
        
        // Set random color
        BoxObject boxScript = newBox.GetComponent<BoxObject>();
        if (boxScript != null)
        {
            string randomColor = colorNames[Random.Range(0, colorNames.Length)];
            boxScript.SetBoxColor(randomColor);
        }

        boxesSpawned++;
    }

    public void BoxRemoved()
    {
        boxesSpawned--;
    }

    public void SetSpawnInterval(float interval)
    {
        spawnInterval = interval;
    }

    public void SetMaxBoxes(int maxBoxes)
    {
        maxBoxesAtOnce = maxBoxes;
    }
}
