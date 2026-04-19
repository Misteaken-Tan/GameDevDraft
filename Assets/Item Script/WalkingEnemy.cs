using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    float Direction = 1;
    Rigidbody2D rb;

    public float moveSpeed = 3;
    public int damage = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Apply velocity to the Rigidbody
        rb.linearVelocity = new Vector2(Direction * moveSpeed, rb.linearVelocity.y);
    }

    void flipDirection()
    {
        Direction *= -1;

        // Fix: Use Mathf.Abs so we don't accidentally flip the flip!
        float xScaler = Mathf.Abs(transform.localScale.x);

        if (Direction > 0)
            transform.localScale = new Vector3(xScaler, transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(-xScaler, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Layer 3 is usually 'Ground' - this checks if the foot-trigger left the floor
        if (collision.gameObject.layer == 3)
        {
            flipDirection();
        }
    }

    // Add this so the enemy actually hurts the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Die();
            }
        }
    }
}