using UnityEngine;

/// <summary>
/// Патрульний рух: об'єкт їде в одному напрямку,
/// стикається зі стіною (або досягає кінцевої точки) — розвертається і їде назад.
/// Два режими: через Raycast/Collider або через задані точки.
/// </summary>
public class PatrolMovement : MonoBehaviour
{
    public enum PatrolMode
    {
        Raycast,    // автоматично визначає стіну через Raycast
        Waypoints   // між двома заданими точками
    }

    [Header("Режим патрулювання")]
    public PatrolMode mode = PatrolMode.Raycast;

    [Header("Швидкість")]
    public float speed = 3f;

    [Header("Raycast режим")]
    [Tooltip("Шари, які вважаються стіною/перешкодою")]
    public LayerMask wallLayer;
    [Tooltip("Відстань до стіни, при якій відбувається розворот")]
    public float detectionDistance = 0.3f;

    [Header("Waypoint режим")]
    public Transform pointA;
    public Transform pointB;

    // ── внутрішній стан ──────────────────────────────────────────────────────
    private Vector2 direction = Vector2.right;   // поточний напрямок руху
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }
    }

    void Start()
    {
        if (mode == PatrolMode.Waypoints && pointA != null)
        {
            // Починаємо рух до точки B
            direction = ((Vector2)(pointB.position - pointA.position)).normalized;
        }
    }

    void FixedUpdate()
    {
        if (mode == PatrolMode.Raycast)
            MoveWithRaycast();
        else
            MoveWithWaypoints();
    }

    // ── Raycast режим ─────────────────────────────────────────────────────────

    void MoveWithRaycast()
    {
        // Перевіряємо, чи попереду стіна
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            detectionDistance,
            wallLayer
        );

        if (hit.collider != null)
            Flip();

        ApplyMovement();
    }

    // ── Waypoint режим ────────────────────────────────────────────────────────

    void MoveWithWaypoints()
    {
        if (pointA == null || pointB == null) return;

        // Визначаємо цільову точку
        Transform target = (direction.x >= 0) ? pointB : pointA;

        // Якщо дійшли до цілі — розвертаємось
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
            Flip();

        ApplyMovement();
    }

    // ── Загальні методи ───────────────────────────────────────────────────────

    void ApplyMovement()
    {
        if (rb != null)
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        else
            transform.Translate(direction * speed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        direction = -direction;

        // Перевертаємо спрайт
        if (sr != null)
            sr.flipX = direction.x < 0;
    }

    // ── Гізмо для відображення в редакторі ──────────────────────────────────

    void OnDrawGizmosSelected()
    {
        if (mode == PatrolMode.Raycast)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position,
                transform.position + (Vector3)(direction * detectionDistance));
        }
        else
        {
            if (pointA != null && pointB != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(pointA.position, pointB.position);
                Gizmos.DrawWireSphere(pointA.position, 0.15f);
                Gizmos.DrawWireSphere(pointB.position, 0.15f);
            }
        }
    }
}
