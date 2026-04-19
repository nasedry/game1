using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.3f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>()?.transform;
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
