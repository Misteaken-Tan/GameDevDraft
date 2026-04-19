using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public bool movingRight = true;

    [Header("Detection Settings")]
    public Transform edgeDetection; // Create an empty child object at the enemy's feet
    public float detectionDistance = 0.5f;
    public LayerMask groundLayer; // Set this to your 'Ground' layer

    void Update()
    {
        // Move the enemy
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // Check for the edge of the platform or a wall
        RaycastHit2D groundInfo = Physics2D.Raycast(edgeDetection.position, Vector2.down, detectionDistance, groundLayer);

        // If the raycast doesn't hit anything, we reached an edge
        if (groundInfo.collider == false)
        {
            Flip();
        }
    }

    void Flip()
    {
        if (movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }
}