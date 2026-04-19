using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().UpdateCheckpoint(transform.position);
            // Optional: Add a visual change or sound here
        }
    }
}