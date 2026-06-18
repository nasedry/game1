using System.Collections.Generic;
using UnityEngine;

public class BoxPool : MonoBehaviour
{
    public static BoxPool Instance { get; private set; }

    [Header("Pool")]
    public GameObject boxPrefab;
    public int poolSize = 10;

    [Header("Spawning")]
    public Transform[] spawnPoints;
    public bool spawnOnStart = true;
    public float spawnCheckRadius = 0.2f;

    [Header("Colors")]
    public BoxColorType[] possibleColors = { BoxColorType.Green, BoxColorType.Red, BoxColorType.Blue };

    private readonly Queue<GameObject> available = new Queue<GameObject>();
    private int lastSpawnIndex = -1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Prewarm();

        if (spawnOnStart)
        {
            SpawnInitial();
        }
    }

    void Prewarm()
    {
        if (boxPrefab == null)
        {
            Debug.LogError("BoxPool: boxPrefab not set.");
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject box = Instantiate(boxPrefab, transform);
            box.SetActive(false);
            available.Enqueue(box);
        }
    }

    void SpawnInitial()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            return;
        }
        // for (int i = 0; i < spawnPoints.Length; i++)
        // {
        //     SpawnBoxAt(spawnPoints[i]);
        // }
        SpawnBoxAtRandom();
    }

    public GameObject SpawnBoxAt(Transform spawnPoint)
    {
        if (spawnPoint == null)
        {
            return null;
        }

        return SpawnBoxAt(spawnPoint.position);
    }

    public GameObject SpawnBoxAt(Vector3 position)
    {
        if (available.Count == 0)
        {
            Debug.LogWarning("BoxPool: no boxes available in the pool.");
            return null;
        }

        GameObject box = available.Dequeue();
        PrepareBox(box, position);
        return box;
    }

    public void SpawnBoxAtRandom()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("BoxPool: no spawn points assigned.");
            return;
        }

        List<int> freeIndexes = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (IsSpawnPointFree(spawnPoints[i]))
            {
                freeIndexes.Add(i);
            }
        }

        if (freeIndexes.Count == 0)
        {
            Debug.LogWarning("BoxPool: no free spawn points available.");
            return;
        }

        int index = PickRandomSpawnIndex(freeIndexes);
        SpawnBoxAt(spawnPoints[index]);
        lastSpawnIndex = index;
    }

    public void ReturnBox(GameObject box)
    {
        if (box == null)
        {
            return;
        }

        box.transform.SetParent(transform);
        ResetBox(box);
        box.SetActive(false);
        available.Enqueue(box);
    }

    void PrepareBox(GameObject box, Vector3 position)
    {
        box.transform.SetParent(null);
        box.transform.position = position;
        box.transform.rotation = Quaternion.identity;
        ResetBox(box);
        AssignRandomColor(box);
        box.SetActive(true);
    }

    void ResetBox(GameObject box)
    {
        Collider2D collider = box.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
        if (boxRb != null)
        {
            boxRb.bodyType = RigidbodyType2D.Dynamic;
            boxRb.linearVelocity = Vector2.zero;
            boxRb.angularVelocity = 0f;
        }
    }

    bool IsSpawnPointFree(Transform spawnPoint)
    {
        if (spawnPoint == null)
        {
            return false;
        }

        Collider2D hit = Physics2D.OverlapCircle(spawnPoint.position, spawnCheckRadius);
        if (hit == null)
        {
            return true;
        }

        return !hit.CompareTag("Box");
    }

    void AssignRandomColor(GameObject box)
    {
        if (possibleColors == null || possibleColors.Length == 0)
        {
            return;
        }

        BoxColor color = box.GetComponent<BoxColor>();
        if (color == null)
        {
            return;
        }

        int index = Random.Range(0, possibleColors.Length);
        BoxColorType chosen = possibleColors[index];
        color.SetColor(chosen);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentTask(chosen);
        }
    }

    int PickRandomSpawnIndex(List<int> freeIndexes)
    {
        if (freeIndexes.Count == 1)
        {
            return freeIndexes[0];
        }

        int index = freeIndexes[Random.Range(0, freeIndexes.Count)];
        if (index == lastSpawnIndex)
        {
            int alt = Random.Range(0, freeIndexes.Count - 1);
            index = freeIndexes[alt >= freeIndexes.IndexOf(index) ? alt + 1 : alt];
        }

        return index;
    }
}
