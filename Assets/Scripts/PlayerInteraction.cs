using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform carryPoint;
    public float interactRange = 1.5f;

    private BoxObject carriedBox;

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.IsPaused())
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (carriedBox == null)
                TryPickUpBox();
            else
                DropBox();
        }
    }

    void TryPickUpBox()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D hit in hits)
        {
            BoxObject box = hit.GetComponent<BoxObject>();

            if (box != null && !box.isCarried)
            {
                carriedBox = box;
                box.isCarried = true;

                Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
                if (boxRb != null)
                {
                    boxRb.linearVelocity = Vector2.zero;
                    boxRb.bodyType = RigidbodyType2D.Kinematic;
                }

                box.transform.SetParent(carryPoint);
                box.transform.localPosition = Vector3.zero;

                break;
            }
        }
    }

    void DropBox()
    {
        if (carriedBox == null) return;

        carriedBox.isCarried = false;
        carriedBox.transform.SetParent(null);

        Rigidbody2D boxRb = carriedBox.GetComponent<Rigidbody2D>();
        if (boxRb != null)
        {
            boxRb.bodyType = RigidbodyType2D.Kinematic;
            boxRb.linearVelocity = Vector2.zero;
        }

        carriedBox = null;
    }

    public BoxObject GetCarriedBox()
    {
        return carriedBox;
    }

    public void ClearCarriedBox()
    {
        carriedBox = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}